using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LogProxy.Application.Options;
using LogProxy.Domain.Entities.Identity;
using LogProxy.Persistence.Context;
using System.Text;

namespace LogProxy.Api
{
    public static class StartupAuthentication
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
               .AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            serviceCollection.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("AuthenticationOptions");
            serviceCollection.Configure<AuthenticationOptions>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AuthenticationOptions>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return serviceCollection;
        }
    }
}