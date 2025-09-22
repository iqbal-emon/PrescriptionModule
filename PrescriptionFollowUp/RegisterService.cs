using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using PrescriptionFollowUp.Application.Services;
using PrescriptionFollowUp.Domain.Repositories.PrescriptionFollowUp;
using PrescriptionFollowUp.Insfracture.RepositoriesImplement.PrescriptioinFollowUp;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionFollowUp
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to DoctorPrescription
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionFollowUpQueryRepository, PrescriptionFollowUpQueryRepository>();
            services.AddScoped<IPrescriptionFollowUpCommandRepository, PrescriptionFollowUpCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionFollowUpService>();
        }
    }
}
