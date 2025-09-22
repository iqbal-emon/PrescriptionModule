using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PatienFolowUp.Application.Services;
using PatienFolowUp.Domain.Repositories.Patients;
using PatienFolowUp.Insfracture.RepositoriesImplement.Patients;
using PluginDIService.PluginDependencyRepository;
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
            services.AddScoped<IPatientsCommandRepository, PatientsCommandRepository>();
            services.AddScoped<IPatientsQueryRepository, PatientsQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PatientsService>();

        }
    }
}
