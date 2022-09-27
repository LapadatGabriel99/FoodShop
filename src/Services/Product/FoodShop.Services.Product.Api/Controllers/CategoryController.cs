using FoodShop.Services.Product.Api.Services.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Services.Product.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
    }
}
