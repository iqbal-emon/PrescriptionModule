using DataAccess.DatabaseAccessLayer;
using Doctor.Application.Services;
using Doctor.Domain.Repositories.Doctor;
using Doctor.Insfracture.RepositoriesImplement.Doctor;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDoctorCommandRepository, DoctorCommandRepository>();
            services.AddScoped<IDoctorQueryRepository, DoctorQueryRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DoctorService>();
        }
    }
}
