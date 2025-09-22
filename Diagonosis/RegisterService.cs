using DataAccess.DatabaseAccessLayer;
using Diagonosis.Application.Services;
using Diagonosis.Domain.Repositories.Diagonosis;
using Diagonosis.Insfracture.RepositoriesImplement.Diagnonosis;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Degree
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDiagonosisQueryRepository, DiagononosisQueryRepository>();
            services.AddScoped<IDiagonosisCommandRepository, DiagonosisCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DiagonosisService>();

        }

    }
}
