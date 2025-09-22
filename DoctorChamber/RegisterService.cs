using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Application.Services;
using DoctorChamber.Domain.Repositories.DoctorChamber;
using DoctorChamber.Insfracture.RepositoriesImplement.DoctorChamber;
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
            services.AddScoped<IDoctorChamberQueryRepository, DoctorChamberQueryRepository>();
            services.AddScoped<IDoctorChamberCommandRepository, DoctorChamberCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DoctorChamberService>();

        }

    }
}
