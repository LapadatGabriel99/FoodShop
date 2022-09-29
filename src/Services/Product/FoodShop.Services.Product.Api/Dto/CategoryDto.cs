using FoodShop.Services.Product.Api.Models.Contracts;

namespace FoodShop.Services.Product.Api.Dto
{
    public sealed class CategoryDto : ICanConvert
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
