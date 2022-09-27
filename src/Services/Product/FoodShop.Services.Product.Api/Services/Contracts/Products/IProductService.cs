namespace FoodShop.Services.Product.Api.Services.Contracts.Products
{
    public interface IProductService
    {
        Task<Models.Product> CreateAsync(Models.Product product);

        Task<Models.Product> UpdateAsync(Models.Product product);

        Task<Models.Product> GetByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}
