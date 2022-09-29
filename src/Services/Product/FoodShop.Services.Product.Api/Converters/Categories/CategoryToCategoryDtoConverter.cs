using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Converters.Categories
{
    internal sealed class CategoryToCategoryDtoConverter : IConverter<Category, CategoryDto>
    {
        public CategoryDto Convert(Category source)
        {
            return new CategoryDto
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        public IEnumerable<CategoryDto> Convert(IEnumerable<Category> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
