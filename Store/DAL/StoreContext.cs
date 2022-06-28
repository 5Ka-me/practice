using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Motherboard" },
                new Category { Id = 2, Name = "Processor" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 1, Name = "Motherboard1", Description = "ProductDescription1", Price = 15, IsOnSale = false },
                new Product { Id = 2, CategoryId = 2, Name = "Processor1", Description = "ProductDescription2", Price = 25, IsOnSale = false },
                new Product { Id = 3, CategoryId = 2, Name = "Processor2", Description = "ProductDescription3", Price = 35, IsOnSale = false }
            );
        }
    }
}
