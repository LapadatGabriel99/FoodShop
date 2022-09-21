using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Converters
{
    internal sealed class UserModelDtoToUserModelConverter : IConverter<UserModelDto, UserModel>
    {
        public UserModel Convert(UserModelDto source)
        {
            var userModel = new UserModel
            {
                ApplicationUser = new ApplicationUser
                {
                    Id = source.Id,
                    UserName = source.UserName,
                    Email = source.Email,
                    PhoneNumber = source.Phone,
                    Address = source.Address,
                    LastName = source.LastName,
                    FirstName = source.LastName
                },
                Password = source.Password,
                Role = source.Role
            };

            return userModel;
        }

        public IEnumerable<UserModel> Convert(IEnumerable<UserModelDto> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
