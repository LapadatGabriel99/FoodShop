using FoodShop.Services.User.Api.Authorization.Requirements;
using FoodShop.Services.User.Api.Services.Contracts.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace FoodShop.Services.User.Api.Authorization.Handlers
{
    public sealed class SameUserIdAndUserManagementAdminRoleHandler : AuthorizationHandler<SameUserIdAndUserManagementAdminRoleRequirement>
    {
        private readonly IUserPermissionService _userPermissionService;

        public SameUserIdAndUserManagementAdminRoleHandler(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserIdAndUserManagementAdminRoleRequirement requirement)
        {
            if (_userPermissionService.IsUserMangementAdminRole())
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

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
