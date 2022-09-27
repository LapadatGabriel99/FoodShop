using System.ComponentModel.DataAnnotations;

namespace FoodShop.Services.Product.Api.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
    }
}
