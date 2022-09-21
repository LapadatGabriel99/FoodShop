using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;

namespace FoodShop.Web.User.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TOutput> GetAsync<TOutput>(this HttpClient client, string uri)
        {
            var fullRoute = client.BaseAddress + uri;
            var response =  await client.GetAsync(fullRoute);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var objectResult = JsonConvert.DeserializeObject<TOutput>(jsonResult);

            return objectResult;
        }
    }
}
