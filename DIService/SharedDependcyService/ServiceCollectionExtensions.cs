using Microsoft.Extensions.DependencyInjection;

namespace DIService.SharedDependcyService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
