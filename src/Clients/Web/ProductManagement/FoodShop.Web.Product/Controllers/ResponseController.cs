using FoodShop.Web.Product.Dto.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Product.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ILogger<ResponseController> _logger;

        public ResponseController(ILogger<ResponseController> logger)
        {
            _logger = logger;
        }

        public IActionResult Success(string message, string origin)
        {
            var successResponse = new SuccessResponseModel { Message = message, ControllerOrigin = origin };

            return View(successResponse);
        }

        public IActionResult Error(string message, string errors, string origin)
        {
            var errorResponse = new ErrorResponseModel { Message = message, Errors = errors.Split(';').ToList(), ControllerOrigin = origin };

            return View(errorResponse);
        }
    }
}
