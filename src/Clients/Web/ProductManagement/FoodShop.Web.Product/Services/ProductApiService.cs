using FoodShop.Web.Product.Dto;
using FoodShop.Web.Product.Extensions;
using FoodShop.Web.Product.Services.Contracts;
using Microsoft.AspNetCore.Routing;

namespace FoodShop.Web.Product.Services
{
    internal sealed class ProductApiService : IProductApiService
    {
        private readonly ILogger<ProductApiService> _logger;
        private readonly HttpClient _httpClient;

        public ProductApiService(ILogger<ProductApiService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<TOutput> Create<TOutput, TInput>(string route, TInput input)
        {
            return await _httpClient.PostAsync<TOutput, TInput>(route, input);
        }

        public async Task<TOutput> Delete<TOutput>(string route, string id)
        {
            return await _httpClient.DeleteAsync<TOutput>($"{route}/{id}");
        }

        public async Task<TOutput> GetAll<TOutput>(string route)
        {
            return await _httpClient.GetAsync<TOutput>(route);
        }

        public async Task<TOutput> GetById<TOutput>(string route, string id)
        {
            return await _httpClient.GetAsync<TOutput>($"{route}/{id}");
        }

        public async Task<TOutput> Update<TOutput, TInput>(string route, TInput input)
        {
            return await _httpClient.PutAsync<TOutput, TInput>(route, input);
        }
    }
}
