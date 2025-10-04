using DotNetLab.MinimalAPI.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

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
