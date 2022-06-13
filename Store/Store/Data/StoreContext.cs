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

        //public DbSet<Receipt> Receipts { get; set; }

        //public DbSet<Storage> Storage { get; set; }

        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, ProductName = "Product1", Description = "ProductDiscription1"},
                    new Product { ProductId = 2, ProductName = "Product2", Description = "ProductDiscription2"},
                    new Product { ProductId = 3, ProductName = "Product3", Description = "ProductDiscription3"}
            );
        }
    }
}
