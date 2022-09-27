using System.ComponentModel.DataAnnotations;

namespace FoodShop.Services.Product.Api.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
    }
}
