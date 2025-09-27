using DotNetLab.MinimalAPI.Host.Endpoints;

namespace DotNetLab.MinimalAPI.Host.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapApplicationEndpoints(this WebApplication app)
    {
        app.MapUserEndpoints();
        return app;
    }
}
