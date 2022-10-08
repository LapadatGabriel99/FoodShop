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

        public ApplicationDbContext Context => _genericRepository.Context;

        public async Task<Models.Product> AddAsync(Models.Product entity)
        {
            return await _genericRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Models.Product entity)
        {
            return await _genericRepository.DeleteAsync(entity);
        }

        public async Task<IReadOnlyList<Models.Product>> GetAllAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<Models.Product>> GetAsync(Expression<Func<Models.Product, bool>> predicate)
        {
            return await _genericRepository.GetAsync(predicate);
        }

        public async Task<IReadOnlyList<Models.Product>> GetAsync(Expression<Func<Models.Product, bool>> predicate = null, Func<IQueryable<Models.Product>, IOrderedQueryable<Models.Product>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            return await _genericRepository.GetAsync(predicate, orderBy, includeString, disableTracking);
        }

        public async Task<IReadOnlyList<Models.Product>> GetAsync(Expression<Func<Models.Product, bool>> predicate = null, Func<IQueryable<Models.Product>, IOrderedQueryable<Models.Product>> orderBy = null, List<Expression<Func<Models.Product, object>>> includes = null, bool disableTracking = true)
        {
            return await _genericRepository.GetAsync(predicate, orderBy, includes, disableTracking);
        }

        public async Task<Models.Product> GetByIdAsync(object id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public async Task<int> SavaChangesAsync()
        {
            return await _genericRepository.SavaChangesAsync();
        }

        public async Task<Models.Product> UpdateAsync(Models.Product entity)
        {
            return await _genericRepository.UpdateAsync(entity);
        }
    }
}
