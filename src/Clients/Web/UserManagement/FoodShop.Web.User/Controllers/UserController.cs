using FoodShop.Web.User.Dto;
using FoodShop.Web.User.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodShop.Web.User.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Authorize(Roles = Role.UserMangementAdmin)]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll("api/user/get-all");

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = Role.UserMangementAdmin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.UserMangementAdmin)]
        public async Task<IActionResult> Create([FromForm] UserModelDto userModelDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userModelDto);
            }

            await _userService.Create("api/user/create", userModelDto);

            return RedirectToAction("Success", "Response", new { message = "User created successfully" });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var userDetails = await _userService.GetById("api/user/get", id);

            return View(userDetails);
        }

        [HttpGet]
        [Authorize]
        public IActionResult PersonalDetails()
        {
            var loggedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return RedirectToAction(nameof(Details), "User", new { id = loggedUserId });
        }
    }
}
