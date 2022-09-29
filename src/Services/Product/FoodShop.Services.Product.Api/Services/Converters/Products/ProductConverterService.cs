using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Services.Contracts.Converters;
using FoodShop.Services.Product.Api.Services.Contracts.Converters.Products;

namespace FoodShop.Services.Product.Api.Services.Converters.Products
{
    internal sealed class ProductConverterService : IProductConverterService
    {
        private readonly IObjectConverterService _objectConverterService;
        private readonly IConverter<ProductDto, Models.Product> _productDtoToProductConverter;
        private readonly IConverter<Models.Product, ProductDto> _productToProductDtoConverter;

        public ProductConverterService(
           IObjectConverterService objectConverterService,
           IConverter<ProductDto, Models.Product> productDtoToProductConverter,
           IConverter<Models.Product, ProductDto> productToProductDtoConverter)
        {
            _objectConverterService = objectConverterService;
            _productDtoToProductConverter = productDtoToProductConverter;
            _productToProductDtoConverter = productToProductDtoConverter;
        }

        public Models.Product Convert(ProductDto source)
        {
            return _objectConverterService.Convert(_productDtoToProductConverter, source);
        }

        public ProductDto Convert(Models.Product source)
        {
            return _objectConverterService.Convert(_productToProductDtoConverter, source);
        }

        public IEnumerable<Models.Product> Convert(IEnumerable<ProductDto> source)
        {
            return _objectConverterService.Convert(_productDtoToProductConverter, source);
        }

        public IEnumerable<ProductDto> Convert(IEnumerable<Models.Product> source)
        {
            return _objectConverterService.Convert(_productToProductDtoConverter, source);
        }
    }
}
