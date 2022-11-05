namespace FoodShop.Web.User.Dto
{
    public sealed class UpdatePasswordDto
    {
        public string Id { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmed { get; set; }
    }
}
