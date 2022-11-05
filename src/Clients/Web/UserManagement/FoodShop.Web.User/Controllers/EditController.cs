using FoodShop.Web.User.Dto;
using FoodShop.Web.User.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FoodShop.Web.User.Controllers
{
    [Authorize]
    public class EditUserController : Controller
    {
        private readonly ILogger<EditUserController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public EditUserController(ILogger<EditUserController> logger, IUserService userService, IAuthService authService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;
            _configuration = configuration;
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
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromForm] UpdatePasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _userService.UpdatePassword("api/user/update-password", dto);

            return RedirectToAction(nameof(Edit), "EditUser", new { id = dto.Id });
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
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeBasicCredentials([FromForm] UpdateBasicCredentialsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _userService.UpdateBasicCredentials("api/user/update-basic-credentials", dto);

            return RedirectToAction(nameof(Edit), "EditUser", new { id = dto.Id });
        }

        [HttpGet]
        public IActionResult ChangeUserName(string id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserName([FromForm] UpdateUserNameDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _userService.UpdateUserName("api/user/update-username", dto);

            await RefreshUserAuthenticationAccess();

            return RedirectToAction(nameof(Edit), "EditUser", new { id = dto.Id });
        }

        private async Task RefreshUserAuthenticationAccess()
        {
            var refreshTokenResponse = await _authService.GetRefreshTokenRsponse(_configuration["UserManagement:ClientId"], _configuration["UserManagement:ClientSecret"]);
            await _authService.AuthenticateUser(refreshTokenResponse.UpdatedTokens);
        }
    }
}
