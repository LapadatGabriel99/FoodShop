namespace FoodShop.Web.Product.Dto
{
    public sealed class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public List<string> Categories { get; set; }

        public string Category { get; set; }
    }
}
