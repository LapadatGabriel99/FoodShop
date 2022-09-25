using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();

        Task<UserModel> Get(string id);

        Task<UserModel> UpdateBasicCredentials(UserModel user);

        Task<UserModel> UpdateUserName(string id, string userName);

        Task<UserModel> UpdateEmail(string id, string email, string token);

        Task<UserModel> UpdatePassword(string id, string password, string newPassword);

        Task<UserModel> UpdatePhoneNumber(string id, string phoneNumber, string token);

        Task<bool> Delete(string id);

        Task<UserModel> Create(UserModel user);
    }
}
