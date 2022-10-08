using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Services.Contracts.Authorization;
using FoodShop.Services.Product.Api.Services.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Converters.Products;
using FoodShop.Services.Product.Api.Services.Contracts.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FoodShop.Services.Product.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    [Authorize(Roles = Role.ProductManagementAdmin)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductConverterService _productConverterService;
        private readonly ICategoryService _categoryService;
        private readonly IUserAuthorizationService _userAuthorizationService;

        public ProductController(
            IProductService productService,
            IProductConverterService productConverterService,
            ICategoryService categoryService,
            IUserAuthorizationService userAuthorizationService)
        {
            _productService = productService;
            _productConverterService = productConverterService;
            _categoryService = categoryService;
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GenericResponseDto<ProductDto>>> Create([FromBody] ProductDto dto)
        {
            var product = _productConverterService.Convert(dto);
            product.CreatedBy = _userAuthorizationService.GetSubjectUsername();
            var categories = await _categoryService.GetAllByNameAsync(dto.Categories);
            var result = await _productService.CreateWithCategories(product, categories);
            var resultDto = _productConverterService.Convert(result);

            return new JsonResult(new GenericResponseDto<ProductDto> 
            {
                StatusCode = StatusCodes.Status201Created,
                Data = resultDto,
                Errors = null,
            });
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<GenericResponseDto<IEnumerable<ProductDto>>>> Get()
        {
            var products = await _productService.GetAllAsync();
            var productsDto = _productConverterService.Convert(products);

            return new JsonResult(new GenericResponseDto<IEnumerable<ProductDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = productsDto,
                Errors = null,
            });
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<GenericResponseDto<ProductDto>>> Get(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _productConverterService.Convert(product);

            return new JsonResult(new GenericResponseDto<ProductDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = productDto,
                Errors = null,
            });
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<GenericResponseDto<ProductDto>>> Update([FromBody] ProductDto dto)
        {
            var product = _productConverterService.Convert(dto);
            var categories = await _categoryService.GetAllByNameAsync(dto.Categories);

            Expression<Func<Models.Product, object>> expression = x => x.Categories;
            var list = new List<Expression<Func<Models.Product, object>>>();
            list.Add(expression);

            var existingProduct = await _productService.GetByIdAsync(product.Id, list);
            var result = await _productService.UpdateWithCategories(existingProduct, product, categories);
            var resultDto = _productConverterService.Convert(result);

            return new JsonResult(new GenericResponseDto<ProductDto>
            {
                StatusCode = StatusCodes.Status201Created,
                Data = resultDto,
                Errors = null,
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<GenericResponseDto<bool>>> Delete(string id)
        {
            var result = await _productService.DeleteAsync(id);

            return new JsonResult(new GenericResponseDto<bool>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = result,
                Errors = null
            });
        }
    }
}
