using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Services.Contracts.Categories
{
    public interface ICategoryService
    {
        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task<Category> GetByIdAsync(string id);

        Task<Category> GetByNameAsync(string name);

        Task<IEnumerable<Category>> GetAllByNameAsync(List<string> names);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<bool> DeleteAsync(string id);
    }
}
