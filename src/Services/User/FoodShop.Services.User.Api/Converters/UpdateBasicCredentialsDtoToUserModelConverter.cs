using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;

namespace FoodShop.Services.User.Api.Converters
{
    internal sealed class UpdateBasicCredentialsDtoToUserModelConverter : IConverter<UpdateBasicCredentialsDto, UserModel>
    {
        public UserModel Convert(UpdateBasicCredentialsDto source)
        {
            var userModel = new UserModel
            {
                ApplicationUser = new ApplicationUser
                {
                    Id = source.Id,                    
                    Address = source.Address,
                    LastName = source.LastName,
                    FirstName = source.FirstName
                },
            };

            return userModel;
        }

        public IEnumerable<UserModel> Convert(IEnumerable<UpdateBasicCredentialsDto> source)
        {
            foreach (var item in source)
            {
                yield return Convert(item);
            }
        }
    }
}
