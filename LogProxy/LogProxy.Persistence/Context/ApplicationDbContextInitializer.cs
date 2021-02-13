using LogProxy.Common.Constants;
using LogProxy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LogProxy.Persistence.Context
{
    public class ApplicationDbContextInitializer
    {
        public static void SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            SeedRoles(serviceProvider);
            SeedUsers(serviceProvider);
        }

        private static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (!roleManager.RoleExistsAsync(Roles.Anonymous).Result)
            {
                ApplicationRole role = new ApplicationRole
                {
                    Name = Roles.Anonymous
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

        private static void SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            const string username = "abed.itani";
            if (userManager.FindByNameAsync(username).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = "abed.itani@localhost.com",
                    FullName = "Abed Itani"
                };

                var result = userManager.CreateAsync(user, "123456").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.Anonymous).Wait();
                }
            }
        }
    }
}
