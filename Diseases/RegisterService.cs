using DataAccess.DatabaseAccessLayer;
using Diseases.Domain.Repositories.Diseases;
using Diseases.Insfracture.RepositoriesImplement.Diseases;
using DoctorPrescription.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;


namespace Diseases
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to DoctorPrescription
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDiseasesQueryRepository, DiseasesQueryRepository>();
            services.AddScoped<IDiseasesCommandRepository, DiseasesCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DiseasesService>();

        }
    }
}
