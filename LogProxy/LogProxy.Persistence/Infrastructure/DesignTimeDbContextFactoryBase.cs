using LogProxy.Common.Constants;
using LogProxy.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace LogProxy.Persistence.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            string connectionStringName = null;
            if (args != null && args.Length > 0)
                connectionStringName = args[0];

            connectionStringName ??= ConfigurationNames.ConnectionStringName;

            return Create(connectionStringName);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string connectionStringName)
        {
            var environmentName = Environment.GetEnvironmentVariable(ConfigurationNames.AspNetCoreEnvironment);
            var configurationBuilder = ConfigurationBuilderExtensions.GetConfigurationBuilder(environmentName);
            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"Connection string '{connectionStringName}' is null or empty.", nameof(connectionString));

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
