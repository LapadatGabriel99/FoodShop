using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Web.Contracts;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace FoodShop.Web.Product.Services.Contracts.Filters
{
    public interface IGenericResponseHandlerService
    {
        IGenericResponseHandler<TInput> HandleGenericResponse<TInput>(GenericResponseDto<TInput> responseDto);
    }
}
