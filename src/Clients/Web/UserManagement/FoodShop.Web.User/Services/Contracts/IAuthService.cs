using FoodShop.Web.User.Models;
using Microsoft.AspNetCore.Authentication;

namespace FoodShop.Web.User.Services.Contracts
{
    public interface IAuthService
    {
        Task<RefreshTokenResponse> GetRefreshTokenRsponse(string cliendId, string clientSecret);

        Task<bool> CheckIfAccessTokenExpired();

        /// <summary>
        /// Gets the current access token, which is still valid
        /// </summary>
        /// <returns>Access token string</returns>
        Task<string> GetAccessToken();

        Task AuthenticateUser(List<AuthenticationToken> authenticationTokens);
    }
}
