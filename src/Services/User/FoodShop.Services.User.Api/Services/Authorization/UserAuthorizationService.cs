using FoodShop.Services.User.Api.Services.Contracts.Authorization;
using System.Security.Claims;
using System.Text;

namespace FoodShop.Services.User.Api.Services.Authorization
{
    internal sealed class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthorizationService(IHttpContextAccessor httpContextAccessor)
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
            var requestedId = (_httpContextAccessor.HttpContext.GetRouteValue("id") ?? _httpContextAccessor.HttpContext.Request.Headers["id"]).ToString();

            return requesterId == requestedId;
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
