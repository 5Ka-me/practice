using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL
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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product1", Description = "ProductDescription1", Price = 15, IsOnSale = false},
                new Product { Id = 2, Name = "Product2", Description = "ProductDescription2", Price = 25, IsOnSale = false},
                new Product { Id = 3, Name = "Product3", Description = "ProductDescription3", Price = 35, IsOnSale = false}
            );
        }
    }
}
