using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Converters
{
    internal sealed class UserModelToUserModelDtoConverter : IConverter<UserModel, UserModelDto>
    {
        public UserModelDto Convert(UserModel source)
        {
            var userModelDto = new UserModelDto
            {
                Id = source.ApplicationUser.Id,
                Email = source.ApplicationUser.Email,
                Address = source.ApplicationUser.Address,
                Phone = source.ApplicationUser.PhoneNumber,
                UserName = source.ApplicationUser.UserName,
                FirstName = source.ApplicationUser.FirstName,
                LastName = source.ApplicationUser.LastName,
                Role = source.Role
            };

            return userModelDto;
        }

        public IEnumerable<UserModelDto> Convert(IEnumerable<UserModel> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
