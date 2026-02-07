using AuthenticationSystem;
using AuthenticationSystem.Application.MiddlewareService;
using AuthenticationSystem.Application.PluginLoaderService;
using AuthenticationSystem.Application.Services;
using DIService.SharedDependcyService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using Utility.Permission;

// Ensure Logs directory exists before configuring Serilog
var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
if (!Directory.Exists(logsDirectory))
{
    Directory.CreateDirectory(logsDirectory);
}

// Configure Serilog early to catch startup errors
// Log files will be created as: log-2024-12-15.txt (daily rolling)
var logFilePath = Path.Combine(logsDirectory, "log-.txt");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: logFilePath,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        shared: true,
        fileSizeLimitBytes: 10485760, // 10 MB per file
        rollOnFileSizeLimit: true)
    .CreateLogger();

// Log the log file path for verification
Log.Information("Logging initialized. Log files will be written to: {LogPath}", logFilePath);

try
{
    Log.Information("Starting PrescriptionModule AuthenticationSystem application");

    var builder = WebApplication.CreateBuilder(args);
    
    // Use Serilog for logging
    builder.Host.UseSerilog();

    builder.Services.AddDIServices();
    builder.Services.AddAuthServices(); // Register AuthenticationSystem services

    // Add services to the container.
    builder.Services.AddControllers();

    // Add Swagger services with JWT Authentication
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        // Add JWT authentication to Swagger
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' followed by your JWT token."
        });

        // Enforce authentication for API calls
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
    });

    builder.Services.AddSwaggerGen(c =>
    {
        c.CustomSchemaIds(type => type.FullName);
    });
    
    // Configure CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"))
            };
        });

    // Apply Authorization Policies from Custom Class
    builder.Services.AddCustomAuthorizationPolicies();

    IConfigurationSection pluginPaths = builder.Configuration.GetSection("PluginPaths:PluginDirectoryPath");
    var pluginBasePath = pluginPaths.Value;

    // Use fallback if plugin path is empty
    if (string.IsNullOrWhiteSpace(pluginBasePath))
    {
        pluginBasePath = Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net8.0");
    }

    PluginLoader.LoadPlugins(builder.Services, Path.Combine(pluginBasePath, "Plugins"));
    PluginLoader.LoadPlugin(builder.Services, Path.Combine(pluginBasePath, "Plugins"));

    var app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableSwaggerInProduction"))
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Get PDF path and ensure directory exists
    var pdfPath = builder.Configuration["AppSettings:PDFCREATEDPATH"];
    if (string.IsNullOrWhiteSpace(pdfPath))
    {
        pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "Prescriptions");
    }

    // Ensure Prescriptions directory exists
    if (!Directory.Exists(pdfPath))
    {
        Directory.CreateDirectory(pdfPath);
    }

    // Ensure Static subdirectory exists for logo
    var staticPath = Path.Combine(pdfPath, "Static");
    if (!Directory.Exists(staticPath))
    {
        Directory.CreateDirectory(staticPath);
    }

    // WWW Root Folder
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(pdfPath),
        RequestPath = "/Prescriptions",
        EnableDirectoryBrowsing = false
    });

    // Apply CORS globally
    app.UseCors("AllowAll");

    // Add correlation ID middleware first (before any other middleware that might log)
    app.UseMiddleware<CorrelationIdMiddleware>();

    // Add request logging middleware to log all API requests and responses
    app.UseMiddleware<RequestLoggingMiddleware>();

    // Add global exception handling middleware early in pipeline
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseMiddleware<TokenAuthenticationMiddleware>();

    app.UseRouting();

    // Add authentication & authorization middleware
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    Log.Information("Application started successfully. Environment: {Environment}", app.Environment.EnvironmentName);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
