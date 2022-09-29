using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Dto;

namespace FoodShop.Services.Product.Api.Converters.Products
{
    internal sealed class ProductDtoToProductConverter : IConverter<ProductDto, Models.Product>
    {
        public Models.Product Convert(ProductDto source)
        {
            return new Models.Product
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                Summary = source.Summary,
                Description = source.Description,
                ImageUrl = source.ImageUrl,
            };
        }

        public IEnumerable<Models.Product> Convert(IEnumerable<ProductDto> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
