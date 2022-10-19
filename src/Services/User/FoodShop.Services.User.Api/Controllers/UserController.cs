using FoodShop.Integration.ServiceBus.Messages;
using FoodShop.Services.User.Api.Data;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Services.Contracts;
using FoodShop.Services.User.Api.Services.Contracts.Authorization;
using FoodShop.Services.User.Api.Services.Contracts.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FoodShop.Services.User.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IUserConverterService _userConverterService;
        private readonly IUserFilterService _userFilterService;
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IMessagePublisherService _messagePublisherService;
        private readonly IConfiguration _configuration;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IUserConverterService userConverterService,
            IUserFilterService userFilterService,
            IUserAuthorizationService userAuthorizationService,
            IMessagePublisherService messagePublisherService,
            IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _userConverterService = userConverterService;
            _userFilterService = userFilterService;
            _userAuthorizationService = userAuthorizationService;
            _messagePublisherService = messagePublisherService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("get-all")]
        [Authorize(Policy = PolicyList.UserReadAdmin)]
        public async Task<ActionResult<IEnumerable<UserModelDto>>> Get()
        {
            var users = await _userService.GetAll();
            var filteredUsers = _userFilterService.ReturnUserListWithoutUserThatMadeTheRequest(users);
            var dtos = _userConverterService.Convert(filteredUsers);

            return Ok(dtos);
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Policy = PolicyList.UserRead)]
        public async Task<ActionResult<UserModelDto>> Get(string id)
        {
            var user = await _userService.Get(id);
            var dto = _userConverterService.Convert(user);

            return Ok(dto);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = PolicyList.UserCreateAdmin)]
        public async Task<ActionResult<UserModelDto>> Create([FromBody] UserModelDto userDto)
        {
            var userFromDto = _userConverterService.Convert(userDto);
            var user = await _userService.Create(userFromDto);
            var userDtoFromUser = _userConverterService.Convert(user);

            return Ok(userDtoFromUser);
        }

        [HttpPut]
        [Route("update-username")]
        [Authorize(Policy = PolicyList.UserUpdate)]
        public async Task<ActionResult<UserModelDto>> UpdateUserName([FromBody] UpdateUserNameDto dto)
        {
            var user = await _userService.UpdateUserName(dto.Id, dto.UserName);
            var userDtoFromUser = _userConverterService.Convert(user);
            await _messagePublisherService.SendMessageAsync(new CreateEmailMessage
            {
                UserEmail = _userAuthorizationService.GetUserEmail(),
                Reason = "Update-UserName",
                Content = dto.UserName
            }, _configuration["FoodShopServiceBus:EmailServiceQueue:Name"]);

            return Ok(userDtoFromUser);
        }

        [HttpPut]
        [Route("update-email")]
        [Authorize(Policy = PolicyList.UserUpdate)]
        public async Task<ActionResult<UserModelDto>> UpdateEmail([FromBody] UpdateEmailDto dto)
        {
            var user = await _userService.UpdateEmail(dto.Id, dto.Email, dto.Token);
            var userDtoFromUser = _userConverterService.Convert(user);

            return Ok(userDtoFromUser);
        }

        [HttpPut]
        [Route("update-phone-number")]
        [Authorize(Policy = PolicyList.UserUpdate)]
        public async Task<ActionResult<UserModelDto>> UpdatePhoneNumber([FromBody] UpdatePhoneNumberDto dto)
        {
            var user = await _userService.UpdateEmail(dto.Id, dto.PhoneNumber, dto.Token);
            var userDtoFromUser = _userConverterService.Convert(user);

            return Ok(userDtoFromUser);
        }

        [HttpPut]
        [Route("update-basic-credentials")]
        [Authorize(Policy = PolicyList.UserUpdate)]
        public async Task<ActionResult<UserModelDto>> Update([FromBody] UpdateBasicCredentialsDto userDto)
        {
            var userFromDto = _userConverterService.Convert(userDto);
            var user = await _userService.UpdateBasicCredentials(userFromDto);
            var userDtoFromUser = _userConverterService.Convert(user);

            return Ok(userDtoFromUser);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Policy = PolicyList.UserDeleteAdmin)]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return await _userService.Delete(id);
        }
    }
}
