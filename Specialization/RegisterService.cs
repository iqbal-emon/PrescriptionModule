using DataAccess.DatabaseAccessLayer;
using Specialization.Application.Services;
using Specialization.Domain.Repositories.Specialization;
using Specialization.Insfracture.RepositoriesImplement.Specialization;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Specialization
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISpecializationQueryRepository, SpecializationQueryRepository>();
            services.AddScoped<ISpecializationCommandRepository, SpecializationCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<SpecializationService>();

        }

    }
}

