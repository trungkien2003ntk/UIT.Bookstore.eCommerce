﻿using KKBookstore.Domain.Branches;
using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ProductTypes;
using KKBookstore.Domain.ShoppingCarts;
using KKBookstore.Domain.Staffs;
using KKBookstore.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace KKBookstore.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<IdentityRole<int>> Roles { get; set; }
    public DbSet<IdentityUserRole<int>> UserRoles { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<BranchAddress> BranchAddresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }
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
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductVariantOptionValue> ProductVariantOptionValues { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<UnitMeasure> UnitMeasures { get; set; }

    EntityEntry Entry(object entity);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
