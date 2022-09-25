using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace FoodShop.Services.User.Api.Web.Filters
{
    public sealed class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            Debug.WriteLine(context.Exception);

            context.Result = new BadRequestObjectResult(context.Exception);
        }
    }
}
