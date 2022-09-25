using FoodShop.Services.User.Api.Models.Contracts;

namespace FoodShop.Services.User.Api.Dto
{
    public sealed class UpdateBasicCredentialsDto : ICanConvert
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
