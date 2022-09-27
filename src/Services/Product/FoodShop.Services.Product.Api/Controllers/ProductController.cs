using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Services.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Services.Product.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GenericResponseDto<ProductDto>>> Create(ProductDto dto)
        {

            return new JsonResult(new GenericResponseDto<ProductDto> 
            {
                StatusCode = StatusCodes.Status200OK,
                Data = null,
                Errors = null,
            });
        }
    }
}
