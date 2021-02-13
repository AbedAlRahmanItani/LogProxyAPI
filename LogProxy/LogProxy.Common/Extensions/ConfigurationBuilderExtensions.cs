using Microsoft.Extensions.Configuration;
using System;

namespace LogProxy.Common.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder GetConfigurationBuilder(string environmentName, string basePath = null)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder = configurationBuilder.SetBasePath(basePath ?? AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                   .AddEnvironmentVariables();

            return configurationBuilder;
        }
    }
}
