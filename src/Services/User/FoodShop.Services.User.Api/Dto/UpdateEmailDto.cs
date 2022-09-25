namespace FoodShop.Services.User.Api.Dto
{
    public sealed class UpdateEmailDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
