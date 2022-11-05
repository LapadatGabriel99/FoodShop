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

        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public JwtTokenHeaderHandlerService(IHttpContextAccessor accessor, IAuthService authService, IConfiguration configuration)
        {
            _accessor = accessor;
            _authService = authService;
            _configuration = configuration;
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
            if (await _authService.CheckIfAccessTokenExpired())
            {
                return await _authService.GetAccessToken();
            }

            var refreshTokenResponse = await _authService
                .GetRefreshTokenRsponse(_configuration["ProductManagement:ClientId"], _configuration["ProductManagement:ClientSecret"]);

            await _authService.AuthenticateUser(refreshTokenResponse.UpdatedTokens);

            return refreshTokenResponse.AccessToken;
        }
    }
}
