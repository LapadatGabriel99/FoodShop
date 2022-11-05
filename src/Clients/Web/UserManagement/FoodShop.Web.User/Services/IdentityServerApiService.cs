using FoodShop.Web.User.Services.Contracts;
using IdentityModel.Client;

namespace FoodShop.Web.User.Services
{
    public sealed class IdentityServerApiService : IIdentityServerApiService
    {
        private readonly ILogger<IdentityServerApiService> _logger;
        private readonly HttpClient _client;

        public IdentityServerApiService(ILogger<IdentityServerApiService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<TokenResponse> RequestRefreshTokenAsync(string cliendId, string clientSecret, string refreshToken)
        {
            var discoveryResponse = await GetDiscoveryDocumentAsync();

            var refreshResponse = await _client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = cliendId,
                ClientSecret = clientSecret,
                RefreshToken = refreshToken
            });

            return refreshResponse;
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync()
        {
            return await _client.GetDiscoveryDocumentAsync();
        }
    }
}
