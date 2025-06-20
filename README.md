# Minimal API Demo

A simple C# .NET 8 Minimal API project demonstrating basic API endpoints with comprehensive configuration management.

## Features

- **Minimal API** - No controllers, just endpoints defined in Program.cs
- **Swagger/OpenAPI** - Interactive API documentation (configurable per environment)
- **Weather Forecast** - Sample endpoint returning weather data
- **Echo Service** - POST endpoint that echoes back messages
- **HTTPS Support** - Configured with development certificate
- **Environment-Specific Configuration** - Separate settings for Development and Production
- **Strongly-Typed Configuration** - Type-safe configuration access
- **CORS Support** - Configurable cross-origin resource sharing
- **Health Checks** - Built-in health monitoring endpoint

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 (optional, but recommended)

### Running the Application

1. **Using Visual Studio:**
   - Open `MinimalApiDemo.sln` in Visual Studio
   - Press `F5` or click the "Run" button
   - The application will automatically open Swagger UI in your browser

2. **Using Command Line:**
   ```bash
   # Development environment (default)
   dotnet run
   
   # Production environment
   dotnet run --environment Production
   
   # Custom environment
   dotnet run --environment Staging
   ```

3. **Using Docker (if you have Docker installed):**
   ```bash
   docker build -t minimal-api-demo .
   docker run -p 7000:7000 -p 7001:7001 minimal-api-demo
   ```

## API Endpoints

Once running, you can access:

- **Swagger UI**: `https://localhost:7001/swagger` (or `http://localhost:7000/swagger`)
- **Root**: `GET /` - Returns API information and configuration
- **Hello**: `GET /api/hello` - Returns a JSON response with timestamp and environment
- **Weather**: `GET /api/weather` - Returns a 5-day weather forecast
- **Echo**: `POST /api/echo` - Echoes back the message you send
- **Health**: `GET /health` - Health check endpoint
- **Config**: `GET /api/config` - Configuration details (Development only)

## Configuration Structure

The application uses a hierarchical configuration system:

### Base Configuration (`appsettings.json`)
- Common settings shared across all environments
- Default values for logging, CORS, rate limiting, etc.

### Development Configuration (`appsettings.Development.json`)
- Enhanced logging with Debug level
- Swagger enabled
- Detailed error messages
- Local database connection strings
- Development-specific CORS origins

### Production Configuration (`appsettings.Production.json`)
- Optimized logging (Warning level)
- Swagger disabled for security
- HTTPS required
- Production database connection strings
- Performance optimizations
- Security hardening

## Port Configuration

The application is configured to run on:
- **HTTP**: `http://localhost:7000` (Development) / `http://0.0.0.0:80` (Production)
- **HTTPS**: `https://localhost:7001` (Development) / `https://0.0.0.0:443` (Production)

## HTTPS Troubleshooting

If you encounter HTTPS issues:

1. **Check Development Certificate:**
   ```bash
   dotnet dev-certs https --check
   ```

2. **Trust the Development Certificate:**
   ```bash
   dotnet dev-certs https --trust
   ```

3. **Clear and Recreate Certificate (if needed):**
   ```bash
   dotnet dev-certs https --clean
   dotnet dev-certs https --trust
   ```

4. **Verify Certificate in Windows:**
   - Open "Manage User Certificates" (certmgr.msc)
   - Check "Personal" → "Certificates" for "localhost" certificate

## Example Usage

### Get API Information
```bash
curl https://localhost:7001/
```

### Get Weather Forecast
```bash
curl https://localhost:7001/api/weather
```

### Echo a Message
```bash
curl -X POST https://localhost:7001/api/echo \
  -H "Content-Type: application/json" \
  -d '{"message": "Hello, Minimal API!"}'
```

### Health Check
```bash
curl https://localhost:7001/health
```

### Configuration (Development only)
```bash
curl https://localhost:7001/api/config
```

### Using HTTP (if HTTPS doesn't work)
```bash
curl http://localhost:7000/api/hello
```

## Project Structure

```
MinimalApiDemo/
├── Program.cs                    # Main application entry point and API endpoints
├── MinimalApiDemo.csproj         # Project file with dependencies
├── appsettings.json              # Base configuration
├── appsettings.Development.json  # Development-specific configuration
├── appsettings.Production.json   # Production-specific configuration
├── Configuration/
│   └── AppSettings.cs            # Strongly-typed configuration classes
├── Properties/
│   └── launchSettings.json       # Visual Studio launch configuration
├── MinimalApiDemo.sln            # Visual Studio solution file
├── .gitignore                    # Git ignore rules
└── README.md                     # This file
```

## Configuration Classes

The application uses strongly-typed configuration classes for better IntelliSense and type safety:

- `AppSettings` - Main configuration container
- `ApiSettings` - API metadata (name, version, description)
- `CorsSettings` - Cross-origin resource sharing configuration
- `SwaggerSettings` - OpenAPI documentation settings
- `SecuritySettings` - Security and HTTPS configuration
- `DatabaseSettings` - Database connection and options
- `PerformanceSettings` - Performance optimization settings

## Key Features of Minimal APIs

- **No Controllers** - Endpoints are defined directly in Program.cs
- **Top-level statements** - No need for Program class or Main method
- **Lambda expressions** - Simple, concise endpoint definitions
- **Built-in JSON serialization** - Automatic serialization/deserialization
- **OpenAPI support** - Automatic Swagger documentation generation
- **HTTPS redirection** - Automatic redirection from HTTP to HTTPS
- **Environment-specific configuration** - Different settings per environment
- **Strongly-typed configuration** - Type-safe access to settings

## Environment Variables

You can override configuration using environment variables:

```bash
# Set environment
set ASPNETCORE_ENVIRONMENT=Production

# Override specific settings
set ApiSettings__Name="My Custom API"
set Database__ConnectionString="your-connection-string"
```

## Next Steps

To extend this Minimal API, consider adding:
- Database integration (Entity Framework Core)
- Authentication and authorization
- Additional API endpoints
- Custom middleware
- Logging and monitoring
- Unit tests
- Docker containerization
- CI/CD pipeline configuration 