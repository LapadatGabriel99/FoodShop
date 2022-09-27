using FoodShop.Services.Product.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.Product.Api.Data.Contracts
{
    public interface IApplicationDbContext : IDbContext
    {
        DbSet<Models.Product> Products { get; set; }

        DbSet<Category> Categories { get; set; }
    }
}
