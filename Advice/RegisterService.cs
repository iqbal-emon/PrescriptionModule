using Advice.Application.Services;
using Advice.Domain.Repositories.Advice;
using Advice.Domain.Repositories.AdviceTranslations;
using Advice.Insfracture.RepositoriesImplement.Advice;
using Advice.Insfracture.RepositoriesImplement.AdviceTranslation;
using DataAccess.DatabaseAccessLayer;
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

namespace Advice
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to Advice
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAdviceQueryRepository, AdviceQueryRepository>();
            services.AddScoped<IAdviceCommandRepository, AdviceCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<AdviceService>();

            // Register services specific to AdviceTranslations
            services.AddScoped<IAdviceTranslationsCommandRepository, AdviceTranslationsCommandRepository>();
            services.AddScoped<IAdviceTranslationsQueryRepository, AdviceTranslationsQueryRepository>();
            services.AddScoped<AdviceTranslationsService>();
        }
    }
}
