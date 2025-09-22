using DataAccess.DatabaseAccessLayer;
using FollowUp.Application.Services;
using FollowUp.Domain.Repositories.FollowUp;
using FollowUp.Insfracture.RepositoriesImplement.FollowUp;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace PatientFollowUp
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFollowUpCommandRepository, FollowUpCommandRepository>();
            services.AddScoped<IFollowUpQueryRepository, FollowUpQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<FollowUpService>();

        }
    }
}
