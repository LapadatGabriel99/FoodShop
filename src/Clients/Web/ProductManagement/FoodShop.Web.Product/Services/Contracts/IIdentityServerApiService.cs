using IdentityModel.Client;

namespace FoodShop.Web.Product.Services.Contracts
{
    public interface IIdentityServerApiService
    {
        Task<TokenResponse> RequestRefreshTokenAsync(string cliendId, string clientSecret, string refreshToken);
    }
}
