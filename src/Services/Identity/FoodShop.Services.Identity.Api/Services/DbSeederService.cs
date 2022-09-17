using FoodShop.Services.Identity.Api.Models;
using FoodShop.Services.Identity.Api.Services.Contracts;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FoodShop.Services.Identity.Api.Services
{
    internal sealed class DbSeederService : IDbSeederService
    {
        private readonly ILogger<DbSeederService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeederService(ILogger<DbSeederService> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDatabaseAsync()
        {
            var adminRole = await _roleManager.FindByNameAsync(Role.UserMangementAdmin);

            if (adminRole != null)
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole(Role.UserMangementAdmin));

            var adminUser = new ApplicationUser()
            {
                UserName = "UserAdmin",
                Email = "userAdmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07707721654",
                FirstName = "Gabi",
                LastName = "Lapadat"
            };

            await _userManager.CreateAsync(adminUser, "Admin123*");
            await _userManager.AddToRoleAsync(adminUser, Role.UserMangementAdmin);
            await _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, Role.UserMangementAdmin)
            });
        }
    }
}
