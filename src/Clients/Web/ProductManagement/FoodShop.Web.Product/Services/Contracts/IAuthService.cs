using FoodShop.Web.Product.Models;
using Microsoft.AspNetCore.Authentication;

namespace FoodShop.Web.Product.Services.Contracts
{
    public interface IAuthService
    {
        Task<RefreshTokenResponse> GetRefreshTokenRsponse(string cliendId, string clientSecret, string refreshToken);

        Task<bool> CheckIfAccessTokenExpired();

        /// <summary>
        /// Gets the current access token, which is still valid
        /// </summary>
        /// <returns>Access token string</returns>
        Task<string> GetAccessToken();

        Task AuthenticateUser(List<AuthenticationToken> authenticationTokens);
    }
}
