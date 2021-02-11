using LogProxy.Application.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace LogProxy.Persistence.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private IConfiguration _configuration;

        public DesignTimeDbContextFactoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
            var connectionString = _configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"Connection string '{connectionStringName}' is null or empty.", nameof(connectionString));
            
            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
