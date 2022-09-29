using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Models.Contracts;

namespace FoodShop.Services.Product.Api.Services.Contracts.Converters
{
    public interface IObjectConverterService
    {
        TDestination Convert<TSource, TDestination>(IConverter<TSource, TDestination> converter, TSource source)
            where TDestination : class, ICanConvert
            where TSource : class, ICanConvert;

        IEnumerable<TDestination> Convert<TSource, TDestination>(IConverter<TSource, TDestination> converter, IEnumerable<TSource> source)
            where TDestination : class, ICanConvert
            where TSource : class, ICanConvert;
    }
}
