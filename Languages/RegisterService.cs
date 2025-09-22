using DataAccess.DatabaseAccessLayer;
using Languages.Application.Services;
using Languages.Domain.Repositories.Languages;
using Languages.Insfracture.RepositoriesImplement.Languages;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Languages
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILanguagesQueryRepository, LanguagesQueryRepository>();
            services.AddScoped<ILanguagesCommandRepository, LanguagesCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<LanguagesService>();

        }
    }
}
