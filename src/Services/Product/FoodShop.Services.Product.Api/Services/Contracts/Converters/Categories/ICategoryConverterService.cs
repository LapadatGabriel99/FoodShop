using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Services.Contracts.Converters.Categories
{
    public interface ICategoryConverterService
    {
        Category Convert(CategoryDto source);

        CategoryDto Convert(Category source);

        IEnumerable<CategoryDto> Convert(IEnumerable<Category> source);

        IEnumerable<Category> Convert(IEnumerable<CategoryDto> source);
    }
}
