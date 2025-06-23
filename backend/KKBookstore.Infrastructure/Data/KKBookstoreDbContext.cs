using KKBookstore.Banners;
using KKBookstore.Branches;
using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Data.Configurations.Products;
using KKBookstore.Identity;
using KKBookstore.Orders;
using KKBookstore.Products;
using KKBookstore.Products.Events;
using KKBookstore.ProductTypes;
using KKBookstore.ShoppingCarts;
using KKBookstore.Staffs;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using KKBookstore.StockTransactions.StockTransfers;
using KKBookstore.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KKBookstore.Data;

public class KKBookstoreDbContext(
    DbContextOptions<KKBookstoreDbContext> options
) : IdentityDbContext<User, IdentityRole<int>, int>(options), IApplicationDbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<BranchAddress> BranchAddresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<DiscountVoucher> DiscountVouchers { get; set; }
    public DbSet<VoucherUsage> VoucherUsages { get; set; }
    public DbSet<VoucherCustomerType> VoucherCustomerTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<OrderFulfillment> OrderFulfillments { get; set; }
    public DbSet<OrderLineAllocation> OrderLineAllocations { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }
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
    public DbSet<RatingImage> RatingImages { get; set; }
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductVariantOptionValue> ProductVariantOptionValues { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<UnitMeasure> UnitMeasures { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<StockAdjustment> StockAdjustments { get; set; }
    public DbSet<StockAdjustmentItem> StockAdjustmentItems { get; set; }
    public DbSet<StockTransfer> StockTransfers { get; set; }
    public DbSet<StockTransferItem> StockTransferItems { get; set; }
    public DbSet<RatingReport> RatingReports { get; set; }
    public DbSet<ModerationAuditLog> ModerationAuditLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

        foreach (var relationship in builder.Model.GetEntityTypes().Where(e => e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
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
}