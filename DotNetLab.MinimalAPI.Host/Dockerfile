# Use the official .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy only solution file first
COPY *.sln ./

# Copy project files for dependency restoration
COPY DotNetLab.MinimalAPI.Host/DotNetLab.MinimalAPI.Host.csproj ./DotNetLab.MinimalAPI.Host/
COPY DotNetLab.Model/DotNetLab.Model.csproj ./DotNetLab.Model/
COPY DotNetLab.Data/DotNetLab.Data.csproj ./DotNetLab.Data/
COPY DotNetLab.Services/DotNetLab.Services.csproj ./DotNetLab.Services/

# Restore dependencies (this layer will be cached if project files don't change)
RUN dotnet restore DotNetLab.MinimalAPI.Host/DotNetLab.MinimalAPI.Host.csproj

# Copy only source code directories (exclude bin, obj, and other build artifacts)
COPY DotNetLab.MinimalAPI.Host/. ./DotNetLab.MinimalAPI.Host/
COPY DotNetLab.Model/. ./DotNetLab.Model/
COPY DotNetLab.Data/. ./DotNetLab.Data/
COPY DotNetLab.Services/. ./DotNetLab.Services/

# Build and publish the application
RUN dotnet publish DotNetLab.MinimalAPI.Host/DotNetLab.MinimalAPI.Host.csproj -c Release -o out --no-restore

# Use the official .NET 9.0 runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the built application
COPY --from=build-env /app/out .

# Expose port
EXPOSE 8080

# Set environment variables - bind to both IPv4 and IPv6
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV ASPNETCORE_ENVIRONMENT=Docker

# Start the application
ENTRYPOINT ["dotnet", "DotNetLab.MinimalAPI.Host.dll"]
