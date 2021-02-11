using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogProxy.Application.Services.Security;
using LogProxy.Infrastructure.Services.Security;
using LogProxy.Persistence.Context;

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

            return serviceCollection;
        }
    }
}
