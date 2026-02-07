using DataAccess.DatabaseAccessLayer;
using Speciality.Application.Services;
using Speciality.Domain.Repositories.Speciality;
using Speciality.Insfracture.RepositoriesImplement.Speciality;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Speciality
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISpecialityQueryRepository, SpecialityQueryRepository>();
            services.AddScoped<ISpecialityCommandRepository, SpecialityCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<SpecialityService>();

        }

    }
}

