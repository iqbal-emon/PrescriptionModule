using DataAccess.DatabaseAccessLayer;
using Medication.Application.Services;
using Medication.Domain.Repositories.Medication;
using Medication.Domain.Repositories.MedicationBrand;
using Medication.Domain.Repositories.MedicationManufacturer;
using Medication.Infrastructure.RepositoriesImplement.MedicationBrand;
using Medication.Insfracture.RepositoriesImplement.Medication;
using Medication.Insfracture.RepositoriesImplement.MedicationBrand;
using Medication.Insfracture.RepositoriesImplement.MedicationManufacturer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace DoctorPrescription
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to Medication
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMedicationQueryRepository, MedicationQueryRepository>();
            services.AddScoped<IMedicationCommandRepository, MedicationCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<MedicationService>();

            // Register services specific to MedicationBrand
            services.AddScoped<IMedicationBrandQueryRepository, MedicationBrandQueryRepository>();
            services.AddScoped<IMedicationBrandCommandRepository, MedicationBrandCommandRepository>();
            services.AddScoped<MedicationBrandService>();

            // Register services specific to Medication Manufacturer
            services.AddScoped<IMedicationManufacturerQueryRepository, MedicationManufacturerQueryRepository>();
            services.AddScoped<IMedicationManufacturerCommandRepository, MedicationManufacturerCommandRepository>();
            services.AddScoped<MedicationManufacturerService>();
        }
    }
}
