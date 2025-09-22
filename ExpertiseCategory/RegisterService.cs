using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Insfracture.RepositoriesImplement.DoctorChamber;
using ExpertiseCategory.Application.Services;
using ExpertiseCategory.Domain.Repositories.ExpertiseCategory;
using ExpertiseCategory.Insfracture.RepositoriesImplement.ExpertiseCategory;
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
            services.AddScoped<IExpertiseCategoryQueryRepository, ExpertiseCategoryQueryRepository>();
            services.AddScoped<IExpertiseCategoryCommandRepository, ExpertiseCategoryCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<ExpertiseCategoryService>();

        }

    }
}
