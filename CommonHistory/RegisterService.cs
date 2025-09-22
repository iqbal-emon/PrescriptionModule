using CommonHistory.Application.Services;
using CommonHistory.Domain.Repositories.CommonHistory;
using CommonHistory.Insfracture.CommonHistory;
using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Diagonosis
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICommonHistoryCommandRepository, CommonHistoryCommandRepository>();
            services.AddScoped<ICommonHistoryQueryRepository, CommonHistoryQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<CommonHistoryService>();

        }

    }
}
