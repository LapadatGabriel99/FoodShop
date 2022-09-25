using FoodShop.Services.User.Api.Services.Contracts.Authorization;
using System.Security.Claims;

namespace FoodShop.Services.User.Api.Services.Authorization
{
    internal sealed class UserPermissionService : IUserPermissionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserPermissionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserMangementAdminRole()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value == Role.UserMangementAdmin;
        }

        public bool IsRequestedUserIdSameAsRequesterUserId()
        {
            var requesterId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var requestedId = _httpContextAccessor.HttpContext.GetRouteValue("id").ToString();

            return requesterId == requestedId;
        }
    }
}
