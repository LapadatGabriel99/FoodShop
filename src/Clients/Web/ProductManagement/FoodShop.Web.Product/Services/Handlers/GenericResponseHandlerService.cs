using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Services.Contracts.Filters;
using FoodShop.Web.Product.Web.Contracts;
using FoodShop.Web.Product.Web.Response;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace FoodShop.Web.Product.Services.Filters
{
    internal sealed class GenericResponseHandlerService : IGenericResponseHandlerService
    {
        private readonly ILogger<GenericResponseHandlerService> _logger;

        public GenericResponseHandlerService(ILogger<GenericResponseHandlerService> logger)
        {
            _logger = logger;
        }

        public IGenericResponseHandler<TInput> HandleGenericResponse<TInput>(GenericResponseDto<TInput> responseDto)
        {
            if (responseDto.StatusCode >= 200 && responseDto.StatusCode <= 226)
            {
                return new GenericResponseHandler<TInput>(true, responseDto.Data, null);
            }

            if (responseDto.StatusCode >= 400 && responseDto.StatusCode <= 451)
            {
                return new GenericResponseHandler<TInput>(false, default(TInput), responseDto.Errors);
            }

            if (responseDto.StatusCode >= 500 && responseDto.StatusCode <= 511)
            {
                return new GenericResponseHandler<TInput>(false, default(TInput), responseDto.Errors);
            }

            throw new NotSupportedException($"The following status code is not recognized: {responseDto.StatusCode}");
        }
    }
}
