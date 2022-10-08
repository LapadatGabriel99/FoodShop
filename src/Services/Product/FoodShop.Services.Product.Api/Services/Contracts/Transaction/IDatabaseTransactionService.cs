using FoodShop.Services.Product.Api.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.Product.Api.Services.Contracts.Transaction
{
    public interface IDatabaseTransactionService<TContext>
        where TContext : DbContext, IDbContext
    {
        Task Execute(Action action);
    }
}
