using Microsoft.Extensions.DependencyInjection;
using PDTCreator.Application;
using PluginDIService.PluginDependencyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDTCreator
{
    internal class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<PDFWHKService>();
        }
    }
}
