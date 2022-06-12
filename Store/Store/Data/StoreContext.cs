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

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Storage> Storage { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { UserId = 1, UserName = "Tom"},
                    new User { UserId = 2, UserName = "Bob"},
                    new User { UserId = 3, UserName = "Sam"}
            );
        }
    }
}
