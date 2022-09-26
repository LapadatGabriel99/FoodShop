using FoodShop.Web.User.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.User.Controllers
{
    [Authorize(Roles = Role.UserMangementAdmin)]
    public class RequestProfileChangeController : Controller
    {
        private readonly ILogger<EditUserController> _logger;
        private readonly IUserService _userService;

        public RequestProfileChangeController(ILogger<EditUserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ChangeProfile(string id)
        {
            var userDetails = await _userService.GetById("api/user/get", id);

            return View(userDetails);
        }

        [HttpGet]
        public IActionResult RequestPasswordChange(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestEmailChange(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestPhoneNumberChange(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestBasicCredentialsChange(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestUserNameChange(string id)
        {
            return View();
        }
    }
}
