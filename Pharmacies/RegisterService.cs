using DataAccess.DatabaseAccessLayer;

using Microsoft.Extensions.DependencyInjection;
using Pharmacies.Application.Services;
using Pharmacies.Domain.Repositories.Pharmacies;
using Pharmacies.Insfracture.RepositoriesImplement.Pharmacy;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace Pharmacies
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPharmaciesQueryRepository, PharmaciesQueryRepository>();
            services.AddScoped<IPharmaciesCommandRepository, PharmaciesCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PharmacyService>();


        }
    }
}
