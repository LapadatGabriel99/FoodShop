using FoodShop.Services.Product.Api.Data.Contracts;
using FoodShop.Services.Product.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.Product.Api.Data
{
    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Models.Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Product>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Models.Product>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Models.Product>()
                .HasMany(x => x.Categories)
                .WithMany(x => x.Products)
                .UsingEntity<ProductCategory>(
                    "ProductsCategories",
                    x => x.HasOne(prop => prop.Category).WithMany().HasForeignKey(prop => prop.CategoryId),
                    x => x.HasOne(prop => prop.Product).WithMany().HasForeignKey(prop => prop.ProductId),
                    x =>
                    {
                        x.HasKey(prop => new { prop.ProductId, prop.CategoryId });
                    }
                );
        }
    }
}
