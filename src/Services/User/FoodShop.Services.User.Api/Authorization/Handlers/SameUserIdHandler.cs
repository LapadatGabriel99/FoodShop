using FoodShop.Services.User.Api.Authorization.Requirements;
using FoodShop.Services.User.Api.Services.Contracts.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace FoodShop.Services.User.Api.Authorization.Handlers
{
    public sealed class SameUserIdHandler : AuthorizationHandler<SameUserIdRequirement>
    {
        private readonly IUserPermissionService _userPermissionService;

        public SameUserIdHandler(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserIdRequirement requirement)
        {
            if (_userPermissionService.IsRequestedUserIdSameAsRequesterUserId())
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            context.Fail();

            return Task.CompletedTask;
        }
    }
}
