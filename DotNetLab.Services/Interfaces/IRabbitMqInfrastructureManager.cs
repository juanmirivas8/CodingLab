namespace DotNetLab.Services.Interfaces;

/// <summary>
/// Interface for managing RabbitMQ infrastructure (exchanges, queues, bindings)
/// </summary>
public interface IRabbitMqInfrastructureManager
{
    /// <summary>
    /// Ensures that all required exchanges, queues, and bindings are created
    /// </summary>
    Task EnsureInfrastructureAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Declares an exchange
    /// </summary>
    Task DeclareExchangeAsync(string exchangeName, string exchangeType, bool durable = true, bool autoDelete = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Declares a queue
    /// </summary>
    Task<string> DeclareQueueAsync(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Binds a queue to an exchange
    /// </summary>
    Task BindQueueAsync(string queueName, string exchangeName, string routingKey = "", CancellationToken cancellationToken = default);
}

