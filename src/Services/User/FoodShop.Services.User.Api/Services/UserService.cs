using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Services.Contracts;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace FoodShop.Services.User.Api.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ILogger<UserService> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserModel> Create(UserModel user)
        {
            var existingRole = await _roleManager.FindByNameAsync(user.Role);

            if (existingRole == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(user.Role));
            }

            user.ApplicationUser.PhoneNumberConfirmed = true;
            user.ApplicationUser.EmailConfirmed = true;

            await _userManager.CreateAsync(user.ApplicationUser, user.Password);
            await _userManager.AddToRoleAsync(user.ApplicationUser, user.Role);
            await _userManager.AddClaimsAsync(user.ApplicationUser, new Claim[]
            {
            new Claim(JwtClaimTypes.Name, user.ApplicationUser.FirstName + " " + user.ApplicationUser.LastName),
            new Claim(JwtClaimTypes.GivenName, user.ApplicationUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.ApplicationUser.LastName),
            new Claim(JwtClaimTypes.Role, Role.UserMangementAdmin),
            new Claim(JwtClaimTypes.Address, user.ApplicationUser.Address),
            new Claim(JwtClaimTypes.PhoneNumber, user.ApplicationUser.PhoneNumber)
            });

            return user;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<UserModel> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return new UserModel { ApplicationUser = user, Role = role };
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            var userModels = new List<UserModel>();

            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                userModels.Add(new UserModel { ApplicationUser = user, Role = role });
            }

            return userModels;
        }

        public async Task<UserModel> UpdateBasicCredentials(UserModel user)
        {
            await _userManager.UpdateAsync(user.ApplicationUser);

            return user;
        }

        public async Task<UserModel> UpdateUserName(string id, string userName)
        {
            var user = await Get(id);

            var result = await _userManager.SetUserNameAsync(user.ApplicationUser, userName);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Could not update username of user with id: {id}");
            }

            return user;
        }

        public async Task<UserModel> UpdateEmail(string id, string email, string token)
        {
            var user = await Get(id);

            var result = await _userManager.ChangeEmailAsync(user.ApplicationUser, email, token);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Could not update email of user with id: {id}");
            }

            return user;
        }

        public async Task<UserModel> UpdatePassword(string id, string password, string newPassword)
        {
            var user = await Get(id);

            var result = await _userManager.ChangePasswordAsync(user.ApplicationUser, password, newPassword);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Could not update password of user with id: {id}");
            }

            return await Get(id);
        }

        public async Task<UserModel> UpdatePhoneNumber(string id, string phoneNumber, string token)
        {
            var user = await Get(id);

            var result = await _userManager.ChangePhoneNumberAsync(user.ApplicationUser, phoneNumber, token);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Could not update password of user with id: {id}");
            }

            return user;
        }
    }
}
