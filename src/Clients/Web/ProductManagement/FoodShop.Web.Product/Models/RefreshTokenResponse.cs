using Microsoft.AspNetCore.Authentication;

namespace FoodShop.Web.Product.Models
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }

        public List<AuthenticationToken> UpdatedTokens { get; set; }
    }
}
