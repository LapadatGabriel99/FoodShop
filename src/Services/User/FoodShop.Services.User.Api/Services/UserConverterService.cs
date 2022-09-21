using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Services.Contracts;

namespace FoodShop.Services.User.Api.Services
{
    internal sealed class UserConverterService : IUserConverterService
    {
        private readonly IObjectConverterService _objectConverterService;
        private readonly IConverter<UserModelDto, UserModel> _userModelDtoToUserModelConverter;
        private readonly IConverter<UserModel, UserModelDto> _userModelToUserModelDtoConverter;

        public UserConverterService(
            IObjectConverterService objectConverterService,
            IConverter<UserModelDto, UserModel> userModelDtoToUserModelConverter,
            IConverter<UserModel, UserModelDto> userModelToUserModelDtoConverter)
        {
            _objectConverterService = objectConverterService;
            _userModelDtoToUserModelConverter = userModelDtoToUserModelConverter;
            _userModelToUserModelDtoConverter = userModelToUserModelDtoConverter;
        }

        public UserModel Convert(UserModelDto source)
        {
            return _objectConverterService.Convert(_userModelDtoToUserModelConverter, source);
        }

        public UserModelDto Convert(UserModel source)
        {
            return _objectConverterService.Convert(_userModelToUserModelDtoConverter, source);
        }

        public IEnumerable<UserModel> Convert(IEnumerable<UserModelDto> source)
        {
            return _objectConverterService.Convert(_userModelDtoToUserModelConverter, source);
        }

        public IEnumerable<UserModelDto> Convert(IEnumerable<UserModel> source)
        {
            return _objectConverterService.Convert(_userModelToUserModelDtoConverter, source);
        }
    }
}
