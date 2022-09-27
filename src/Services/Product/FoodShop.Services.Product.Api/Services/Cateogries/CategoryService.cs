using FoodShop.Services.Product.Api.Models;
using FoodShop.Services.Product.Api.Repository.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Categories;

namespace FoodShop.Services.Product.Api.Services.Cateogries
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(Category product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> UpdateAsync(Category product)
        {
            throw new NotImplementedException();
        }
    }
}
