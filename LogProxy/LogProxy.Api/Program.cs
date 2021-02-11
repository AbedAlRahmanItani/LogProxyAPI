using System;
using System.IO;
using LogProxy.Application.Constants;
using LogProxy.Application.Extensions;
using LogProxy.Persistence.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LogProxy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dbContextArgs = new string[]
            {
                ConfigurationNames.ConnectionStringName
            };

            var environmentName = Environment.GetEnvironmentVariable(ConfigurationNames.AspNetCoreEnvironment);
            var configurationBuilder = ConfigurationBuilderExtensions.GetConfigurationBuilder(environmentName);
            var configuration = configurationBuilder.Build();
            
            var factory = new ApplicationDbContextFactory(configuration);
            using (var dbContext = factory.CreateDbContext(dbContextArgs))
            {
                dbContext.Database.Migrate();
            }

            CreateWebHostBuilder(args, configuration).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration configuration) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
