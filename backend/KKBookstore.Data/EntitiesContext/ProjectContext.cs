using KKBookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KKBookstore.Data.EntitiesContext
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global turn off delete behaviour on foreign keys
            

            modelBuilder.Entity<RefAddressType>()
                .HasKey(r => r.RefAddressTypeCode);

            // define key for UnitMeasure
            modelBuilder.Entity<UnitMeasure>()
                .HasKey(u => u.UnitMeasureCode);

            // Todo: define key for ProductType
            modelBuilder.Entity<ProductType>()
                .HasKey(pt => pt.ProductTypeCode);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerID);
            modelBuilder.Entity<User>()
                .HasOne(u => u.LastEditedByUser)
                .WithMany()
                .HasForeignKey(u => u.LastEditedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShippingAddress>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.ShippingAddresses)
                .HasForeignKey(sa => sa.UserID);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<UserPassword>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPasswords)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<ProductPriceHistory>()
                .HasKey(table => new
                {
                    table.SkuID,
                    table.StartWhen
                });
            modelBuilder.Entity<Product>()
                .HasOne(p => p.UnitMeasure)
                .WithMany()
                .HasForeignKey(p => p.UnitMeasureCode);

            modelBuilder.Entity<SkuOptionValue>()
                .HasKey(table => new
                {
                    table.SkuID,
                    table.OptionID
                });

            modelBuilder.Entity<WrittenBy>()
                .HasKey(table => new
                {
                    table.ProductID,
                    table.AuthorID
                });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<WrittenBy> WrittenBys { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<DiscountVoucher> DiscountVouchers { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SkuImage> ProductImages { get; set; }
        public DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RefAddressType> RefAddressTypes { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuOptionValue> SkuOptionValues { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
    }
}