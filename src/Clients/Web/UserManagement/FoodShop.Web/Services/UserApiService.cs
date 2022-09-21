using FoodShop.Web.User.Dto;
using FoodShop.Web.User.Extensions;
using FoodShop.Web.User.Services.Contracts;
using System.Runtime.CompilerServices;

namespace FoodShop.Web.User.Services
{
    internal sealed class UserApiService : IUserApiService
    {
        private readonly ILogger<UserApiService> _logger;
        private readonly HttpClient _httpClient;

        public UserApiService(ILogger<UserApiService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserModelDto>> GetAll(string route)
        {
            return await _httpClient.GetAsync<IEnumerable<UserModelDto>>(route);
        }

        public async Task<UserModelDto> GetById(string route, string id)
        {
            return await _httpClient.GetAsync<UserModelDto>($"{route}/{id}");
        }
    }
}
