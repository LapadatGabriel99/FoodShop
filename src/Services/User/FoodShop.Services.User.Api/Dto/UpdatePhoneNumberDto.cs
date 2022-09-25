namespace FoodShop.Services.User.Api.Dto
{
    public sealed class UpdatePhoneNumberDto
    {
        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }
    }
}
