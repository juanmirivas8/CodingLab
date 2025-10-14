using System.Text;
using System.Text.Json;
using DotNetLab.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DotNetLab.Services.Implementation;

/// <summary>
/// RabbitMQ consumer implementation for receiving and processing messages
/// </summary>
public class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly RabbitMqOptions _options;
    private readonly ILogger<RabbitMqConsumer> _logger;
    private IConnection? _connection;
    private IChannel? _channel;
    private bool _disposed;

    public RabbitMqConsumer(IOptions<RabbitMqOptions> options, ILogger<RabbitMqConsumer> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting RabbitMQ consumer connection to {HostName}:{Port}", _options.HostName, _options.Port);
            
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                AutomaticRecoveryEnabled = true, // Enable automatic recovery
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = await factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
            
            // Set QoS - process one message at a time per consumer
            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken);
            
            _logger.LogInformation("RabbitMQ consumer successfully connected");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start RabbitMQ consumer");
            throw;
        }
    }

    public void Subscribe<TMessage>(string queueName, Func<TMessage, CancellationToken, Task> handler, CancellationToken cancellationToken) where TMessage : class
    {
        if (_channel == null)
        {
            throw new InvalidOperationException("Consumer not started. Call StartAsync first.");
        }

        _logger.LogInformation("Subscribing to queue: {QueueName}", queueName);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);
            
            _logger.LogDebug("Received message from queue {QueueName}: {Message}", queueName, messageJson);

            try
            {
                var message = JsonSerializer.Deserialize<TMessage>(messageJson);
                
                if (message != null)
                {
                    await handler(message, cancellationToken);
                    
                    // Acknowledge the message after successful processing
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken);
                    
                    _logger.LogInformation("Successfully processed message from queue {QueueName}", queueName);
                }
                else
                {
                    _logger.LogWarning("Failed to deserialize message from queue {QueueName}", queueName);
                    // Reject and requeue the message
                    await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message from queue {QueueName}", queueName);
                
                // Reject the message and don't requeue (send to DLX if configured)
                await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken);
            }
        };

        // Start consuming
        _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken);
        
        _logger.LogInformation("Successfully subscribed to queue: {QueueName}", queueName);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping RabbitMQ consumer");
        
        if (_channel != null)
        {
            await _channel.CloseAsync(cancellationToken);
        }

        if (_connection != null)
        {
            await _connection.CloseAsync(cancellationToken);
        }
        
        _logger.LogInformation("RabbitMQ consumer stopped");
    }

    public void Dispose()
    {
        if (_disposed) return;

        _channel?.Dispose();
        _connection?.Dispose();
        
        _disposed = true;
        _logger.LogDebug("RabbitMQ consumer disposed");
    }
}
