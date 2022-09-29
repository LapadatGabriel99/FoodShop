using FoodShop.Services.Product.Api.Converters.Categories;
using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Models;
using FoodShop.Services.Product.Api.Services.Contracts.Converters;
using FoodShop.Services.Product.Api.Services.Contracts.Converters.Categories;

namespace FoodShop.Services.Product.Api.Services.Converters.Categories
{
    internal sealed class CategoryConverterService : ICategoryConverterService
    {
        private readonly IObjectConverterService _objectConverterService;
        private readonly IConverter<CategoryDto, Category> _categoryDtoToCategoryConverter;
        private readonly IConverter<Category, CategoryDto> _categoryToCategoryDtoConverter;

        public CategoryConverterService(
            IObjectConverterService objectConverterService,
            IConverter<CategoryDto, Category> categoryDtoToCategoryConverter,
            IConverter<Category, CategoryDto> categoryToCategoryDtoConverter)
        {
            _objectConverterService = objectConverterService;
            _categoryDtoToCategoryConverter = categoryDtoToCategoryConverter;
            _categoryToCategoryDtoConverter = categoryToCategoryDtoConverter;
        }

        public Category Convert(CategoryDto source)
        {
            return _objectConverterService.Convert(_categoryDtoToCategoryConverter, source);
        }

        public CategoryDto Convert(Category source)
        {
            return _objectConverterService.Convert(_categoryToCategoryDtoConverter, source);
        }

        public IEnumerable<CategoryDto> Convert(IEnumerable<Category> source)
        {
            return _objectConverterService.Convert(_categoryToCategoryDtoConverter, source);
        }

        public IEnumerable<Category> Convert(IEnumerable<CategoryDto> source)
        {
            return _objectConverterService.Convert(_categoryDtoToCategoryConverter, source);
        }
    }
}
