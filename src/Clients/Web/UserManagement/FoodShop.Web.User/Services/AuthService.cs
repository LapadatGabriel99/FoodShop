using FoodShop.Web.User.Models;
using FoodShop.Web.User.Services.Contracts;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;

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

        private void foo()
        {
            //var disco = await DiscoveryClient.GetAsync(this.applicationSettings.IdentityServerAuthority);
            //if (disco.IsError) throw new Exception(disco.Error);

            //var userInfoClient = new UserInfoClient(disco.UserInfoEndpoint);
            //var tokenClient = new TokenClient(disco.TokenEndpoint, this.applicationSettings.IdentityServerAuthorityClient, this.applicationSettings.IdentityServerAuthorityPassword);
            //var rt = await this.httpContext.HttpContext.GetTokenAsync("refresh_token");
            //var tokenResult = await tokenClient.RequestRefreshTokenAsync(rt);

            //if (!tokenResult.IsError)
            //{
            //    var old_id_token = await this.httpContext.HttpContext.GetTokenAsync("id_token");
            //    var new_access_token = tokenResult.AccessToken;
            //    var new_refresh_token = tokenResult.RefreshToken;

            //    var tokens = new List<AuthenticationToken>();
            //    tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.IdToken, Value = old_id_token });
            //    tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = new_access_token });
            //    tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = new_refresh_token });

            //    var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResult.ExpiresIn);
            //    tokens.Add(new AuthenticationToken { Name = "expires_at", Value = expiresAt.ToString("o", CultureInfo.InvariantCulture) });
            //    var info = await this.httpContext.HttpContext.AuthenticateAsync("Cookies");
            //    //get the updated user profile (claims)
            //    var response = await userInfoClient.GetAsync(new_access_token);
            //    info.Properties.StoreTokens(tokens);

            //    //merge the new claims with the current principal
            //    var currentIdentity = info.Principal.Identity as ClaimsIdentity;
            //    var distinctClaimTypes = response.Claims.Select(x => x.Type).Distinct();
            //    foreach (var claimType in distinctClaimTypes)
            //    {
            //        var currentCount = currentIdentity.Claims.Count(x => x.Type == claimType);
            //        if (currentCount > 0)
            //        {
            //            //remove the claims from the current
            //            var currentClaims = currentIdentity.Claims.Where(x => x.Type == claimType).ToList();
            //            foreach (var currentClaim in currentClaims)
            //            {
            //                currentIdentity.RemoveClaim(currentClaim);
            //            }
            //        }

            //        //add the new claims
            //        currentIdentity.AddClaims(response.Claims.Where(x => x.Type == claimType));
            //    }

            //    //update the cookies with the new principal and identity
            //    await this.httpContext.HttpContext.SignInAsync("Cookies", info.Principal, info.Properties);
            //}
        }
    }
}
