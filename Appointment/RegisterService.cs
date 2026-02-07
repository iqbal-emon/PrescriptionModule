using Appointment.Application.Services;
using Appointment.Domain.Repositories.Appointment;
using Appointment.Infrastructure.RepositoriesImplement;
using DataAccess.DatabaseAccessLayer;
using Doctor.Application.Services;

using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Pharmacies
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAppointmentQueryRepository, AppointmentQueryRepository>();
            services.AddScoped<IAppointmentCommandRepository, AppointmentCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<AppointmentService>();
            // Note: DoctorScheduleDaySessionService is registered in Doctor.RegisterService
            // It will be available through DI if Doctor module is loaded


        }
    }
}
