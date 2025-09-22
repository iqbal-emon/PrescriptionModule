using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class AppSettings
    {
        private static IConfigurationRoot configuration;

        static AppSettings()
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
        }

        public static string ConnectionStringForDapper => configuration.GetSection("ConnectionStrings:Default").Value;
        public static string ConnectionStringForEntityFramework => configuration.GetSection("ConnectionStrings:EntityConnection").Value;
        public static string Token => configuration.GetSection("AppSettings:Token").Value;
        public static string PublicApiKey => configuration.GetSection("AppSettings:PublicApiKey").Value;
        public static string TokenExpireTimeInSecound => configuration.GetSection("AppSettings:TokenExpireTimeInSecound").Value;
    }
}
