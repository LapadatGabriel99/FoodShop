using FoodShop.Web.Product.Dto;

namespace FoodShop.Web.Product.Services.Contracts
{
    public interface IProductService
    {
        Task<GenericResponseDto<IEnumerable<ProductDto>>> GetAll(string route);

        Task<GenericResponseDto<ProductDto>> GetById(string route, string id);

        Task<GenericResponseDto<ProductDto>> Create(string route, ProductDto input);

        Task<GenericResponseDto<ProductDto>> Update(string route, ProductDto input);

        Task<GenericResponseDto<bool>> Delete(string route, string id);
    }
}
