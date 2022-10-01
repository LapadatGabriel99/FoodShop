namespace FoodShop.Web.Product.Dto.Errors
{
    public class SuccessResponseModel : ResponseModel
    {
        public override string Message { get; set; }

        public override string ControllerOrigin { get; set; }
    }
}
