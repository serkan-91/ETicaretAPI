using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence
{
    static class Configurations
    {
        private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EticaretAPI.API"))
            .AddJsonFile("appsettings.json")
            .Build(); 
        public static string ConnectionString => configuration.GetConnectionString("PostgreSQL");  
    }
}
