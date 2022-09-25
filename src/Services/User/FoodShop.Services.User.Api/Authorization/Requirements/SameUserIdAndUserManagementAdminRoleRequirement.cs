using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace FoodShop.Services.User.Api.Authorization.Requirements
{
    public sealed class SameUserIdAndUserManagementAdminRoleRequirement : IAuthorizationRequirement
    {
    }
}
