using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Interfaces;
using KKBookstore.Infrastructure.Data.Configurations;
using KKBookstore.Infrastructure.Data.Seeders;
using KKBookstore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace KKBookstore.Infrastructure.Data
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : IdentityDbContext<User, IdentityRole<int>, int>(options), IApplicationDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Model.GetEntityTypes()
                .Where(entityType => typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                .ToList()
                .ForEach(et =>
                    builder.Entity(et.ClrType)
                        .HasQueryFilter(ConvertFilterExpression<ISoftDelete>(e => !e.IsDeleted, et.ClrType))
                );

            DataSeeder.Seed(builder);
        }
        private static LambdaExpression ConvertFilterExpression<TInterface>(
            Expression<Func<TInterface, bool>> filterExpression,
            Type entityType)
        {
            var newParam = Expression.Parameter(entityType);
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

            return Expression.Lambda(newBody, newParam);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            await Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await Database.RollbackTransactionAsync(cancellationToken);
    }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<DiscountVoucher> DiscountVouchers { get; set; }
        public DbSet<VoucherUsage> VoucherUsages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
        public DbSet<ProductTypeAttribute> ProductTypeAttributes { get; set; }
        public DbSet<ProductTypeAttributeMapping> ProductTypeAttributeMappings { get; set; }
        public DbSet<ProductTypeAttributeValue> ProductTypeAttributeValues { get; set; }
        public DbSet<ProductTypeAttributeProductValue> ProductTypeAttributeProductValues { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingLike> RatingLikes { get; set; }
        public DbSet<RefAddressType> RefAddressTypes { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuOptionValue> SkuOptionValues { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
    }
}