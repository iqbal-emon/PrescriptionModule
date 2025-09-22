using Microsoft.Extensions.DependencyInjection;

namespace PluginDIService.PluginDependencyRepository
{
    public interface IPlugin
    {
        void RegisterServices(IServiceCollection services);
    }
}
