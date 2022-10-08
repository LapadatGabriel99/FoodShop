using FoodShop.Services.Product.Api.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace FoodShop.Services.Product.Api.Models
{
    public class Category : IEntity, ICanConvert
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
