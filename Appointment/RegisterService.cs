using ApiCallService.BaseApiCallService;
using Appointment.Application.Services;
using Appointment.Domain.Repositories.Appointment;
using Appointment.Infrastructure.RepositoriesImplement;
using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Appointment
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
            services.AddScoped<IBaseRestClientApiService, BaseRestClientApiService>();
            services.AddScoped<AppointmentService>();
            // Note: HttpClientFactory is registered by default in ASP.NET Core
            // DoctorScheduleDaySessionService is now called via API endpoint instead of direct dependency
        }
    }
}
