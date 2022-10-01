using FoodShop.Services.Product.Api.Dto;
using FoodShop.Services.Product.Api.Repository;
using FoodShop.Services.Product.Api.Services.Contracts.Categories;
using FoodShop.Services.Product.Api.Services.Contracts.Converters.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FoodShop.Services.Product.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    [Authorize(Roles = Role.ProductManagementAdmin)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryConverterService _categoryConverterService;

        public CategoryController(ICategoryService categoryService, ICategoryConverterService categoryConverterService)
        {
            _categoryService = categoryService;
            _categoryConverterService = categoryConverterService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GenericResponseDto<CategoryDto>>> Create([FromBody] CategoryDto dto)
        {
            var category = _categoryConverterService.Convert(dto);
            var result = await _categoryService.CreateAsync(category);
            var resultDto = _categoryConverterService.Convert(result);

            return new JsonResult(new GenericResponseDto<CategoryDto>
            {
                StatusCode = StatusCodes.Status201Created,
                Data = resultDto,
                Errors = null
            });
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<GenericResponseDto<List<CategoryDto>>>> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _categoryConverterService.Convert(categories);

            return new JsonResult(new GenericResponseDto<IEnumerable<CategoryDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = categoriesDto,
                Errors = null
            });
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<GenericResponseDto<CategoryDto>>> Get(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var categoryDto = _categoryConverterService.Convert(category);

            return new JsonResult(new GenericResponseDto<CategoryDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = categoryDto,
                Errors = null
            });
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<GenericResponseDto<CategoryDto>>> Update([FromBody] CategoryDto dto)
        {
            var category = _categoryConverterService.Convert(dto);
            var result = await _categoryService.UpdateAsync(category);
            var resultDto = _categoryConverterService.Convert(result);

            return new JsonResult(new GenericResponseDto<CategoryDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = resultDto,
                Errors = null
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<GenericResponseDto<bool>>> Delete(string id)
        {
            var result = await _categoryService.DeleteAsync(id);

            return new JsonResult(new GenericResponseDto<bool>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = result,
                Errors = null
            });
        }
    }
}
