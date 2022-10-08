using FoodShop.Services.Product.Api.Repository.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Seeder;
using System.Runtime.CompilerServices;

namespace FoodShop.Services.Product.Api.Services.Seeder
{
    public sealed class DbSeederService : IDbSeederService
    {
        private readonly ILogger<DbSeederService> _logger;
        private readonly ICategoryService _categoryService;

        public DbSeederService(ILogger<DbSeederService> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public async Task SeedDatabaseAsync()
        {
        }
    }
}
