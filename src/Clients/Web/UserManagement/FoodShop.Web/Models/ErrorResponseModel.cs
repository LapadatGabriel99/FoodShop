namespace FoodShop.Web.User.Dto
{
    public class ErrorResponseModel : ResponseModel
    {
        public override string Message { get; set; }

        public List<string> Errors { get; set; }
    }
}
