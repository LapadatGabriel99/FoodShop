using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Services.Contracts;

namespace FoodShop.Web.Product.Services
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IProductApiService _productApiService;

        public CategoryService(ILogger<CategoryService> logger, IProductApiService userApiService)
        {
            _logger = logger;
            _productApiService = userApiService;
        }

        public async Task<GenericResponseDto<CategoryDto>> Create(string route, CategoryDto input)
        {
            return await _productApiService.Create<GenericResponseDto<CategoryDto>, CategoryDto>(route, input);
        }

        public async Task<GenericResponseDto<bool>> Delete(string route, string id)
        {
            return await _productApiService.Delete<GenericResponseDto<bool>>(route, id);
        }

        public async Task<GenericResponseDto<IEnumerable<CategoryDto>>> GetAll(string route)
        {
            return await _productApiService.GetAll<GenericResponseDto<IEnumerable<CategoryDto>>>(route);
        }

        public async Task<GenericResponseDto<CategoryDto>> GetById(string route, string id)
        {
            return await _productApiService.GetById<GenericResponseDto<CategoryDto>>(route, id);
        }

        public async Task<GenericResponseDto<CategoryDto>> Update(string route, CategoryDto input)
        {
            return await _productApiService.Update<GenericResponseDto<CategoryDto>, CategoryDto>(route, input);
        }
    }
}
