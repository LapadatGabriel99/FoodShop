using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Services.Contracts
{
    public interface IUserFilterService
    {
        IEnumerable<UserModel> ReturnUserListWithoutUserThatMadeTheRequest(IEnumerable<UserModel> list);
    }
}
