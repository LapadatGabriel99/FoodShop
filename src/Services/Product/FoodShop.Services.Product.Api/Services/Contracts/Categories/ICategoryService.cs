using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Services.Contracts.Categories
{
    public interface ICategoryService
    {
        Task<Category> CreateAsync(Category product);

        Task<Category> UpdateAsync(Category product);

        Task<Category> GetByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}
