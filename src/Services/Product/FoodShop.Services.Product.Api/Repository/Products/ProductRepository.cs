using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Models;
using FoodShop.Services.Product.Api.Repository.Contracts;
using FoodShop.Services.Product.Api.Repository.Contracts.Products;
using System.Linq.Expressions;

namespace FoodShop.Services.Product.Api.Repository.Products
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<ApplicationDbContext, Models.Product> _genericRepository;

        public ProductRepository(IGenericRepository<ApplicationDbContext, Models.Product> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Models.Product> AddAsync(Models.Product entity)
        {
            return await _genericRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Models.Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Models.Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Models.Product>> GetAsync(Expression<Func<Models.Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Models.Product>> GetAsync(Expression<Func<Models.Product, bool>> predicate = null, Func<IQueryable<Models.Product>, IOrderedQueryable<Models.Product>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Models.Product>> GetAsync(Expression<Func<Models.Product, bool>> predicate = null, Func<IQueryable<Models.Product>, IOrderedQueryable<Models.Product>> orderBy = null, List<Expression<Func<Models.Product, object>>> includes = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Product> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SavaChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Models.Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
