using DataAccess.DatabaseAccessLayer;
using Diagonosis.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using PrescriptionDiagonosis.Domain.Repositories.PrescriptionDiagonosis;
using PrescriptionDiagonosis.Insfracture.RepositoriesImplement.PrescriptionDiagonosis;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace PrescriptionDiagonosis
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionDiagonsisQueryRepository, PrescriptionDiagonsisQueryRepository>();
            services.AddScoped<IPrescriptionDiagonsisCommandRepository, PrescriptionDiagonsisCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionDiagonsisService>();

        }

    }
}
