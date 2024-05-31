using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KKBookstore.Infrastructure.Data.Seeders;

internal static class DataSeeder
{
    private static readonly int DefaultAdminId = 2;
    private static bool Seeded = false;

    // User paths
    private static readonly string UserJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Users/User.json";
    private static readonly string ShippingAddressJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Users/ShippingAddress.json";

    // Product paths
    private static readonly string AuthorJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Author.json";
    private static readonly string BookAuthorJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/BookAuthor.json";
    private static readonly string ProductOptionJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductOption.json";
    private static readonly string ProductOptionValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductOptionValue.json";
    private static readonly string ProductJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Product.json";
    private static readonly string ProductImageJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductImage.json";
    private static readonly string ProductTypeJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductType.json";
    private static readonly string ProductTypeAttributeJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttribute.json";
    private static readonly string ProductTypeAttributeMappingJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttributeMapping.json";
    private static readonly string ProductTypeAttributeProductValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttributeProductValue.json";
    private static readonly string ProductTypeAttributeValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttributeValue.json";
    private static readonly string RatingJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Rating.json"; 
    private static readonly string RatingLikeJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/RatingLike.json";
    private static readonly string SkuJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Sku.json";
    private static readonly string SkuOptionValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/SkuOptionValue.json";
    private static readonly string UnitMeasureJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/UnitMeasure.json";

    // Order paths
    private static readonly string OrderJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Orders/Order.json";
    private static readonly string OrderLineJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Orders/OrderLine.json";

    // ShoppingCartItem paths
    private static readonly string ShoppingCartItemJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/ShoppingCartItems/ShoppingCartItem.json";

    // User related data
    private static readonly List<User> _users = [];
    private static readonly List<ShippingAddress> _shippingAddresses = [];

    // Product related data
    private static readonly List<Author> _authors = [];
    private static readonly List<UnitMeasure> _unitMeasures = [];
    private static readonly List<ProductType> _productTypes = [];
    private static readonly List<ProductTypeAttribute> _productTypeAttributes = [];
    private static readonly List<ProductTypeAttributeMapping> _productTypeAttributeMappings = [];
    private static readonly List<ProductTypeAttributeValue> _productTypeAttributeValues = [];
    private static readonly List<ProductTypeAttributeProductValue> _productTypeAttributeProductValues = [];
    private static readonly List<Product> _products = [];
    private static readonly List<ProductImage> _productImages = [];
    private static readonly List<Rating> _ratings = [];
    private static readonly List<RatingLike> _ratingLikes = [];
    private static readonly List<Sku> _skus = [];
    private static readonly List<BookAuthor> _bookAuthors = [];
    private static readonly List<ProductOption> _productOptions = [];
    private static readonly List<ProductOptionValue> _productOptionValues = [];
    private static readonly List<SkuOptionValue> _skuOptionValues = [];
    
    // Order related data
    private static readonly List<DeliveryMethod> _deliveryMethods = [];
    private static readonly List<PaymentMethod> _paymentMethods = [];
    private static readonly List<RefAddressType> _refAddressTypes = [];
    private static readonly List<Order> _orders = [];
    private static readonly List<OrderLine> _orderLines = [];

    // ShoppingCartItem related data
    private static readonly List<ShoppingCartItem> _shoppingCartItems = [];

    private static readonly JsonSerializerOptions enumOption = new() { Converters = { new JsonStringEnumConverter() } };

    public static void Seed(ModelBuilder builder)
    {
        if (!Seeded)
        {
            Seeded = true;
        }
        else
        {
            SeedUsersRelatedData(builder);
            SeedProductRelatedData(builder);
            SeedOrderRelatedData(builder);
            SeedShoppingCartItem(builder);
        }
    }

    private static void SeedUsersRelatedData(ModelBuilder builder)
    {
        SeedRoles(builder);
        SeedUsers(builder);
        SeedUserRoles(builder);
        SeedShippingAddress(builder);
    }

