using DataAccess.DatabaseAccessLayer;
using Examinations.Application.Services;
using Examinations.Domain.Repositories.Examinations;
using Examinations.Insfracture.RepositoriesImplement.Examinations;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Examinations
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IExaminationsCommandRepository, ExaminationsCommandRepository>();
            services.AddScoped<IExaminationsQueryRepository, ExaminationsQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<ExaminationsService>();


        }
    }
}
