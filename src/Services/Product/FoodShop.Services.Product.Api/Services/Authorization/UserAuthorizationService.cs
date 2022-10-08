using FoodShop.Services.Product.Api.Services.Contracts.Authorization;
using System.Security.Claims;

namespace FoodShop.Services.Product.Api.Services.Authorization
{
    internal sealed class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSubjectUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "preferred_username")?.Value;
        }
    }
}
