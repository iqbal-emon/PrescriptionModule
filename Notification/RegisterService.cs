using Microsoft.Extensions.DependencyInjection;
using Notification.Application;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;

namespace Notification
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<GreenWebSMSService>();
        }
    }
}
