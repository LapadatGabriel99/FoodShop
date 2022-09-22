using FoodShop.Web.User.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.User.Controllers
{
    [Authorize(Roles = Role.UserMangementAdmin)]
    public class EditUserController : Controller
    {
        private readonly ILogger<EditUserController> _logger;
        private readonly IUserService _userService;

        public EditUserController(ILogger<EditUserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userDetails = await _userService.GetById("api/user/get", id);

            return View(userDetails);
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangeEmail(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePhoneNumber(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangeBasicCredentials(string id)
        {
            return View();
        }
    }
}
