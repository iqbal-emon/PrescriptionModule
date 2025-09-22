using DataAccess.DatabaseAccessLayer;
using Medication.Application.Services;
using Medication.Infrastructure.RepositoriesImplement.MedicationBrand;
using Medication.Insfracture.RepositoriesImplement.MedicationBrand;
using MedicationBrand.Domain.Repositories.MedicationBrand;
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

namespace MedicationBrand
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to DoctorPrescription
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMedicationBrandQueryRepository, MedicationBrandQueryRepository>();
            services.AddScoped<IMedicationBrandCommandRepository, MedicationBrandCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<MedicationBrandService>();
        }
    }
}
