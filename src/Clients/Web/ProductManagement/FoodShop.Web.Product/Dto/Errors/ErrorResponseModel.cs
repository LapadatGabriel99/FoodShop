namespace FoodShop.Web.Product.Dto.Errors
{
    public class ErrorResponseModel : ResponseModel
    {
        public override string Message { get; set; }

        public List<string> Errors { get; set; }

        public override string ControllerOrigin { get; set; }
    }
}
