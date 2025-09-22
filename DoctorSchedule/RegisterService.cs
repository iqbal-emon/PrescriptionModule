using DataAccess.DatabaseAccessLayer;
using DoctorSchedule.Application.Services;
using DoctorSchedule.Domain.Repositories.DoctorSchedule;
using DoctorSchedule.Insfracture.RepositoriesImplement.DoctorSchedule;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Degree
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDoctorScheduleQueryRepository, DoctorScheduleQueryRepository>();
            services.AddScoped<IDoctorScheduleCommandRepository, DoctorScheduleCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DoctorScheduleService>();

        }

    }
}
