using DotNetLab.Services.Interfaces;
using DotNetLab.Services.Implementation;
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
        
        // Add Services
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}
