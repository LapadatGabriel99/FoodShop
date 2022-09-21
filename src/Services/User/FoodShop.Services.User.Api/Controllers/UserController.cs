using FoodShop.Services.User.Api.Data;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FoodShop.Services.User.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Roles = Role.UserMangementAdmin)]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IUserConverterService _userConverterService;
        private readonly IUserFilterService _userFilterService;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IUserConverterService userConverterService,
            IUserFilterService userFilterService)
        {
            _logger = logger;
            _userService = userService;
            _userConverterService = userConverterService;
            _userFilterService = userFilterService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<UserModelDto>>> Get()
        {
            var users = await _userService.GetAll();
            var filteredUsers = _userFilterService.ReturnUserListWithoutUserThatMadeTheRequest(users);
            var dtos = _userConverterService.Convert(filteredUsers);

            return Ok(dtos);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<UserModelDto>> Get(string id)
        {
            var user = await _userService.Get(id);
            var dto = _userConverterService.Convert(user);

            return Ok(dto);
        }
    }
}
