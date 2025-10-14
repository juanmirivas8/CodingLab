using DotNetLab.Services.Implementation;
using DotNetLab.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetLab.Services.Extensions;

/// <summary>
/// Extension methods for registering all RabbitMQ services
/// </summary>
public static class RabbitMqServiceExtensions
{
    /// <summary>
    /// Adds all RabbitMQ services (publisher, consumer, infrastructure management)
    /// </summary>
    public static IServiceCollection AddRabbitMqServices(this IServiceCollection services)
    {
        // Register message bus (publisher)
        services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        
        // Register infrastructure manager
        services.AddSingleton<IRabbitMqInfrastructureManager, RabbitMqInfrastructureManager>();
        
        // Register consumer
        services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
        
        // Register hosted services
        services.AddHostedService<RabbitMqInfrastructureHostedService>();
        services.AddHostedService<RabbitMqConsumerHostedService>();
        
        return services;
    }
}

