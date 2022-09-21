using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Models.Contracts;

namespace FoodShop.Services.User.Api.Services.Contracts
{
    public interface IObjectConverterService
    {
        TDestination Convert<TSource, TDestination>(IConverter<TSource, TDestination> converter, TSource source)
            where TDestination : class, ICanConvert
            where TSource : class, ICanConvert;

        IEnumerable<TDestination> Convert<TSource, TDestination>(IConverter<TSource, TDestination> converter, IEnumerable<TSource> source)
            where TDestination: class, ICanConvert
            where TSource: class, ICanConvert;
    }
}
