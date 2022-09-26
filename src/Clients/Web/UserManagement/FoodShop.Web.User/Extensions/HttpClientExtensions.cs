using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text;

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

        public static async Task<TOutput> PostAsync<TOutput, TInput>(this HttpClient client, string uri, TInput input)
        {
            var fullRoute = client.BaseAddress + uri;
            var serializedInput = JsonConvert.SerializeObject(input);
            var response = await client.PostAsync(fullRoute, new StringContent(serializedInput, Encoding.UTF8, "application/json"));
            var jsonResult = await response.Content.ReadAsStringAsync();
            var objectResult = JsonConvert.DeserializeObject<TOutput>(jsonResult);

            return objectResult;
        }
    }
}
