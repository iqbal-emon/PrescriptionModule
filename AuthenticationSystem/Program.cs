
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
using System.Text;
using Utility.Permission;

var builder = WebApplication.CreateBuilder(args);

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

app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseRouting();

// Add authentication & authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
