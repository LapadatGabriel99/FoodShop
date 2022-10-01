﻿using FoodShop.Services.Product.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodShop.Services.Product.Api.Authorization.Filters.Exceptions
{
    public sealed class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            context.Result = new JsonResult(new GenericResponseDto<dynamic>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Data = null,
                Errors = new List<string> { context.Exception.Message }
            });
        }
    }
}
