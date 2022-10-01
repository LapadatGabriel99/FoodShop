using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Services.Contracts;
using FoodShop.Web.Product.Services.Contracts.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Product.Controllers
{
    [Authorize(Roles = Role.ProductManagementAdmin)]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IGenericResponseHandlerService _genericResponseHandlerService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IGenericResponseHandlerService genericResponseHandlerService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _genericResponseHandlerService = genericResponseHandlerService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll("api/category/get-all");

            return View(categories?.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            var response = await _categoryService.Create("api/category/create", categoryDto);

            var result = _genericResponseHandlerService.HandleGenericResponse(response);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(
                "Error",
                "Response",
                new { message = "Error while trying to create user", errors = result.Errors, origin = "Category" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var category = await _categoryService.GetById("api/category/get", id);

            return View(category?.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            await _categoryService.Update("api/category/update", categoryDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.Delete("api/category/delete", id);

            return RedirectToAction(nameof(Index));
        }
    }
}
