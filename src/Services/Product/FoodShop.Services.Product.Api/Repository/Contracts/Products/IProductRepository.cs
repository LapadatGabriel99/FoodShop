using FoodShop.Services.Product.Api.Data;

namespace FoodShop.Services.Product.Api.Repository.Contracts.Products
{
    public interface IProductRepository : IGenericRepository<ApplicationDbContext, Models.Product>
    {
    }
}
