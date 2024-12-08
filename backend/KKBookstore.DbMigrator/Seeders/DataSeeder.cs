using Bogus;
using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ProductTypes;
using KKBookstore.Domain.ShoppingCarts;
using KKBookstore.Domain.Staffs;
using KKBookstore.Domain.Users;
using KKBookstore.Infrastructure.Data;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KKBookstore.DbMigrator.Seeders;

internal class DataSeeder
{
    #region Json paths
    // User paths
    private const string BASE_PATH = "./Seeders/JsonData";
    private readonly string UserJsonPath = $"{BASE_PATH}/Users/User.json";
    private readonly string ShippingAddressJsonPath = $"{BASE_PATH}/Users/ShippingAddress.json";

    // Customer paths
    private readonly string CustomerJsonPath = $"{BASE_PATH}/Customers/Customers.json";
    private readonly string CustomerTypeJsonPath = $"{BASE_PATH}/Customers/CustomerTypes.json";

    // Staff paths
    private readonly string StaffJsonPath = $"{BASE_PATH}/Staffs/Staffs.json";

    // Product paths
    private readonly string AuthorJsonPath = $"{BASE_PATH}/Products/Author.json";
    private readonly string BookAuthorJsonPath = $"{BASE_PATH}/Products/BookAuthor.json";
    private readonly string ProductOptionJsonPath = $"{BASE_PATH}/Products/ProductOption.json";
    private readonly string ProductOptionValueJsonPath = $"{BASE_PATH}/Products/ProductOptionValue.json";
    private readonly string ProductJsonPath = $"{BASE_PATH}/Products/Product.json";
    private readonly string ProductImageJsonPath = $"{BASE_PATH}/Products/ProductImage.json";
    private readonly string ProductTypeJsonPath = $"{BASE_PATH}/Products/ProductType.json";
    private readonly string ProductTypeAttributeJsonPath = $"{BASE_PATH}/Products/ProductTypeAttribute.json";
    private readonly string ProductTypeAttributeMappingJsonPath = $"{BASE_PATH}/Products/ProductTypeAttributeMapping.json";
    private readonly string ProductTypeAttributeProductValueJsonPath = $"{BASE_PATH}/Products/ProductTypeAttributeProductValue.json";
    private readonly string ProductTypeAttributeValueJsonPath = $"{BASE_PATH}/Products/ProductTypeAttributeValue.json";
    private readonly string RatingJsonPath = $"{BASE_PATH}/Products/Rating.json";
    private readonly string RatingLikeJsonPath = $"{BASE_PATH}/Products/RatingLike.json";
    private readonly string ProductVariantJsonPath = $"{BASE_PATH}/Products/ProductVariant.json";
    private readonly string ProductVariantOptionValueJsonPath = $"{BASE_PATH}/Products/ProductVariantOptionValue.json";
    private readonly string UnitMeasureJsonPath = $"{BASE_PATH}/Products/UnitMeasure.json";

    // Order paths
    private readonly string OrderJsonPath = $"{BASE_PATH}/Orders/Order.json";
    private readonly string OrderLineJsonPath = $"{BASE_PATH}/Orders/OrderLine.json";

    // Discount paths
    private readonly string DiscountVoucherJsonPath = $"{BASE_PATH}/DiscountVouchers/DiscountVoucher.json";
    private readonly string VoucherUsageJsonPath = $"{BASE_PATH}/DiscountVouchers/VoucherUsage.json";


    // ShoppingCartItem paths
    private readonly string ShoppingCartItemJsonPath = $"{BASE_PATH}/ShoppingCartItems/ShoppingCartItem.json";
    #endregion Json paths

    #region Data lists
    // User related data
    private readonly List<User> _users = [];
    private readonly List<ShippingAddress> _shippingAddresses = [];

    // Customer related data
    private readonly List<Customer> _customers = [];
    private readonly List<CustomerType> _customerTypes = [];

    // Staff related data
    private readonly List<Staff> _staffs = [];

