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

        public async Task<Category> CreateAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _categoryRepository.DeleteAsync(await GetByIdAsync(id));
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return (await _categoryRepository.GetAsync(x => x.Name == name)).FirstOrDefault();
        }

        public async Task<IEnumerable<Category>> GetAllByNameAsync(List<string> names)
        {
            var list = new List<Category>();

            foreach (var name in names)
            {
                list.Add(await GetByNameAsync(name));
            }

            return list;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }
    }
}
