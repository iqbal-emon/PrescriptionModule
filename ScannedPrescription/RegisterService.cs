using DataAccess.DatabaseAccessLayer;
using DoctorPrescription.Application.Services;
using DoctorPrescription.Insfracture.RepositoriesImplement.ScannedPrescription;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using ScannedPrescription.Domain.Repositories.ScannedPrescription;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace ScannedPrescription
{
    public class RegisterService: IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IScannedPrescrptionCommandRepository, ScannedPrescriptionCommandRepository>();
            services.AddScoped<IScannedPrescrptionQueryRepository, ScannedPrescriptionQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<ScannedPrescriptionService>();

        }

    }
}
