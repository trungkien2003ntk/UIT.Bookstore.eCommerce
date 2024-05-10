using KKBookstore.Domain.ShoppingCart;
using KKBookstore.Domain.DiscountAggregate;
using KKBookstore.Domain.OrderAggregate;
using KKBookstore.Domain.ProductAggregate;
using KKBookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KKBookstore.Infrastructure.Identity;

namespace KKBookstore.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefAddressType>()
                .HasKey(r => r.RefAddressTypeCode);

            modelBuilder.Entity<UnitMeasure>()
                .HasKey(u => u.UnitMeasureCode);

            modelBuilder.Entity<ProductType>()
                .HasKey(pt => pt.ProductTypeCode);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.LastEditedByUser)
                .WithMany()
                .HasForeignKey(u => u.LastEditedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShippingAddress>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.ShippingAddresses)
                .HasForeignKey(sa => sa.UserId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<UserPassword>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPasswords)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<ProductPriceHistory>()
                .HasKey(table => new
                {
                    table.SkuId,
                    table.StartWhen
                });
            modelBuilder.Entity<Product>()
                .HasOne(p => p.UnitMeasure)
                .WithMany()
                .HasForeignKey(p => p.UnitMeasureCode);

            modelBuilder.Entity<SkuOptionValue>()
                .HasKey(table => new
                {
                    table.SkuId,
                    table.OptionId
                });

            modelBuilder.Entity<WrittenBy>()
                .HasKey(table => new
                {
                    table.ProductId,
                    table.AuthorId
                });

            modelBuilder.Entity<Order>()
                .Property(o => o.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TaxRate)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderLine>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sku>()
                .Property(s => s.RecommendedRetailPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sku>()
                .Property(s => s.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sku>()
                .Property(s => s.TaxRate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductPriceHistory>()
                .Property(s => s.RecommendedRetailPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductPriceHistory>()
                .Property(s => s.UnitPrice)
                .HasPrecision(18, 2);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
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