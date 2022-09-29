using FoodShop.Services.Product.Api.Models.Contracts;

namespace FoodShop.Services.Product.Api.Converters.Contracts
{
    public interface IConverter<in TSource, out TDestination>
        where TSource : class, ICanConvert
        where TDestination : class, ICanConvert
    {
        TDestination Convert(TSource source);

        IEnumerable<TDestination> Convert(IEnumerable<TSource> source);
    }
}
