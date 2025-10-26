using DotNetLab.MinimalAPI.Host.Extensions;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Configure OpenTelemetry
var resourceBuilder = ResourceBuilder
    .CreateDefault()
    .AddService(builder.Environment.ApplicationName, serviceVersion: "1.0.0", serviceInstanceId: Environment.MachineName);

// Add OpenTelemetry services
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation();

        if (!string.IsNullOrEmpty(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            tracing.AddOtlpExporter();
        }
    })
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation();
            
        if (!string.IsNullOrEmpty(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            metrics.AddOtlpExporter();
        }
    });

// Configure logging to send to OpenTelemetry
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
    logging.SetResourceBuilder(resourceBuilder);
    
    // Only add OTLP exporter if endpoint is configured
    if (!string.IsNullOrEmpty(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
    {
        logging.AddOtlpExporter();
    }
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Only use HTTPS redirection in development (not in Docker)
if (!app.Environment.EnvironmentName.Equals("Docker"))
{
    app.UseHttpsRedirection();
}

// Add health check endpoint
app.MapHealthChecks("/health");

// Map all endpoints
app.MapApplicationEndpoints();
app.MapMcp("api-mcp");

app.Run();
