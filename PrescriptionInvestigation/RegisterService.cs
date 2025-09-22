using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using PrescriptionInvestigation.Application.services;
using PrescriptionInvestigation.Domain.Repositories.PrescriptionInvestigation;
using PrescriptionInvestigation.Insfracture.RepositoriesImplement.PrescriptionInvestigation;
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
            services.AddScoped<IPrescriptionInvestigationQueryRepository, PrescriptionInvestigationQueryRepository>();
            services.AddScoped<IPrescriptionInvestigationCommandRepository, PrescriptionInvestigationCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionInvestigationService>();
        }
    }
}
