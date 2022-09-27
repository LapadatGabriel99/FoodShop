using FoodShop.Services.Product.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.Product.Api.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<Models.Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategories>()
                .HasKey(x => new { x.ProductId, x.CategoryId });
            modelBuilder.Entity<Models.Product>()
                .HasMany(x => x.ProductCategories)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Category>()
                .HasMany(x => x.ProductCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
