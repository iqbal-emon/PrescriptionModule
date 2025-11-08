//using AuthenticationSystem;
//using AuthenticationSystem.MiddlewareService;
//using AuthenticationSystem.Models.MiddlewareModels;
//using AuthenticationSystem.PluginLoaderService;
//using AuthenticationSystem.Services;
//using DIService.SharedDependcyService;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Utility.Permission;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDIServices();

//// Add services to the container.
//builder.Services.AddControllers();

//// Add Swagger services
//builder.Services.AddEndpointsApiExplorer(); // This enables the Swagger UI for API endpoints.
//builder.Services.AddSwaggerGen(); // Adds the Swagger generator to generate OpenAPI spec.



//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserService, UserService>();

//// Configure CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()  // Allow requests from any origin
//               .AllowAnyMethod()  // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
//               .AllowAnyHeader(); // Allow any headers in requests
//    });
//});

//builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidateLifetime = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"))
//                };
//            });
////builder.Services.AddAuthorization(options =>
////{
////    options.AddPolicy("PermissionPolicy", policy =>
////        policy.Requirements.Add(new PermissionRequirement("SomePermission")));
////});

//// Apply Authorization Policies from Custom Class
//builder.Services.AddCustomAuthorizationPolicies();


//IConfigurationSection pluginPaths = builder.Configuration.GetSection("PluginPaths:PluginDirectoryPath");

//var pluginBasePath = pluginPaths.Value;

//PluginLoader.LoadPlugins(builder.Services, Path.Combine(pluginBasePath, "Plugins"));
//PluginLoader.LoadPlugin(builder.Services, Path.Combine(pluginBasePath, "Plugins"));

//var app = builder.Build();

////// Fetch and Register Dynamic Policies After Service Build (e.g., from DB)
////using (var scope = app.Services.CreateScope())
////{
////    var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
////    var authorizationOptions = scope.ServiceProvider.GetRequiredService<IAuthorizationPolicyProvider>() as AuthorizationOptions;

////    var permissions = permissionService.GetPermissions(); // Fetch permissions from DB

////    foreach (var permission in permissions)
////    {
////        authorizationOptions?.AddPolicy(permission.PolicyName, policy =>
////            policy.Requirements.Add(new PermissionRequirement(permission.PermissionKey)));
////    }
////}

//if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableSwaggerInProduction"))
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//// Add Authorization policies



//// Apply CORS globally
//app.UseCors("AllowAll");

//app.UseMiddleware<TokenAuthenticationMiddleware>();

//app.UseRouting();

//// Add authentication & authorization middleware if applicable
//app.UseAuthentication();
//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers(); // Expose all loaded controllers.
//});

//app.Run();



using AuthenticationSystem;
using AuthenticationSystem.MiddlewareService;
using AuthenticationSystem.Models.MiddlewareModels;
using AuthenticationSystem.PluginLoaderService;
using AuthenticationSystem.Services;
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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserService, UserService>();
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

PluginLoader.LoadPlugins(builder.Services, Path.Combine(pluginBasePath, "Plugins"));
PluginLoader.LoadPlugin(builder.Services, Path.Combine(pluginBasePath, "Plugins"));

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableSwaggerInProduction"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//IConfigurationSection getPublicRootPath = builder.Configuration.GetSection("AppSettings:PDFCREATEDPATH");
//var PublicRootPath = getPublicRootPath.Value.Replace("\\","/");
// WWW Root Folder
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
        builder.Configuration["AppSettings:PDFCREATEDPATH"] ??
        Path.Combine(Directory.GetCurrentDirectory(), "Prescriptions")),
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
