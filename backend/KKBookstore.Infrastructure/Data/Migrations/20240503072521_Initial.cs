using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPreferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Users_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Authors_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.DeliveryMethodId);
                    table.ForeignKey(
                        name: "FK_DeliveryMethods_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ProductTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ParentProductTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentProductTypeProductTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeCode);
                    table.ForeignKey(
                        name: "FK_ProductTypes_ProductTypes_ParentProductTypeProductTypeCode",
                        column: x => x.ParentProductTypeProductTypeCode,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefAddressTypes",
                columns: table => new
                {
                    RefAddressTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAddressTypes", x => x.RefAddressTypeCode);
                    table.ForeignKey(
                        name: "FK_RefAddressTypes_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasures",
                columns: table => new
                {
                    UnitMeasureCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasures", x => x.UnitMeasureCode);
                    table.ForeignKey(
                        name: "FK_UnitMeasures_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswords",
                columns: table => new
                {
                    UserPasswordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChangedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswords", x => x.UserPasswordId);
                    table.ForeignKey(
                        name: "FK_UserPasswords_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPasswords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commune = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AddressTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false),
                    AddressTypeRefAddressTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.ShippingAddressId);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_RefAddressTypes_AddressTypeRefAddressTypeCode",
                        column: x => x.AddressTypeRefAddressTypeCode,
                        principalTable: "RefAddressTypes",
                        principalColumn: "RefAddressTypeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAuthor = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UnitMeasureCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ProductTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeCode",
                        column: x => x.ProductTypeCode,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_UnitMeasureCode",
                        column: x => x.UnitMeasureCode,
                        principalTable: "UnitMeasures",
                        principalColumn: "UnitMeasureCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountVouchers",
                columns: table => new
                {
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    IsShippingVoucher = table.Column<bool>(type: "bit", nullable: false),
                    MinApplyQuantity = table.Column<int>(type: "int", nullable: false),
                    MaxApplyQuantity = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StartWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountVouchers", x => x.DiscountVoucherId);
                    table.ForeignKey(
                        name: "FK_DiscountVouchers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountVouchers_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Options_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Options_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skus",
                columns: table => new
                {
                    SkuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecommendedRetailPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidFrom = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ValidTo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DiscontinuedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skus", x => x.SkuId);
                    table.ForeignKey(
                        name: "FK_Skus_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skus_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WrittenBys",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WrittenWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WrittenBys", x => new { x.ProductId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_WrittenBys_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WrittenBys_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WrittenBys_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExpectedDeliveryWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryInstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false),
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: false),
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickingCompletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ConfirmedDeliveryWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ConfirmedReceivedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OrderWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "DeliveryMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DiscountVouchers_DiscountVoucherId",
                        column: x => x.DiscountVoucherId,
                        principalTable: "DiscountVouchers",
                        principalColumn: "DiscountVoucherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "ShippingAddresses",
                        principalColumn: "ShippingAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OptionValues",
                columns: table => new
                {
                    OptionValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionValues", x => x.OptionValueId);
                    table.ForeignKey(
                        name: "FK_OptionValues_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionValues_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    SkuImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LargeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.SkuImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImages_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceHistories",
                columns: table => new
                {
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    StartWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RecommendedRetailPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceHistories", x => new { x.SkuId, x.StartWhen });
                    table.ForeignKey(
                        name: "FK_ProductPriceHistories_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistories_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    OrderLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PickingCompletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.OrderLineId);
                    table.ForeignKey(
                        name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                        column: x => x.DiscountVoucherId,
                        principalTable: "DiscountVouchers",
                        principalColumn: "DiscountVoucherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkuOptionValues",
                columns: table => new
                {
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    OptionValueId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedByUserUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuOptionValues", x => new { x.SkuId, x.OptionId });
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_OptionValues_OptionValueId",
                        column: x => x.OptionValueId,
                        principalTable: "OptionValues",
                        principalColumn: "OptionValueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_Users_LastEditedByUserUserId",
                        column: x => x.LastEditedByUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_LastEditedByUserUserId",
                table: "Authors",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMethods_LastEditedByUserUserId",
                table: "DeliveryMethods",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_LastEditedByUserUserId",
                table: "DiscountVouchers",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_ProductId",
                table: "DiscountVouchers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_LastEditedByUserUserId",
                table: "Options",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId",
                table: "Options",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_LastEditedByUserUserId",
                table: "OptionValues",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_OptionId",
                table: "OptionValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_DiscountVoucherId",
                table: "OrderLines",
                column: "DiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_LastEditedByUserUserId",
                table: "OrderLines",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_SkuId",
                table: "OrderLines",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountVoucherId",
                table: "Orders",
                column: "DiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LastEditedByUserUserId",
                table: "Orders",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_LastEditedByUserUserId",
                table: "PaymentMethods",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_LastEditedByUserUserId",
                table: "ProductImages",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_SkuId",
                table: "ProductImages",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistories_LastEditedByUserUserId",
                table: "ProductPriceHistories",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LastEditedByUserUserId",
                table: "Products",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeCode",
                table: "Products",
                column: "ProductTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitMeasureCode",
                table: "Products",
                column: "UnitMeasureCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_LastEditedByUserUserId",
                table: "ProductTypes",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ParentProductTypeProductTypeCode",
                table: "ProductTypes",
                column: "ParentProductTypeProductTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_LastEditedByUserUserId",
                table: "Ratings",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SkuId",
                table: "Ratings",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefAddressTypes_LastEditedByUserUserId",
                table: "RefAddressTypes",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_AddressTypeRefAddressTypeCode",
                table: "ShippingAddresses",
                column: "AddressTypeRefAddressTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_LastEditedByUserUserId",
                table: "ShippingAddresses",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CustomerId",
                table: "ShoppingCartItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_LastEditedByUserUserId",
                table: "ShoppingCartItems",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_SkuId",
                table: "ShoppingCartItems",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_LastEditedByUserUserId",
                table: "SkuOptionValues",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_OptionId",
                table: "SkuOptionValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_OptionValueId",
                table: "SkuOptionValues",
                column: "OptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Skus_LastEditedByUserUserId",
                table: "Skus",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Skus_ProductId",
                table: "Skus",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasures_LastEditedByUserUserId",
                table: "UnitMeasures",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswords_LastEditedByUserUserId",
                table: "UserPasswords",
                column: "LastEditedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswords_UserId",
                table: "UserPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastEditedBy",
                table: "Users",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenBys_AuthorId",
                table: "WrittenBys",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenBys_LastEditedByUserUserId",
                table: "WrittenBys",
                column: "LastEditedByUserUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductPriceHistories");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "SkuOptionValues");

            migrationBuilder.DropTable(
                name: "UserPasswords");

            migrationBuilder.DropTable(
                name: "WrittenBys");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OptionValues");

            migrationBuilder.DropTable(
                name: "Skus");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "DiscountVouchers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "RefAddressTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "UnitMeasures");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
