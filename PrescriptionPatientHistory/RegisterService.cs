using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass.PrescriptionEntity;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using PrescriptionPatientHistory.Application.Services;
using PrescriptionPatientHistory.Domain.Repositories.PrescriptionPatientHistory;
using PrescriptionPatientHistory.Insfracture.RepositoriesImplement.PrescriptionPatientHistory;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;


namespace Diseases
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Register services specific to DoctorPrescription
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionPatientHistoryQueryRepository, PrescriptionPatientHistoryQueryRepository>();
            services.AddScoped<IPrescriptionPatientHistoryCommandRepository, PrescriptionPatientHistoryCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionPatientHistoryService>();

        }
    }
}
