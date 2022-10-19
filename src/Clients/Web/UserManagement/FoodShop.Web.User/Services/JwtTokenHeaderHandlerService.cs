using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FoodShop.Web.User.Services
{
    internal sealed class JwtTokenHeaderHandlerService : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public JwtTokenHeaderHandlerService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.TryAddWithoutValidation("id", _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
