using FoodShop.Web.User.Dto;
using FoodShop.Web.User.Services.Contracts;
using Newtonsoft.Json;

namespace FoodShop.Web.User.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserApiService _userApiService;

        public UserService(ILogger<UserService> logger, IUserApiService userApiService)
        {
            _logger = logger;
            _userApiService = userApiService;
        }

        public async Task<IEnumerable<UserModelDto>> GetAll(string route)
        {
            var users = await _userApiService.GetAll(route);

            return users;
        }

        public async Task<UserModelDto> GetById(string route, string id)
        {
            var user = await _userApiService.GetById(route, id);

            return user;
        }

        public async Task<UserModelDto> Create(string route, UserModelDto input)
        {
            var user = await _userApiService.Create(route, input);

            return user;
        }

        public async Task<UserModelDto> UpdateUserName(string route, UpdateUserNameDto input)
        {
            var user = await _userApiService.UpdateUserName(route, input);

            return user;
        }

        public async Task<UserModelDto> UpdatePassword(string route, UpdatePasswordDto input)
        {
            return await _userApiService.UpdatePassword(route, input);
        }

        public async Task<UserModelDto> UpdateBasicCredentials(string route, UpdateBasicCredentialsDto input)
        {
            return await _userApiService.UpdateBasicCredentials(route, input);
        }
    }
}