    private static void SeedProductRelatedData(ModelBuilder builder)
    {
        SeedAuthors(builder);
        SeedUnitMeasures(builder);
        SeedProductTypes(builder);
        SeedProductTypeAttributes(builder);
        SeedProductTypeAttributeMappings(builder);
        SeedProductTypeAttributeValues(builder);
        SeedProductTypeAttributeProductValues(builder);
        SeedProducts(builder);
        SeedProductImages(builder);
        SeedSkus(builder);
        SeedBookAuthors(builder);
        SeedOptions(builder);
        SeedOptionValues(builder);
        SeedSkuOptionValues(builder);
        SeedRatings(builder);
    }

    private static void SeedOrderRelatedData(ModelBuilder builder)
    {
        SeedReferenceValues(builder);
        SeedOrders(builder);
        SeedOrderLines(builder);
    }

    private static void SeedShoppingCartItem(ModelBuilder builder)
    {
        var shoppingCartItemJson = File.ReadAllText(ShoppingCartItemJsonPath, Encoding.UTF8);
        var shoppingCartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(shoppingCartItemJson);

        AddAudit(shoppingCartItems);

        _shoppingCartItems.AddRange(shoppingCartItems);

        builder.Entity<ShoppingCartItem>()
            .HasData(_shoppingCartItems);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        var roles = new List<IdentityRole<int>>
        {
            new() { Id = 1, Name = "Customer", NormalizedName = "CUSTOMER" },
            new() { Id = 2, Name = "Admin", NormalizedName = "ADMIN" },
            new() { Id = 3, Name = "SalesStaff", NormalizedName = "SALESSTAFF" },
            new() { Id = 4, Name = "CustomerCareStaff", NormalizedName = "CUSTOMERCARESTAFF" },
        };

        builder.Entity<IdentityRole<int>>()
            .HasData(roles);
    }

