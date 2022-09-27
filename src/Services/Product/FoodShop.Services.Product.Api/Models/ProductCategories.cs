namespace FoodShop.Services.Product.Api.Models
{
    public class ProductCategories
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
