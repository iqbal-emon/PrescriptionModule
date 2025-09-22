using DataAccess.DatabaseAccessLayer;
using DoctorPrescription.Application.Services;
using DoctorPrescription.Domain.Repositories.PrescriptionAdvice;
using DoctorPrescription.Insfracture.RepositoriesImplement.PrescriptionAdvice;
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
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionAdviceQueryRepository, PrescriptionAdviceQueryRepository>();
            services.AddScoped<IPrescriptionAdviceCommandRepository, PrescriptionAdviceCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionAdviceService>();
        }
    }
}
