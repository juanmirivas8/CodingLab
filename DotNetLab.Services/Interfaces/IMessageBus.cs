namespace DotNetLab.Services.Interfaces;

/// <summary>
/// Interface for publishing messages to a message bus
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// Publishes a message to the specified exchange with a routing key
    /// </summary>
    Task PublishAsync<T>(string exchange, string routingKey, T message, CancellationToken cancellationToken = default);
}
