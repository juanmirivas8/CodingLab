using DotNetLab.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetLab.Services.Implementation;

/// <summary>
/// Hosted service that ensures RabbitMQ infrastructure (exchanges, queues, bindings) is set up on startup
/// </summary>
public class RabbitMqInfrastructureHostedService : IHostedService
{
    private readonly IRabbitMqInfrastructureManager _infrastructureManager;
    private readonly RabbitMqInfrastructureConfiguration _configuration;
    private readonly ILogger<RabbitMqInfrastructureHostedService> _logger;

    public RabbitMqInfrastructureHostedService(
        IRabbitMqInfrastructureManager infrastructureManager,
        IOptions<RabbitMqInfrastructureConfiguration> configuration,
        ILogger<RabbitMqInfrastructureHostedService> logger)
    {
        _infrastructureManager = infrastructureManager;
        _configuration = configuration.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Infrastructure Hosted Service is starting - setting up exchanges, queues, and bindings");

        try
        {
            // Declare all exchanges
            foreach (var exchange in _configuration.Exchanges)
            {
                await _infrastructureManager.DeclareExchangeAsync(
                    exchange.Name,
                    exchange.Type,
                    exchange.Durable,
                    exchange.AutoDelete,
                    cancellationToken);
            }

            // Declare all queues
            foreach (var queue in _configuration.Queues)
            {
                await _infrastructureManager.DeclareQueueAsync(
                    queue.Name,
                    queue.Durable,
                    queue.Exclusive,
                    queue.AutoDelete,
                    cancellationToken);
            }

            // Create all bindings
            foreach (var binding in _configuration.Bindings)
            {
                await _infrastructureManager.BindQueueAsync(
                    binding.QueueName,
                    binding.ExchangeName,
                    binding.RoutingKey,
                    cancellationToken);
            }

            _logger.LogInformation("RabbitMQ infrastructure setup completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to set up RabbitMQ infrastructure");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Infrastructure Hosted Service is stopping");
        return Task.CompletedTask;
    }
}
