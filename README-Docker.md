# DotNetLab Docker Deployment

This document explains how to run DotNetLab with a separated Docker Compose structure:

- Core stack (`sqlserver`, `rabbitmq`, `azurite`, `aspire-dashboard`, `api`)
- AI stack (`ollama`, `open-webui`)

Compose files are located in `DeploymentFiles`:

- `DeploymentFiles/docker-compose.core.yml`
- `DeploymentFiles/docker-compose.ai.yml`
- `DeploymentFiles/docker-compose.yml` (original full stack file)

## Prerequisites

- Docker Desktop installed and running
- .NET 9.0 SDK (for local development)

## One-time setup

Create the shared network used by both stacks:

```powershell
Set-Location "C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DeploymentFiles"
docker network create dotnetlab-shared
```

If the network already exists, Docker will return an "already exists" message and you can continue.

## Start stacks independently

Run only the core services:

```powershell
Set-Location "C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DeploymentFiles"
docker compose -p dotnetlab-core -f .\docker-compose.core.yml up -d
```

Run only the AI services:

```powershell
Set-Location "C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DeploymentFiles"
docker compose -p dotnetlab-ai -f .\docker-compose.ai.yml up -d
```

## Start both stacks together

```powershell
Set-Location "C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DeploymentFiles"
docker compose -p dotnetlab -f .\docker-compose.core.yml -f .\docker-compose.ai.yml up -d
```

## Stop stacks

```powershell
Set-Location "C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DeploymentFiles"
docker compose -p dotnetlab-core -f .\docker-compose.core.yml down
docker compose -p dotnetlab-ai -f .\docker-compose.ai.yml down
```

## Services and endpoints

- API: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`
- SQL Server: `localhost:1433`
- RabbitMQ Management UI: `http://localhost:15672`
- Aspire Dashboard: `http://localhost:18888`
- Ollama API: `http://localhost:11434`
- Open WebUI: `http://localhost:3000`

## Logs

```powershell
Set-Location "C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DeploymentFiles"
docker compose -p dotnetlab-core -f .\docker-compose.core.yml logs -f api
docker compose -p dotnetlab-ai -f .\docker-compose.ai.yml logs -f open-webui
```

## Notes

- Current environment values are read from `DeploymentFiles/.env` by default.
- The core compose fixes the telemetry variable typo to `OTEL_EXPORTER_OTLP_PROTOCOL`.
- If you do not use an NVIDIA GPU, remove the `devices` section in `DeploymentFiles/docker-compose.ai.yml`.
