using DataAccess.DatabaseAccessLayer;
using EmailTemplate.Application.Services;
using EmailTemplate.Domain.Repositories.EmailTemplate;
using EmailTemplate.Insfracture.RepositoriesImplement.EmailTemplate;
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
            services.AddScoped<IEmailTemplateQueryRepository, EmailTemplateQueryRepository>();
            services.AddScoped<IEmailTemplateCommandRepository, EmailTemplateCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<EmailTemplateService>();

        }
    }
}
