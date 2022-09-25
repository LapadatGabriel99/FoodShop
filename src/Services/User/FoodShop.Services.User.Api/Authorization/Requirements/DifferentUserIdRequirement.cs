using Microsoft.AspNetCore.Authorization;

namespace FoodShop.Services.User.Api.Authorization.Requirements
{
    public sealed class DifferentUserIdRequirement : IAuthorizationRequirement
    {
    }
}
