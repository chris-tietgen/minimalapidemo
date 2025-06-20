using MinimalApiDemo.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure strongly-typed settings
builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger based on settings
if (appSettings?.Swagger.Enabled == true)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new()
        {
            Title = appSettings.Swagger.Title,
            Description = appSettings.Swagger.Description,
            Version = appSettings.Swagger.Version,
            Contact = new()
            {
                Name = appSettings.Swagger.Contact.Name,
                Email = appSettings.Swagger.Contact.Email
            }
        });
    });
}

// Configure HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7001;
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(appSettings?.Cors.AllowedOrigins ?? Array.Empty<string>())
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    if (appSettings?.Swagger.Enabled == true)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    if (appSettings?.Development.ShowDetailedErrors == true)
    {
        app.UseDeveloperExceptionPage();
    }
}

// Use HTTPS redirection if required
if (appSettings?.Security.RequireHttps == true)
{
    app.UseHttpsRedirection();
}

// Use CORS
app.UseCors();

// Define minimal API endpoints
app.MapGet("/", () => new 
{ 
    message = "Hello World! This is a Minimal API.",
    name = appSettings?.ApiSettings.Name,
    version = appSettings?.ApiSettings.Version,
    description = appSettings?.ApiSettings.Description
});

app.MapGet("/api/hello", () => new 
{ 
    message = "Hello from Minimal API!", 
    timestamp = DateTime.UtcNow,
    environment = app.Environment.EnvironmentName
});

app.MapGet("/api/weather", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    
    return forecast;
});

app.MapPost("/api/echo", (EchoRequest request) => 
    new { message = request.Message, receivedAt = DateTime.UtcNow });

// Health check endpoint
if (appSettings?.HealthChecks.Enabled == true)
{
    app.MapGet(appSettings.HealthChecks.Endpoint, () => new 
    { 
        status = "Healthy", 
        timestamp = DateTime.UtcNow,
        environment = app.Environment.EnvironmentName
    });
}

// Configuration endpoint (only in development)
if (app.Environment.IsDevelopment())
{
    app.MapGet("/api/config", () => new
    {
        apiSettings = appSettings?.ApiSettings,
        cors = new { allowedOrigins = appSettings?.Cors.AllowedOrigins },
        swagger = new { enabled = appSettings?.Swagger.Enabled },
        security = new { requireHttps = appSettings?.Security.RequireHttps }
    });
}

app.Run();

// Record types for the API
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record EchoRequest(string Message); 