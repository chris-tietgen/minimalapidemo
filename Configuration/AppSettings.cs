namespace MinimalApiDemo.Configuration;

public class AppSettings
{
    public ApiSettings ApiSettings { get; set; } = new();
    public CorsSettings Cors { get; set; } = new();
    public RateLimitingSettings RateLimiting { get; set; } = new();
    public HealthChecksSettings HealthChecks { get; set; } = new();
    public SwaggerSettings Swagger { get; set; } = new();
    public DatabaseSettings Database { get; set; } = new();
    public SecuritySettings Security { get; set; } = new();
    public DevelopmentSettings Development { get; set; } = new();
    public CachingSettings Caching { get; set; } = new();
    public MonitoringSettings Monitoring { get; set; } = new();
    public PerformanceSettings Performance { get; set; } = new();
}

public class ApiSettings
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CorsSettings
{
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
}

public class RateLimitingSettings
{
    public int PermitLimit { get; set; } = 100;
    public string Window { get; set; } = "00:01:00";
    public int SegmentsPerWindow { get; set; } = 10;
}

public class HealthChecksSettings
{
    public bool Enabled { get; set; } = true;
    public string Endpoint { get; set; } = "/health";
}

public class SwaggerSettings
{
    public bool Enabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public ContactInfo Contact { get; set; } = new();
}

public class ContactInfo
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool EnableSensitiveDataLogging { get; set; } = false;
    public bool EnableDetailedErrors { get; set; } = false;
}

public class SecuritySettings
{
    public bool RequireHttps { get; set; } = true;
    public bool EnableDetailedErrors { get; set; } = false;
    public HstsSettings Hsts { get; set; } = new();
}

public class HstsSettings
{
    public bool Enabled { get; set; } = true;
    public string MaxAge { get; set; } = "365.00:00:00";
    public bool IncludeSubDomains { get; set; } = true;
    public bool Preload { get; set; } = true;
}

public class DevelopmentSettings
{
    public bool EnableHotReload { get; set; } = true;
    public bool ShowDetailedErrors { get; set; } = true;
    public bool EnableDeveloperExceptionPage { get; set; } = true;
}

public class CachingSettings
{
    public bool Enabled { get; set; } = true;
    public int DefaultExpirationMinutes { get; set; } = 30;
    public int MaxSize { get; set; } = 1000;
}

public class MonitoringSettings
{
    public bool EnableMetrics { get; set; } = true;
    public bool EnableTracing { get; set; } = true;
    public ApplicationInsightsSettings ApplicationInsights { get; set; } = new();
}

public class ApplicationInsightsSettings
{
    public string InstrumentationKey { get; set; } = string.Empty;
}

public class PerformanceSettings
{
    public bool EnableResponseCompression { get; set; } = true;
    public bool EnableResponseCaching { get; set; } = true;
    public int MaxConcurrentRequests { get; set; } = 100;
} 