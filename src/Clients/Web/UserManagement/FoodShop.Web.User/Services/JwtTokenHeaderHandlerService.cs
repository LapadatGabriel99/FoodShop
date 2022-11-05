using FoodShop.Web.User.Services.Contracts;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FoodShop.Web.User.Services
{
    internal sealed class JwtTokenHeaderHandlerService : DelegatingHandler
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _accessor;

        public JwtTokenHeaderHandlerService(IAuthService authService, IHttpContextAccessor accessor)
        {
            _authService = authService;
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authService.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.TryAddWithoutValidation("id", _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
