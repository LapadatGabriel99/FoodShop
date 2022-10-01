using FoodShop.Web.Product.Web.Contracts;

namespace FoodShop.Web.Product.Web.Response
{
    public class GenericResponseHandler<TData> : IGenericResponseHandler<TData>
    {
        public GenericResponseHandler(bool succeeded, TData data, List<string> errors)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = ComposeErrors(errors);
        }

        public bool Succeeded { get; }

        public TData Data { get; }

        public string Errors { get; }

        private string ComposeErrors(List<string> errors)
        {
            if (errors == null || errors.Count == 0)
            {
                return string.Empty;
            }

            return errors.Aggregate((a, b) => a + ";" + b);
        }
    }
}
