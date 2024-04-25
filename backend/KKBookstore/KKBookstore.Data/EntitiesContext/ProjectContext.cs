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
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        
    }
}