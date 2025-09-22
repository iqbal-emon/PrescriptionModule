using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using PrescriptionPdf.Application.Services;
using PrescriptionPdf.Domain.Repositories.PrescriptionPdf;
using PrescriptionPdf.Infrastructure.RepositoriesImplement.PrescriptionPdf;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace PrescriptionPdf
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionPdfCommandRepository, PrescriptionPdfCommandRepository>();
            services.AddScoped<IPrescriptionPdfQueryRepository, PrescriptionPdfQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionPdfService>();

        }
    }
}
