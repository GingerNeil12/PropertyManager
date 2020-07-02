using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PropertyManager.Infrastructure.Security.Common;
using PropertyManager.Infrastructure.Security.Models;

namespace PropertyManager.Infrastructure.Persistence.DataSeeding
{
    internal class SeedDataService : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedDataService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync(RoleNames.ADMIN))
            {
                var role = new IdentityRole(RoleNames.ADMIN);
                await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync(RoleNames.USER))
            {
                var role = new IdentityRole(RoleNames.USER);
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task SeedUsersAsync()
        {
            var password = "Test@123";

            var adminEmail = "admin@property.com";
            var user = await _userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Neil",
                    LastName = "Earlam"
                };

                var created = await _userManager.CreateAsync(user, password);
                if (created.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleNames.ADMIN);
                    await _userManager.AddToRoleAsync(user, RoleNames.USER);
                }
            }
        }
    }
}
