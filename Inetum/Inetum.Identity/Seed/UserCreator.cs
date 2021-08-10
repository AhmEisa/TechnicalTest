using Inetum.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Inetum.Identity.Seed
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = "AhmedEisa",
                Email = "aeissa@test.com",
                EmailConfirmed = true,
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, "P@ssw0rd@123");
                await userManager.AddToRoleAsync(applicationUser, "Admin");
            }
        }
    }

    public static class RoleCreator
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole
            {
                Name = "Admin"
            };

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(adminRole);
            }
        }
    }
}
