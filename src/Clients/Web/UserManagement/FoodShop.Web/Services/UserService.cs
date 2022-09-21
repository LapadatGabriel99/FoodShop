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
    }
}