    private static void SeedUsers(ModelBuilder builder)
    {
        var userJson = File.ReadAllText(UserJsonPath, Encoding.UTF8);
        var users = JsonSerializer.Deserialize<List<User>>(userJson, enumOption);

        foreach (var user in users)
        {
            user.LastEditedWhen = DateTimeOffset.Now;
            user.CreatedWhen = DateTimeOffset.Now;
            user.CreatedBy = null;
            user.LastEditedBy = null;
        }

        // use identity password hasher to hash a string as password
        //users[0].PasswordHash = new PasswordHasher<User>().HashPassword(users[0], "Abc123*");
        //users[1].PasswordHash = new PasswordHasher<User>().HashPassword(users[1], "Abc123*");
        //users[2].PasswordHash = new PasswordHasher<User>().HashPassword(users[2], "Abc123*");
        //users[3].PasswordHash = new PasswordHasher<User>().HashPassword(users[3], "Abc123*");
        //users[4].PasswordHash = new PasswordHasher<User>().HashPassword(users[3], "Abc123*");

        foreach (var user in users)
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, user.PasswordHash != null ? user.PasswordHash : "Abc123*");
        }

        _users.AddRange(users);

        builder.Entity<User>()
            .HasData(_users);
    }

    private static void SeedUserRoles(ModelBuilder builder)
    {
        var userRoles = new List<IdentityUserRole<int>>
        {
            new() { UserId = 1, RoleId = 2 },
            new() { UserId = 2, RoleId = 3 },
            new() { UserId = 3, RoleId = 4 },
        };

        foreach (var user in _users)
        {
            switch (user.Id)
            {
                case 1:
                case 2:
                case 3:
                    break;
                default:
                    userRoles.Add(new() { UserId = user.Id, RoleId = 1 });
                    break;
            }
        }

        builder.Entity<IdentityUserRole<int>>()
            .HasData(userRoles);
    }

    private static void SeedShippingAddress(ModelBuilder builder)
    {
        var shippingAddressJson = File.ReadAllText(ShippingAddressJsonPath, Encoding.UTF8);
        var shippingAddresses = JsonSerializer.Deserialize<List<ShippingAddress>>(shippingAddressJson);

        AddAudit(shippingAddresses);

        _shippingAddresses.AddRange(shippingAddresses);

        builder.Entity<ShippingAddress>()
            .HasData(_shippingAddresses);
    }

    private static void SeedReferenceValues(ModelBuilder builder)
    {
        _deliveryMethods.AddRange([
                    new DeliveryMethod { Id = 1, Name = "Giao hàng tiêu chuẩn", Description = "Giao hàng tiêu chuẩn", CreatedBy = DefaultAdminId, CreatedWhen = DateTimeOffset.Now, LastEditedBy = DefaultAdminId, LastEditedWhen = DateTimeOffset.Now },
            new DeliveryMethod { Id = 2, Name = "Giao hàng nhanh", Description = "Giao hàng nhanh", CreatedBy = DefaultAdminId, CreatedWhen = DateTimeOffset.Now, LastEditedBy = DefaultAdminId, LastEditedWhen = DateTimeOffset.Now },
        ]);

        builder.Entity<DeliveryMethod>()
            .HasData(_deliveryMethods);

        _paymentMethods.AddRange([
            new PaymentMethod { Id = 1, Name = "Thanh toán khi nhận hàng", Description = "Thanh toán khi nhận hàng", CreatedBy = DefaultAdminId, CreatedWhen = DateTimeOffset.Now, LastEditedBy = DefaultAdminId, LastEditedWhen = DateTimeOffset.Now },
            new PaymentMethod { Id = 2, Name = "Thanh toán qua thẻ", Description = "Thanh toán qua thẻ", CreatedBy = DefaultAdminId, CreatedWhen = DateTimeOffset.Now, LastEditedBy = DefaultAdminId, LastEditedWhen = DateTimeOffset.Now },
        ]);

        builder.Entity<PaymentMethod>()
            .HasData(_paymentMethods);

        _refAddressTypes.AddRange([
            new RefAddressType { Id = 1, Name = "Nhà riêng", Description = "Nhà riêng", CreatedBy = DefaultAdminId, CreatedWhen = DateTimeOffset.Now, LastEditedBy = DefaultAdminId, LastEditedWhen = DateTimeOffset.Now },
            new RefAddressType { Id = 2, Name = "Văn phòng", Description = "Văn phòng", CreatedBy = DefaultAdminId, CreatedWhen = DateTimeOffset.Now, LastEditedBy = DefaultAdminId, LastEditedWhen = DateTimeOffset.Now },
        ]);

        builder.Entity<RefAddressType>()
            .HasData(_refAddressTypes);
    }

    private static void SeedOrders(ModelBuilder builder)
    {
        var orderJson = File.ReadAllText(OrderJsonPath, Encoding.UTF8);
        var orders = JsonSerializer.Deserialize<List<Order>>(orderJson, enumOption);
        //Debugger.Launch();
        AddAudit(orders);

        _orders.AddRange(orders);

        builder.Entity<Order>()
            .HasData(_orders);
    }

    private static void SeedOrderLines(ModelBuilder builder)
    {  
        var orderLineJson = File.ReadAllText(OrderLineJsonPath, Encoding.UTF8);
        var orderLines = JsonSerializer.Deserialize<List<OrderLine>>(orderLineJson);

        AddAudit(orderLines);

        _orderLines.AddRange(orderLines);

        builder.Entity<OrderLine>()
            .HasData(_orderLines);
    }

    private static void SeedAuthors(ModelBuilder builder)
    {
        var authorJson = File.ReadAllText(AuthorJsonPath, Encoding.UTF8);
        var authors = JsonSerializer.Deserialize<List<Author>>(authorJson);

        AddAudit(authors);

        _authors.AddRange(authors);

        builder.Entity<Author>()
            .HasData(_authors);
    }

    private static void SeedUnitMeasures(ModelBuilder builder)
    {
        var unitMeasureJson = File.ReadAllText(UnitMeasureJsonPath, Encoding.UTF8);
        var unitMeasures = JsonSerializer.Deserialize<List<UnitMeasure>>(unitMeasureJson);
        
        AddAudit(unitMeasures);

        _unitMeasures.AddRange(unitMeasures);

        builder.Entity<UnitMeasure>()
            .HasData(_unitMeasures);
    }

    private static void SeedProductTypes(ModelBuilder builder)
    {
        var productTypeJson = File.ReadAllText(ProductTypeJsonPath, Encoding.UTF8);
        var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeJson);

        AddAudit(productTypes);

        _productTypes.AddRange(productTypes);

        foreach (var productType in _productTypes)
        {
            // set display name to PascalCase
            productType.DisplayName = string.Join(" ", productType.DisplayName.Split(' ').Select(s => s.First().ToString().ToUpper() + s[1..].ToLower()));
        }

        builder.Entity<ProductType>()
            .HasData(_productTypes);
    }

    private static void SeedProductTypeAttributes(ModelBuilder builder)
    {
        var productTypeAttributeJson = File.ReadAllText(ProductTypeAttributeJsonPath, Encoding.UTF8);
        var productTypeAttributes = JsonSerializer.Deserialize<List<ProductTypeAttribute>>(productTypeAttributeJson);

        AddAudit(productTypeAttributes);

        _productTypeAttributes.AddRange(productTypeAttributes);

        builder.Entity<ProductTypeAttribute>()
            .HasData(_productTypeAttributes);
    }

    private static void SeedProductTypeAttributeMappings(ModelBuilder builder)
    {
        var productTypeAttributeMappingJson = File.ReadAllText(ProductTypeAttributeMappingJsonPath, Encoding.UTF8);
        var productTypeAttributeMappings = JsonSerializer.Deserialize<List<ProductTypeAttributeMapping>>(productTypeAttributeMappingJson);

        AddAudit(productTypeAttributeMappings);

        _productTypeAttributeMappings.AddRange(productTypeAttributeMappings);

        builder.Entity<ProductTypeAttributeMapping>()
            .HasData(_productTypeAttributeMappings);
    }

    private static void SeedProductTypeAttributeValues(ModelBuilder builder)
    {
        var productTypeAttributeValueJson = File.ReadAllText(ProductTypeAttributeValueJsonPath, Encoding.UTF8);
        var productTypeAttributeValues = JsonSerializer.Deserialize<List<ProductTypeAttributeValue>>(productTypeAttributeValueJson);

        AddAudit(productTypeAttributeValues);

        _productTypeAttributeValues.AddRange(productTypeAttributeValues);

        builder.Entity<ProductTypeAttributeValue>()
            .HasData(_productTypeAttributeValues);
    }

    private static void SeedProductTypeAttributeProductValues(ModelBuilder builder)
    {
        var productTypeAttributeProductValueJson = File.ReadAllText(ProductTypeAttributeProductValueJsonPath, Encoding.UTF8);
        var productTypeAttributeProductValues = JsonSerializer.Deserialize<List<ProductTypeAttributeProductValue>>(productTypeAttributeProductValueJson);

        AddAudit(productTypeAttributeProductValues);

        _productTypeAttributeProductValues.AddRange(productTypeAttributeProductValues);

        builder.Entity<ProductTypeAttributeProductValue>()
            .HasData(_productTypeAttributeProductValues);
    }

    private static void SeedProducts(ModelBuilder builder)
    {
        var productJson = File.ReadAllText(ProductJsonPath, Encoding.UTF8);
        var products = JsonSerializer.Deserialize<List<Product>>(productJson);

        AddAudit(products);

        _products.AddRange(products);

        builder.Entity<Product>()
            .HasData(_products);
    }

    private static void SeedProductImages(ModelBuilder builder)
    {
        var productImageJson = File.ReadAllText(ProductImageJsonPath, Encoding.UTF8);
        var productImages = JsonSerializer.Deserialize<List<ProductImage>>(productImageJson);

        AddAudit(productImages);

        _productImages.AddRange(productImages);

        builder.Entity<ProductImage>()
            .HasData(_productImages);
    }

    private static void SeedSkus(ModelBuilder builder)
    {
        var skuJson = File.ReadAllText(SkuJsonPath, Encoding.UTF8);
        var skus = JsonSerializer.Deserialize<List<Sku>>(skuJson, enumOption);
        AddAudit(skus);

        _skus.AddRange(skus);

        builder.Entity<Sku>()
            .HasData(_skus);

        foreach (var sku in _skus)
        {
            builder.Entity<Sku>()
                .OwnsOne(s => s.SkuValue)
                .HasData(
                new
                {
                    SkuId = sku.Id,
                    Value = $"SKU{sku.Id:D5}"
                });

            switch (sku.Id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    sku.Weight = 100;
                    builder.Entity<Sku>()
                        .OwnsOne(s => s.Dimension)
                        .HasData(
                        new
                        {
                            SkuId = sku.Id,
                            Height = 25m,
                            Width = 20.5m,
                            Length = 0.2m,
                        });
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    sku.Weight = 150;
                    builder.Entity<Sku>()
                        .OwnsOne(s => s.Dimension)
                        .HasData(
                        new
                        {
                            SkuId = sku.Id,
                            Height = 24.0m,
                            Width = 16.0m,
                            Length = 0.5m,
                        });
                    break;
                case 9:
                    sku.Weight = 190;
                    builder.Entity<Sku>()
                        .OwnsOne(s => s.Dimension)
                        .HasData(
                        new
                        {
                            SkuId = sku.Id,
                            Height = 24.0m,
                            Width = 16.0m,
                            Length = 0.5m,
                        });
                    break;
                case 10:
                    sku.Weight = 1200;
                    builder.Entity<Sku>()
                        .OwnsOne(s => s.Dimension)
                        .HasData(
                        new
                        {
                            SkuId = sku.Id,
                            Height = 24.0m,
                            Width = 17.0m,
                            Length = 3.0m,
                        });
                    break;
                case 11:
                    sku.Weight = 2500;
                    builder.Entity<Sku>()
                        .OwnsOne(s => s.Dimension)
                        .HasData(
                        new
                        {
                            SkuId = sku.Id,
                            Height = 24.0m,
                            Width = 17.0m,
                            Length = 6.0m,
                        });
                    break;
            }
        }
    }

    private static void SeedBookAuthors(ModelBuilder builder)
    {
        var bookAuthorJson = File.ReadAllText(BookAuthorJsonPath, Encoding.UTF8);
        var bookAuthors = JsonSerializer.Deserialize<List<BookAuthor>>(bookAuthorJson);

        AddAudit(bookAuthors);

        _bookAuthors.AddRange(bookAuthors);

        builder.Entity<BookAuthor>()
            .HasData(_bookAuthors);
    }

    private static void SeedOptions(ModelBuilder builder)
    {
        var productOptionJson = File.ReadAllText(ProductOptionJsonPath, Encoding.UTF8);
        var productOptions = JsonSerializer.Deserialize<List<ProductOption>>(productOptionJson);

        AddAudit(productOptions);

        _productOptions.AddRange(productOptions);

        builder.Entity<ProductOption>()
            .HasData(_productOptions);
    }

    private static void SeedOptionValues(ModelBuilder builder)
    {
        var productOptionValueJson = File.ReadAllText(ProductOptionValueJsonPath, Encoding.UTF8);
        var productOptionValues = JsonSerializer.Deserialize<List<ProductOptionValue>>(productOptionValueJson);

        AddAudit(productOptionValues);

        _productOptionValues.AddRange(productOptionValues);

        builder.Entity<ProductOptionValue>()
            .HasData(_productOptionValues);
    }

    private static void SeedSkuOptionValues(ModelBuilder builder)
    {
        var skuOptionValueJson = File.ReadAllText(SkuOptionValueJsonPath, Encoding.UTF8);
        var skuOptionValues = JsonSerializer.Deserialize<List<SkuOptionValue>>(skuOptionValueJson);

        AddAudit(skuOptionValues);

        _skuOptionValues.AddRange(skuOptionValues);

        builder.Entity<SkuOptionValue>()
            .HasData(_skuOptionValues);
    }

    private static void SeedRatings(ModelBuilder builder)
    {
        var ratingJson = File.ReadAllText(RatingJsonPath, Encoding.UTF8);
        var ratings = JsonSerializer.Deserialize<List<Rating>>(ratingJson, enumOption);

        AddAudit(ratings);

        _ratings.AddRange(ratings);

        builder.Entity<Rating>()
            .HasData(_ratings);

        var ratingLikeJson = File.ReadAllText(RatingLikeJsonPath, Encoding.UTF8);
        var ratingLikes = JsonSerializer.Deserialize<List<RatingLike>>(ratingLikeJson);

        _ratingLikes.AddRange(ratingLikes);

        builder.Entity<RatingLike>()
            .HasData(_ratingLikes);
    }

    private static void AddAudit<TAuditableEntity>(List<TAuditableEntity> listItem) where TAuditableEntity : BaseAuditableEntity
    {
        foreach (var item in listItem)
        {
            item.CreatedBy = DefaultAdminId;
            item.CreatedWhen = new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0));
            item.LastEditedBy = DefaultAdminId;
            item.LastEditedWhen = new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0));
        }
    }
    

}
