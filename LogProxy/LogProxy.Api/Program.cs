using System;
using System.IO;
using LogProxy.Application.Constants;
using LogProxy.Application.Extensions;
using LogProxy.Persistence.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LogProxy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            var dbContextArgs = new string[]
            {
                ConfigurationNames.ConnectionStringName
            };

            var factory = new ApplicationDbContextFactory();
            using (var dbContext = factory.CreateDbContext(dbContextArgs))
            {
                dbContext.Database.Migrate();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                    config = ConfigurationBuilderExtensions.GetConfigurationBuilder(environmentName);
                })
                .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
