using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application;
using Notification.Application.Services;
using Notification.Domain.Repositories.Notification;
using Notification.Insfracture.RepositoriesImplement.Notification;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Notification
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INotificationQueryRepository, NotificationQueryRepository>();
            services.AddScoped<INotificationCommandRepository, NotificationCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<GreenWebSMSService>();
        }
    }
}
