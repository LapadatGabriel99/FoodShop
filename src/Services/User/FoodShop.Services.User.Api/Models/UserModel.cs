using FoodShop.Services.User.Api.Models.Contracts;

namespace FoodShop.Services.User.Api.Models
{
    public class UserModel : ICanConvert
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
