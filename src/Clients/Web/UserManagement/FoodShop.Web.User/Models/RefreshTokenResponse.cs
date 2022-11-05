using Microsoft.AspNetCore.Authentication;

namespace FoodShop.Web.User.Models
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }

        public List<AuthenticationToken> UpdatedTokens { get; set; }
    }
}
