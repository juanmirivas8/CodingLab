# DotNetLab Docker Deployment

This document explains how to run the DotNetLab MinimalAPI application using Docker containers.

## Prerequisites

- Docker Desktop installed and running
- .NET 9.0 SDK (for local development)

## Quick Start

1. Navigate to the deployment directory:
   ```cmd
   cd DeploymentFiles
   ```

2. Build and start all services:
   ```cmd
   docker-compose up --build
   ```

3. The application will be available at:
   - API: http://localhost:8080
   - Swagger UI: http://localhost:8080/swagger
   - SQL Server: localhost:1433

## Services

### SQL Server
- **Container**: dotnetlab-sqlserver
- **Port**: 1433
- **Username**: sa
- **Password**: YourStrong@Passw0rd
- **Database**: DotNetLab.Database

### MinimalAPI
- **Container**: dotnetlab-api
- **Port**: 8080
- **Health Check**: http://localhost:8080/health
- **Swagger**: http://localhost:8080/swagger

## Commands

### Start services in background:
```cmd
docker-compose up -d
```

### Stop services:
```cmd
docker-compose down
```

### View logs:
```cmd
docker-compose logs -f api
docker-compose logs -f sqlserver
```

### Rebuild API container:
```cmd
docker-compose build api
docker-compose up -d api
```

## Health Checks

The API includes health checks that verify:
- Application is running
- Database connectivity

Access health status at: http://localhost:8080/health

## Environment Configuration

The application uses different configuration files:
- `appsettings.json` - Local development
- `appsettings.Docker.json` - Docker environment
- Environment variables in docker-compose override connection strings

## Troubleshooting

1. **SQL Server connection issues**: Wait for the SQL Server container to be fully healthy before the API starts
2. **Port conflicts**: Ensure ports 8080 and 1433 are not in use by other applications
3. **Build issues**: Try `docker-compose build --no-cache` to force rebuild
