using FoodShop.Services.User.Api.Models.Contracts;

namespace FoodShop.Services.User.Api.Converters.Contracts
{
    public interface IConverter<in TSource, out TDestination>
        where TSource : class, ICanConvert
        where TDestination : class, ICanConvert
    {
        TDestination Convert(TSource source);

        IEnumerable<TDestination> Convert(IEnumerable<TSource> source);
    }
}
