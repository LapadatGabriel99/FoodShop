using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();

        Task<UserModel> Get(string id);

        Task<UserModel> Update(UserModel user);

        Task<bool> Delete(string id);

        Task<UserModel> Create(UserModel user);
    }
}
