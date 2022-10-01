namespace FoodShop.Web.Product.Web.Contracts
{
    public interface IGenericResponseHandler<TData>
    {
        bool Succeeded { get; }

        TData Data { get; }

        string Errors { get; }
    }
}
