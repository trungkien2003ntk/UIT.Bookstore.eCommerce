using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.DiscountAggregate;
using KKBookstore.Domain.OrderAggregate;
using KKBookstore.Domain.ProductAggregate;
using KKBookstore.Domain.ShoppingCart;
using KKBookstore.Domain.Users;
using KKBookstore.Infrastructure.Data.Configurations;
using KKBookstore.Infrastructure.Data.Seeders;
using KKBookstore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //System.Diagnostics.Debugger.Launch();


            DataSeeder.Seed(builder);
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<DiscountVoucher> DiscountVouchers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
        public DbSet<ProductTypeAttribute> ProductTypeAttributes { get; set; }
        public DbSet<ProductTypeAttributeMapping> ProductTypeAttributeMappings { get; set; }
        public DbSet<ProductTypeAttributeValue> ProductTypeAttributeValues { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }
        public DbSet<SkuImage> SkuImages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RefAddressType> RefAddressTypes { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuOptionValue> SkuOptionValues { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
    }
}