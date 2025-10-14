namespace DotNetLab.Services.Implementation;

/// <summary>
/// Configuration for RabbitMQ exchanges
/// </summary>
public class RabbitMqExchangeConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "fanout"; // fanout, direct, topic, headers
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
}

/// <summary>
/// Configuration for RabbitMQ queues
/// </summary>
public class RabbitMqQueueConfiguration
{
    public string Name { get; set; } = string.Empty;
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
}

/// <summary>
/// Configuration for RabbitMQ queue-to-exchange bindings
/// </summary>
public class RabbitMqBindingConfiguration
{
    public string QueueName { get; set; } = string.Empty;
    public string ExchangeName { get; set; } = string.Empty;
    public string RoutingKey { get; set; } = string.Empty;
}

/// <summary>
/// Complete RabbitMQ infrastructure configuration
/// </summary>
public class RabbitMqInfrastructureConfiguration
{
    public List<RabbitMqExchangeConfiguration> Exchanges { get; set; } = new();
    public List<RabbitMqQueueConfiguration> Queues { get; set; } = new();
    public List<RabbitMqBindingConfiguration> Bindings { get; set; } = new();
}

