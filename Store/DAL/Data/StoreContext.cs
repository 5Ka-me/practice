using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Product1", ProductDescription = "ProductDescription1", ProductPrice = 15, IsOnSale = false},
                new Product { ProductId = 2, ProductName = "Product2", ProductDescription = "ProductDescription2", ProductPrice = 25, IsOnSale = false},
                new Product { ProductId = 3, ProductName = "Product3", ProductDescription = "ProductDescription3", ProductPrice = 35, IsOnSale = false}
            );
        }
    }
}
