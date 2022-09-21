namespace FoodShop.Services.User.Api.Specification.Contracts
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T entity);
    }
}
