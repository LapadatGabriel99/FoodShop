using FoodShop.Services.Product.Api.Models;
using System.Linq.Expressions;

namespace FoodShop.Services.Product.Api.Services.Contracts.Products
{
    public interface IProductService
    {
        Task<Models.Product> CreateAsync(Models.Product product);

        Task<Models.Product> CreateWithCategories(Models.Product product, IEnumerable<Category> categories);

        Task<Models.Product> UpdateWithCategories(
            Models.Product existingProduct,
            Models.Product updatedProduct,
            IEnumerable<Category> newCategories);

        Task<Models.Product> GetByNameAsync(string name);

        Task<Models.Product> GetByIdAsync(string id);

        Task<Models.Product> GetByIdAsync(string id, List<Expression<Func<Models.Product, object>>> includes);

        Task<IEnumerable<Models.Product>> GetAllAsync();

        Task<bool> DeleteAsync(string id);
    }
}
