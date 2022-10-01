namespace FoodShop.Web.Product.Dto.Errors
{
    public abstract class ResponseModel
    {
        public abstract string Message { get; set; }

        public abstract string ControllerOrigin { get; set; }
    }
}
