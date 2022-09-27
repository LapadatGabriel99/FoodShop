using FoodShop.Services.Product.Api.Repository.Contracts.Products;
using FoodShop.Services.Product.Api.Services.Contracts.Products;

namespace FoodShop.Services.Product.Api.Services.Products
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Models.Product> CreateAsync(Models.Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Product> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Product> UpdateAsync(Models.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
