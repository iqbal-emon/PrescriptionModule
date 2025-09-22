using DataAccess.DatabaseAccessLayer;
using DoctorExpertise.Application.Services;
using DoctorExpertise.Domain.Repositories.DoctorExpertise;
using DoctorExpertise.Insfracture.RepositoriesImplement.DoctorExpertise;
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
            services.AddScoped<IDoctorExpertiseQueryRepository, DoctorExpertiseQueryRepository>();
            services.AddScoped<IDoctorExpertiseCommandRepository, DoctorExpertiseCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DoctorExpertiseService>();

        }

    }
}
