using FoodShop.Web.Product.Services.Contracts;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;

namespace FoodShop.Web.Product.Services
{
    public sealed class JwtTokenHeaderHandlerService : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        private readonly IIdentityServerApiService _identityServerApiService;

        public JwtTokenHeaderHandlerService(IHttpContextAccessor accessor, IIdentityServerApiService identityServerApiService)
        {
            _accessor = accessor;
            _identityServerApiService = identityServerApiService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await GetAccessTokenAsync();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.SetBearerToken(accessToken);
            }

            request.Headers.TryAddWithoutValidation("id", _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return await base.SendAsync(request, cancellationToken);
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var expiresAt = await _accessor.HttpContext.GetTokenAsync("expires_at");

            var expiresAtAsDateTimeOffset = DateTimeOffset.Parse(expiresAt, CultureInfo.InvariantCulture);

            if (expiresAtAsDateTimeOffset.AddSeconds(-60).ToUniversalTime() > DateTime.UtcNow)
            {
                return await _accessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            }

            var refreshToken = await _accessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var refreshResponse = await _identityServerApiService
                .RequestRefreshTokenAsync("foodShop-Product-Management-Admin", "511536EF-F270-4058-80CA-1C89C192F69A", refreshToken);

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

            var currentAuthenticateResult = await _accessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            currentAuthenticateResult.Properties.StoreTokens(updatedTokens);

            await _accessor
                .HttpContext
                .SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    currentAuthenticateResult.Principal,
                    currentAuthenticateResult.Properties);

            return refreshResponse.AccessToken;
        }
    }
}
