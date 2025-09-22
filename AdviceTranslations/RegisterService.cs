using AdviceTranslations.Domain.Repositories.AdviceTranslations;
using DataAccess.DatabaseAccessLayer;
using DoctorPrescription.Application.Services;
using DoctorPrescription.Insfracture.RepositoriesImplement.AdviceTranslation;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to DoctorPrescription
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAdviceTranslationsCommandRepository, AdviceTranslationsCommandRepository>();
            services.AddScoped<IAdviceTranslationsQueryRepository, AdviceTranslationsQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<AdviceTranslationsService>();
        }
    }
}
