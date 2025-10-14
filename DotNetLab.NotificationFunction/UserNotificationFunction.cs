using System.Text.Json;
using DotNetLab.Model.IntegrationEvents;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DotNetLab.NotificationFunction;

public class UserNotificationFunction
{
    private readonly ILogger<UserNotificationFunction> _logger;

    public UserNotificationFunction(ILogger<UserNotificationFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(OnUserUpserted))]
    public void OnUserUpserted(
        [RabbitMQTrigger("notification-service-queue", ConnectionStringSetting = "RabbitMQConnection")] string message)
    {
        _logger.LogInformation("Received user upserted event: {Message}", message);
        
        try
        {
            var userEvent = JsonSerializer.Deserialize<UserUpsertedIntegrationEvent>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            if (userEvent != null)
            {
                _logger.LogInformation("Processing user upserted notification for User ID: {UserId}, CorrelationId: {CorrelationId}", 
                    userEvent.Data, userEvent.CorrelationId);
                
                _logger.LogInformation("User upserted notification processed successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing user upserted notification");
            throw;
        }
    }

    
}

