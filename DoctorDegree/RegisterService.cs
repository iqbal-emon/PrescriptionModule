using DataAccess.DatabaseAccessLayer;
using DoctorDegree.Application.Services;
using DoctorDegree.Domain.Repositories.DoctorDegree;
using DoctorDegree.Insfracture.RepositoriesImplement.DoctorDegree;
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
            services.AddScoped<IDoctorDegreeQueryRepository, DoctorDegreeQueryRepository>();
            services.AddScoped<IDoctorDegreeCommandRepository, DoctorDegreeCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DoctorDegreeService>();

        }

    }
}
