using FoodShop.Web.User.Models;
using FoodShop.Web.User.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;

namespace FoodShop.Web.User.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IIdentityServerApiService _identityServerApiService;
        private readonly IHttpContextAccessor _accessor;

        public AuthService(ILogger<AuthService> logger, IIdentityServerApiService identityServerApiService, IHttpContextAccessor accessor)
        {
            _identityServerApiService = identityServerApiService;
            _accessor = accessor;
        }

        public async Task AuthenticateUser(List<AuthenticationToken> authenticationTokens)
        {
            var currentAuthenticateResult = await _accessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            currentAuthenticateResult.Properties.StoreTokens(authenticationTokens);

            await _accessor
                .HttpContext
                .SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    currentAuthenticateResult.Principal,
                    currentAuthenticateResult.Properties);
        }

        public async Task<bool> CheckIfAccessTokenExpired()
        {
            var expiresAt = await _accessor.HttpContext.GetTokenAsync("expires_at");

            var expiresAtAsDateTimeOffset = DateTimeOffset.Parse(expiresAt, CultureInfo.InvariantCulture);

            return expiresAtAsDateTimeOffset.AddSeconds(-60).ToUniversalTime() > DateTime.UtcNow;
        }

        public async Task<string> GetAccessToken()
        {
            return await _accessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        }

        public async Task<RefreshTokenResponse> GetRefreshTokenRsponse(string cliendId, string clientSecret)
        {
            var refreshToken = await _accessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var refreshResponse = await _identityServerApiService
                .RequestRefreshTokenAsync(cliendId, clientSecret, refreshToken);

            var updatedTokens = new List<AuthenticationToken>();
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.IdToken,
                Value = refreshResponse.IdentityToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = refreshResponse.AccessToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = refreshResponse.RefreshToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = "expires_at",
                Value = (DateTime.UtcNow + TimeSpan.FromSeconds(refreshResponse.ExpiresIn)).ToString("o", CultureInfo.InvariantCulture)
            });

            return new RefreshTokenResponse
            {
                UpdatedTokens = updatedTokens,
                AccessToken = refreshResponse.AccessToken
            };
        }
    }
}
