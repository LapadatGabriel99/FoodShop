using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Models;

namespace FoodShop.Services.Product.Api.Repository.Contracts.Products
{
    public interface IProductRepository : IGenericRepository<ApplicationDbContext, Models.Product>
    {
    }
}
