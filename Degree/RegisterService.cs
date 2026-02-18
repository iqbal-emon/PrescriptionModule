using DataAccess.DatabaseAccessLayer;
using Degree.Application.Services;
using Degree.Domain.Repositories.Degree;
using Degree.Insfracture.RepositoriesImplement.Degree;
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
            services.AddScoped<IDegreeQueryRepository, DegreeQueryRepository>();
            services.AddScoped<IDegreeCommandRepository, DegreeCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DegreeService>();

        }

    }
}
