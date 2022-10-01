
namespace FoodShop.Web.Product.Dto
{
    public sealed class GenericResponseDto<TData>
    {
        public int StatusCode { get; set; }

        public TData Data { get; set; }

        public List<string> Errors { get; set; }
    }
}
