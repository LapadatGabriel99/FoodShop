using FoodShop.Web.User.Dto;

namespace FoodShop.Web.User.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserModelDto>> GetAll(string route);

        Task<UserModelDto> GetById(string route, string id);

        Task<UserModelDto> Create(string route, UserModelDto input);
    }
}
