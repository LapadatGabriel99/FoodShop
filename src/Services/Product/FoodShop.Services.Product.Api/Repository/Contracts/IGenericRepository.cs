using FoodShop.Services.Product.Api.Data.Contracts;
using FoodShop.Services.Product.Api.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodShop.Services.Product.Api.Repository.Contracts
{
    public interface IGenericRepository<TContext, TEntity> 
        where TEntity : class, IEntity
        where TContext: DbContext, IDbContext
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                       List<Expression<Func<TEntity, object>>> includes = null,
                                       bool disableTracking = true);
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);

        Task<int> SavaChangesAsync();
    }
}
