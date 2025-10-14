using DotNetLab.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetLab.Services.Implementation;

/// <summary>
/// Background service that hosts the RabbitMQ consumer and manages its lifecycle
/// </summary>
public class RabbitMqConsumerHostedService : BackgroundService
{
    private readonly IRabbitMqConsumer _consumer;
    private readonly ILogger<RabbitMqConsumerHostedService> _logger;

    public RabbitMqConsumerHostedService(
        IRabbitMqConsumer consumer, 
        ILogger<RabbitMqConsumerHostedService> logger)
    {
        _consumer = consumer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RabbitMQ Consumer Hosted Service is starting");

        try
        {
            // Start the consumer connection
            await _consumer.StartAsync(stoppingToken);

            // Subscribe to queues here
            // Example: _consumer.Subscribe<YourMessageType>("queue-name", HandleMessageAsync, stoppingToken);
            
            _logger.LogInformation("RabbitMQ Consumer Hosted Service is ready to consume messages");

            // Keep the service running until cancellation is requested
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("RabbitMQ Consumer Hosted Service is stopping due to cancellation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatal error in RabbitMQ Consumer Hosted Service");
            throw;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Consumer Hosted Service is stopping");
        
        await _consumer.StopAsync(cancellationToken);
        
        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}
