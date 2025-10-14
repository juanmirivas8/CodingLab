using DotNetLab.Services.Interfaces;
using DotNetLab.Services.Implementation;
using DotNetLab.Services.Extensions;
using DotNetLab.Data.Context;
using DotNetLab.Data.Repositories.Interfaces;
using DotNetLab.Data.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace DotNetLab.MinimalAPI.Host.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add OpenAPI/Swagger
        services.AddOpenApi();
        
        // Add Entity Framework
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        // Add Repository Pattern dependencies
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        // Add RabbitMQ configuration
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.Configure<RabbitMqInfrastructureConfiguration>(configuration.GetSection("RabbitMq:Infrastructure"));
        
        // Add all RabbitMQ services (publisher, consumer, infrastructure)
        services.AddRabbitMqServices();
        
        services.AddScoped<IUserService, UserService>();
        
        // Add health checks with database connectivity check
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!, name: "database");
        
        return services;
    }
}
