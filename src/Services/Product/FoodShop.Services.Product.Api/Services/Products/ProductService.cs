using FoodShop.Services.Product.Api.Data;
using FoodShop.Services.Product.Api.Models;
using FoodShop.Services.Product.Api.Repository.Contracts.Products;
using FoodShop.Services.Product.Api.Repository.Products;
using FoodShop.Services.Product.Api.Services.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Products;
using FoodShop.Services.Product.Api.Services.Contracts.Transaction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            product.Categories = categories.ToList();

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

        public async Task<Models.Product> GetByIdAsync(string id, List<Expression<Func<Models.Product, object>>> includes)
        {
            return (await _productRepository.GetAsync(x => x.Id == id, null, includes)).FirstOrDefault();
        }

        public async Task<Models.Product> GetByNameAsync(string name)
        {
            return (await _productRepository.GetAsync(x => x.Name == name)).FirstOrDefault();
        }

        public async Task<Models.Product> UpdateAsync(Models.Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<Models.Product> UpdateWithCategories(
            Models.Product existingProduct,
            Models.Product updatedProduct,
            IEnumerable<Category> newCategories)
        {

            var ids = existingProduct.Categories.Select(x => x.Id).ToList();

            foreach (var id in ids)
            {
                var joinEntity = await _productRepository.Context
                                            .Set<ProductCategory>("ProductsCategories")
                                            .FirstOrDefaultAsync(x => x.ProductId == existingProduct.Id && x.CategoryId == id);
                _productRepository.Context.Set<ProductCategory>("ProductsCategories").Remove(joinEntity);

                var category = existingProduct.Categories.FirstOrDefault(x => x.Id == id);
                existingProduct.Categories.Remove(category);
            }

            foreach (var category in newCategories)
            {
                var joinEntity = new ProductCategory
                {
                    Product = existingProduct,
                    ProductId = existingProduct.Id,
                    Category = category,
                    CategoryId = category.Id
                };

                await _productRepository.Context.Set<ProductCategory>("ProductsCategories").AddAsync(joinEntity);
                existingProduct.Categories.Add(category);
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Summary = updatedProduct.Summary;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.ImageUrl = updatedProduct.ImageUrl;

            return await _productRepository.UpdateAsync(existingProduct);
        }
    }
}
