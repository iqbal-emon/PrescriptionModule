using ApiCallService.BaseApiCallService;
using Microsoft.Extensions.DependencyInjection;

namespace DIService.SharedDependcyService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            // Register shared services that are used across multiple plugins
            services.AddScoped<IBaseRestClientApiService, BaseRestClientApiService>();
            return services;
        }
    }
}
