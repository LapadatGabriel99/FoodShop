using System.Security.Claims;

namespace FoodShop.Services.User.Api.Services.Contracts.Authorization
{
    public interface IUserPermissionService
    {
        bool IsUserMangementAdminRole();

        bool IsRequestedUserIdSameAsRequesterUserId();
    }
}
