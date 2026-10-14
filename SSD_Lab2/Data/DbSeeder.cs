using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSD_Lab1.Models;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace SSD_Lab1.Data
{
    public static class DbSeeder
    {

        public static async Task<IdentityResult> SeedUsersAndRoles(IServiceProvider serviceProvider, IConfiguration config)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (roleManager.Roles.Any())
            {
                return IdentityResult.Success;   // already seeded, nothing to do
            }

            IdentityResult rolesResult = await SeedRoles(roleManager);
            if (!rolesResult.Succeeded)
            {
                return rolesResult;
            }

            IdentityResult usersResult = await SeedUsers(userManager, config);
            if (!usersResult.Succeeded)
            {
                return usersResult;
            }

            return IdentityResult.Success;
        }
        private static async Task<IdentityResult> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Supervisor", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        return result;   // stop and hand back first failure
                    }
                }
            }

            return IdentityResult.Success;   // everything worked
        }

        private static async Task<IdentityResult> SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            var supervisorPassword = config["SeedUsers:SupervisorPassword"];
            var employeePassword = config["SeedUsers:EmployeePassword"];
            var adminUser = new ApplicationUser
            {
                UserName = "supervisor@mohawkcollege.ca",
                Email = "supervisor@mohawkcollege.ca",
                FirstName = "Supervisor",
                LastName = "User",
                City = "Hamilton",
                EmailConfirmed = true
            };

            var createAdminResult = await userManager.CreateAsync(adminUser, supervisorPassword);
            if (!createAdminResult.Succeeded)
            {
                return createAdminResult;
            }

            var adminRoleResult = await userManager.AddToRoleAsync(adminUser, "Supervisor");
            if (!adminRoleResult.Succeeded)
            {
                return adminRoleResult;
            }

            var countryClaim = new Claim(ClaimTypes.Country, "Canada");
            var adminClaimResult = await userManager.AddClaimAsync(adminUser, countryClaim);
            if (!adminClaimResult.Succeeded)
            {
                return adminClaimResult;
            }

            var memberUser = new ApplicationUser
            {
                UserName = "employee@mohawkcollege.ca",
                Email = "employee@mohawkcollege.ca",
                FirstName = "Employee",
                LastName = "User",
                City = "Hamilton",
                EmailConfirmed = true
            };

            var createMemberResult = await userManager.CreateAsync(memberUser, employeePassword);
            if (!createMemberResult.Succeeded)
            {
                return createMemberResult;
            }

            var memberRoleResult = await userManager.AddToRoleAsync(memberUser, "Employee");
            if (!memberRoleResult.Succeeded)
            {
                return memberRoleResult;
            }

            return IdentityResult.Success;
        }




    }
}
