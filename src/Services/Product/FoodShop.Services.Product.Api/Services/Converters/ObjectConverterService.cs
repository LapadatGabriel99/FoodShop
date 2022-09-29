using FoodShop.Services.Product.Api.Converters.Contracts;
using FoodShop.Services.Product.Api.Models.Contracts;
using FoodShop.Services.Product.Api.Services.Contracts.Converters;

namespace FoodShop.Services.Product.Api.Services.Converters
{
    internal sealed class ObjectConverterService : IObjectConverterService
    {
        public TDestination Convert<TSource, TDestination>(IConverter<TSource, TDestination> converter, TSource source)
            where TDestination : class, ICanConvert
            where TSource : class, ICanConvert
        {
            return converter.Convert(source);
        }

        IEnumerable<TDestination> IObjectConverterService.Convert<TSource, TDestination>(IConverter<TSource, TDestination> converter, IEnumerable<TSource> source)
        {
            return converter.Convert(source);
        }
    }
}
