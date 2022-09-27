using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Repository.Contracts.Categories
{
    public interface ICategoryRepository : IGenericRepository<ApplicationDbContext, Category>
    {
    }
}
