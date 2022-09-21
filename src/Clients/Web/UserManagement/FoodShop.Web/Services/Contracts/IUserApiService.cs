
using FoodShop.Web.User.Dto;

namespace FoodShop.Web.User.Services.Contracts
{
    public interface IUserApiService
    {
        Task<IEnumerable<UserModelDto>> GetAll(string route);

        Task<UserModelDto> GetById(string routes, string id);
    }
}
