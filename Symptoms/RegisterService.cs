using DataAccess.DatabaseAccessLayer;
using DoctorPrescription.Application.Services;

using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;
using Symptoms.Infrastructure.RepositoriesImplement.Symptom;
using symtoms.Domain.Repositories.Systom;

namespace Symptoms
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISymptomsQueryRepository, SymptomQueryRepository>();
            services.AddScoped<ISymptomsCommandRepository, SymptomCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<SymptomService>();

        }
    }
}
