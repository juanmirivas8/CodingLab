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

    [Function(nameof(OnUserCreated))]
    public void OnUserCreated(
        [RabbitMQTrigger("user.notifications.created", ConnectionStringSetting = "RabbitMQConnection")] string message)
    {
        _logger.LogInformation("Received user created event: {Message}", message);
        
        try
        {
            var userEvent = JsonSerializer.Deserialize<UserUpsertedIntegrationEvent>(message);
            
            if (userEvent != null)
            {
                _logger.LogInformation("Processing user created notification for User ID: {UserId}, CorrelationId: {CorrelationId}", 
                    userEvent.Data, userEvent.CorrelationId);
                
                // TODO: Implement notification logic here
                // For example:
                // - Send email notification
                // - Send push notification
                // - Log to external system
                // - Update analytics
                
                _logger.LogInformation("User created notification processed successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing user created notification");
            throw;
        }
    }

    
}

