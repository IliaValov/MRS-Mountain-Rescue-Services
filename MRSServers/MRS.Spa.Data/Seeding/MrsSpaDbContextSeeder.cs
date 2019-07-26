using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRS.Common;
using MRS.Spa.Data.Models;
using System;
using System.Linq;

namespace MRS.Spa.Data.Seeding
{
    public static class MrsSpaDbContextSeeder
    {
        public static void Seed(MrsSpaDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<MrsSpaRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<MrsSpaUser>>();
            Seed(dbContext, configuration, roleManager, userManager);
        }

        public static void Seed(MrsSpaDbContext dbContext, IConfiguration configuration, RoleManager<MrsSpaRole> roleManager, UserManager<MrsSpaUser> userManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            SeedRoles(roleManager);
            SeedUsers(configuration, userManager);
        }

        private static void SeedRoles(RoleManager<MrsSpaRole> roleManager)
        {
            SeedRole(GlobalConstants.AdministratorRoleName, roleManager);
        }

        private static void SeedUsers(IConfiguration configuration, UserManager<MrsSpaUser> userManager)
        {
            SeedUser(configuration["UserInfo:Username"], configuration["UserInfo:Password"], GlobalConstants.AdministratorRoleName, userManager);
        }

        private static void SeedRole(string roleName, RoleManager<MrsSpaRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                var result = roleManager.CreateAsync(new MrsSpaRole(roleName)).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static void SeedUser(string username, string password, string roleName, UserManager<MrsSpaUser> userManager)
        {
            var user = new MrsSpaUser {Email = null, UserName = username };
            var userresult = userManager.CreateAsync(user, password).GetAwaiter().GetResult();

            if (!userresult.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, userresult.Errors.Select(e => e.Description)));
            }

            var addToUserResult = userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName).GetAwaiter().GetResult();

            if (!addToUserResult.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, addToUserResult.Errors.Select(e => e.Description)));
            }
        }
    }
}

