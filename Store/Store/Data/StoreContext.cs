using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, ProductName = "Product1", ProductDescription = "ProductDiscription1", ProductPrice = 15, IsOnSale = false},
                    new Product { ProductId = 2, ProductName = "Product2", ProductDescription = "ProductDiscription2", ProductPrice = 15, IsOnSale = false},
                    new Product { ProductId = 3, ProductName = "Product3", ProductDescription = "ProductDiscription3", ProductPrice = 15, IsOnSale = false}
            );
        }
    }
}
