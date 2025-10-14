using DotNetLab.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DotNetLab.Services.Implementation;

/// <summary>
/// RabbitMQ implementation of the message bus for publishing messages
/// </summary>
public class RabbitMqMessageBus : IMessageBus, IDisposable
{
    private readonly RabbitMqOptions _options;
    private readonly ILogger<RabbitMqMessageBus> _logger;
    private IConnection? _connection;
    private IChannel? _channel;
    private bool _disposed;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    public RabbitMqMessageBus(IOptions<RabbitMqOptions> options, ILogger<RabbitMqMessageBus> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    private async Task EnsureConnectionAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) return;

        await _connectionLock.WaitAsync(cancellationToken);
        try
        {
            // Double-check after acquiring lock
            if (_channel != null) return;

            _logger.LogInformation("Establishing RabbitMQ connection for message publishing");

            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password
            };

            _connection = await factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

            _logger.LogInformation("RabbitMQ publisher connection established to {HostName}:{Port}", 
                _options.HostName, _options.Port);
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    public async Task PublishAsync<T>(string exchange, string routingKey, T message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(exchange))
            throw new ArgumentException("Exchange name cannot be null or empty", nameof(exchange));

        if (message == null)
            throw new ArgumentNullException(nameof(message));

        await EnsureConnectionAsync(cancellationToken);

        if (_channel == null)
            throw new InvalidOperationException("Channel is not initialized");

        try
        {
            // Serialize message to JSON
            var messageBody = JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            var body = Encoding.UTF8.GetBytes(messageBody);

            // Create basic properties
            var properties = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent, // Make messages persistent
                Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                MessageId = Guid.NewGuid().ToString()
            };

            // Publish message to exchange with routing key
            await _channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                mandatory: false,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Published message to exchange '{Exchange}' with routing key '{RoutingKey}' (MessageId: {MessageId})",
                exchange, routingKey, properties.MessageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Failed to publish message to exchange '{Exchange}' with routing key '{RoutingKey}'",
                exchange, routingKey);
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        _channel?.Dispose();
        _connection?.Dispose();
        _connectionLock.Dispose();

        _disposed = true;
        _logger.LogDebug("RabbitMQ message bus disposed");
    }
}
