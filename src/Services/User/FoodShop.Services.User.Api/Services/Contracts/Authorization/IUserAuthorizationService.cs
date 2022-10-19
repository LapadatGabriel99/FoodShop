using System.Security.Claims;

namespace FoodShop.Services.User.Api.Services.Contracts.Authorization
{
    public interface IUserAuthorizationService
    {
        bool IsUserMangementAdminRole();

        bool IsRequestedUserIdSameAsRequesterUserId();

        string GetUserEmail();
    }
}
