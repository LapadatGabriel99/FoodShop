using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Services.Contracts.Products
{
    public interface IProductService
    {
        Task<Models.Product> CreateAsync(Models.Product product);

        Task<Models.Product> CreateWithCategories(Models.Product product, IEnumerable<Category> categories);

        Task<Models.Product> UpdateAsync(Models.Product product);

        Task<Models.Product> GetByNameAsync(string name);

        Task<Models.Product> GetByIdAsync(string id);

        Task<IEnumerable<Models.Product>> GetAllAsync();

        Task<bool> DeleteAsync(string id);
    }
}
