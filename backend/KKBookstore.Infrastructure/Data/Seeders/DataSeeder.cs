using KKBookstore.Domain.Common;
using KKBookstore.Domain.OrderAggregate;
using KKBookstore.Domain.ProductAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KKBookstore.Infrastructure.Data.Seeders;

internal static class DataSeeder
{
    private static readonly int DefaultAdminId = 9;
    private static bool Seeded = false;

    // Product paths
    private static readonly string AuthorJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Author.json";
    private static readonly string BookAuthorJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/BookAuthor.json";
    private static readonly string ProductOptionJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductOption.json";
    private static readonly string ProductOptionValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductOptionValue.json";
    private static readonly string ProductJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Product.json";
    private static readonly string ProductTypeJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductType.json";
    private static readonly string ProductTypeAttributeJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttribute.json";
    private static readonly string ProductTypeAttributeMappingJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttributeMapping.json";
    private static readonly string ProductTypeAttributeProductValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttributeProductValue.json";
    private static readonly string ProductTypeAttributeValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/ProductTypeAttributeValue.json";
    private static readonly string RatingJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Rating.json"; // empty
    private static readonly string SkuJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/Sku.json";
    private static readonly string SkuImageJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/SkuImage.json";
    private static readonly string SkuOptionValueJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/SkuOptionValue.json";
    private static readonly string UnitMeasureJsonPath = "../KKBookstore.Infrastructure/Data/Seeders/JsonData/Products/UnitMeasure.json";

    // Product related data
    private static readonly List<Author> _authors = [];
    private static readonly List<UnitMeasure> _unitMeasures = [];
    private static readonly List<ProductType> _productTypes = [];
    private static readonly List<ProductTypeAttribute> _productTypeAttributes = [];
    private static readonly List<ProductTypeAttributeMapping> _productTypeAttributeMappings = [];
    private static readonly List<ProductTypeAttributeValue> _productTypeAttributeValues = [];
    private static readonly List<ProductTypeAttributeProductValue> _productTypeAttributeProductValues = [];
    private static readonly List<Product> _products = [];
    private static readonly List<Sku> _skus = [];
    private static readonly List<SkuImage> _skuImages = [];
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

    public static void Seed(ModelBuilder builder)
    {
        if (!Seeded)
        {
            Seeded = true;
        }
        else
        {
            SeedProductRelatedData(builder);
            SeedOrderRelatedData(builder);
        }
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
        SeedSkus(builder);
        SeedSkuImages(builder);
        SeedBookAuthors(builder);
        SeedOptions(builder);
        SeedOptionValues(builder);
        SeedSkuOptionValues(builder);
    }

    

    private static void SeedOrderRelatedData(ModelBuilder builder)
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
            productType.DisplayName = string.Join("", productType.DisplayName.Split(' ').Select(s => s.First().ToString().ToUpper() + s[1..].ToLower()));
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

    private static void SeedSkus(ModelBuilder builder)
    {
        var skuJson = File.ReadAllText(SkuJsonPath, Encoding.UTF8);
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() } // Add this line to enable enum deserialization
        };
        var skus = JsonSerializer.Deserialize<List<Sku>>(skuJson, options);
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

    private static void SeedSkuImages(ModelBuilder builder)
    {
        var skuImageJson = File.ReadAllText(SkuImageJsonPath, Encoding.UTF8);
        var skuImages = JsonSerializer.Deserialize<List<SkuImage>>(skuImageJson);

        AddAudit(skuImages);

        _skuImages.AddRange(skuImages);

        builder.Entity<SkuImage>()
            .HasData(_skuImages);
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

    private static void AddAudit<TAuditableEntity>(List<TAuditableEntity> listItem) where TAuditableEntity : BaseAuditableEntity
    {
        foreach (var item in listItem)
        {
            item.CreatedBy = DefaultAdminId;
            item.CreatedWhen = DateTimeOffset.Now;
            item.LastEditedBy = DefaultAdminId;
            item.LastEditedWhen = DateTimeOffset.Now;
        }
    }
    

}