    // Product related data
    private readonly List<Author> _authors = [];
    private readonly List<UnitMeasure> _unitMeasures = [];
    private readonly List<ProductType> _productTypes = [];
    private readonly List<ProductTypeAttribute> _productTypeAttributes = [];
    private readonly List<ProductTypeAttributeMapping> _productTypeAttributeMappings = [];
    private readonly List<ProductTypeAttributeValue> _productTypeAttributeValues = [];
    private readonly List<ProductTypeAttributeProductValue> _productTypeAttributeProductValues = [];
    private readonly List<Product> _products = [];
    private readonly List<ProductImage> _productImages = [];
    private readonly List<Rating> _ratings = [];
    private readonly List<RatingLike> _ratingLikes = [];
    private readonly List<ProductVariant> _productVariants = [];
    private readonly List<BookAuthor> _bookAuthors = [];
    private readonly List<ProductOption> _productOptions = [];
    private readonly List<ProductOptionValue> _productOptionValues = [];
    private readonly List<ProductVariantOptionValue> _productVariantOptionValues = [];

    // Order related data
    private readonly List<DeliveryMethod> _deliveryMethods = [];
    private readonly List<PaymentMethod> _paymentMethods = [];
    private readonly List<Order> _orders = [];
    private readonly List<OrderLine> _orderLines = [];

    // Discount related data
    private readonly List<DiscountVoucher> _discountVouchers = [];
    private readonly List<VoucherUsage> _voucherUsages = [];

    // ShoppingCartItem related data
    private readonly List<ShoppingCartItem> _shoppingCartItems = [];
    #endregion Data lists

    // Json option
    private readonly JsonSerializerOptions enumOption = new() { Converters = { new JsonStringEnumConverter() } };

    private const int DEFAULT_ADMIN_ID = 1;
    private const int SEED = 1000000;
    private readonly Faker _faker = new("vi") { Random = new Randomizer(SEED) };
    private readonly KKBookstoreDbContext _dbContext;

    public DataSeeder(KKBookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        int order = 0;
        Log.Information($"\t{++order}. Seeding users related data and reference values");

        await SeedUsersRelatedDataAndReferenceValue();

        //Log.Information($"\t{++order}. Seeding product related data");
        //await SeedProductRelatedData();

        //Log.Information($"\t{++order}. Seeding discount related data");
        //await SeedDiscountVouchers();

        //Log.Information($"\t{++order}. Seeding order related data");
        //await SeedOrderRelatedData();

        //Log.Information($"\t{++order}. Seeding voucher usages");
        //await SeedVoucherUsages();

        //Log.Information($"\t{++order}. Seeding shopping cart item related data");
        //await SeedShoppingCartItemRelatedDataAsync();
    }

    private async Task SeedUsersRelatedDataAndReferenceValue()
    {
        Log.Information($"\t\t1. Seeding roles");

        await SeedRoles();

        Log.Information($"\t\t2. Seeding staffs");
        await SeedStaffs();

        Log.Information($"\t\t3. Seeding customer types");
        await SeedCustomerTypes();

        Log.Information($"\t\t4. Seeding customers");
        await SeedCustomers();
        //await SeedUsers();

        Log.Information($"\t\t5. Seeding user roles");
        await SeedUserRoles();
        //await SeedReferenceValues();
        //await SeedShippingAddress();
    }

    private async Task SeedCustomerTypes()
    {
        if (_dbContext.CustomerTypes.Any())
        {
            return;
        }

        var customerTypeJson = File.ReadAllText(CustomerTypeJsonPath, Encoding.UTF8);
        var customerTypes = JsonSerializer.Deserialize<List<CustomerType>>(customerTypeJson, enumOption);

        AddAudit(customerTypes);

        _customerTypes.AddRange(customerTypes);
        _dbContext.AddRange(customerTypes);
        await _dbContext.SaveChangesWithIdentityInsertAsync<CustomerType>();
    }

