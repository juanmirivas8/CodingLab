using DotNetLab.Services.Interfaces;
using DotNetLab.Services.Implementation;

namespace DotNetLab.MinimalAPI.Host.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add OpenAPI/Swagger
        services.AddOpenApi();
        
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}
