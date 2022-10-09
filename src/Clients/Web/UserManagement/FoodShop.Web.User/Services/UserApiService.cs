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

        public async Task<UserModelDto> Create(string route, UserModelDto input)
        {
            return await _httpClient.PostAsync<UserModelDto, UserModelDto>(route, input);
        }

        public async Task<UserModelDto> UpdateUserName(string route, UpdateUserNameDto input)
        {
            return await _httpClient.PutAsync<UserModelDto, UpdateUserNameDto>(route, input);
        }

        public async Task<UserModelDto> UpdatePassword(string route, UpdatePasswordDto input)
        {
            return await _httpClient.PutAsync<UserModelDto, UpdatePasswordDto>(route, input);
        }

        public async Task<UserModelDto> UpdateBasicCredentials(string route, UpdateBasicCredentialsDto input)
        {
            return await _httpClient.PutAsync<UserModelDto, UpdateBasicCredentialsDto>(route, input);
        }
    }
}
