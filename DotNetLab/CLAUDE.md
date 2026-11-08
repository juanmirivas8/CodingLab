# DotNetLab — guidance

.NET 10 solution for experimenting with a minimal API, EF Core, messaging and Azure Functions.
The manifests (`*.csproj`, `*.sqlproj`) are authoritative. `README-Docker.md` still mentions .NET 9 and Windows paths.

## Projects (`DotNetLab.sln`)

| Project | Role |
|---------|------|
| `DotNetLab.Model` | Shared models, `Framework/` types and `IntegrationEvents/` messages |
| `DotNetLab.Data` | EF Core (SQL Server) `Context/` and `Repositories/` |
| `DotNetLab.Services` | Business logic: `Interfaces/`, `Implementation/`, DI `Extensions/` |
| `DotNetLab.MinimalAPI.Host` | ASP.NET Core minimal API: `Endpoints/`, OpenAPI, health checks at `/health`, an MCP endpoint (`MapMcp("api-mcp")`), OpenTelemetry, RabbitMQ publishing |
| `DotNetLab.NotificationFunction` | Azure Functions isolated worker triggered by RabbitMQ |
| `DotNetLab.Database` | SQL database project (`Microsoft.Build.Sql`) with post-deployment scripts |

## Commands (run from `DotNetLab/`)

```sh
dotnet restore DotNetLab.sln
dotnet build DotNetLab.sln
dotnet run --project DotNetLab.MinimalAPI.Host   # http://localhost:5123 (https 7256)
```

There is no test project. `dotnet build` is the validation step; add a focused test project if a task introduces non-trivial logic.

## Local services (Docker)

Compose files are in `DeploymentFiles/`: `docker-compose.core.yml` (sqlserver 1433, rabbitmq 5672/15672, azurite,
aspire-dashboard 18888, api 8080) and `docker-compose.ai.yml` (ollama, open-webui). Both use the external network
`dotnetlab-shared`. See `README-Docker.md`.

- Inspect the compose file before starting anything. Never run `docker compose down -v` or remove volumes unless asked: they hold the local databases.
- The API container uses port 8080, the same as the TypeScriptLab dev server.

## Secrets

- Local secrets live in .NET User Secrets (see `README-Secrets.md`), in `DeploymentFiles/.env` and in `NotificationFunction/local.settings.json`.
  Never read, print or overwrite them. Claude's settings deny reading these files.
- Use `appsettings.json` / `appsettings.Development.json` for non-secret configuration only.

## Conventions

- Keep the layering: Host → Services → Data → Model. Endpoints stay thin and call service interfaces registered through the `Extensions/` classes.
- Match the existing patterns (extension-method registration, repository interfaces) rather than introducing new frameworks.
