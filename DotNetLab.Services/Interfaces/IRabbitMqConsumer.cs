namespace DotNetLab.Services.Interfaces;

/// <summary>
/// Interface for RabbitMQ consumer operations
/// </summary>
public interface IRabbitMqConsumer : IDisposable
{
    /// <summary>
    /// Subscribe to a queue and process messages with the provided handler
    /// </summary>
    /// <typeparam name="TMessage">The type of message expected</typeparam>
    /// <param name="queueName">The queue to subscribe to</param>
    /// <param name="handler">The handler to process messages</param>
    /// <param name="cancellationToken">Cancellation token</param>
    void Subscribe<TMessage>(string queueName, Func<TMessage, CancellationToken, Task> handler, CancellationToken cancellationToken) where TMessage : class;
    
    /// <summary>
    /// Start consuming messages
    /// </summary>
    Task StartAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Stop consuming messages
    /// </summary>
    Task StopAsync(CancellationToken cancellationToken);
}

