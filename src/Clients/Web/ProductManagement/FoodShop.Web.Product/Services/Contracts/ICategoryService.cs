using FoodShop.Web.Product.Dto;

namespace FoodShop.Web.Product.Services.Contracts
{
    public interface ICategoryService
    {
        Task<GenericResponseDto<IEnumerable<CategoryDto>>> GetAll(string route);

        Task<GenericResponseDto<CategoryDto>> GetById(string route, string id);

        Task<GenericResponseDto<CategoryDto>> Create(string route, CategoryDto input);

        Task<GenericResponseDto<CategoryDto>> Update(string route, CategoryDto input);

        Task<GenericResponseDto<bool>> Delete(string route, string id);
    }
}
