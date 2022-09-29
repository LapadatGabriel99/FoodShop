using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Dto;

namespace FoodShop.Services.Product.Api.Converters.Products
{
    internal sealed class ProductToProductDtoConverter : IConverter<Models.Product, ProductDto>
    {
        public ProductDto Convert(Models.Product source)
        {
            return new ProductDto
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Summary = source.Summary,
                ImageUrl = source.ImageUrl,
                Price = source.Price
            };
        }

        public IEnumerable<ProductDto> Convert(IEnumerable<Models.Product> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
