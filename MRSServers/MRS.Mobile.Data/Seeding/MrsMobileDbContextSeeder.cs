using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MRS.Common;
using MRS.Mobile.Data.Models;
using System;
using System.Linq;

namespace MRS.Mobile.Data.Seeding
{
    public static class MrsMobileDbContextSeeder
    {
        public static void Seed(MrsMobileDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<MrsMobileRole>>();
            Seed(dbContext, roleManager);
        }

        public static void Seed(MrsMobileDbContext dbContext, RoleManager<MrsMobileRole> roleManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            SeedRoles(roleManager);
        }

        private static void SeedRoles(RoleManager<MrsMobileRole> roleManager)
        {
            SeedRole(GlobalConstants.AdministratorRoleName, roleManager);
            SeedRole(GlobalConstants.NormalUserType, roleManager);
            SeedRole(GlobalConstants.SaviorUserType, roleManager);
        }

        private static void SeedRole(string roleName, RoleManager<MrsMobileRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                var result = roleManager.CreateAsync(new MrsMobileRole(roleName)).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

