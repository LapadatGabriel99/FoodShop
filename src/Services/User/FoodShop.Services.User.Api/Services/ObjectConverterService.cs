using FoodShop.Services.User.Api.Converters.Contracts;
using FoodShop.Services.User.Api.Models.Contracts;
using FoodShop.Services.User.Api.Services.Contracts;

namespace FoodShop.Services.User.Api.Services
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
