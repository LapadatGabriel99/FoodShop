using FoodShop.Services.User.Api.Specification.Contracts;

namespace FoodShop.Services.User.Api.Filters.Contracts
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> collection, ISpecification<T> specification);
    }
}