    private async Task SeedCustomers()
    {
        if (_dbContext.Customers.Any())
        {
            return;
        }

        var customerJson = File.ReadAllText(CustomerJsonPath, Encoding.UTF8);
        var customers = JsonSerializer.Deserialize<List<Customer>>(customerJson, enumOption);

        customers?.ForEach(AddAudit);

        foreach (var user in customers)
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, user.PasswordHash ?? "Abc123*");
        }


        _customers.AddRange(customers);
        _dbContext.AddRange(customers);
        await _dbContext.SaveChangesWithIdentityInsertAsync<Customer>();
    }

    private async Task SeedStaffs()
    {
        if (_dbContext.Staffs.Any())
        {
            return;
        }
        
        var staffJson = File.ReadAllText(StaffJsonPath, Encoding.UTF8);
        var staffs = JsonSerializer.Deserialize<List<Staff>>(staffJson, enumOption);

        staffs?.ForEach(AddAudit);

        foreach (var user in staffs)
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, user.PasswordHash ?? "Abc123*");
        }

        _staffs.AddRange(staffs);
        _dbContext.AddRange(staffs);
        await _dbContext.SaveChangesWithIdentityInsertAsync<Staff>();
    }


    private async Task SeedProductRelatedData()
    {
        await SeedAuthors();
        await SeedUnitMeasures();
        await SeedProductTypes();
        await SeedProductTypeAttributes();
        await SeedProductTypeAttributeMappings();
        await SeedProductTypeAttributeValues();
        await SeedProducts();
        await SeedProductTypeAttributeProductValues();
        await SeedProductImages();
        await SeedProductVariants();
        await SeedBookAuthors();
        await SeedOptions();
        await SeedOptionValues();
        await SeedProductVariantOptionValues();
        await SeedRatings();
    }

    private async Task SeedOrderRelatedData()
    {
        await SeedOrders();
        await SeedOrderLines();
    }

    private async Task SeedShoppingCartItemRelatedDataAsync()
    {
        if (_dbContext.ShoppingCartItems.Any())
        {
            return;
        }

        var shoppingCartItemJson = File.ReadAllText(ShoppingCartItemJsonPath, Encoding.UTF8);
        var shoppingCartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(shoppingCartItemJson);

        AddAudit(shoppingCartItems);

        _shoppingCartItems.AddRange(shoppingCartItems);

        _dbContext.AddRange(_shoppingCartItems);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ShoppingCartItem>();
    }



    #region Seed User related data
    private async Task SeedRoles()
    {
        if (_dbContext.Roles.Any())
        {
            return;
        }

        var roles = new List<IdentityRole<int>>
        {
            new() { Id = 1, Name = "Customer", NormalizedName = "CUSTOMER" },
            new() { Id = 2, Name = "Admin", NormalizedName = "ADMIN" },
            new() { Id = 3, Name = "SalesStaff", NormalizedName = "SALESSTAFF" },
            new() { Id = 4, Name = "CustomerCareStaff", NormalizedName = "CUSTOMERCARESTAFF" },
        };

        _dbContext.AddRange(roles);
        await _dbContext.SaveChangesWithIdentityInsertAsync<IdentityRole<int>>();
    }

    private async Task SeedUsers()
    {
        if (_dbContext.Users.Any())
        {
            return;
        }

        var userJson = File.ReadAllText(UserJsonPath, Encoding.UTF8);
        var users = JsonSerializer.Deserialize<List<User>>(userJson, enumOption);

        users?.ForEach(AddAudit);

        foreach (var user in users)
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, user.PasswordHash ?? "Abc123*");
        }

        _users.AddRange(users);
        _dbContext.AddRange(_users);
        await _dbContext.SaveChangesWithIdentityInsertAsync<User>();
    }

    private async Task SeedUserRoles()
    {
        if (_dbContext.UserRoles.Any())
        {
            return;
        }
        var adminRoleId = 2;
        var salesStaffRoleId = 3;
        var customerCareStaffRoleId = 4;
        var customerRoleId = 1;

        var userRoles = new List<IdentityUserRole<int>>();
        var addedStaffCount = 0;
        // we need 1 admin, 1 sales staff, others is customer care staffs frorm list _staffs, we'll take it from top to bottom
        foreach (var staff in _staffs)
        {
            if (addedStaffCount == 0)
            {
                userRoles.Add(new IdentityUserRole<int> { UserId = staff.Id, RoleId = adminRoleId });
                addedStaffCount++;
            }
            else if (addedStaffCount == 1)
            {
                userRoles.Add(new IdentityUserRole<int> { UserId = staff.Id, RoleId = salesStaffRoleId });
                addedStaffCount++;
            }
            else
            {
                userRoles.Add(new IdentityUserRole<int> { UserId = staff.Id, RoleId = customerCareStaffRoleId });
            }
        }

        // set role for _customers
        var customerRoles = _customers.Select(c => new IdentityUserRole<int> { UserId = c.Id, RoleId = customerRoleId });
        userRoles.AddRange(customerRoles);

        _dbContext.AddRange(userRoles);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedShippingAddress()
    {
        if (_dbContext.ShippingAddresses.Any())
        {
            return;
        }

        var shippingAddressJson = File.ReadAllText(ShippingAddressJsonPath, Encoding.UTF8);
        var shippingAddresses = JsonSerializer.Deserialize<List<ShippingAddress>>(shippingAddressJson);

        AddAudit(shippingAddresses);

        _shippingAddresses.AddRange(shippingAddresses);

        _dbContext.AddRange(_shippingAddresses);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ShippingAddress>();
    }
    #endregion Seed User related data


    #region Seed Product related data
    private async Task SeedAuthors()
    {
        if (_dbContext.Authors.Any())
        {
            return;
        }

        var authorJson = File.ReadAllText(AuthorJsonPath, Encoding.UTF8);
        var authors = JsonSerializer.Deserialize<List<Author>>(authorJson);

        AddAudit(authors);

        _authors.AddRange(authors);

        _dbContext.AddRange(_authors);
        await _dbContext.SaveChangesWithIdentityInsertAsync<Author>();
    }

    private async Task SeedUnitMeasures()
    {
        if (_dbContext.UnitMeasures.Any())
        {
            return;
        }

        var unitMeasureJson = File.ReadAllText(UnitMeasureJsonPath, Encoding.UTF8);
        var unitMeasures = JsonSerializer.Deserialize<List<UnitMeasure>>(unitMeasureJson);

        AddAudit(unitMeasures);

        _unitMeasures.AddRange(unitMeasures);

        _dbContext.AddRange(_unitMeasures);
        await _dbContext.SaveChangesWithIdentityInsertAsync<UnitMeasure>();
    }

    private async Task SeedProductTypes()
    {
        if (_dbContext.ProductTypes.Any())
        {
            return;
        }

        var productTypeJson = File.ReadAllText(ProductTypeJsonPath, Encoding.UTF8);
        var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeJson);

        productTypes?.ForEach(pt =>
        {
            pt.DisplayName = CapitalizeFirstLetter(pt.DisplayName);
        });

        AddAudit(productTypes);

        _productTypes.AddRange(productTypes);

        _dbContext.AddRange(_productTypes);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductType>();
    }

    private async Task SeedProductTypeAttributes()
    {
        if (_dbContext.ProductTypeAttributes.Any())
        {
            return;
        }

        var productTypeAttributeJson = File.ReadAllText(ProductTypeAttributeJsonPath, Encoding.UTF8);
        var productTypeAttributes = JsonSerializer.Deserialize<List<ProductTypeAttribute>>(productTypeAttributeJson);

        AddAudit(productTypeAttributes);

        _productTypeAttributes.AddRange(productTypeAttributes);

        _dbContext.AddRange(_productTypeAttributes);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductTypeAttribute>();
    }

    private async Task SeedProductTypeAttributeMappings()
    {
        if (_dbContext.ProductTypeAttributeMappings.Any())
        {
            return;
        }

        var productTypeAttributeMappingJson = File.ReadAllText(ProductTypeAttributeMappingJsonPath, Encoding.UTF8);
        var productTypeAttributeMappings = JsonSerializer.Deserialize<List<ProductTypeAttributeMapping>>(productTypeAttributeMappingJson);

        AddAudit(productTypeAttributeMappings);

        _productTypeAttributeMappings.AddRange(productTypeAttributeMappings);

        _dbContext.AddRange(_productTypeAttributeMappings);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductTypeAttributeMapping>();
    }

    private async Task SeedProductTypeAttributeValues()
    {
        if (_dbContext.ProductTypeAttributeValues.Any())
        {
            return;
        }

        var productTypeAttributeValueJson = File.ReadAllText(ProductTypeAttributeValueJsonPath, Encoding.UTF8);
        var productTypeAttributeValues = JsonSerializer.Deserialize<List<ProductTypeAttributeValue>>(productTypeAttributeValueJson);

        AddAudit(productTypeAttributeValues);

        _productTypeAttributeValues.AddRange(productTypeAttributeValues);

        _dbContext.AddRange(_productTypeAttributeValues);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductTypeAttributeValue>();
    }

    private async Task SeedProductTypeAttributeProductValues()
    {
        if (_dbContext.ProductTypeAttributeProductValues.Any())
        {
            return;
        }

        var productTypeAttributeProductValueJson = File.ReadAllText(ProductTypeAttributeProductValueJsonPath, Encoding.UTF8);
        var productTypeAttributeProductValues = JsonSerializer.Deserialize<List<ProductTypeAttributeProductValue>>(productTypeAttributeProductValueJson);

        AddAudit(productTypeAttributeProductValues);

        _productTypeAttributeProductValues.AddRange(productTypeAttributeProductValues);

        _dbContext.AddRange(_productTypeAttributeProductValues);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductTypeAttributeProductValue>();
    }

    private async Task SeedProducts()
    {
        if (_dbContext.Products.Any())
        {
            return;
        }

        var productJson = File.ReadAllText(ProductJsonPath, Encoding.UTF8);
        var products = JsonSerializer.Deserialize<List<Product>>(productJson);

        AddAudit(products);

        _products.AddRange(products);

        _dbContext.AddRange(_products);
        await _dbContext.SaveChangesWithIdentityInsertAsync<Product>();
    }

    private async Task SeedProductImages()
    {
        if (_dbContext.ProductImages.Any())
        {
            return;
        }

        var productImageJson = File.ReadAllText(ProductImageJsonPath, Encoding.UTF8);
        var productImages = JsonSerializer.Deserialize<List<ProductImage>>(productImageJson);

        AddAudit(productImages);

        _productImages.AddRange(productImages);

        _dbContext.AddRange(_productImages);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductImage>();
    }

    private async Task SeedProductVariants()
    {
        if (_dbContext.ProductVariants.Any())
        {
            return;
        }

        var productVariantJson = File.ReadAllText(ProductVariantJsonPath, Encoding.UTF8);
        var productVariants = JsonSerializer.Deserialize<List<ProductVariant>>(productVariantJson, enumOption);
        AddAudit(productVariants);

        _productVariants.AddRange(productVariants);

        _dbContext.AddRange(_productVariants);

        foreach (var variant in _productVariants)
        {
            variant.SkuValue = new SkuValue
            {
                Value = $"SKU{variant.Id:D5}"
            };

            switch (variant.Id)
            {
                case 1:
                    variant.Dimension = new Dimension { Height = 14m, Width = 20.5m, Length = 0.3m };
                    break;
                case 2:
                    variant.Dimension = new Dimension { Height = 20.5m, Width = 14.5m, Length = 0.5m };
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    variant.Dimension = new Dimension { Height = 22.0m, Width = 12.0m, Length = 0.5m };
                    break;
                case 9:
                    variant.Dimension = new Dimension { Height = 17.0m, Width = 9.0m, Length = 8m };
                    break;
                case 10:
                    variant.Dimension = new Dimension { Height = 16.0m, Width = 8.0m, Length = 7m };
                    break;
                case 11:
                    variant.Dimension = new Dimension { Height = 12.0m, Width = 8.0m, Length = 8m };
                    break;
                case 12:
                    variant.Dimension = new Dimension { Height = 9.0m, Width = 9.0m, Length = 8m };
                    break;
                case 13:
                    variant.Dimension = new Dimension { Height = 1.0m, Width = 9.0m, Length = 3.5m };
                    break;
                case 14:
                    variant.Dimension = new Dimension { Height = 7.0m, Width = 7.0m, Length = 1m };
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    variant.Dimension = new Dimension { Height = 20.5m, Width = 18.5m, Length = 0.4m };
                    break;
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                    variant.Dimension = new Dimension { Height = 17.6m, Width = 11.3m, Length = 1m };
                    break;
                case 26:
                    variant.Dimension = new Dimension { Height = 24.0m, Width = 17.0m, Length = 6.0m };
                    break;
                case 27:
                    variant.Dimension = new Dimension { Height = 24.0m, Width = 16.0m, Length = 2.1m };
                    break;
                case 28:
                    variant.Dimension = new Dimension { Height = 20.0m, Width = 14.5m, Length = 0.5m };
                    break;
                case 29:
                    variant.Dimension = new Dimension { Height = 24.0m, Width = 16.0m, Length = 1.4m };
                    break;
                case 30:
                    variant.Dimension = new Dimension { Height = 20.5m, Width = 14.5m, Length = 1.4m };
                    break;
                case 31:
                    variant.Dimension = new Dimension { Height = 20.0m, Width = 14.5m, Length = 1.6m };
                    break;
                case 32:
                    variant.Dimension = new Dimension { Height = 20.5m, Width = 14m, Length = 1m };
                    break;
                case 33:
                    variant.Dimension = new Dimension { Height = 20.5m, Width = 13.0m, Length = 2.5m };
                    break;
                case 34:
                case 35:
                case 36:
                    variant.Dimension = new Dimension { Height = 10.5m, Width = 5m, Length = 1.5m };
                    break;
                case 37:
                    variant.Dimension = new Dimension { Height = 5m, Width = 5m, Length = 2m };
                    break;
                case 38:
                    variant.Dimension = new Dimension { Height = 15m, Width = 2m, Length = 2m };
                    break;
                case 39:
                    variant.Dimension = new Dimension { Height = 6.5m, Width = 6.5m, Length = 2m };
                    break;
                case 40:
                    variant.Dimension = new Dimension { Height = 8m, Width = 8m, Length = 8m };
                    break;
            }
        }

        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductVariant>();
    }

    private async Task SeedBookAuthors()
    {
        if (_dbContext.BookAuthors.Any())
        {
            return;
        }

        var bookAuthorJson = File.ReadAllText(BookAuthorJsonPath, Encoding.UTF8);
        var bookAuthors = JsonSerializer.Deserialize<List<BookAuthor>>(bookAuthorJson);

        AddAudit(bookAuthors);

        _bookAuthors.AddRange(bookAuthors);

        _dbContext.AddRange(_bookAuthors);
        await _dbContext.SaveChangesWithIdentityInsertAsync<BookAuthor>();
    }

    private async Task SeedOptions()
    {
        if (_dbContext.ProductOptions.Any())
        {
            return;
        }

        var productOptionJson = File.ReadAllText(ProductOptionJsonPath, Encoding.UTF8);
        var productOptions = JsonSerializer.Deserialize<List<ProductOption>>(productOptionJson);

        AddAudit(productOptions);

        _productOptions.AddRange(productOptions);

        _dbContext.AddRange(_productOptions);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductOption>();
    }

    private async Task SeedOptionValues()
    {
        if (_dbContext.ProductOptionValues.Any())
        {
            return;
        }

        var productOptionValueJson = File.ReadAllText(ProductOptionValueJsonPath, Encoding.UTF8);
        var productOptionValues = JsonSerializer.Deserialize<List<ProductOptionValue>>(productOptionValueJson);

        AddAudit(productOptionValues);

        _productOptionValues.AddRange(productOptionValues);

        _dbContext.AddRange(_productOptionValues);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductOptionValue>();
    }

    private async Task SeedProductVariantOptionValues()
    {
        if (_dbContext.ProductVariantOptionValues.Any())
        {
            return;
        }

        var productVariantOptionValueJson = File.ReadAllText(ProductVariantOptionValueJsonPath, Encoding.UTF8);
        var productVariantOptionValues = JsonSerializer.Deserialize<List<ProductVariantOptionValue>>(productVariantOptionValueJson);

        AddAudit(productVariantOptionValues);

        _productVariantOptionValues.AddRange(productVariantOptionValues);

        _dbContext.AddRange(_productVariantOptionValues);
        await _dbContext.SaveChangesWithIdentityInsertAsync<ProductVariantOptionValue>();
    }

    private async Task SeedRatings()
    {
        if (_dbContext.Ratings.Any())
        {
            return;
        }

        var ratingJson = File.ReadAllText(RatingJsonPath, Encoding.UTF8);
        var ratings = JsonSerializer.Deserialize<List<Rating>>(ratingJson, enumOption);

        AddAudit(ratings);

        _ratings.AddRange(ratings);

        _dbContext.AddRange(_ratings);
        await _dbContext.SaveChangesWithIdentityInsertAsync<Rating>();

        var ratingLikeJson = File.ReadAllText(RatingLikeJsonPath, Encoding.UTF8);
        var ratingLikes = JsonSerializer.Deserialize<List<RatingLike>>(ratingLikeJson);

        AddAudit(ratingLikes);

        _ratingLikes.AddRange(ratingLikes);


        _dbContext.AddRange(_ratingLikes);
        await _dbContext.SaveChangesWithIdentityInsertAsync<RatingLike>();
    }

    #endregion Seed Product related data


    #region Seed Order related data
    private async Task SeedReferenceValues()
    {
        if (_dbContext.DeliveryMethods.Any())
        {
            return;
        }

        _deliveryMethods.AddRange([
            new DeliveryMethod { Id = 1, Name = "Giao hàng tiêu chuẩn", Description = "Giao hàng tiêu chuẩn", CreatorId = DEFAULT_ADMIN_ID, CreationTime = DateTimeOffset.Now, LastModifierId = DEFAULT_ADMIN_ID, LastModificationTime = DateTimeOffset.Now },
            new DeliveryMethod { Id = 2, Name = "Giao hàng nhanh", Description = "Giao hàng nhanh", CreatorId = DEFAULT_ADMIN_ID, CreationTime = DateTimeOffset.Now, LastModifierId = DEFAULT_ADMIN_ID, LastModificationTime = DateTimeOffset.Now },
        ]);

        _dbContext.AddRange(_deliveryMethods);
        await _dbContext.SaveChangesWithIdentityInsertAsync<DeliveryMethod>();

        if (_dbContext.PaymentMethods.Any())
        {
            return;
        }

        _paymentMethods.AddRange([
            new PaymentMethod { Id = 1, Name = "Thanh toán khi nhận hàng", Description = "Thanh toán khi nhận hàng", CreatorId = DEFAULT_ADMIN_ID, CreationTime = DateTimeOffset.Now, LastModifierId = DEFAULT_ADMIN_ID, LastModificationTime = DateTimeOffset.Now },
            new PaymentMethod { Id = 2, Name = "Thanh toán qua thẻ", Description = "Thanh toán qua thẻ", CreatorId = DEFAULT_ADMIN_ID, CreationTime = DateTimeOffset.Now, LastModifierId = DEFAULT_ADMIN_ID, LastModificationTime = DateTimeOffset.Now },
        ]);

        _dbContext.AddRange(_paymentMethods);
        await _dbContext.SaveChangesWithIdentityInsertAsync<PaymentMethod>();
    }

    private async Task SeedOrders()
    {
        if (_dbContext.Orders.Any())
        {
            return;
        }

        var orderJson = File.ReadAllText(OrderJsonPath, Encoding.UTF8);
        var orders = JsonSerializer.Deserialize<List<Order>>(orderJson, enumOption);
        //Debugger.Launch();
        AddAudit(orders);

        _orders.AddRange(orders);

        _dbContext.AddRange(_orders);
        await _dbContext.SaveChangesWithIdentityInsertAsync<Order>();
    }

    private async Task SeedOrderLines()
    {
        if (_dbContext.OrderLines.Any())
        {
            return;
        }

        var orderLineJson = File.ReadAllText(OrderLineJsonPath, Encoding.UTF8);
        var orderLines = JsonSerializer.Deserialize<List<OrderLine>>(orderLineJson);

        AddAudit(orderLines);

        _orderLines.AddRange(orderLines);

        _dbContext.AddRange(_orderLines);
        await _dbContext.SaveChangesWithIdentityInsertAsync<OrderLine>();
    }
    #endregion Seed Order related data


    #region Seed Discount related data
    private async Task SeedDiscountVouchers()
    {
        if (_dbContext.DiscountVouchers.Any())
        {
            return;
        }

        var discountVoucherJson = File.ReadAllText(DiscountVoucherJsonPath, Encoding.UTF8);
        var discountVouchers = JsonSerializer.Deserialize<List<DiscountVoucher>>(discountVoucherJson, enumOption);

        AddAudit(discountVouchers);

        _discountVouchers.AddRange(discountVouchers);

        _dbContext.AddRange(_discountVouchers);
        await _dbContext.SaveChangesWithIdentityInsertAsync<DiscountVoucher>();
    }

    private async Task SeedVoucherUsages()
    {
        if (_dbContext.VoucherUsages.Any())
        {
            return;
        }

        var voucherUsageJson = File.ReadAllText(VoucherUsageJsonPath, Encoding.UTF8);
        var voucherUsages = JsonSerializer.Deserialize<List<VoucherUsage>>(voucherUsageJson);

        AddAudit(voucherUsages);

        _voucherUsages.AddRange(voucherUsages);

        _dbContext.AddRange(_voucherUsages);
        await _dbContext.SaveChangesWithIdentityInsertAsync<VoucherUsage>();
    }

    #endregion Seed Discount related data

    private void AddAudit<TAuditableEntity>(List<TAuditableEntity> listItem) where TAuditableEntity : BaseAuditedEntity
    {
        foreach (var item in listItem)
        {
            // createWhen using Bogus 
            var createdWhen = _faker.Date.PastOffset(2);
            var lastEditedWhen = _faker.Date.BetweenOffset(createdWhen, createdWhen.AddYears(1)); // Generate a random date between createdWhen and now

            item.CreatorId = DEFAULT_ADMIN_ID;
            item.CreationTime = createdWhen;
            item.LastModifierId = DEFAULT_ADMIN_ID;
            item.LastModificationTime = lastEditedWhen;
        }
    }

    private void AddAudit(User user)
    {
        // createWhen using Bogus 

        var createdWhen = _faker.Date.PastOffset(2);
        var lastEditedWhen = _faker.Date.BetweenOffset(createdWhen, createdWhen.AddYears(1)); // Generate a random date between createdWhen and now

        user.CreatorId = DEFAULT_ADMIN_ID;
        user.CreationTime = createdWhen;
        user.LastModifierId = DEFAULT_ADMIN_ID;
        user.LastModificationTime = lastEditedWhen;

    }


    private string CapitalizeFirstLetter(string input)
    {
        return string.Join(" ", input.Split(' ').Select(s => s.First().ToString().ToUpper() + s[1..].ToLower()));
    }
}
