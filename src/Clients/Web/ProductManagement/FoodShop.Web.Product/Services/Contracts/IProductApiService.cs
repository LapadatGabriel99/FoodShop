using FoodShop.Web.Product.Dto;

namespace FoodShop.Web.Product.Services.Contracts
{
    public interface IProductApiService
    {
        Task<TOutput> GetAll<TOutput>(string route);

        Task<TOutput> GetById<TOutput>(string route, string id);

        Task<TOutput> Create<TOutput, TInput>(string route, TInput input);

        Task<TOutput> Delete<TOutput>(string route, string id);

        Task<TOutput> Update<TOutput, TInput>(string route, TInput input);
    }
}
