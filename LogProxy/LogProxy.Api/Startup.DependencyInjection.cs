using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogProxy.Application.Services.Security;
using LogProxy.Infrastructure.Services.Security;
using LogProxy.Persistence.Context;
using LogProxy.Application.Options;
using LogProxy.Application.Interfaces.Providers;
using LogProxy.Infrastructure.Providers;

namespace LogProxy.Api
{
    public static class StartupDependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
               .AddScoped<DbContext, ApplicationDbContext>()
               .AddDbContext<ApplicationDbContext>(options =>
               {
                   options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
               });

            // Security
            serviceCollection
                .AddScoped<IUserClaimService, UserClaimService>()
                .AddScoped<IAuthService, BearerAuthService>();

            // Airtable
            serviceCollection
                .Configure<AirtableOptions>(configuration.GetSection("AirtableOptions"))
                .AddScoped<IAirtableApiClient, AirtableApiClient>();

            return serviceCollection;
        }
    }
}
