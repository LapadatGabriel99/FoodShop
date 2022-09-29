using FoodShop.Services.Product.Api.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace FoodShop.Services.Product.Api.Models
{
    public class Category : IEntity, ICanConvert
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
    }
}
