using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Models;
using FoodShop.Services.Product.Api.Repository.Contracts;
using FoodShop.Services.Product.Api.Repository.Contracts.Categories;
using System.Linq.Expressions;

namespace FoodShop.Services.Product.Api.Repository.Categories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly IGenericRepository<ApplicationDbContext, Category> _genericRepository;

        public CategoryRepository(IGenericRepository<ApplicationDbContext, Category> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public ApplicationDbContext Context => _genericRepository.Context;

        public async Task<Category> AddAsync(Category entity)
        {
            return await _genericRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Category entity)
        {
            return await _genericRepository.DeleteAsync(entity);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<Category>> GetAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _genericRepository.GetAsync(predicate);
        }

        public async Task<IReadOnlyList<Category>> GetAsync(Expression<Func<Category, bool>> predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            return await _genericRepository.GetAsync(predicate, orderBy, includeString, disableTracking);
        }

        public async Task<IReadOnlyList<Category>> GetAsync(Expression<Func<Category, bool>> predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, List<Expression<Func<Category, object>>> includes = null, bool disableTracking = true)
        {
            return await _genericRepository.GetAsync(predicate, orderBy, includes, disableTracking);
        }

        public async Task<Category> GetByIdAsync(object id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public async Task<int> SavaChangesAsync()
        {
            return await _genericRepository.SavaChangesAsync();
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            return await _genericRepository.UpdateAsync(entity);
        }
    }
}
