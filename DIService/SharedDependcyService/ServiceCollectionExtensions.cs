using ApiCallService.BaseApiCallService;
using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace DIService.SharedDependcyService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            // Register shared services that are used across multiple plugins
            // These services are registered centrally to avoid duplicate registrations
            // Individual plugins may still register them, but this ensures they're available
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<IBaseRestClientApiService, BaseRestClientApiService>();
            return services;
        }
    }
}
