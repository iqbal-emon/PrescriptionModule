using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using Schedule.Application.Services;
using Schedule.Domain.Repositories.Schedule;
using Schedule.Insfracture.RepositoriesImplement.Schedule;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace PatienFolowUp
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IScheduleCommandRepository, ScheduleCommandRepository>();
            services.AddScoped<IScheduleQueryRepository, ScheduleQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<ScheduleService>();

        }
    }
}
