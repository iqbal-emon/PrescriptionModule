using DataAccess.DatabaseAccessLayer;
using Investigation.Application.Services;
using Investigation.Domain.Repositories.Investigation;
using Investigation.Insfracture.RepositoriesImplement.Investigation;
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

namespace Investigation
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to DoctorPrescription
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IInvestigationQueryRepository, InvestigationQueryRepository>();
            services.AddScoped<IInvestigationCommandRepository, InvestigationCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<InvestigationService>();
        }
    }
}
