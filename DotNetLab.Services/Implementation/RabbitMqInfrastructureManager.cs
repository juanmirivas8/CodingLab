using DotNetLab.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DotNetLab.Services.Implementation;

/// <summary>
/// Manages RabbitMQ infrastructure setup (exchanges, queues, bindings)
/// </summary>
public class RabbitMqInfrastructureManager : IRabbitMqInfrastructureManager, IDisposable
{
    private readonly RabbitMqOptions _options;
    private readonly ILogger<RabbitMqInfrastructureManager> _logger;
    private IConnection? _connection;
    private IChannel? _channel;
    private bool _disposed;

    public RabbitMqInfrastructureManager(IOptions<RabbitMqOptions> options, ILogger<RabbitMqInfrastructureManager> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    private async Task EnsureConnectionAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) return;

        _logger.LogInformation("Establishing RabbitMQ connection for infrastructure management");

        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        _logger.LogInformation("RabbitMQ infrastructure connection established");
    }

    public async Task EnsureInfrastructureAsync(CancellationToken cancellationToken = default)
    {
        await EnsureConnectionAsync(cancellationToken);

        _logger.LogInformation("Starting RabbitMQ infrastructure setup");

        // This method will be overridden or configured based on your specific needs
        // For now, it's a placeholder that can be extended
        
        _logger.LogInformation("RabbitMQ infrastructure setup completed");
    }

    public async Task DeclareExchangeAsync(string exchangeName, string exchangeType, bool durable = true, bool autoDelete = false, CancellationToken cancellationToken = default)
    {
        await EnsureConnectionAsync(cancellationToken);

        if (_channel == null)
        {
            throw new InvalidOperationException("Channel is not initialized");
        }

        _logger.LogInformation("Declaring exchange: {ExchangeName} (Type: {ExchangeType}, Durable: {Durable})", 
            exchangeName, exchangeType, durable);

        await _channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: exchangeType,
            durable: durable,
            autoDelete: autoDelete,
            arguments: null,
            cancellationToken: cancellationToken);

        _logger.LogInformation("Exchange {ExchangeName} declared successfully", exchangeName);
    }

    public async Task<string> DeclareQueueAsync(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, CancellationToken cancellationToken = default)
    {
        await EnsureConnectionAsync(cancellationToken);

        if (_channel == null)
        {
            throw new InvalidOperationException("Channel is not initialized");
        }

        _logger.LogInformation("Declaring queue: {QueueName} (Durable: {Durable}, Exclusive: {Exclusive})", 
            queueName, durable, exclusive);

        var result = await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: durable,
            exclusive: exclusive,
            autoDelete: autoDelete,
            arguments: null,
            cancellationToken: cancellationToken);

        _logger.LogInformation("Queue {QueueName} declared successfully (Messages: {MessageCount}, Consumers: {ConsumerCount})",
            queueName, result.MessageCount, result.ConsumerCount);

        return result.QueueName;
    }

    public async Task BindQueueAsync(string queueName, string exchangeName, string routingKey = "", CancellationToken cancellationToken = default)
    {
        await EnsureConnectionAsync(cancellationToken);

        if (_channel == null)
        {
            throw new InvalidOperationException("Channel is not initialized");
        }

        _logger.LogInformation("Binding queue {QueueName} to exchange {ExchangeName} with routing key '{RoutingKey}'", 
            queueName, exchangeName, routingKey);

        await _channel.QueueBindAsync(
            queue: queueName,
            exchange: exchangeName,
            routingKey: routingKey,
            arguments: null,
            cancellationToken: cancellationToken);

        _logger.LogInformation("Queue {QueueName} bound to exchange {ExchangeName} successfully", queueName, exchangeName);
    }

    public void Dispose()
    {
        if (_disposed) return;

        _channel?.Dispose();
        _connection?.Dispose();

        _disposed = true;
        _logger.LogDebug("RabbitMQ infrastructure manager disposed");
    }
}
