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
            var userAdminRole = await _roleManager.FindByNameAsync(Role.UserMangementAdmin);

            if (userAdminRole != null)
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
                LastName = "Lapadat",
                Address = "Romania, Craiova, Dolj, Str Simion Stoilov nr 12, bl E9",
                PhoneNumberConfirmed = true,
            };

            await _userManager.CreateAsync(adminUser, "Admin123*");
            await _userManager.AddToRoleAsync(adminUser, Role.UserMangementAdmin);
            await _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, Role.UserMangementAdmin),
                new Claim(JwtClaimTypes.Address, adminUser.Address),
                new Claim(JwtClaimTypes.PhoneNumber, adminUser.PhoneNumber)
            });

            var adminUser2 = new ApplicationUser()
            {
                UserName = "UserAdmin2",
                Email = "userAdmin2@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07707721654",
                FirstName = "Alex",
                LastName = "Popescu",
                Address = "Romania, Craiova, Dolj, Str Simion Stoilov nr 12, bl E10",
                PhoneNumberConfirmed = true,
            };

            await _userManager.CreateAsync(adminUser2, "Admin123*");
            await _userManager.AddToRoleAsync(adminUser2, Role.UserMangementAdmin);
            await _userManager.AddClaimsAsync(adminUser2, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser2.FirstName + " " + adminUser2.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser2.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser2.LastName),
                new Claim(JwtClaimTypes.Role, Role.UserMangementAdmin),
                new Claim(JwtClaimTypes.Address, adminUser2.Address),
                new Claim(JwtClaimTypes.PhoneNumber, adminUser2.PhoneNumber)
            });

            var adminUser3 = new ApplicationUser()
            {
                UserName = "UserAdmin3",
                Email = "userAdmin3@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07707721654",
                FirstName = "Robert",
                LastName = "Dorobantu",
                Address = "Romania, Craiova, Dolj, Str Simion Stoilov nr 12, bl E10",
                PhoneNumberConfirmed = true,
            };

            await _userManager.CreateAsync(adminUser3, "Admin123*");
            await _userManager.AddToRoleAsync(adminUser3, Role.UserMangementAdmin);
            await _userManager.AddClaimsAsync(adminUser3, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser3.FirstName + " " + adminUser3.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser3.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser3.LastName),
                new Claim(JwtClaimTypes.Role, Role.UserMangementAdmin),
                new Claim(JwtClaimTypes.Address, adminUser3.Address),
                new Claim(JwtClaimTypes.PhoneNumber, adminUser3.PhoneNumber)
            });
        }
    }
}
