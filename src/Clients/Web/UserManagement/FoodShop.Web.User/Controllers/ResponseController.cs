using FoodShop.Web.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.User.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ILogger<ResponseController> _logger;

        public ResponseController(ILogger<ResponseController> logger)
        {
            _logger = logger;
        }

        public IActionResult Success(string message)
        {
            var successResponse = new SuccessResponseModel { Message = message };

            return View(successResponse);
        }

        public IActionResult Error(string message, string errors)
        {
            var errorResponse = new ErrorResponseModel { Message = message, Errors = errors.Split(';').ToList() };

            return View(errorResponse);
        }
    }
}
