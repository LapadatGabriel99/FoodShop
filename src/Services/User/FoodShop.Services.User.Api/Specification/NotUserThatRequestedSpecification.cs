using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Specification.Contracts;
using System.Security.Claims;

namespace FoodShop.Services.User.Api.Specification
{
    internal sealed class NotUserThatRequestedSpecification : ISpecification<UserModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotUserThatRequestedSpecification(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsSatisfied(UserModel entity)
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return entity.ApplicationUser.Id != id;
        }
    }
}
