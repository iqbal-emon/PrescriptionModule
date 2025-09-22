using DataAccess.DatabaseAccessLayer;
using MedicationManufacture.Application.Services;
using MedicationManufacturer.Domain.Repositories.MedicationManufacturer;
using MedicationManufacturer.Insfracture.RepositoriesImplement.MedicationManufacturer;
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
            services.AddScoped<IMedicationManufacturerQueryRepository, MedicationManufacturerQueryRepository>();
            services.AddScoped<IMedicationManufacturerCommandRepository, MedicationManufacturerCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<MedicationManufacturerService>();
        }
    }
}
