using KKBookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Data.EntitiesContext
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<User> usersList = [
                new() { },
            ];

            modelBuilder.Entity<User>().HasData(usersList);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerID);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}