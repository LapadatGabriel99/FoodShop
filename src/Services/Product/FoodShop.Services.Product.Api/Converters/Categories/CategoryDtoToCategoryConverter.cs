using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Converters.Categories
{
    internal sealed class CategoryDtoToCategoryConverter : IConverter<CategoryDto, Category>
    {
        public Category Convert(CategoryDto source)
        {
            return new Category
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        public IEnumerable<Category> Convert(IEnumerable<CategoryDto> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
