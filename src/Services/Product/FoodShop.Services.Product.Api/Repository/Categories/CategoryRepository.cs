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

        public Task<Category> AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Category>> GetAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Category>> GetAsync(Expression<Func<Category, bool>> predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Category>> GetAsync(Expression<Func<Category, bool>> predicate = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, List<Expression<Func<Category, object>>> includes = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SavaChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
