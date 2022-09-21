using FoodShop.Services.User.Api.Models.Contracts;
using System.Text.Json.Serialization;

namespace FoodShop.Services.User.Api.Dto
{
    public class UserModelDto : ICanConvert
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
