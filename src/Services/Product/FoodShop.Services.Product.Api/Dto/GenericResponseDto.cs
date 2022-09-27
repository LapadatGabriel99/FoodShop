namespace FoodShop.Services.Product.Api.Dto
{
    public class GenericResponseDto<TData>
    {
        public int StatusCode { get; set; }

        public TData Data { get; set; }

        public List<string> Errors { get; set; }
    }
}
