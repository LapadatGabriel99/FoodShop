using FoodShop.Services.User.Api.Converters;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Services.Contracts
{
    public interface IUserConverterService
    {
        UserModel Convert(UserModelDto source);

        UserModelDto Convert(UserModel source);

        IEnumerable<UserModel> Convert(IEnumerable<UserModelDto> source);

        IEnumerable<UserModelDto> Convert(IEnumerable<UserModel> source);

        UserModel Convert(UpdateBasicCredentialsDto source);
    }
}
