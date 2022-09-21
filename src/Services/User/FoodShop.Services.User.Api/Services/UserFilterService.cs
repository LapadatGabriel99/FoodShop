using FoodShop.Services.User.Api.Filters;
using FoodShop.Services.User.Api.Filters.Contracts;
using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Services.Contracts;
using FoodShop.Services.User.Api.Specification;
using FoodShop.Services.User.Api.Specification.Contracts;

namespace FoodShop.Services.User.Api.Services
{
    internal sealed class UserFilterService : IUserFilterService
    {
        private readonly Func<FilterEnum, IFilter<UserModel>> _filterFactory;
        private readonly Func<SpecificationEnum, ISpecification<UserModel>> _specificationFactory;

        public UserFilterService(Func<FilterEnum, IFilter<UserModel>> filterFactory, Func<SpecificationEnum, ISpecification<UserModel>> specificationFactory)
        {
            _filterFactory = filterFactory;
            _specificationFactory = specificationFactory;
        }

        public IEnumerable<UserModel> ReturnUserListWithoutUserThatMadeTheRequest(IEnumerable<UserModel> list)
        {
            var filter = _filterFactory(FilterEnum.UserModel);
            var specification = _specificationFactory(SpecificationEnum.NotUserThatRequestedSpecification);

            return filter.Filter(list, specification);
        }
    }
}
