using FoodShop.Services.Product.Api.Dto;

namespace FoodShop.Services.Product.Api.Services.Contracts.Converters.Products
{
    public interface IProductConverterService
    {
        Models.Product Convert(ProductDto source);

        ProductDto Convert(Models.Product source);

        IEnumerable<Models.Product> Convert(IEnumerable<ProductDto> source);

        IEnumerable<ProductDto> Convert(IEnumerable<Models.Product> source);
    }
}
