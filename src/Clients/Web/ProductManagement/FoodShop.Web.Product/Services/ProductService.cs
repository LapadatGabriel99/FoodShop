using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Services.Contracts;

namespace FoodShop.Web.Product.Services
{
    internal sealed class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductApiService _productApiService;

        public ProductService(ILogger<ProductService> logger, IProductApiService userApiService)
        {
            _logger = logger;
            _productApiService = userApiService;
        }

        public async Task<GenericResponseDto<ProductDto>> Create(string route, ProductDto input)
        {
            return await _productApiService.Create<GenericResponseDto<ProductDto>, ProductDto>(route, input);
        }

        public async Task<GenericResponseDto<IEnumerable<ProductDto>>> GetAll(string route)
        {
            return await _productApiService.GetAll<GenericResponseDto<IEnumerable<ProductDto>>>(route);
        }

        public async Task<GenericResponseDto<ProductDto>> GetById(string route, string id)
        {
            return await _productApiService.GetById<GenericResponseDto<ProductDto>>(route, id);
        }
    }
}
