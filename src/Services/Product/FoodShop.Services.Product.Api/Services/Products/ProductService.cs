using FoodShop.Services.Product.Api.Models;
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

        public async Task<Models.Product> CreateWithCategories(Models.Product product, IEnumerable<Category> categories)
        {
            product.ProductCategories = categories.Select(category => new ProductCategories { Product = product, Category = category }).ToList();

            return await CreateAsync(product);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _productRepository.DeleteAsync(await GetByIdAsync(id));
        }

        public async Task<IEnumerable<Models.Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Models.Product> GetByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Models.Product> GetByNameAsync(string name)
        {
            return (await _productRepository.GetAsync(x => x.Name == name)).FirstOrDefault();
        }

        public async Task<Models.Product> UpdateAsync(Models.Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
    }
}
