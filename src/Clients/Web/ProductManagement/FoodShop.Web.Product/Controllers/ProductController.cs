using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Services.Contracts;
using FoodShop.Web.Product.Services.Contracts.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Product.Controllers
{
    [Authorize(Roles = Role.ProductManagementAdmin)]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IGenericResponseHandlerService _genericResponseHandlerService;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IGenericResponseHandlerService genericResponseHandlerService)
        {
            _logger = logger;
            _productService = productService;
            _genericResponseHandlerService = genericResponseHandlerService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll("api/product/get-all");

            return View(products?.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            var categories = productDto.Category.Split(' ');
            productDto.Categories = categories.ToList();

            var response = await _productService.Create("api/product/create", productDto);

            var result = _genericResponseHandlerService.HandleGenericResponse(response);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(
                "Error",
                "Response",
                new { message = "Error while trying to create product", errors = result.Errors, origin = "Product" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _productService.GetById("api/product/get", id);

            return View(product?.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            var categories = productDto.Category.Split(' ');
            productDto.Categories = categories.ToList();

            await _productService.Update("api/product/update", productDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _productService.Delete("api/product/delete", id);

            return RedirectToAction(nameof(Index));
        }
    }
}
