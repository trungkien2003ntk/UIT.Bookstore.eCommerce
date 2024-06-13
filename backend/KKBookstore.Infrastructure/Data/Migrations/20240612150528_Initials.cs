using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserPreferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: true),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Authors_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authors_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.DeliveryMethodId);
                    table.ForeignKey(
                        name: "FK_DeliveryMethods_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryMethods_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountVouchers",
                columns: table => new
                {
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    VoucherType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaximumDiscountValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MinimumSpend = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UsageLimitPerUser = table.Column<int>(type: "int", nullable: true),
                    UsageLimitOverall = table.Column<int>(type: "int", nullable: false),
                    StartWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountVouchers", x => x.DiscountVoucherId);
                    table.ForeignKey(
                        name: "FK_DiscountVouchers_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountVouchers_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributes",
                columns: table => new
                {
                    ProductTypeAttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributes", x => x.ProductTypeAttributeId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributes_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ParentProductTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeId);
                    table.ForeignKey(
                        name: "FK_ProductTypes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypes_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypes_ProductTypes_ParentProductTypeId",
                        column: x => x.ParentProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefAddressTypes",
                columns: table => new
                {
                    RefAddressTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAddressTypes", x => x.RefAddressTypeId);
                    table.ForeignKey(
                        name: "FK_RefAddressTypes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefAddressTypes_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasures",
                columns: table => new
                {
                    UnitMeasureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasures", x => x.UnitMeasureId);
                    table.ForeignKey(
                        name: "FK_UnitMeasures_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitMeasures_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributeValues",
                columns: table => new
                {
                    ProductTypeAttributeValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductTypeAttributeId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributeValues", x => x.ProductTypeAttributeValueId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeValues_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
                        column: x => x.ProductTypeAttributeId,
                        principalTable: "ProductTypeAttributes",
                        principalColumn: "ProductTypeAttributeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountApplyToProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DiscountVoucherId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DiscountApplyToProductTypeId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_DiscountVouchers_DiscountVoucherId",
                        column: x => x.DiscountVoucherId,
                        principalTable: "DiscountVouchers",
                        principalColumn: "DiscountVoucherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_DiscountVouchers_DiscountVoucherId1",
                        column: x => x.DiscountVoucherId1,
                        principalTable: "DiscountVouchers",
                        principalColumn: "DiscountVoucherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributeMappings",
                columns: table => new
                {
                    ProductTypeAttributeMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributeMappings", x => x.ProductTypeAttributeMappingId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_ProductTypeAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductTypeAttributes",
                        principalColumn: "ProductTypeAttributeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Commune = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DetailAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AddressTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.ShippingAddressId);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_RefAddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "RefAddressTypes",
                        principalColumn: "RefAddressTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBook = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "UnitMeasureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryInstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false),
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: false),
                    ShippingFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceDiscountVoucherId = table.Column<int>(type: "int", nullable: true),
                    ShippingDiscountVoucherId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DueWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PaidWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExpectedDeliveryWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PickingCompletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ConfirmedDeliveryWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ConfirmedReceivedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "DeliveryMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DiscountVouchers_PriceDiscountVoucherId",
                        column: x => x.PriceDiscountVoucherId,
                        principalTable: "DiscountVouchers",
                        principalColumn: "DiscountVoucherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DiscountVouchers_ShippingDiscountVoucherId",
                        column: x => x.ShippingDiscountVoucherId,
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
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookAuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WrittenWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => x.BookAuthorId);
                    table.ForeignKey(
                        name: "FK_BookAuthors_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookAuthors_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    LargeImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImages_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    ProductOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsOptionWithImage = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.ProductOptionId);
                    table.ForeignKey(
                        name: "FK_ProductOptions_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOptions_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributeProductValues",
                columns: table => new
                {
                    ProductTypeAttributeProductValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeValueId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributeProductValues", x => x.ProductTypeAttributeProductValueId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValues_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValues_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValues_ProductTypeAttributeValues_AttributeValueId",
                        column: x => x.AttributeValueId,
                        principalTable: "ProductTypeAttributeValues",
                        principalColumn: "ProductTypeAttributeValueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skus",
                columns: table => new
                {
                    SkuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuValue_Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendedRetailPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ValidFrom = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ValidTo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DiscontinuedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: false),
                    Dimension_Length = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dimension_Width = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dimension_Height = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skus", x => x.SkuId);
                    table.ForeignKey(
                        name: "FK_Skus_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skus_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skus_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BankCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    BankTranNo = table.Column<string>(type: "varchar(260)", nullable: false),
                    CardType = table.Column<string>(type: "varchar(25)", nullable: false),
                    OrderInfo = table.Column<string>(type: "varchar(260)", nullable: false),
                    PayDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionNo = table.Column<int>(type: "int", nullable: false),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoucherUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UsedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VoucherUsageId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherUsages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherUsages_DiscountVouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "DiscountVouchers",
                        principalColumn: "DiscountVoucherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherUsages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptionValues",
                columns: table => new
                {
                    ProductOptionValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    LargeImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptionValues", x => x.ProductOptionValueId);
                    table.ForeignKey(
                        name: "FK_ProductOptionValues_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOptionValues_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOptionValues_ProductOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "ProductOptionId",
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
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PickingCompletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.OrderLineId);
                    table.ForeignKey(
                        name: "FK_OrderLines_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceHistories",
                columns: table => new
                {
                    ProductPriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    StartWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RecommendedRetailPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceHistories", x => x.ProductPriceHistoryId);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistories_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistories_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistories_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    ReportedCount = table.Column<int>(type: "int", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
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
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkuOptionValues",
                columns: table => new
                {
                    SkuOptionValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    OptionValueId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuOptionValues", x => x.SkuOptionValueId);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_ProductOptionValues_OptionValueId",
                        column: x => x.OptionValueId,
                        principalTable: "ProductOptionValues",
                        principalColumn: "ProductOptionValueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_ProductOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "ProductOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuOptionValues_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingLikes",
                columns: table => new
                {
                    RatingLikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Liked = table.Column<bool>(type: "bit", nullable: false),
                    LikedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingLikes", x => x.RatingLikeId);
                    table.ForeignKey(
                        name: "FK_RatingLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingLikes_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Customer", "CUSTOMER" },
                    { 2, null, "Admin", "ADMIN" },
                    { 3, null, "SalesStaff", "SALESSTAFF" },
                    { 4, null, "CustomerCareStaff", "CUSTOMERCARESTAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "CreatedWhen", "DateOfBirth", "DeletedWhen", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "IsActive", "IsAdmin", "IsDeleted", "IsEmployee", "LastEditedBy", "LastEditedWhen", "LastName", "LockoutEnabled", "LockoutEnd", "LoginType", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "UserPreferences" },
                values: new object[,]
                {
                    { 1, 0, "c9f837c2-b1d4-4297-c035-e92856db3g51", null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(7853), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "admin@example.com", true, "Admin", null, true, true, false, false, null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(7279), new TimeSpan(0, 7, 0, 0, 0)), "Admin", true, null, "Email", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEIKN6vV/0mlNmxO0VAUK1PUVBIETwq7pFFBVqSOhUdsCy/rACUH16k7pU77EMEchA==", "1234567890", true, "XJH4QW2N8GZRMLV5KF3Y7PCT9USB6ADO", "Active", false, "admin@example.com", null },
                    { 2, 0, "d3e126c2-b2d3-4398-b024-e97372db4f60", null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8428), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1985, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "salesstaff@example.com", true, "Sales", null, true, false, false, true, null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8424), new TimeSpan(0, 7, 0, 0, 0)), "Staff", true, null, "Email", "SALESSTAFF@EXAMPLE.COM", "SALESSTAFF@EXAMPLE.COM", "AQAAAAIAAYagAAAAEF3Dv5BYiFDwz0G+4bE/7sAA0A9cr2zC4R4hnOqIU9qp/pEq7T6bb3U7kN5R/ly3WQ==", "0987654321", true, "LV9Q8G6ZRN2KM4PSTXJY5W3FB7HOCD1U", "Active", false, "salesstaff@example.com", null },
                    { 3, 0, "e4f036c3-b2d3-4398-c034-e98272db4f61", null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8430), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1992, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "customercare@example.com", true, "Customer Care", null, true, false, false, true, null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8430), new TimeSpan(0, 7, 0, 0, 0)), "Staff", true, null, "Email", "CUSTOMERCARE@EXAMPLE.COM", "CUSTOMERCARE@EXAMPLE.COM", "AQAAAAIAAYagAAAAENO5MYaGeFBzdpz+hIsTQUJEo8Ndv+uDhpXUV1/R90LDOuYkqn9WiP9MLe3+bni2AA==", "1122334455", true, "P5YHXJLZMO9V3G2KTRN4S8QWCFUB7D1A", "Active", false, "customercare@example.com", null },
                    { 4, 0, "f4e026b2-b1d3-4198-a024-e97272da3f50", null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8432), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2003, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "trungkien2003ntk@gmail.com", true, "Trung Kiên", null, true, true, false, false, null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8431), new TimeSpan(0, 7, 0, 0, 0)), "Nguyễn", true, null, "Email", "TRUNGKIEN2003NTK@GMAIL.COM", "TRUNGKIEN2003NTK@GMAIL.COM", "AQAAAAIAAYagAAAAEHL+HWZWSNUhbj9S3uGIpVCkX3hQEk3TJa8hvR4NOURPDG8zOWxoTzvx1uahdyfP3Q==", "0866919340", true, "SENTMLQ2I6NCSBGSCLVXP4UOHGJF4G66", "Active", false, "trungkien2003ntk@gmail.com", null },
                    { 5, 0, "f0klsm1m-b1d6-4496-d036-e96378ef3g67", null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8433), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2003, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "truykichtk20031@gmail.com", true, "Tiêm Kích", null, true, true, false, false, null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8433), new TimeSpan(0, 7, 0, 0, 0)), "Trần", true, null, "Email", "TRUYKICHTK20031@GMAIL.COM", "TRUYKICHTK20031@GMAIL.COM", "AQAAAAIAAYagAAAAEP7Z79V6Kd7SAB0gZq308v6+CYs4Y8SAbup3pmohkAIKupRy42X2CRHUA3WT3ZJOkQ==", "0939199946", true, "MGJSHVQ2IJ28SKMVCLVXP4LAM8J2KJ5O", "Active", false, "truykichtk20031@gmail.com", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 1, 4 },
                    { 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Luis Sepúlveda", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Luis Sepúlveda" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Antoine De Saint-Exupéry", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Antoine De Saint-Exupéry" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả B R O Group", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "B R O Group" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Hồ Tâm", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Hồ Tâm" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Fujiko F Fujio", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Fujiko F Fujio" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Yukihiro Mitani", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Yukihiro Mitani" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Miyazaki Masaru", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Miyazaki Masaru" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Cao Minh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Cao Minh" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Miyazaki Masaru", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Miyazaki Masaru" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Diệp Hồng Vũ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Diệp Hồng Vũ" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả 	Từ Thính Phong", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "	Từ Thính Phong" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả 	J Krishnamurti", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "	J Krishnamurti" },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả Vãn Tình", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Vãn Tình" },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả 	Roxie Nafousi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "	Roxie Nafousi" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tác giả 	Minh Niệm", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "	Minh Niệm" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "DeliveryMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3569), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng tiêu chuẩn", false, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3596), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng tiêu chuẩn" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3598), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng nhanh", false, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3599), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng nhanh" }
                });

            migrationBuilder.InsertData(
                table: "DiscountVouchers",
                columns: new[] { "DiscountVoucherId", "Code", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "EndWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "MaximumDiscountValue", "MinimumSpend", "Name", "StartWhen", "UsageLimitOverall", "UsageLimitPerUser", "Value", "ValueType", "VoucherType" },
                values: new object[,]
                {
                    { 1, "VOUCHER15P500K1", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Nhận giảm giá 15%, tối đa ₫500k cho tổng giá trị đơn hàng của bạn.", new DateTimeOffset(new DateTime(2024, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 500000m, 100000m, "Giảm giá 15%, tối đa ₫500k", new DateTimeOffset(new DateTime(2024, 6, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 100, 1, 0.15m, 1, "Order" },
                    { 2, "VOUCHER50K1", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Nhận giảm giá ₫50k cho tổng giá trị đơn hàng của bạn.", new DateTimeOffset(new DateTime(2024, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 500000m, "Giảm giá ₫50k", new DateTimeOffset(new DateTime(2024, 6, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 200, 1, 50000m, 0, "Order" },
                    { 3, "SHIP10P100K1", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Nhận giảm giá 80%, tối đa ₫30k cho phí vận chuyển.", new DateTimeOffset(new DateTime(2024, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 30000m, 59000m, "Giảm giá 80%, tối đa ₫30k", new DateTimeOffset(new DateTime(2024, 6, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 150, 1, 0.8m, 1, "Shipping" },
                    { 4, "SHIP50K1", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Nhận giảm giá ₫15k cho phí vận chuyển.", new DateTimeOffset(new DateTime(2024, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 50000m, "Giảm giá ₫15k phí vận chuyển", new DateTimeOffset(new DateTime(2024, 6, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 300, 2, 15000m, 0, "Shipping" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5826), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán khi nhận hàng", false, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5838), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán khi nhận hàng" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5841), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán qua thẻ", false, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5842), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán qua thẻ" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributes",
                columns: new[] { "ProductTypeAttributeId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tác giả" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "NXB" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Năm xuất bản" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Ngôn ngữ" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Số trang" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Hình thức" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Thương hiệu" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Xuất xứ thương hiệu" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Nơi gia công & sản xuất" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Màu sắc" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Chất liệu" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Độ tuổi" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách viết bằng tiếng Việt", "Sách Tiếng Việt", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "sach-tieng-viet" },
                    { 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Foreign Books", "Foreign Books", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "foreign-books" },
                    { 47, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Văn Phòng Phẩm - Dụng Cụ Học Sinh", "Văn Phòng Phẩm - Dụng Cụ Học Sinh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "van-phong-pham-dung-cu-hoc-sinh" },
                    { 89, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đồ Chơi", "Đồ Chơi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "do-choi" },
                    { 94, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bách Hóa Tổng Hợp", "Bách Hóa Tổng Hợp", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "bach-hoa-tong-hop" },
                    { 107, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Lưu Niệm", "Lưu Niệm", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "luu-niem" }
                });

            migrationBuilder.InsertData(
                table: "RefAddressTypes",
                columns: new[] { "RefAddressTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7136), new TimeSpan(0, 7, 0, 0, 0)), null, "Nhà riêng", false, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7142), new TimeSpan(0, 7, 0, 0, 0)), "Nhà riêng" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7145), new TimeSpan(0, 7, 0, 0, 0)), null, "Văn phòng", false, 2, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7146), new TimeSpan(0, 7, 0, 0, 0)), "Văn phòng" }
                });

            migrationBuilder.InsertData(
                table: "UnitMeasures",
                columns: new[] { "UnitMeasureId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Dùng cho các loại sách", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "quyển" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Dùng cho các loại đồ dùng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "cái" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Dùng cho các loại đồ dùng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "chiếc" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeMappings",
                columns: new[] { "ProductTypeAttributeMappingId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductAttributeId", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 1 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 1 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 1 },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 1 },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 33 },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 33 },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 33 },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 33 },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 33 },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 33 },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 47 },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 47 },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, 47 },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, 47 },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, 47 },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, 89 },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 89 },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 89 },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, 89 },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, 89 },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, 89 },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 94 },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 94 },
                    { 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, 94 },
                    { 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, 94 },
                    { 28, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, 94 },
                    { 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 107 },
                    { 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 107 },
                    { 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, 107 },
                    { 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, 107 },
                    { 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, 107 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeValues",
                columns: new[] { "ProductTypeAttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductTypeAttributeId", "Value" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Hội Nhà Văn" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "144" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "Bìa Mềm" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2019" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2022" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "Tiếng Việt" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "102" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Báo Sinh Viên Việt Nam - Hoa Học Trò" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2023" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "100" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "WanLongDa" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, "Trung Quốc" },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "Trung Quốc" },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "Nâu" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "Bông" },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "Vàng" },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "Nhựa" },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Thanh niên" },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "48" },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Kim Đồng" },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "204" },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Thế giới" },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2021" },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "424" },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "344" },
                    { 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "280" },
                    { 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Hồng Đức" },
                    { 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "332" },
                    { 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "200" },
                    { 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Tổng hợp TPHCM" },
                    { 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "479" },
                    { 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "Morning Glory" },
                    { 34, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, "Hàn Quốc" },
                    { 35, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "Hàn Quốc" },
                    { 36, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "Nhựa, Kim Loại" },
                    { 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "OEM" },
                    { 38, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "Trong suốt" },
                    { 39, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "Việt Nam" },
                    { 40, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "Đỏ - Đen" },
                    { 41, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "Nano" },
                    { 42, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "Đen" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách dành cho thiếu nhi", "Thiếu Nhi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "thieu-nhi" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách giáo khoa và sách tham khảo", "Giáo Khoa - Tham Khảo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "giao-khoa-tham-khao" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các tác phẩm văn học", "Văn Học", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "van-hoc" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách về tâm lý và kỹ năng sống", "Tâm Lý - Kỹ Năng Sống", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "tam-ly-ky-nang-song" },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Truyện tranh Manga và Comic", "Manga - Comic", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "manga-comic" },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách học ngoại ngữ", "Sách Học Ngoại Ngữ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "sach-hoc-ngoai-ngu" },
                    { 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách về kinh tế", "Kinh Tế", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "kinh-te" },
                    { 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại từ điển", "Từ Điển", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "tu-dien" },
                    { 34, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Children's Books", "Children's Books", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 33, "childrens-books" },
                    { 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Dictionaries & Languages", "Dictionaries & Languages", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 33, "dictionaries-languages" },
                    { 41, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Computing", "Computing", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 33, "computing" },
                    { 45, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Romance", "Romance", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 33, "romance" },
                    { 46, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Humour", "Humour", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 33, "humour" },
                    { 48, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút - Viết", "Bút - Viết", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "but-viet" },
                    { 54, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sản Phẩm Về Giấy", "Sản Phẩm Về Giấy", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "san-pham-ve-giay" },
                    { 63, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Dụng Cụ Học Sinh", "Dụng Cụ Học Sinh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "dung-cu-hoc-sinh" },
                    { 74, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Dụng Cụ Vẽ", "Dụng Cụ Vẽ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "dung-cu-ve" },
                    { 78, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Dụng Cụ Văn Phòng", "Dụng Cụ Văn Phòng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "dung-cu-van-phong" },
                    { 83, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sản Phẩm VPP Khác", "Sản Phẩm Vpp Khác", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "san-pham-vpp-khac" },
                    { 88, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Thiệp", "Thiệp", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 47, "thiep" },
                    { 90, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Board Game", "Board Game", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 89, "board-game" },
                    { 91, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đồ Chơi Vận Động", "Đồ Chơi Vận Động", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 89, "do-choi-van-dong" },
                    { 92, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đồ Chơi Điều Khiển", "Đồ Chơi Điều Khiển", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 89, "do-choi-dieu-khien" },
                    { 93, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Robot - Siêu Nhân", "Robot - Siêu Nhân", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 89, "robot-sieu-nhan" },
                    { 95, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Nhà Cửa - Đời Sống", "Nhà Cửa - Đời Sống", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 94, "nha-cua-doi-song" },
                    { 99, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đồ Dùng Cá Nhân", "Đồ Dùng Cá Nhân", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 94, "do-dung-ca-nhan" },
                    { 103, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đồ Điện Gia Dụng", "Đồ Điện Gia Dụng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 94, "do-dien-gia-dung" },
                    { 108, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Móc Khóa", "Móc Khóa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 107, "moc-khoa" },
                    { 109, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Hộp Quà - Túi Quà", "Hộp Quà - Túi Quà", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 107, "hop-qua-tui-qua" },
                    { 110, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Kẹp Ảnh Gỗ", "Kẹp Ảnh Gỗ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 107, "kep-anh-go" },
                    { 111, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Khung Hình", "Khung Hình", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 107, "khung-hinh" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsActive", "IsBook", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductTypeId", "UnitMeasureId" },
                values: new object[] { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Gương Mini Cầm Tay 2 Mặt Hình Capybara - WanLongDa ZT185-2549A1-3với bề mặt hình tròn, sáng, rõ nét được bao quanh bởi lớp inox chắc chắn, có độ bền cao giúp giữ cho gương không bị vỡ khi rơi.\n\nSản phẩm là một trong những vật dụng không thể thiếu của các bạn gái hiện đại. Một chiếc gương nhỏ xinh sẽ giúp các cô gái tự tin hơn để thể hiện bản thân.\n\nNgoài ra, có thể sử dụng sản phẩm để làm quà tặng cho bạn bè, người thân trong các dịp đặc biệt.", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Gương Mini Cầm Tay 2 Mặt Hình Capybara  - WanLongDa ZT185-2549A1-3 - Hình Tròn Vàng", 107, 2 });

            migrationBuilder.InsertData(
                table: "ShippingAddresses",
                columns: new[] { "ShippingAddressId", "AddressTypeId", "Commune", "CreatedBy", "CreatedWhen", "DeletedWhen", "DetailAddress", "District", "IsDefault", "IsDeleted", "LastEditedBy", "LastEditedWhen", "PhoneNumber", "Province", "ReceiverName", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Xã Đức Hòa Hạ", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Số nhà 394", "Huyện Đức Hòa", false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "0866919340", "Long An", "Nguyễn Trung Kiên", 4 },
                    { 2, 2, "Phường Linh Trung", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Ktx khu A, ĐHQG", "Quận Thủ Đức", true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "0866919340", "Hồ Chí Minh", "Nguyễn Trung Kiên", 4 },
                    { 3, 1, "Xã Đức Hòa Thượng", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Chung cư 977", "Huyện Đức Hòa", true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "0866919340", "Long An", "Nguyễn Truy Kích", 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Comment", "ConfirmedDeliveryWhen", "ConfirmedReceivedWhen", "CreatedBy", "CreatedWhen", "CustomerId", "DeliveryInstruction", "DeliveryMethodId", "DueWhen", "ExpectedDeliveryWhen", "LastEditedBy", "LastEditedWhen", "OrderNumber", "OrderWhen", "PaidWhen", "PaymentMethodId", "PickingCompletedWhen", "PriceDiscountVoucherId", "ShippingAddressId", "ShippingDiscountVoucherId", "ShippingFee", "Status", "TaxRate" },
                values: new object[,]
                {
                    { 1, "Careful", new DateTimeOffset(new DateTime(2023, 2, 23, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, new DateTimeOffset(new DateTime(2023, 2, 26, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-2-21-92ab8", new DateTimeOffset(new DateTime(2023, 2, 21, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2023, 2, 22, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 1, 1, null, 30000m, "Delivered", 0m },
                    { 2, "Careful", new DateTimeOffset(new DateTime(2023, 8, 16, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 20, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2023, 8, 19, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-8-14-b3e0c", new DateTimeOffset(new DateTime(2023, 8, 14, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2023, 8, 15, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), null, 1, 4, 15000m, "Received", 0m },
                    { 3, "Careful", new DateTimeOffset(new DateTime(2022, 12, 18, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2022, 12, 21, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-12-16-fe174", new DateTimeOffset(new DateTime(2022, 12, 16, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2022, 12, 17, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 2, 1, null, 30000m, "Delivered", 0m },
                    { 4, "Careful", new DateTimeOffset(new DateTime(2024, 4, 15, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2024, 4, 18, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-4-13-2f823", new DateTimeOffset(new DateTime(2024, 4, 13, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), null, 1, 3, 6000m, "Delivered", 0m },
                    { 5, "Careful", new DateTimeOffset(new DateTime(2022, 12, 13, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, new DateTimeOffset(new DateTime(2022, 12, 16, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-12-11-ba604", new DateTimeOffset(new DateTime(2022, 12, 11, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Shipped", 0m },
                    { 6, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2022, 3, 4, 11, 53, 20, 558, DateTimeKind.Unspecified).AddTicks(7420), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-2-27-7b23f", new DateTimeOffset(new DateTime(2022, 2, 27, 11, 53, 20, 558, DateTimeKind.Unspecified).AddTicks(7420), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, null, 1, null, 30000m, "Pending", 0m },
                    { 7, "Careful", new DateTimeOffset(new DateTime(2023, 9, 27, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 10, 1, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2023, 9, 30, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-9-25-74459", new DateTimeOffset(new DateTime(2023, 9, 25, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2023, 9, 26, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Received", 0m },
                    { 8, "Careful", new DateTimeOffset(new DateTime(2024, 1, 28, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2024, 1, 31, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-1-26-19f7f", new DateTimeOffset(new DateTime(2024, 1, 26, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2024, 1, 27, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Delivered", 0m },
                    { 9, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2024, 2, 20, 9, 36, 58, 867, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-2-15-c75f4", new DateTimeOffset(new DateTime(2024, 2, 15, 9, 36, 58, 867, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, null, 1, null, 30000m, "Processing", 0m },
                    { 10, "Careful", new DateTimeOffset(new DateTime(2024, 3, 1, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2024, 3, 4, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-2-28-44c33", new DateTimeOffset(new DateTime(2024, 2, 28, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2024, 2, 29, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Delivered", 0m },
                    { 11, "Careful", new DateTimeOffset(new DateTime(2023, 7, 11, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 1, null, new DateTimeOffset(new DateTime(2023, 7, 14, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-7-9-6de5b", new DateTimeOffset(new DateTime(2023, 7, 9, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Shipped", 0m },
                    { 12, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, new DateTimeOffset(new DateTime(2024, 2, 23, 20, 41, 11, 372, DateTimeKind.Unspecified).AddTicks(2310), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-2-18-7040f", new DateTimeOffset(new DateTime(2024, 2, 18, 20, 41, 11, 372, DateTimeKind.Unspecified).AddTicks(2310), new TimeSpan(0, 7, 0, 0, 0)), null, 2, null, null, 1, null, 30000m, "Processing", 0m },
                    { 13, "Careful", new DateTimeOffset(new DateTime(2022, 6, 21, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, new DateTimeOffset(new DateTime(2022, 6, 24, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-6-19-05eb3", new DateTimeOffset(new DateTime(2022, 6, 19, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2022, 6, 20, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Shipped", 0m },
                    { 14, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2022, 2, 11, 20, 54, 18, 599, DateTimeKind.Unspecified).AddTicks(7440), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-2-6-d0d99", new DateTimeOffset(new DateTime(2022, 2, 6, 20, 54, 18, 599, DateTimeKind.Unspecified).AddTicks(7440), new TimeSpan(0, 7, 0, 0, 0)), null, 2, null, null, 1, null, 30000m, "Pending", 0m },
                    { 15, "Careful", new DateTimeOffset(new DateTime(2022, 6, 21, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2022, 6, 25, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2022, 6, 24, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-6-19-d7f83", new DateTimeOffset(new DateTime(2022, 6, 19, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2022, 6, 20, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Received", 0m },
                    { 16, "Careful", new DateTimeOffset(new DateTime(2024, 3, 29, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 2, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, new DateTimeOffset(new DateTime(2024, 4, 1, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-3-27-3d9be", new DateTimeOffset(new DateTime(2024, 3, 27, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2024, 3, 28, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Received", 0m },
                    { 17, "Careful", new DateTimeOffset(new DateTime(2022, 1, 14, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2022, 1, 18, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2022, 1, 17, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-1-12-aa39a", new DateTimeOffset(new DateTime(2022, 1, 12, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Received", 0m },
                    { 18, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, new DateTimeOffset(new DateTime(2023, 10, 18, 5, 51, 13, 33, DateTimeKind.Unspecified).AddTicks(9820), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-10-13-a2695", new DateTimeOffset(new DateTime(2023, 10, 13, 5, 51, 13, 33, DateTimeKind.Unspecified).AddTicks(9820), new TimeSpan(0, 7, 0, 0, 0)), null, 2, null, null, 1, null, 30000m, "Processing", 0m },
                    { 19, "Careful", new DateTimeOffset(new DateTime(2023, 9, 2, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2023, 9, 5, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-8-31-e96f3", new DateTimeOffset(new DateTime(2023, 8, 31, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2023, 9, 1, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Delivered", 0m },
                    { 20, "Careful", new DateTimeOffset(new DateTime(2023, 12, 4, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, new DateTimeOffset(new DateTime(2023, 12, 7, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-12-2-53b91", new DateTimeOffset(new DateTime(2023, 12, 2, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2023, 12, 3, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 30000m, "Shipped", 0m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "ProductId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498503-mau1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498503-mau1.jpg" },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498503-mau1-_1__4.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498503-mau1-_1__4.jpg" },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498503-mau1-_9_.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498503-mau1-_9_.jpg" },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498480-_2__4.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/6/9/6970601498480-_2__4.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeProductValues",
                columns: new[] { "ProductTypeAttributeProductValueId", "AttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductId" },
                values: new object[,]
                {
                    { 21, 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 22, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 23, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 24, 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 25, 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Truyện Thiếu Nhi", "Truyện Thiếu Nhi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 2, "truyen-thieu-nhi" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Kiến Thức - Kỹ Năng Sống Cho Trẻ", "Kiến Thức - Kỹ Năng Sống Cho Trẻ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 2, "kien-thuc-ky-nang-song-cho-tre" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tô Màu, Luyện Chữ", "Tô Màu, Luyện Chữ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 2, "to-mau-luyen-chu" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sách Tham Khảo", "Sách Tham Khảo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 6, "sach-tham-khao" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sách Giáo Khoa", "Sách Giáo Khoa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 6, "sach-giao-khoa" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tiểu Thuyết", "Tiểu Thuyết", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 9, "tieu-thuyet" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Truyện Ngắn - Tản Văn", "Truyện Ngắn - Tản Văn", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 9, "truyen-ngan-tan-van" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Light Novel", "Light Novel", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 9, "light-novel" },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Ngôn Tình", "Ngôn Tình", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 9, "ngon-tinh" },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Truyện Tranh", "Truyện Tranh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 9, "truyen-tranh" },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Kỹ Năng Sống", "Kỹ Năng Sống", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 15, "ky-nang-song" },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tâm Lý", "Tâm Lý", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 15, "tam-ly" },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sách Cho Tuổi Mới Lớn", "Sách Cho Tuổi Mới Lớn", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 15, "sach-cho-tuoi-moi-lon" },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Manga", "Manga", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 19, "manga" },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Comic - Truyện Tranh", "Comic - Truyện Tranh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 19, "comic-truyen-tranh" },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tiếng Anh", "Tiếng Anh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 22, "tieng-anh" },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tiếng Hoa", "Tiếng Hoa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 22, "tieng-hoa" },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tiếng Nhật", "Tiếng Nhật", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 22, "tieng-nhat" },
                    { 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tiếng Hàn", "Tiếng Hàn", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 22, "tieng-han" },
                    { 28, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Quản Trị - Lãnh Đạo", "Quản Trị - Lãnh Đạo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 27, "quan-tri-lanh-dao" },
                    { 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Marketing - Bán Hàng", "Marketing - Bán Hàng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 27, "marketing-ban-hang" },
                    { 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Khởi Nghiệp - Làm Giàu", "Khởi Nghiệp - Làm Giàu", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 27, "khoi-nghiep-lam-giau" },
                    { 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Chứng Khoán - Bất Động Sản - Đầu Tư", "Chứng Khoán - Bất Động Sản - Đầu Tư", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 27, "chung-khoan-bat-dong-san-dau-tu" },
                    { 35, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Picture & Activity Books", "Picture & Activity Books", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 34, "picture-activity-books" },
                    { 36, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Education", "Education", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 34, "education" },
                    { 38, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Thesauri", "Thesauri", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 37, "thesauri" },
                    { 39, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Dictionaries", "Dictionaries", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 37, "dictionaries" },
                    { 40, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Linguistics", "Linguistics", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 37, "linguistics" },
                    { 42, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Computer Science", "Computer Science", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 41, "computer-science" },
                    { 43, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Databases", "Databases", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 41, "databases" },
                    { 44, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Computer Programming - Software Development", "Computer Programming - Software Development", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 41, "computer-programming-software-development" },
                    { 49, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Chì - Ruột Bút Chì", "Bút Chì - Ruột Bút Chì", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 48, "but-chi-ruot-but-chi" },
                    { 50, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Bi - Ruột Bút Bi", "Bút Bi - Ruột Bút Bi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 48, "but-bi-ruot-but-bi" },
                    { 51, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Lông", "Bút Lông", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 48, "but-long" },
                    { 52, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Dạ Quang", "Bút Dạ Quang", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 48, "but-da-quang" },
                    { 53, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Mực - Bút Máy", "Bút Mực - Bút Máy", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 48, "but-muc-but-may" },
                    { 55, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sổ Các Loại", "Sổ Các Loại", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "so-cac-loai" },
                    { 56, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tập - Vở", "Tập - Vở", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "tap-vo" },
                    { 57, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Giấy Note", "Giấy Note", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "giay-note" },
                    { 58, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sticker", "Sticker", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "sticker" },
                    { 59, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Nhãn Vở - Nhãn Tên", "Nhãn Vở - Nhãn Tên", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "nhan-vo-nhan-ten" },
                    { 60, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Giấy Kiểm Tra", "Giấy Kiểm Tra", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "giay-kiem-tra" },
                    { 61, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Giấy Photo", "Giấy Photo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "giay-photo" },
                    { 62, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Giấy Bìa", "Giấy Bìa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 54, "giay-bia" },
                    { 64, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Gôm - Tẩy", "Gôm - Tẩy", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "gom-tay" },
                    { 65, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Gọt Bút Chì", "Gọt Bút Chì", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "got-but-chi" },
                    { 66, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bóp Viết - Hộp Bút", "Bóp Viết - Hộp Bút", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "bop-viet-hop-but" },
                    { 67, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Cặp - Ba Lô", "Cặp - Ba Lô", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "cap-ba-lo" },
                    { 68, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Thước", "Thước", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "thuoc" },
                    { 69, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Compa", "Compa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "compa" },
                    { 70, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bảng Viết - Bông Lau Bảng", "Bảng Viết - Bông Lau Bảng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "bang-viet-bong-lau-bang" },
                    { 71, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bao Tập - Bao Sách", "Bao Tập - Bao Sách", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "bao-tap-bao-sach" },
                    { 72, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Mực", "Mực", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "muc" },
                    { 73, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Phấn - Hộp Đựng Phấn", "Phấn - Hộp Đựng Phấn", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 63, "phan-hop-dung-phan" },
                    { 75, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Vẽ", "Bút Vẽ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 74, "but-ve" },
                    { 76, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Màu Vẽ", "Màu Vẽ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 74, "mau-ve" },
                    { 77, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Tập Vẽ - Giấy Vẽ", "Tập Vẽ - Giấy Vẽ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 74, "tap-ve-giay-ve" },
                    { 79, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bìa - File Hồ Sơ", "Bìa - File Hồ Sơ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 78, "bia-file-ho-so" },
                    { 80, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đồ Bấm Kim - Kim Bấm - Gỡ Kim - Kim Kẹp", "Đồ Bấm Kim - Kim Bấm - Gỡ Kim - Kim Kẹp", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 78, "do-bam-kim-kim-bam-go-kim-kim-kep" },
                    { 81, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Kẹp Giấy - Kẹp Bướm - Kẹp Các Loại", "Kẹp Giấy - Kẹp Bướm - Kẹp Các Loại", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 78, "kep-giay-kep-buom-kep-cac-loai" },
                    { 82, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Cắm Bút", "Cắm Bút", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 78, "cam-but" },
                    { 84, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Dao Rọc Giấy - Lưỡi Dao Rọc Giấy - Kéo", "Dao Rọc Giấy - Lưỡi Dao Rọc Giấy - Kéo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 83, "dao-roc-giay-luoi-dao-roc-giay-keo" },
                    { 85, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bút Xóa Nước - Xóa Kéo", "Bút Xóa Nước - Xóa Kéo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 83, "but-xoa-nuoc-xoa-keo" },
                    { 86, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Băng Keo - Cắt Băng Keo", "Băng Keo - Cắt Băng Keo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 83, "bang-keo-cat-bang-keo" },
                    { 87, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Keo Khô - Hồ Dán", "Keo Khô - Hồ Dán", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 83, "keo-kho-ho-dan" },
                    { 96, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Ly, Cốc, Bình Nước", "Ly, Cốc, Bình Nước", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 95, "ly-coc-binh-nuoc" },
                    { 97, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Sửa Chữa Nhà Cửa", "Sửa Chữa Nhà Cửa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 95, "sua-chua-nha-cua" },
                    { 98, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Bảo Vệ Nhà Cửa", "Bảo Vệ Nhà Cửa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 95, "bao-ve-nha-cua" },
                    { 100, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Túi - Ví Thời Trang", "Túi - Ví Thời Trang", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 99, "tui-vi-thoi-trang" },
                    { 101, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Phụ Kiện Tóc", "Phụ Kiện Tóc", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 99, "phu-kien-toc" },
                    { 102, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Phụ Kiện Thời Trang", "Phụ Kiện Thời Trang", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 99, "phu-kien-thoi-trang" },
                    { 104, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Pin & Dụng Cụ Sạc Pin", "Pin & Dụng Cụ Sạc Pin", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 103, "pin-dung-cu-sac-pin" },
                    { 105, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Đèn Bàn", "Đèn Bàn", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 103, "den-ban" },
                    { 106, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại Ổ Cắm Điện", "Ổ Cắm Điện", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 103, "o-cam-dien" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsActive", "IsBook", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductTypeId", "UnitMeasureId" },
                values: new object[] { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sản phẩm được làm từ chất liệu an toàn cho người sử dụng.\n\nMóc khóa có kích thước vừa vặn đáng yêu, có thể cho vào balo hoặc túi xách dễ dàng.\n\nSản phẩm được thiết kế hình Capybara nổi tiếng vô cùng dễ thương.\n\nMóc khóa giúp bạn bảo quản và cất giữ chìa khóa dễ dàng, không lo rơi rớt, thất lạc.\n\nBộ móc khóa không chỉ giữ chìa khóa mà còn có thể trang trí, tạo không khí tươi mới hơn.", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Móc Khóa Capybara", 108, 2 });

            migrationBuilder.InsertData(
                table: "Skus",
                columns: new[] { "SkuId", "Dimension_Height", "Dimension_Length", "Dimension_Width", "Barcode", "Comment", "CreatedBy", "CreatedWhen", "DeletedWhen", "DiscontinuedWhen", "IsActive", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "Quantity", "RecommendedRetailPrice", "Status", "Tags", "TaxRate", "UnitPrice", "ValidFrom", "ValidTo", "Weight", "SkuValue_Value" },
                values: new object[] { 14, 7.0m, 1m, 7.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 20, 55000m, "InStock", " ", 0m, 49500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 53, "SKU00014" });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "ProductId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310117.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310117.jpg" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310117-_4_.jpg?_gl=1*1ba1ol*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ0MTkuNDkuMC4xNTUxNzk5MTUz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310117-_4_.jpg?_gl=1*1ba1ol*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ0MTkuNDkuMC4xNTUxNzk5MTUz" },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240311008-_1_.jpg?_gl=1*1ba1ol*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ0MTkuNDkuMC4xNTUxNzk5MTUz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240311008-_1_.jpg?_gl=1*1ba1ol*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ0MTkuNDkuMC4xNTUxNzk5MTUz" },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240318007-_2_.jpg?_gl=1*1ba1ol*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ0MTkuNDkuMC4xNTUxNzk5MTUz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240318007-_2_.jpg?_gl=1*1ba1ol*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ0MTkuNDkuMC4xNTUxNzk5MTUz" },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310124-_3_.jpg?_gl=1*7g85ts*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ1MzEuNDcuMC4xNTUxNzk5MTUz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310124-_3_.jpg?_gl=1*7g85ts*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQ1MzEuNDcuMC4xNTUxNzk5MTUz" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ProductOptionId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "IsOptionWithImage", "LastEditedBy", "LastEditedWhen", "Name", "ProductId" },
                values: new object[] { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, true, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Phân loại", 4 });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeProductValues",
                columns: new[] { "ProductTypeAttributeProductValueId", "AttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductId" },
                values: new object[,]
                {
                    { 16, 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 17, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 18, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 19, 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 20, 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsActive", "IsBook", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductTypeId", "UnitMeasureId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Cô hải âu Kengah bị nhấn chìm trong váng dầu – thứ chất thải nguy hiểm mà những con người xấu xa bí mật đổ ra đại dương. Với nỗ lực đầy tuyệt vọng, cô bay vào bến cảng Hamburg và rơi xuống ban công của con mèo mun, to đùng, mập ú Zorba. Trong phút cuối cuộc đời, cô sinh ra một quả trứng và con mèo mun hứa với cô sẽ thực hiện ba lời hứa chừng như không tưởng với loài mèo:\n\nKhông ăn quả trứng.\nChăm sóc cho tới khi nó nở.\nDạy cho con hải âu bay.\n\nLời hứa của một con mèo cũng là trách nhiệm của toàn bộ mèo trên bến cảng, bởi vậy bè bạn của Zorba bao gồm ngài mèo Đại Tá đầy uy tín, mèo Secretario nhanh nhảu, mèo Einstein uyên bác, mèo Bốn Biển đầy kinh nghiệm đã chung sức giúp nó hoàn thành trách nhiệm. Tuy nhiên, việc chăm sóc, dạy dỗ một con hải âu đâu phải chuyện đùa, sẽ có hàng trăm rắc rối nảy sinh và cần có những kế hoạch đầy linh hoạt được bàn bạc kỹ càng…\n\nChuyện con mèo dạy hải âu bay là kiệt tác dành cho thiếu nhi của nhà văn Chi Lê nổi tiếng Luis Sepúlveda – tác giả của cuốn Lão già mê đọc truyện tình đã bán được 18 triệu bản khắp thế giới. Tác phẩm không chỉ là một câu chuyện ấm áp, trong sáng, dễ thương về loài vật mà còn chuyển tải thông điệp về trách nhiệm với môi trường, về sự sẻ chia và yêu thương cũng như ý nghĩa của những nỗ lực – “Chỉ những kẻ dám mới có thể bay”.\n\nCuốn sách mở đầu cho mùa hè với minh họa dễ thương, hài hước là món quà dành cho mọi trẻ em và người lớn.\n\nMã hàng	8935235222113\nTên Nhà Cung Cấp	Nhã Nam\nTác giả	Luis Sepúlveda\nNgười Dịch	Phương Huyên\nNXB	NXB Hội Nhà Văn\nNăm XB	2019\nTrọng lượng (gr)	150\nKích Thước Bao Bì	14 x 20.5\nSố trang	144\nHình thức	Bìa Mềm\nSản phẩm hiển thị trong	\nTuyển Tập Sách Tác Giả Luis Sepúlveda\nVNPAY\nSản phẩm bán chạy nhất	Top 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nCô hải âu Kengah bị nhấn chìm trong váng dầu – thứ chất thải nguy hiểm mà những con người xấu xa bí mật đổ ra đại dương. Với nỗ lực đầy tuyệt vọng, cô bay vào bến cảng Hamburg và rơi xuống ban công của con mèo mun, to đùng, mập ú Zorba. Trong phút cuối cuộc đời, cô sinh ra một quả trứng và con mèo mun hứa với cô sẽ thực hiện ba lời hứa chừng như không tưởng với loài mèo:\n\nKhông ăn quả trứng.\nChăm sóc cho tới khi nó nở.\nDạy cho con hải âu bay.\n\nLời hứa của một con mèo cũng là trách nhiệm của toàn bộ mèo trên bến cảng, bởi vậy bè bạn của Zorba bao gồm ngài mèo Đại Tá đầy uy tín, mèo Secretario nhanh nhảu, mèo Einstein uyên bác, mèo Bốn Biển đầy kinh nghiệm đã chung sức giúp nó hoàn thành trách nhiệm. Tuy nhiên, việc chăm sóc, dạy dỗ một con hải âu đâu phải chuyện đùa, sẽ có hàng trăm rắc rối nảy sinh và cần có những kế hoạch đầy linh hoạt được bàn bạc kỹ càng…\n\nChuyện con mèo dạy hải âu bay là kiệt tác dành cho thiếu nhi của nhà văn Chi Lê nổi tiếng Luis Sepúlveda – tác giả của cuốn Lão già mê đọc truyện tình đã bán được 18 triệu bản khắp thế giới. Tác phẩm không chỉ là một câu chuyện ấm áp, trong sáng, dễ thương về loài vật mà còn chuyển tải thông điệp về trách nhiệm với môi trường, về sự sẻ chia và yêu thương cũng như ý nghĩa của những nỗ lực – “Chỉ những kẻ dám mới có thể bay”.\n\nCuốn sách mở đầu cho mùa hè với minh họa dễ thương, hài hước là món quà dành cho mọi trẻ em và người lớn.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Chuyện Con Mèo Dạy Hải Âu Bay (Tái Bản 2019)", 3, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "LẦN ĐẦU TIÊN CÓ BẢN QUYỀN CHÍNH THỨC TẠI VIỆT NAM\n\nHoàng tử bé được viết ở New York trong những ngày tác giả sống lưu vong và được xuất bản lần đầu tiên tại New York vào năm 1943, rồi đến năm 1946 mới được xuất bản tại Pháp. Không nghi ngờ gì, đây là tác phẩm nổi tiếng nhất, được đọc nhiều nhất và cũng được yêu mến nhất của Saint-Exupéry. Cuốn sách được bình chọn là tác phẩm hay nhất thế kỉ 20 ở Pháp, đồng thời cũng là cuốn sách Pháp được dịch và được đọc nhiều nhất trên thế giới. Với 250 ngôn ngữ dịch khác nhau kể cả phương ngữ cùng hơn 200 triệu bản in trên toàn thế giới, Hoàng tử bé được coi là một trong những tác phẩm bán chạy nhất của nhân loại. \n\nỞ Việt Nam, Hoàng tử bé được dịch và xuất bản khá sớm. Từ năm 1966 đã có  đồng thời hai bản dịch: Hoàng tử bé của Bùi Giáng do An Tiêm xuất bản và Cậu hoàng con của Trần Thiện Đạo do Khai Trí xuất bản. Từ đó đến nay đã có thêm nhiều bản dịch Hoàng tử bé mới của các dịch giả khác nhau. Bản dịch Hoàng tử bé lần này, xuất bản nhân dịp kỷ niệm 70 năm Hoàng tử bé ra đời, cũng là ấn bản đầu tiên được Gallimard chính thức chuyển nhượng bản quyền tại Việt Nam, hy vọng sẽ góp phần làm phong phú thêm việc dịch và tiếp nhận tác phẩm quan trọng này với các thế hệ độc giả. \n\nTôi cứ sống cô độc như vậy, chẳng có một ai để chuyện trò thật sự, cho tới một lần gặp nạn ở sa mạc Sahara cách đây sáu năm. Có thứ gì đó bị vỡ trong động cơ máy bay. Và vì ở bên cạnh chẳng có thợ máy cũng như hành khách nào nên một mình tôi sẽ phải cố mà sửa cho bằng được vụ hỏng hóc nan giải này. Với tôi đó thật là một việc sống còn. Tôi chỉ có vừa đủ nước để uống trong tám ngày.\n\nThế là đêm đầu tiên tôi ngủ trên cát, cách mọi chốn có người ở cả nghìn dặm xa. Tôi cô đơn hơn cả một kẻ đắm tàu sống sót trên bè giữa đại dương. Thế nên các bạn cứ tưởng tượng tôi đã ngạc nhiên làm sao, khi ánh ngày vừa rạng, thì một giọng nói nhỏ nhẹ lạ lùng đã đánh thức tôi. Giọng ấy nói:\n\n“Ông làm ơn… vẽ cho tôi một con cừu!”\n\n- Trích \"Hoàng tử bé\"\n\nNhận định\n\n“Đây là một câu chuyện tự nó đã rất đáng yêu, ẩn giấu một triết lý quá đỗi nhẹ nhàng và thi vị.”\n\n- The New York Times\n\nMã hàng	8935235221734\nTên Nhà Cung Cấp	Nhã Nam\nTác giả	Antoine De Saint-Exupéry\nNgười Dịch	Trác Phong\nNXB	Hội Nhà Văn\nNăm XB	2022\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	120\nKích Thước Bao Bì	23 x 15 cm\nSố trang	102\nHình thức	Bìa Mềm\nSản phẩm hiển thị trong	\nTop sách được phiên dịch nhiều nhất\nVNPAY\nSản phẩm bán chạy nhất	Top 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nLẦN ĐẦU TIÊN CÓ BẢN QUYỀN CHÍNH THỨC TẠI VIỆT NAM\n\nHoàng tử bé được viết ở New York trong những ngày tác giả sống lưu vong và được xuất bản lần đầu tiên tại New York vào năm 1943, rồi đến năm 1946 mới được xuất bản tại Pháp. Không nghi ngờ gì, đây là tác phẩm nổi tiếng nhất, được đọc nhiều nhất và cũng được yêu mến nhất của Saint-Exupéry. Cuốn sách được bình chọn là tác phẩm hay nhất thế kỉ 20 ở Pháp, đồng thời cũng là cuốn sách Pháp được dịch và được đọc nhiều nhất trên thế giới. Với 250 ngôn ngữ dịch khác nhau kể cả phương ngữ cùng hơn 200 triệu bản in trên toàn thế giới, Hoàng tử bé được coi là một trong những tác phẩm bán chạy nhất của nhân loại. \n\nỞ Việt Nam, Hoàng tử bé được dịch và xuất bản khá sớm. Từ năm 1966 đã có  đồng thời hai bản dịch: Hoàng tử bé của Bùi Giáng do An Tiêm xuất bản và Cậu hoàng con của Trần Thiện Đạo do Khai Trí xuất bản. Từ đó đến nay đã có thêm nhiều bản dịch Hoàng tử bé mới của các dịch giả khác nhau. Bản dịch Hoàng tử bé lần này, xuất bản nhân dịp kỷ niệm 70 năm Hoàng tử bé ra đời, cũng là ấn bản đầu tiên được Gallimard chính thức chuyển nhượng bản quyền tại Việt Nam, hy vọng sẽ góp phần làm phong phú thêm việc dịch và tiếp nhận tác phẩm quan trọng này với các thế hệ độc giả. \n\nTôi cứ sống cô độc như vậy, chẳng có một ai để chuyện trò thật sự, cho tới một lần gặp nạn ở sa mạc Sahara cách đây sáu năm. Có thứ gì đó bị vỡ trong động cơ máy bay. Và vì ở bên cạnh chẳng có thợ máy cũng như hành khách nào nên một mình tôi sẽ phải cố mà sửa cho bằng được vụ hỏng hóc nan giải này. Với tôi đó thật là một việc sống còn. Tôi chỉ có vừa đủ nước để uống trong tám ngày.\n\nThế là đêm đầu tiên tôi ngủ trên cát, cách mọi chốn có người ở cả nghìn dặm xa. Tôi cô đơn hơn cả một kẻ đắm tàu sống sót trên bè giữa đại dương. Thế nên các bạn cứ tưởng tượng tôi đã ngạc nhiên làm sao, khi ánh ngày vừa rạng, thì một giọng nói nhỏ nhẹ lạ lùng đã đánh thức tôi. Giọng ấy nói:\n\n“Ông làm ơn… vẽ cho tôi một con cừu!”\n\n- Trích \"Hoàng tử bé\"\n\nNhận định\n\n“Đây là một câu chuyện tự nó đã rất đáng yêu, ẩn giấu một triết lý quá đỗi nhẹ nhàng và thi vị.”\n\n- The New York Times", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Hoàng Tử Bé (Tái Bản)", 3, 1 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "LỚP HỌC MẬT NGỮ\n\nPHIÊN BẢN NHỎ MÀ CÓ VÕ!\n\nChào năm mới, Lớp Học Mật Ngữ sẽ trình làng một phiên bản siêu xịn sò: Nhẹ ví hơn, phát hành đều đặn hơn và gay cấn hơn, cùng khám phá nhé!\n\nThì ra mùa Xuân hoa nở là vì Lớp Học Mật Ngữ MỚI\n\nNếu trước đây cứ phải “cồn cào” đợi Lớp Học Mật Ngữ phát hành 2-3 tháng một lần, thì từ sau Tết Quý Mão, bạn sẽ được gặp bộ truyện tranh best seller này HẰNG THÁNG.\n\nPhải rồi, bạn không đọc nhầm đâu! Cứ mỗi giữa tháng chúng ta sẽ có cuộc hẹn với các cung hoàng đạo nhé! Số này là ngày 15/2, thì đến 15/3 chúng ta lại tay bắt mặt mừng nè.\n\nPhiên bản này cũng rất đáng yêu vì giá bìa nhẹ xiều chỉ 25 “con cá”, quá đã!\n\nÚ òa! Trở thành chiến thần “tay hòm chìa khóa”\n\nMời bạn đến với truyện tranh mới Hiệp hội viêm màng túi.\n\nĐang buồn vì bị bố mẹ cắt giảm tiền tiêu vặt, ấy vậy mà Song Tử nam còn vào lớp thông báo rằng “Ngân Hà luxury rồi”, giờ cái gì cũng lên giá, cũng đắt hết trơn... Biết bao giờ mới đủ tiền mua truyện, đồ chơi đây? Trong khi cả lớp “lan toả năng lượng” u ám thì Kim Ngưu nam vẫn bình thản ăn đùi gà chiên nước mắm. Các bạn đánh giá món này bây giờ rất đắt! Sao Kim Ngưu có tiền mua ăn sáng? Thế là mọi người nhờ vả Kim Ngưu chia sẻ bí quyết giàu sang, giúp các bạn vượt qua cuộc “khủng hoảng kinh tế” này...\n\nGét gô đến thế giới game siêu trí tuệ\n\nTập này không chỉ được học cách tiết kiệm và quản lý tiền, bạn còn có thể chơi game vui tẹt ga với hội cạ hoặc cả nhà tại “khu vui chơi” mang chủ đề của truyện. Nào là truy tìm từ vựng tiếng Anh, phân công nhiệm vụ cho heo đất rồi tham dự cuộc đua rinh Lớp Học Mật Ngữ nữa.\n\nChưa kể tập này còn được lì xì kèm phụ kiện mới siêu hot: Standee Lớp Học Mật Ngữ.\n\nMã hàng	8938507003304\nNhà Cung Cấp	BÁO TIỀN PHONG\nTác giả	B R O Group\nNXB	Báo Sinh Viên Việt Nam - Hoa Học Trò\nNăm XB	2023\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	80\nKích Thước Bao Bì	22 x 15 x 0.5 cm\nSố trang	100\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Truyện Tranh Thiếu Nhi bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nLỚP HỌC MẬT NGỮ\n\nPHIÊN BẢN NHỎ MÀ CÓ VÕ!\n\nChào năm mới, Lớp Học Mật Ngữ sẽ trình làng một phiên bản siêu xịn sò: Nhẹ ví hơn, phát hành đều đặn hơn và gay cấn hơn, cùng khám phá nhé!\n\nThì ra mùa Xuân hoa nở là vì Lớp Học Mật Ngữ MỚI\n\nNếu trước đây cứ phải “cồn cào” đợi Lớp Học Mật Ngữ phát hành 2-3 tháng một lần, thì từ sau Tết Quý Mão, bạn sẽ được gặp bộ truyện tranh best seller này HẰNG THÁNG.\n\nPhải rồi, bạn không đọc nhầm đâu! Cứ mỗi giữa tháng chúng ta sẽ có cuộc hẹn với các cung hoàng đạo nhé! Số này là ngày 15/2, thì đến 15/3 chúng ta lại tay bắt mặt mừng nè.\n\nPhiên bản này cũng rất đáng yêu vì giá bìa nhẹ xiều chỉ 25 “con cá”, quá đã!\n\nÚ òa! Trở thành chiến thần “tay hòm chìa khóa”\n\nMời bạn đến với truyện tranh mới Hiệp hội viêm màng túi.\n\nĐang buồn vì bị bố mẹ cắt giảm tiền tiêu vặt, ấy vậy mà Song Tử nam còn vào lớp thông báo rằng “Ngân Hà luxury rồi”, giờ cái gì cũng lên giá, cũng đắt hết trơn... Biết bao giờ mới đủ tiền mua truyện, đồ chơi đây? Trong khi cả lớp “lan toả năng lượng” u ám thì Kim Ngưu nam vẫn bình thản ăn đùi gà chiên nước mắm. Các bạn đánh giá món này bây giờ rất đắt! Sao Kim Ngưu có tiền mua ăn sáng? Thế là mọi người nhờ vả Kim Ngưu chia sẻ bí quyết giàu sang, giúp các bạn vượt qua cuộc “khủng hoảng kinh tế” này...\n\nGét gô đến thế giới game siêu trí tuệ\n\nTập này không chỉ được học cách tiết kiệm và quản lý tiền, bạn còn có thể chơi game vui tẹt ga với hội cạ hoặc cả nhà tại “khu vui chơi” mang chủ đề của truyện. Nào là truy tìm từ vựng tiếng Anh, phân công nhiệm vụ cho heo đất rồi tham dự cuộc đua rinh Lớp Học Mật Ngữ nữa.\n\nChưa kể tập này còn được lì xì kèm phụ kiện mới siêu hot: Standee Lớp Học Mật Ngữ.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Lớp Học Mật Ngữ Phiên Bản Mới", 3, 1 },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Trong xã hội ngày nay, việc trang bị cho trẻ em những kiến thức an toàn để tự bảo vệ bản thân là điều hết sức cần thiết. Thấu hiểu điều này, bộ sách Giáo dục đầu đời cho trẻ - Những bài học tự bảo vệ bản thân đã ra đời nhằm giúp các em nhận thức rõ về cơ thể mình, bồi dưỡng những quan niệm đúng đắn về giới tính, đồng thời nắm được những nguy cơ tiềm tàng trong cuộc sống, tránh xa mọi hiểm nguy.\n\nVới nội dung được lồng ghép tinh tế kèm tranh minh họa sinh động, chắc chắn các em sẽ tiếp thu những điều bổ ích trong sách rất nhanh và dễ dàng áp dụng vào cuộc sống đấy! Hãy mở sách và khám phá nhé!\n\nMã hàng	8935212362450\nTên Nhà Cung Cấp	Đinh Tị\nTác giả	Hồ Tâm\nNgười Dịch	Nguyễn Đức Vịnh\nNXB	Thanh Niên\nNăm XB	2023\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	133\nKích Thước Bao Bì	20.5 x 18.5 x 0.4 cm\nSố trang	48\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Sách Tranh Kỹ Năng Sống Cho Trẻ bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nGiáo Dục Đầu Đời Cho Trẻ - Những Bài Học Tự Bảo Vệ Bản Thân - Bố Mẹ Luôn Yêu Con\n\nTrong xã hội ngày nay, việc trang bị cho trẻ em những kiến thức an toàn để tự bảo vệ bản thân là điều hết sức cần thiết. Thấu hiểu điều này, bộ sách Giáo dục đầu đời cho trẻ - Những bài học tự bảo vệ bản thân đã ra đời nhằm giúp các em nhận thức rõ về cơ thể mình, bồi dưỡng những quan niệm đúng đắn về giới tính, đồng thời nắm được những nguy cơ tiềm tàng trong cuộc sống, tránh xa mọi hiểm nguy.\n\nVới nội dung được lồng ghép tinh tế kèm tranh minh họa sinh động, chắc chắn các em sẽ tiếp thu những điều bổ ích trong sách rất nhanh và dễ dàng áp dụng vào cuộc sống đấy! Hãy mở sách và khám phá nhé!", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Giáo Dục Đầu Đời Cho Trẻ - Những Bài Học Tự Bảo Vệ Bản Thân", 4, 1 },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Vua của thế giới yêu ma gửi thư tuyên chiến đến toàn nhân loại!\n\nĐội quân Doraemon quyết tâm xâm nhập vào tận hang ổ của kẻ địch!", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Đội Quân Doraemon Đặc Biệt", 20, 1 },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Thiên Tài Bên Trái, Kẻ Điên Bên Phải\n\nNẾU MỘT NGÀY ANH THẤY TÔI ĐIÊN, THỰC RA CHÍNH LÀ ANH ĐIÊN ĐẤY!\n\nHỡi những con người đang oằn mình trong cuộc sống, bạn biết gì về thế giới của mình? Là vô vàn thứ lý thuyết được các bậc vĩ nhân kiểm chứng, là luật lệ, là cả nghìn thứ sự thật bọc trong cái lốt hiển nhiên, hay những triết lý cứng nhắc của cuộc đời?\n\nLại đây, vượt qua thứ nhận thức tẻ nhạt bị đóng kín bằng con mắt trần gian, khai mở toàn bộ suy nghĩ, để dòng máu trong bạn sục sôi trước những điều kỳ vĩ, phá vỡ mọi quy tắc. Thế giới sẽ gọi bạn là kẻ điên, nhưng vậy thì có sao? Ranh giới duy nhất giữa kẻ điên và thiên tài chẳng qua là một sợi chỉ mỏng manh: Thiên tài chứng minh được thế giới của mình, còn kẻ điên chưa kịp làm điều đó. Chọn trở thành một kẻ điên để vẫy vùng giữa nhân gian loạn thế hay khóa hết chúng lại, sống mãi một cuộc đời bình thường khiến bạn cảm thấy hạnh phúc hơn?\n\nThiên tài bên trái, kẻ điên bên phải là cuốn sách dành cho những người điên rồ, những kẻ gây rối, những người chống đối, những mảnh ghép hình tròn trong những ô vuông không vừa vặn… những người nhìn mọi thứ khác biệt, không quan tâm đến quy tắc. Bạn có thể đồng ý, có thể phản đối, có thể vinh danh hay lăng mạ họ, nhưng điều duy nhất bạn không thể làm là phủ nhận sự tồn tại của họ. Đó là những người luôn tạo ra sự thay đổi trong khi hầu hết con người chỉ sống rập khuôn như một cái máy. Đa số đều nghĩ họ thật điên rồ nhưng nếu nhìn ở góc khác, ta lại thấy họ thiên tài. Bởi chỉ những người đủ điên nghĩ rằng họ có thể thay đổi thế giới mới là những người làm được điều đó. \n\nChào mừng đến với thế giới của những kẻ điên.\n\nMã hàng	8936186546815\nTên Nhà Cung Cấp	AZ Việt Nam\nTác giả	Cao Minh\nNgười Dịch	Thu Hương\nNXB	NXB Thế Giới\nNăm XB	2021\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	450\nKích Thước Bao Bì	24 x 16 x 2.1 cm\nSố trang	424\nHình thức	Bìa Mềm\nSản phẩm hiển thị trong	\nAZ Việt Nam\nĐồ Chơi Cho Bé - Giá Cực Tốt\nTủ Sách Tâm Lý Kỹ Năng\nSản phẩm bán chạy nhất	Top 100 sản phẩm Tâm lý bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nThiên Tài Bên Trái, Kẻ Điên Bên Phải\n\nNẾU MỘT NGÀY ANH THẤY TÔI ĐIÊN, THỰC RA CHÍNH LÀ ANH ĐIÊN ĐẤY!\n\nHỡi những con người đang oằn mình trong cuộc sống, bạn biết gì về thế giới của mình? Là vô vàn thứ lý thuyết được các bậc vĩ nhân kiểm chứng, là luật lệ, là cả nghìn thứ sự thật bọc trong cái lốt hiển nhiên, hay những triết lý cứng nhắc của cuộc đời?\n\nLại đây, vượt qua thứ nhận thức tẻ nhạt bị đóng kín bằng con mắt trần gian, khai mở toàn bộ suy nghĩ, để dòng máu trong bạn sục sôi trước những điều kỳ vĩ, phá vỡ mọi quy tắc. Thế giới sẽ gọi bạn là kẻ điên, nhưng vậy thì có sao? Ranh giới duy nhất giữa kẻ điên và thiên tài chẳng qua là một sợi chỉ mỏng manh: Thiên tài chứng minh được thế giới của mình, còn kẻ điên chưa kịp làm điều đó. Chọn trở thành một kẻ điên để vẫy vùng giữa nhân gian loạn thế hay khóa hết chúng lại, sống mãi một cuộc đời bình thường khiến bạn cảm thấy hạnh phúc hơn?\n\nThiên tài bên trái, kẻ điên bên phải là cuốn sách dành cho những người điên rồ, những kẻ gây rối, những người chống đối, những mảnh ghép hình tròn trong những ô vuông không vừa vặn… những người nhìn mọi thứ khác biệt, không quan tâm đến quy tắc. Bạn có thể đồng ý, có thể phản đối, có thể vinh danh hay lăng mạ họ, nhưng điều duy nhất bạn không thể làm là phủ nhận sự tồn tại của họ. Đó là những người luôn tạo ra sự thay đổi trong khi hầu hết con người chỉ sống rập khuôn như một cái máy. Đa số đều nghĩ họ thật điên rồ nhưng nếu nhìn ở góc khác, ta lại thấy họ thiên tài. Bởi chỉ những người đủ điên nghĩ rằng họ có thể thay đổi thế giới mới là những người làm được điều đó. \n\nChào mừng đến với thế giới của những kẻ điên.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Thiên Tài Bên Trái, Kẻ Điên Bên Phải (Tái Bản 2021)", 17, 1 },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Thuật Thao Túng\n\nBạn có muốn giành phần thắng cuối cùng trong các cuộc tranh luận?\n\nBạn có muốn dẹp đi bộ mặt kiêu ngạo của các đồng nghiệp xung quanh mình?\n\nBạn có muốn chứng minh rằng bạn đã đúng về mọi thứ?\n\nĐây là quyển sách chứa đựng đáp án mà bạn mong muốn. Thuật thao túng sẽ giúp bạn thuần thục các kỹ năng thuộc bộ môn “nghệ thuật” làm chủ cảm xúc, làm chủ vận mệnh, điều chỉnh tâm lý và đạt được thứ bạn muốn một cách tinh vi: thao túng tâm lý người đối diện, khiến họ hành động theo hướng ta mong đợi. Không những vậy, quyển sách còn giúp bạn nhìn nhận lại về định nghĩa thao túng, những tốt-xấu ẩn giấu đằng sau và giải đáp vấn đề đạo đức con người mà bạn luôn trăn trở khi thực hiện những hành vi này. Bật mí, con người khi vừa sinh ra đã làm một thao tác thao túng tâm lý người khác rồi đấy!\n\nCó thể bạn chưa biết, bạn đã và đang thao túng người khác hoặc bị người khác thao túng thông qua cử chỉ ngôn hành mỗi ngày, như-một-trò-đùa.\n\nCó thể bạn chưa biết, nạn nhân bị thao túng chưa chắc đã rơi vào tình thế bất lợi, nhưng rơi vào tình thế bất lợi chắc chắn đã bị thao túng.\n\nCó thể bạn chưa biết, người có đạo đức chắc chắn không thao túng người khác, nhưng kẻ thao túng người khác chưa chắc đã vô đạo đức.\n\nVới 10 kỹ năng và 37 thủ thuật, Thuật thao túng sẽ giúp bạn nhận ra và thoát khỏi những suy nghĩ xấu xa nơi tiềm thức của chính mình, đồng thời vạch trần góc tối ẩn giấu sau mỗi câu nói của đối phương, đưa những chiêu trò thao túng ấy ra ánh sáng để mọi người không lần nữa rơi vào cạm bẫy. Hơn cả, quyển sách này sẽ dẫn lối bạn trở thành một “nghệ nhân” thao túng có đạo đức.\n\nVề tác giả\n\nTác giả người Đức Wladislaw Jachtchenko - diễn giả hàng đầu châu Âu, người sáng lập Học viện Argumentorik giảng dạy về giao tiếp - dạy bạn cách giao tiếp phù hợp để đạt được điều bạn muốn.\n\nMã hàng	8935325009006\nTên Nhà Cung Cấp	AZ Việt Nam\nTác giả	Wladislaw Jachtchenko\nNgười Dịch	Vũ Trung Phi Yến\nNXB	Thế Giới\nNăm XB	2022\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	350\nKích Thước Bao Bì	20 x 14.5 cm\nSố trang	344\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Tâm lý bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nThuật Thao Túng\n\nBạn có muốn giành phần thắng cuối cùng trong các cuộc tranh luận?\n\nBạn có muốn dẹp đi bộ mặt kiêu ngạo của các đồng nghiệp xung quanh mình?\n\nBạn có muốn chứng minh rằng bạn đã đúng về mọi thứ?\n\nĐây là quyển sách chứa đựng đáp án mà bạn mong muốn. Thuật thao túng sẽ giúp bạn thuần thục các kỹ năng thuộc bộ môn “nghệ thuật” làm chủ cảm xúc, làm chủ vận mệnh, điều chỉnh tâm lý và đạt được thứ bạn muốn một cách tinh vi: thao túng tâm lý người đối diện, khiến họ hành động theo hướng ta mong đợi. Không những vậy, quyển sách còn giúp bạn nhìn nhận lại về định nghĩa thao túng, những tốt-xấu ẩn giấu đằng sau và giải đáp vấn đề đạo đức con người mà bạn luôn trăn trở khi thực hiện những hành vi này. Bật mí, con người khi vừa sinh ra đã làm một thao tác thao túng tâm lý người khác rồi đấy!\n\nCó thể bạn chưa biết, bạn đã và đang thao túng người khác hoặc bị người khác thao túng thông qua cử chỉ ngôn hành mỗi ngày, như-một-trò-đùa.\n\nCó thể bạn chưa biết, nạn nhân bị thao túng chưa chắc đã rơi vào tình thế bất lợi, nhưng rơi vào tình thế bất lợi chắc chắn đã bị thao túng.\n\nCó thể bạn chưa biết, người có đạo đức chắc chắn không thao túng người khác, nhưng kẻ thao túng người khác chưa chắc đã vô đạo đức.\n\nVới 10 kỹ năng và 37 thủ thuật, Thuật thao túng sẽ giúp bạn nhận ra và thoát khỏi những suy nghĩ xấu xa nơi tiềm thức của chính mình, đồng thời vạch trần góc tối ẩn giấu sau mỗi câu nói của đối phương, đưa những chiêu trò thao túng ấy ra ánh sáng để mọi người không lần nữa rơi vào cạm bẫy. Hơn cả, quyển sách này sẽ dẫn lối bạn trở thành một “nghệ nhân” thao túng có đạo đức.\n\nVề tác giả\n\nTác giả người Đức Wladislaw Jachtchenko - diễn giả hàng đầu châu Âu, người sáng lập Học viện Argumentorik giảng dạy về giao tiếp - dạy bạn cách giao tiếp phù hợp để đạt được điều bạn muốn.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Thuật Thao Túng - Góc Tối Ẩn Sau Mỗi Câu Nói", 17, 1 },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "TÂM LÝ HỌC TỘI PHẠM - PHÁC HỌA CHÂN DUNG KẺ PHẠM TỘI\n\nTội phạm, nhất là những vụ án mạng, luôn là một chủ đề thu hút sự quan tâm của công chúng, khơi gợi sự hiếu kỳ của bất cứ ai. Một khi đã bắt đầu theo dõi vụ án, hẳn bạn không thể không quan tâm tới kết cục, đặc biệt là cách thức và động cơ của kẻ sát nhân, từ những vụ án phạm vi hẹp cho đến những vụ án làm rúng động cả thế giới.\n\nLấy 36 vụ án CÓ THẬT kinh điển nhất trong hồ sơ tội phạm của FBI, “Tâm lý học tội phạm - phác họa chân dung kẻ phạm tội” mang đến cái nhìn toàn cảnh của các chuyên gia về chân dung tâm lý tội phạm. Trả lời cho câu hỏi: Làm thế nào phân tích được tâm lý và hành vi tội phạm, từ đó khôi phục sự thật thông qua các manh mối, từ hiện trường vụ án, thời gian, dấu tích,… để tìm ra kẻ sát nhân thực sự. \n\nĐằng sau máu và nước mắt là các câu chuyện rợn tóc gáy về tội ác, góc khuất xã hội và những màn đấu trí đầy gay cấn giữa điều tra viên và kẻ phạm tội. Trong số đó có những con quỷ ăn thịt người; những cô gái xinh đẹp nhưng xảo quyệt; và cả cách trả thù đầy man rợ của các nhà khoa học,… Một số đã sa vào lưới pháp luật ngay khi chúng vừa ra tay, nhưng cũng có những kẻ cứ vậy ngủ yên hơn hai mươi năm. \n\nBằng giọng văn sắc bén, “Tâm lý học tội phạm - phác họa chân dung kẻ phạm tội” hứa hẹn dẫn dắt người đọc đi qua các cung bậc cảm xúc từ tò mò, ngạc nhiên đến sợ hãi, hoang mang tận cùng. Chúng ta sẽ lần tìm về quá khứ để từng bước gỡ những nút thắt chưa được giải, khiến ta \"ngạt thở\" đọc tới tận trang cuối cùng. \n\nHy vọng cuốn sách sẽ giúp bạn có cái nhìn sâu sắc, rõ ràng hơn về bộ môn tâm lý học tội phạm và có thể rèn luyện thêm sự tư duy, nhạy bén.\n\nMã hàng	8935325001819\nTên Nhà Cung Cấp	AZ Việt Nam\nTác giả	Diệp Hồng Vũ\nNgười Dịch	Đỗ Ái Nhi\nNXB	NXB Thanh Niên\nNăm XB	2021\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	300\nKích Thước Bao Bì	24 x 16 cm x 1.4\nSố trang	280\nHình thức	Bìa Mềm\nSản phẩm hiển thị trong	\nAZ Việt Nam\nĐồ Chơi Cho Bé - Giá Cực Tốt\nTủ Sách Tâm Lý Kỹ Năng\nSản phẩm bán chạy nhất	Top 100 sản phẩm Tâm lý bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nTÂM LÝ HỌC TỘI PHẠM - PHÁC HỌA CHÂN DUNG KẺ PHẠM TỘI\n\nTội phạm, nhất là những vụ án mạng, luôn là một chủ đề thu hút sự quan tâm của công chúng, khơi gợi sự hiếu kỳ của bất cứ ai. Một khi đã bắt đầu theo dõi vụ án, hẳn bạn không thể không quan tâm tới kết cục, đặc biệt là cách thức và động cơ của kẻ sát nhân, từ những vụ án phạm vi hẹp cho đến những vụ án làm rúng động cả thế giới.\n\nLấy 36 vụ án CÓ THẬT kinh điển nhất trong hồ sơ tội phạm của FBI, “Tâm lý học tội phạm - phác họa chân dung kẻ phạm tội” mang đến cái nhìn toàn cảnh của các chuyên gia về chân dung tâm lý tội phạm. Trả lời cho câu hỏi: Làm thế nào phân tích được tâm lý và hành vi tội phạm, từ đó khôi phục sự thật thông qua các manh mối, từ hiện trường vụ án, thời gian, dấu tích,… để tìm ra kẻ sát nhân thực sự. \n\nĐằng sau máu và nước mắt là các câu chuyện rợn tóc gáy về tội ác, góc khuất xã hội và những màn đấu trí đầy gay cấn giữa điều tra viên và kẻ phạm tội. Trong số đó có những con quỷ ăn thịt người; những cô gái xinh đẹp nhưng xảo quyệt; và cả cách trả thù đầy man rợ của các nhà khoa học,… Một số đã sa vào lưới pháp luật ngay khi chúng vừa ra tay, nhưng cũng có những kẻ cứ vậy ngủ yên hơn hai mươi năm. \n\nBằng giọng văn sắc bén, “Tâm lý học tội phạm - phác họa chân dung kẻ phạm tội” hứa hẹn dẫn dắt người đọc đi qua các cung bậc cảm xúc từ tò mò, ngạc nhiên đến sợ hãi, hoang mang tận cùng. Chúng ta sẽ lần tìm về quá khứ để từng bước gỡ những nút thắt chưa được giải, khiến ta \"ngạt thở\" đọc tới tận trang cuối cùng. \n\nHy vọng cuốn sách sẽ giúp bạn có cái nhìn sâu sắc, rõ ràng hơn về bộ môn tâm lý học tội phạm và có thể rèn luyện thêm sự tư duy, nhạy bén.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tâm Lý Học Tội Phạm - Phác Họa Chân Dung Kẻ Phạm Tội", 17, 1 },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Định Luật Murphy – Mọi Bí Mật Tâm Lý Thao Túng Cuộc Đời Bạn\n\nNếu một điều tồi tệ có thể xảy ra, nó sẽ xảy ra!\n\nKhi một món đồ quan trọng bị rơi, nó có xu hướng lăn tới dưới ngăn tủ nặng nhất.\n\nKhi hai tay bạn cầm đầy đồ đạc, mũi bạn bắt đầu ngứa. \n\nKhi bạn sợ gặp một người nào đó ở bên ngoài, bạn luôn vô tình gặp phải người đó.\n\n…\n\nCó phải hằng ngày bạn hay gặp những chuyện dở khóc dở cười tương tự như vậy? Những hiện tượng này đều có thể giải thích bằng một khái niệm tâm lý học thú vị: Định luật Murphy. Nó nhắc nhở chúng ta rằng việc xấu luôn có “cơ may” cao hơn và sai lầm luôn là một phần của thế giới này. Dù cho mỗi chúng ta đều cố gắng hết sức để tránh khỏi sai lầm, nhưng trên thực tế đó là điều bất khả thi.\n\nTuy nhiên, định luật Murphy mang tính cảnh tỉnh và dẫn dắt rất lớn trong cuộc sống hằng ngày. Cuốn sách này giới thiệu đến với bạn đọc về kiến thức cơ bản, hiện tượng thường gặp cùng với các hiệu ứng tâm lý học biểu hiện trong ý thức cá nhân, tính cạnh tranh, quan hệ xã hội,… của định luật Murphy thông qua góc nhìn thực tiễn. Từ đó giúp bạn đọc hiểu được bản chất con người, bản chất xã hội rồi ứng dụng nó vào giải quyết các vấn đề gặp phải trong cuộc sống.\n\nMã hàng	8935325009600\nTên Nhà Cung Cấp	AZ Việt Nam\nTác giả	Từ Thính Phong\nNgười Dịch	Hà Dung\nNXB	Thế Giới\nNăm XB	2022\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	300\nKích Thước Bao Bì	20.5 x 14 cm\nSố trang	280\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Tâm lý bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nĐịnh Luật Murphy – Mọi Bí Mật Tâm Lý Thao Túng Cuộc Đời Bạn\n\nNếu một điều tồi tệ có thể xảy ra, nó sẽ xảy ra!\n\nKhi một món đồ quan trọng bị rơi, nó có xu hướng lăn tới dưới ngăn tủ nặng nhất.\n\nKhi hai tay bạn cầm đầy đồ đạc, mũi bạn bắt đầu ngứa. \n\nKhi bạn sợ gặp một người nào đó ở bên ngoài, bạn luôn vô tình gặp phải người đó.\n\n…\n\nCó phải hằng ngày bạn hay gặp những chuyện dở khóc dở cười tương tự như vậy? Những hiện tượng này đều có thể giải thích bằng một khái niệm tâm lý học thú vị: Định luật Murphy. Nó nhắc nhở chúng ta rằng việc xấu luôn có “cơ may” cao hơn và sai lầm luôn là một phần của thế giới này. Dù cho mỗi chúng ta đều cố gắng hết sức để tránh khỏi sai lầm, nhưng trên thực tế đó là điều bất khả thi.\n\nTuy nhiên, định luật Murphy mang tính cảnh tỉnh và dẫn dắt rất lớn trong cuộc sống hằng ngày. Cuốn sách này giới thiệu đến với bạn đọc về kiến thức cơ bản, hiện tượng thường gặp cùng với các hiệu ứng tâm lý học biểu hiện trong ý thức cá nhân, tính cạnh tranh, quan hệ xã hội,… của định luật Murphy thông qua góc nhìn thực tiễn. Từ đó giúp bạn đọc hiểu được bản chất con người, bản chất xã hội rồi ứng dụng nó vào giải quyết các vấn đề gặp phải trong cuộc sống.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Định Luật Murphy - Mọi Bí Mật Tâm Lý Thao Túng Cuộc Đời Bạn", 17, 1 },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Bạn có biết cuộc đời là gì không? Cuộc đời của chúng ta kéo dài từ khoảnh khắc ta được sinh ra cho đến khoảnh khắc ta chết đi, và có lẽ còn hơn thế nữa. Bạn đang nghịch gì với đời mình do triết gia Jiddu Krishnamurti viết giúp bạn hiểu và đi tìm ý nghĩa - mục đích của cuộc sống. \n \nBạn đang nghịch gì với đời mình xoay quanh những suy nghĩ của J.Krishnamurti về nhiều vấn đề trong cuộc sống của các bạn trẻ là những ý kiến đóng góp độc đáo và chân thực nhất cho tư tưởng giáo dục thế kỷ 21. Những điều mà ông cố gắng truyền tải không can dự gì đến triết lý cuộc đời, chúng là nghệ thuật khám phá cả thế giới bên ngoài lẫn những suy nghĩ và hành vi bên trong mỗi chúng ta;  là sự hiểu biết về tâm trí và trái tim, cũng như về tính toàn thể, vẹn tròn của cuộc sống. \n\nKhi còn trẻ, bạn hay tôi thật khó để biết mình yêu thích công việc gì, bởi vì chúng ta muốn làm rất nhiều thứ. Bạn muốn trở thành một kỹ sư, một người lái tàu, một phi công mang mơ ước bay vào trời xanh; hoặc có thể bạn muốn trở thành một nhà hùng biện hay một chính khách nổi tiếng. Bạn cũng có thể muốn trở thành một nghệ sĩ, một nhà hóa học, một nhà thơ, hay một thợ mộc. Bạn có thể muốn làm việc trí óc hay làm việc chân tay. Liệu những công việc này có phải là những việc mà bạn thực sự yêu thích, hay sự hứng thú với chúng chỉ đến từ phản ứng trước áp lực của xã hội? Làm thế nào để có thể tìm thấy công việc mình yêu thích? \n\nBạn đang nghịch gì với đời mình bao gồm bốn phần – Bản ngã và cuộc đời của bạn – Hiểu biết bản thân, chìa khóa của tự do – Giáo dục công việc và tiền bạc – những mối tương quan. Qua mỗi phần sẽ có nhiều chương nhỏ để triết gia J. Krishnamurti dẫn dắt người đọc đến những vấn đề thực tiễn từ suy nghĩ, thấu thị, hiểu biết, đến hành động cụ thể. Ông chỉ ra rằng ngay với cả nỗi sợ hãi, sự buồn chán, hạnh phúc hay đau khổ, thành công thất bại đều có thể hòa giải trong ý nghĩ của một tâm hồn biết tĩnh lặng.\n\nBạn sao, thế giới vậy, và vấn đề của bạn là vấn đề của thế giới, thế nhưng bằng một cách nào đó chúng ta dường như luôn lãng quên điều này. Vậy thì hãy bắt đầu học cách tĩnh lặng trong phạm vi gần gũi thôi, hãy lưu tâm đến sự hiện hữu hằng ngày của chính mình, từ ý nghĩ, cảm xúc cho đến những hành vi, hoạt động sống cơ bản, cũng như lưu tâm đến mối tương quan giữa chúng ta và các ý tưởng hay niềm tin.\n\nĐọc Bạn đang nghịch gì với đời mình - bạn sẽ khám phá được rằng những ai phải đủ thông minh, sự khôn ngoan, không sợ hãi và từ chối bước trên lối mòn truyền thống của xã hội mới tìm thấy điều mình yêu thích. \n\nCuộc sống là một nguồn nước sâu. Người ta có thể đến với nó với những cái xô nhỏ và chỉ múc được một ít nước, hoặc người ta có thể đến với nó với những cái thùng lớn, múc nhiều nước để sinh hoạt và dự trữ. Thời trẻ là quãng thời gian lý tưởng để người ta tìm tòi và trải nghiệm mọi thứ. Chúc bạn thành công.\n\nĐôi nét về tác giả\n\nJiddu Krishnamurti (1895 – 1986) là một triết gia và nhà diễn thuyết nổi tiếng về các vấn đề triết học và tinh thần, các chủ đề bao gồm: mục đích của thiền định, mối quan hệ giữa con người và phương cách để tạo nên sự thay đổi tích cực cho xã hội.\n\nĐược sinh ra trong một gia đình thuộc tầng lớp Brahmin tại Ấn Độ, nhưng Krishnamurti khẳng định rằng mình không thuộc bất cứ quốc tịch, tầng lớp, tôn giáo hay trường phái triết học nào. Ông dành suốt quãng đời còn lại của mình đi khắp thế giới như một nhà diễn thuyết độc lập. \n\nMã hàng	8935086856000\nTên Nhà Cung Cấp	FIRST NEWS\nTác giả	J Krishnamurti\nNgười Dịch	Huỳnh Hiếu Thuận\nNXB	Hồng Đức\nNăm XB	2022\nTrọng lượng (gr)	320\nKích Thước Bao Bì	20.5 x 14.5 x 1.4\nSố trang	304\nHình thức	Bìa Mềm\nSản phẩm hiển thị trong	\nFIRST NEWS\nSản phẩm bán chạy nhất	Top 100 sản phẩm Kỹ năng sống bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nBạn có biết cuộc đời là gì không? Cuộc đời của chúng ta kéo dài từ khoảnh khắc ta được sinh ra cho đến khoảnh khắc ta chết đi, và có lẽ còn hơn thế nữa. Bạn đang nghịch gì với đời mình do triết gia Jiddu Krishnamurti viết giúp bạn hiểu và đi tìm ý nghĩa - mục đích của cuộc sống. \n \nBạn đang nghịch gì với đời mình xoay quanh những suy nghĩ của J.Krishnamurti về nhiều vấn đề trong cuộc sống của các bạn trẻ là những ý kiến đóng góp độc đáo và chân thực nhất cho tư tưởng giáo dục thế kỷ 21. Những điều mà ông cố gắng truyền tải không can dự gì đến triết lý cuộc đời, chúng là nghệ thuật khám phá cả thế giới bên ngoài lẫn những suy nghĩ và hành vi bên trong mỗi chúng ta;  là sự hiểu biết về tâm trí và trái tim, cũng như về tính toàn thể, vẹn tròn của cuộc sống. \n\nKhi còn trẻ, bạn hay tôi thật khó để biết mình yêu thích công việc gì, bởi vì chúng ta muốn làm rất nhiều thứ. Bạn muốn trở thành một kỹ sư, một người lái tàu, một phi công mang mơ ước bay vào trời xanh; hoặc có thể bạn muốn trở thành một nhà hùng biện hay một chính khách nổi tiếng. Bạn cũng có thể muốn trở thành một nghệ sĩ, một nhà hóa học, một nhà thơ, hay một thợ mộc. Bạn có thể muốn làm việc trí óc hay làm việc chân tay. Liệu những công việc này có phải là những việc mà bạn thực sự yêu thích, hay sự hứng thú với chúng chỉ đến từ phản ứng trước áp lực của xã hội? Làm thế nào để có thể tìm thấy công việc mình yêu thích? ", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Bạn Đang Nghịch Gì Với Đời Mình?", 16, 1 },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Một Đời Được Mất\n\n- Mọi vấn đề khó quyết định trong cuộc đời này, đều có thể suy xét dưới góc nhìn “Được” và “Mất”.\n\n- Có những thứ bạn nghĩ mình “được”, nhưng thực chất chỉ là ảo mộng hão huyền. Cũng có những thứ bạn nghĩ mình “mất”, nhưng cuộc sống chắc chắn sẽ “trả lại” cho bạn dưới một hình thức khác.\n\n- Tất cả những điều ấy – đều không thể đoán trước được.\n\nBạn chỉ cần sống hiên ngang, tự tin – không thẹn với lòng mà thôi!\n\nĐó là những lời nhắn gửi chân thành và tinh túy được đúc rút từ cuốn sách mới nhất của Vãn Tình – Một đời được mất. Đây là cuốn sách thứ chín của cô xuất bản tại thị trường Việt Nam bởi thương hiệu Bloom Books, đánh dấu son rực rỡ trên hành trình phấn đấu và trưởng thành của nữ tác giả đầu sách best seller Bạn đắt giá bao nhiêu? và Khí chất bao nhiêu, hạnh phúc bấy nhiêu.\n\nNăm tháng không lấy đi nhiệt huyết của cô mà còn ban tặng cho cô những kinh nghiệm vô cùng quý giá - dưới góc nhìn của một người phụ nữ đã đi qua bóng tối cuộc đời, cũng đã chạm đến đỉnh cao danh vọng, sống một đời phong phú, viên mãn. Những câu chuyện trong Một đời được mất vẫn được “mổ xẻ” một cách sắc bén, trực diện – nhưng có sự suy xét tinh tế hơn cả về lý lẽ và tình cảm, điều mà hiếm ai có thể làm được nếu chưa trải qua đủ những cung bậc thăng trầm của cuộc đời, gặp đủ nhiều người và trò chuyện đủ lâu với họ để soi thấu những điều cần tỏ tường.\n\nLần trở lại này, Một đời được mất đem đến hơn bốn mươi câu chuyện xoay quanh những vấn đề cơ bản của cuộc sống: đi làm, thăng tiến, hôn nhân, gia đình, quan hệ mẹ chồng – nàng dâu, quan hệ bạn bè,... với tâm thế:\n\nPhụ nữ mạnh mẽ, là người có khả năng cầm lên được, bỏ xuống được\n\nTrích dẫn hay của Vãn Tình trong Một đời được mất:\n\n1. Con người ta sống trên đời, nhiều khi chỉ muốn “nhận được”, mà không nỡ “bỏ đi” – thực ra cũng là lẽ thường tình, nhưng sự đời thường là: Phải có dũng khí “bỏ đi” thì mới “nhận được” thành quả. Người cái gì cũng muốn, cuối cùng lại thường mất đi tất cả. Khi bạn đã hiểu được đạo lý này, bạn sẽ biết mình nên lựa chọn ra sao.\n\n2. Nỗi đau mà đàn ông gây ra không phải là đau khổ thực sự mà chỉ là cảm xúc nhất thời. Phụ nữ mà không có khả năng nuôi sống bản thân mới thực sự là đau khổ. Phụ nữ không có tiền mà ly hôn mới gọi là “ly hôn”, phụ nữ có tiền mà ly hôn thì được gọi là “trở lại trạng thái độc thân”. Phụ nữ không có tiền kết hôn gọi là tìm kiếm “phiếu cơm dài hạn”, phụ nữ có tiền kết hôn gọi là “theo đuổi tình yêu đích thực”.\n\n3. Khi một mối quan hệ cần bạn nhẫn nhịn chịu đựng để duy trì, buộc bạn không ngừng hy sinh lợi ích của mình để gìn giữ, thì thực ra nó nên chấm dứt từ lâu lắm rồi.\n\n4. Những cô gái sống có cá tính thường nghe theo tiếng gọi của trái tim, nên luôn tạo cho người ta cảm giác chân thực, không làm bộ, không giả dối. Thế nên chúng ta hãy cứ sống thật với chính mình, những người có tư tưởng tương đồng sẽ dần dần tới bên chúng ta.\n\n5. Khi bước qua tuổi ba mươi, tôi thấy phụ nữ nên sống thế này:\n\nCó người thương biết chở che ấm lạnh, không có phản bội và lừa dối, xứng đáng để chúng ta trao gửi tấm chân tình, nếu không, cứ sống độc thân vui vẻ cũng chẳng sao. Ít nhất chúng ta không phải sống trong đau khổ và dằn vặt.\n\nCó sự nghiệp mà mình yêu quý, dù không kiếm ra nhiều tiền, thậm chí vô cùng cực khổ, nhưng còn tốt hơn là ngày ngày đi làm mà như đi “thăm mả”. Đừng ép bản thân phải làm những chuyện mà mình không thích, nếu không bạn sẽ thấy mình ngày càng u uất chán nản, mệt mỏi ủ ê.\n\nCó vài người bạn tâm giao, không cần “tám” chuyện suốt ngày, không cần tụ tập mọi lúc, nhưng tâm ý luôn tương thông, quan trọng là không thấy mệt mỏi khi ở bên nhau. Đừng ép bản thân phải giao du với những người có tư tưởng khác mình, nếu không người chịu khổ là chính bạn đấy.\n\nVề tác giả:\n\nVãn Tình là nhà biên kịch - tác giả của những đầu sách bán chạy tại Trung Quốc. Các tác phẩm của cô đều thẳng thắn, trực diện, đánh trúng tâm lý các cô gái.\n\nỞ Việt Nam, Vãn Tình được coi như “nữ hoàng” của dòng sách cảm hứng sống dành cho phái nữ. Cuốn sách Bạn đắt giá bao nhiêu? của cô trở thành cuốn sách Bán chạy nhất trên nền tảng Tiki (2019), tạo nên một làn sóng mạnh mẽ nhằm cổ vũ tinh thần, thay đổi quan điểm hạnh phúc của bất kỳ ai từng đọc cuốn sách.\n\nMã hàng	8935325017353\nTên Nhà Cung Cấp	AZ Việt Nam\nTác giả	Vãn Tình\nNgười Dịch	Mỹ Linh\nNXB	Thế Giới\nNăm XB	2023\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	340\nKích Thước Bao Bì	20 x 14.5 x 1.6 cm\nSố trang	322\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Kỹ năng sống bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nMột Đời Được Mất\n\n- Mọi vấn đề khó quyết định trong cuộc đời này, đều có thể suy xét dưới góc nhìn “Được” và “Mất”.\n\n- Có những thứ bạn nghĩ mình “được”, nhưng thực chất chỉ là ảo mộng hão huyền. Cũng có những thứ bạn nghĩ mình “mất”, nhưng cuộc sống chắc chắn sẽ “trả lại” cho bạn dưới một hình thức khác.\n\n- Tất cả những điều ấy – đều không thể đoán trước được.\n\nBạn chỉ cần sống hiên ngang, tự tin – không thẹn với lòng mà thôi!\n\nĐó là những lời nhắn gửi chân thành và tinh túy được đúc rút từ cuốn sách mới nhất của Vãn Tình – Một đời được mất. Đây là cuốn sách thứ chín của cô xuất bản tại thị trường Việt Nam bởi thương hiệu Bloom Books, đánh dấu son rực rỡ trên hành trình phấn đấu và trưởng thành của nữ tác giả đầu sách best seller Bạn đắt giá bao nhiêu? và Khí chất bao nhiêu, hạnh phúc bấy nhiêu.\n\nNăm tháng không lấy đi nhiệt huyết của cô mà còn ban tặng cho cô những kinh nghiệm vô cùng quý giá - dưới góc nhìn của một người phụ nữ đã đi qua bóng tối cuộc đời, cũng đã chạm đến đỉnh cao danh vọng, sống một đời phong phú, viên mãn. Những câu chuyện trong Một đời được mất vẫn được “mổ xẻ” một cách sắc bén, trực diện – nhưng có sự suy xét tinh tế hơn cả về lý lẽ và tình cảm, điều mà hiếm ai có thể làm được nếu chưa trải qua đủ những cung bậc thăng trầm của cuộc đời, gặp đủ nhiều người và trò chuyện đủ lâu với họ để soi thấu những điều cần tỏ tường.\n\nLần trở lại này, Một đời được mất đem đến hơn bốn mươi câu chuyện xoay quanh những vấn đề cơ bản của cuộc sống: đi làm, thăng tiến, hôn nhân, gia đình, quan hệ mẹ chồng – nàng dâu, quan hệ bạn bè,... với tâm thế:\n\nPhụ nữ mạnh mẽ, là người có khả năng cầm lên được, bỏ xuống được", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Một Đời Được Mất", 16, 1 },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Manifest - 7 Bước Để Thay Đổi Cuộc Đời Bạn Mãi Mãi\n\n“Ai đã từng nói với bạn rằng bạn không thể có tất cả.\n\nTôi ở đây để nói với bạn rằng bạn có thể.”\n\n_ Roxie Nafousi _\n\nMở ra cánh cửa Manifest và giải phóng tiềm năng vô hạn của chính mình cùng cuốn sách MANIFEST – 7 bước để thay đổi cuộc đời bạn mãi mãi.\n\nCuốn sách này là một chỉ dẫn cần thiết cho bất kỳ ai muốn tự làm chủ cuộc đời mình. Chỉ với 7 bước cơ bản, bạn có thể hoàn toàn hiểu được Manifest thực sự là gì và tạo ra một cuộc sống mà bạn hằng mong ước.\n\nLà một sự giao thoa giữa khoa học và sự thông thái, Manifest là một dạng rèn luyện phát triển bản thân, học cách yêu bản thân, giúp bạn vươn tới mục tiêu và sống một cuộc sống viên mãn nhất.\n\nMANIFEST – 7 bước để thay đổi cuộc đời bạn mãi mãi được in bằng chất liệu giấy in cao cấp; bìa cán mờ, được tặng kèm 1 bookmark.\n\nMã hàng	8935325016325\nTên Nhà Cung Cấp	AZ Việt Nam\nTác giả	Roxie Nafousi\nNgười Dịch	Bạc Hà\nNXB	Thế Giới\nNăm XB	2023\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	220\nKích Thước Bao Bì	20.5 x 14 x 1 cm\nSố trang	200\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Kỹ năng sống bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nManifest - 7 Bước Để Thay Đổi Cuộc Đời Bạn Mãi Mãi\n\n“Ai đã từng nói với bạn rằng bạn không thể có tất cả.\n\nTôi ở đây để nói với bạn rằng bạn có thể.”\n\n_ Roxie Nafousi _\n\nMở ra cánh cửa Manifest và giải phóng tiềm năng vô hạn của chính mình cùng cuốn sách MANIFEST – 7 bước để thay đổi cuộc đời bạn mãi mãi.\n\nCuốn sách này là một chỉ dẫn cần thiết cho bất kỳ ai muốn tự làm chủ cuộc đời mình. Chỉ với 7 bước cơ bản, bạn có thể hoàn toàn hiểu được Manifest thực sự là gì và tạo ra một cuộc sống mà bạn hằng mong ước.\n\nLà một sự giao thoa giữa khoa học và sự thông thái, Manifest là một dạng rèn luyện phát triển bản thân, học cách yêu bản thân, giúp bạn vươn tới mục tiêu và sống một cuộc sống viên mãn nhất.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Manifest - 7 Bước Để Thay Đổi Cuộc Đời Bạn Mãi Mãi", 16, 1 },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "HIỂU VỀ TRÁI TIM – CUỐN SÁCH MỞ CỬA THẾ GIỚI CẢM XÚC CỦA MỖI NGƯỜI  \n\n“Hiểu về trái tim” là một cuốn sách đặc biệt được viết bởi thiền sư Minh Niệm. Với phong thái và lối hành văn gần gũi với những sinh hoạt của người Việt, thầy Minh Niệm đã thật sự thổi hồn Việt vào cuốn sách nhỏ này.\n\nXuất bản lần đầu tiên vào năm 2011, Hiểu Về Trái Tim trình làng cũng lúc với kỷ lục: cuốn sách có số lượng in lần đầu lớn nhất: 10000 bản. Trung tâm sách kỷ lục Việt Nam công nhận kỳ tích ấy nhưng đến nay, con số phát hành của Hiểu về trái tim vẫn chưa có dấu hiệu chậm lại.\n\nLà tác phẩm đầu tay của nhà sư Minh Niệm, người sáng lập dòng thiền hiểu biết (Understanding Meditation), kết hợp giữa tư tưởng Phật giáo Đại thừa và Thiền nguyên thủy Vipassana, nhưng Hiểu Về Trái Tim không phải tác phẩm thuyết giáo về Phật pháp. Cuốn sách rất “đời” với những ưu tư của một người tu nhìn về cõi thế. Ở đó, có hạnh phúc, có đau khổ, có tình yêu, có cô đơn, có tuyệt vọng, có lười biếng, có yếu đuối, có buông xả… Nhưng, tất cả những hỉ nộ ái ố ấy đều được khoác lên tấm áo mới, một tấm áo tinh khiết và xuyên suốt, khiến người đọc khi nhìn vào, đều thấy mọi sự như nhẹ nhàng hơn…\n\nTrong dòng chảy tất bật của cuộc sống, có bao giờ chúng ta dừng lại và tự hỏi: Tại sao ta giận? Tại sao ta buồn? Tại sao ta hạnh phúc? Tại sao ta cô đơn?... Tất cả những hiện tượng tâm lý ấy không ngừng biến hóa trong ta và tác động lên đời sống của ta, nhưng ta lại biết rất ít về nguồn gốc và sự vận hành của nó. Chỉ cần một cơn giận, hay một ý niệm nghi ngờ, cũng có thể quét sạch năng lượng bình yên trong ta và khiến ta nhìn mọi thứ đều sai lệch. Từ thất bại này đến đổ vỡ khác mà ta không lý giải nổi, chỉ biết dùng ý chí để tự nhắc nhở mình cố gắng tiến bộ hơn. Cho nên, hiểu về trái tim chính là nhu cầu căn bản nhất của con người.\n\nHiểu về trái tim là thái độ trở về tiếp nhận và làm mới lại tâm hồn mình. Bởi hiểu được chính mình, ta sẽ dễ dàng hiểu được người khác, để ta có thể thương nhau chân thật.\n\nXuyên suốt cuốn sách, tác giả đã đưa ra 50 khái niệm trong cuộc sống, 50 bài viết tâm lý trị liệu, được trình bày rất chân phương, dễ hiểu, thực tế,  vốn dĩ rất đời thường nhưng nếu suy ngẫm một chút chúng ta sẽ thấy thật sâu sắc như Khổ đau, Hạnh phúc, Tình yêu, Tức giận, Ghen tuông, Ích kỷ, Tham vọng, Thành thật, Nghi ngờ, Lo lắng, Do dự, Buông xả, Thảnh thơi, Bình yên, Cô đơn, Ái ngữ, Lắng nghe… Đúng như tựa đề sách, sách sẽ giúp ta hiểu về trái tim, hiểu về những tâm trạng, tính cách sâu thẳm trong trái tim ta.\n\nLúc sinh thời cố Giáo sư, Tiến sĩ Trần Văn Khuê, có dịp tiếp cận với Hiểu Về Trái Tim. Ông nhận xét, như một cuốn sách đầu tiên thuộc chủ đề Hạt Giống Tâm Hồn do một tác giả Việt Nam viết, cuốn sách sẽ giúp người đọc hiểu được cảm xúc của tâm hồn, trái tim của chính mình và của người khác. Để, tận cùng là loại bỏ nỗi buồn, tổn thương và tìm được hạnh phúc trong cuộc sống. Có lẽ, vì điều này mà hơn 10 năm qua, Hiểu về trái tim vẫn là cuốn sách liên tục được tái bản và chưa có dấu hiệu “hạ nhiệt”, nhiều năm trời liên tục nằm trong top sách bán chạy nhất tại Việt Nam.\n\nĐáng quý hơn, tòan bộ lợi nhuận thu được từ việc phát hành cuốn sách này đều được chuyển về quỹ từ thiện cùng tên “Hiểu về trái tim” để giúp đỡ trẻ em có hoàn cảnh khó khăn, bất hạnh tại Việt Nam. Và đây cũng chính là niềm hạnh phúc cũng như ý nghĩa nhân ái lớn nhất mà cuốn sách đã mang lại, đặc biệt là khi tất cả hành trình này còn có sự đồng hành và góp sức của hàng trăm nghìn bản đọc trên khắp cả nước Việt Nam.\n\nNgười nổi tiếng nói về cuốn sách:\n\n“Để chữa lành những tổn thương và nổi đau, cách tốt nhứt và hữu hiệu nhất là cần hiểu rõ được trái tim, tâm hồn của mình, và của người khác, cuốn sách Hiểu về Trái Tim chính là cuốn sách giúp bạn đọc làm được điều đó: Hiểu rõ và chữa lành trái tim, tâm hồn của mình và của những người xung quanh, để mọi người cùng được sống trong hạnh phúc và yêu thương. Với cuốn sách này, chúc bạn đọc sẽ luôn hạnh phúc và không bao giờ phải sống với một trái tim tan vỡ hay một tâm hồn tổn thương”  - Giáo sư – Tiến sĩ Trần Văn Khê\n\n\"Cuốn sách Hiểu về trái tim được viết ra với những trải nghiệm sâu sắc, nhằm giúp con người hiểu rõ và lý giải những cảm xúc của chính mình để tìm được sự bình an và hạnh phúc thật sự”. - Anh hùng Lao động, Thầy thuốc nhân dân, GS Bác sĩ Nguyễn Thị Ngọc Phượng\n\n\"Đây chính là con đường của đạo Tâm, với những nguyên tắc sống hạnh phúc – một thứ “an lạc hạnh” – từ những sẻ chia chân thành của tác giả. Con đường hạnh phúc đó đòi hỏi sự khổ luyện, chứng nghiệm qua quán chiếu bản thân, từ đó thấy biết bản chất của khổ đau, phiền não, và, vượt thoát…” - Bác sĩ Đỗ Hồng Ngọc. Nguyên Giám Đốc Trung Tâm Truyền Thông – Giáo Dục Sức Khoẻ TP.HCM\n\n\"Một cuốn sách hay, thực tế và rất hữu ích cho mọi người, đặc biệt đối với thanh thiếu niên và các bạn trẻ. Nếu rèn luyện được theo những điều hay như thế thì cuộc sống sẽ tốt đẹp hơn rất nhiều\". - Tạ Bích Loan, Trưởng Ban Thanh thiếu niên Đài truyền hình Việt Nam\n\n\"Đây là một cuốn sách đặc biệt, có tính giáo dục, tự nhận thức cao, được viết từ trái tim để chữa lành những trái tim. Một cuốn sách rất ý nghĩa!”. - Nhà báo Trần Tử Văn, Phó Tổng biên tập Báo Công an TP.HCM\n\n\"Hiểu về trái tim là cuốn sách thứ 180 của Tủ sách Hạt giống tâm hồn mà First News đã xuất bản, nhưng đây là cuốn sách của một tác giả Việt Nam đã để lại trong tôi những cảm xúc đặc biệt nhất. Với những trải nghiệm sâu sắc và tâm huyết mà tác giả đã viết trong 8 năm chắc chắn sẽ mang đến cho bạn đọc những khám phá mới mẻ và thú vị. Một cánh cửa rộng mở đang chờ đón bạn”. - Nguyễn Văn Phước, Giám đốc First News - Trí Việt\n\nBáo chí nói gì về “Hiểu về trái tim”:\n\n“'Hiểu về trái tim' là một cuốn sách đặc biệt, được viết nên từ tâm huyết của một nhà thiền sư mang tên Minh Niệm. Đã bao giờ giữa cuộc đời hối hả, bạn chợt dừng lại và tự hỏi mình rằng ' hạnh phúc là gì?' , '' khổ đau là gì?' hay chưa? Vâng, cuốn sách này sẽ giải đáp cho bạn tất cả những băn khoăn đó.” – baomoi.vn\n\nVề tác giả:\n\nSinh tại Châu Thành, Tiền Giang, xuất gia tại Phật Học Viện Huệ Nghiêm – Sài Gòn, Minh Niệm từng thọ giáo thiền sư Thích Nhất Hạnh tại Pháp và thiền sư Tejaniya tại Mỹ. Kết quả sau quá trình tu tập, lĩnh hội kiến thức… Ông quyết định chọn con đường hướng dẫn thiền và khai triển tâm lý trị liệu cho giới trẻ làm Phật sự của mình. Tiếp cận với nhiều người trẻ, lắng nghe thế giới quan của họ và quan sát những đổi thay trong đời sống hiện đại, ông phát hiện có rất nhiều vấn đề của cuộc sống. Nhưng, tất cả, chỉ xuất phát từ một nguyên nhân: Chúng ta chưa hiểu, và chưa hiểu đúng về trái tim mình là chưa cơ chế vận động của những hỉ, nộ, ái, ố trong mỗi con người.\n\n“Tôi đã từng quyết lòng ra đi tìm hạnh phúc chân thật. Dù thời điểm ấy, ý niệm về hạnh phúc chân thật trong tôi rất mơ hồ nhưng tôi vẫn tin rằng nó có thật và luôn hiện hữu trong thực tại. Hơn mười năm sau, tôi mới thấy con đường. Và cũng chừng ấy năm nữa, tôi mới tự tin đặt bút viết về những điều mình đã khám phá và trải nghiệm…”, tác giả chia sẻ.\n\n \n\n\nMã hàng	8935086857366\nTên Nhà Cung Cấp	FIRST NEWS\nTác giả	Minh Niệm\nNXB	Tổng Hợp TPHCM\nNăm XB	2023\nNgôn Ngữ	Tiếng Việt\nTrọng lượng (gr)	415\nKích Thước Bao Bì	20.5 x 13 x 2.5 cm\nSố trang	479\nHình thức	Bìa Mềm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Kỹ năng sống bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nHIỂU VỀ TRÁI TIM – CUỐN SÁCH MỞ CỬA THẾ GIỚI CẢM XÚC CỦA MỖI NGƯỜI  \n\n“Hiểu về trái tim” là một cuốn sách đặc biệt được viết bởi thiền sư Minh Niệm. Với phong thái và lối hành văn gần gũi với những sinh hoạt của người Việt, thầy Minh Niệm đã thật sự thổi hồn Việt vào cuốn sách nhỏ này.\n\nXuất bản lần đầu tiên vào năm 2011, Hiểu Về Trái Tim trình làng cũng lúc với kỷ lục: cuốn sách có số lượng in lần đầu lớn nhất: 10000 bản. Trung tâm sách kỷ lục Việt Nam công nhận kỳ tích ấy nhưng đến nay, con số phát hành của Hiểu về trái tim vẫn chưa có dấu hiệu chậm lại.\n\nLà tác phẩm đầu tay của nhà sư Minh Niệm, người sáng lập dòng thiền hiểu biết (Understanding Meditation), kết hợp giữa tư tưởng Phật giáo Đại thừa và Thiền nguyên thủy Vipassana, nhưng Hiểu Về Trái Tim không phải tác phẩm thuyết giáo về Phật pháp. Cuốn sách rất “đời” với những ưu tư của một người tu nhìn về cõi thế. Ở đó, có hạnh phúc, có đau khổ, có tình yêu, có cô đơn, có tuyệt vọng, có lười biếng, có yếu đuối, có buông xả… Nhưng, tất cả những hỉ nộ ái ố ấy đều được khoác lên tấm áo mới, một tấm áo tinh khiết và xuyên suốt, khiến người đọc khi nhìn vào, đều thấy mọi sự như nhẹ nhàng hơn…\n\nTrong dòng chảy tất bật của cuộc sống, có bao giờ chúng ta dừng lại và tự hỏi: Tại sao ta giận? Tại sao ta buồn? Tại sao ta hạnh phúc? Tại sao ta cô đơn?... Tất cả những hiện tượng tâm lý ấy không ngừng biến hóa trong ta và tác động lên đời sống của ta, nhưng ta lại biết rất ít về nguồn gốc và sự vận hành của nó. Chỉ cần một cơn giận, hay một ý niệm nghi ngờ, cũng có thể quét sạch năng lượng bình yên trong ta và khiến ta nhìn mọi thứ đều sai lệch. Từ thất bại này đến đổ vỡ khác mà ta không lý giải nổi, chỉ biết dùng ý chí để tự nhắc nhở mình cố gắng tiến bộ hơn. Cho nên, hiểu về trái tim chính là nhu cầu căn bản nhất của con người.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Hiểu Về Trái Tim (Tái Bản 2023)", 16, 1 },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Bút Chì Bấm Jedo Bium 0.5 mm - Morning Glory 32000-89749\n\nSản phẩm thuộc dòng chì bấm kim thông dụng phù hợp với đối tượng học sinh - sinh viên.\n\nThiết kế phần thân màu sắc bắt mắt, hiện đại.\n\nĐầu bấm rất êm dễ dàng thao tác.\n\nNgòi ít bị gãy vụn, dễ xoá sạch, vừa tiết kiệm, vừa tránh được khả năng đi sai nét.\n\nNét ngòi 0.5mm rõ, mịn và đẹp sẽ khiến các tác phẩm vẽ, các bài viết của bạn trông sạch và gọn mắt hơn rất nhiều.\n\nMã hàng	8801237897492-mau3\nTên Nhà Cung Cấp	Morning Glory Corp\nThương Hiệu	Morning Glory\nXuất Xứ Thương Hiệu	Thương Hiệu Hàn Quốc\nNơi Gia Công & Sản Xuất	Hàn Quốc\nMàu sắc	Nâu\nChất liệu	Nhựa, Kim Loại\nTrọng lượng (gr)	10\nKích Thước Bao Bì	10.5 x 5 x 1.5 cm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Bút Chì Bấm bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nBút Chì Bấm Jedo Bium 0.5 mm - Morning Glory 32000-89749\n\nSản phẩm thuộc dòng chì bấm kim thông dụng phù hợp với đối tượng học sinh - sinh viên.\n\nThiết kế phần thân màu sắc bắt mắt, hiện đại.\n\nĐầu bấm rất êm dễ dàng thao tác.\n\nNgòi ít bị gãy vụn, dễ xoá sạch, vừa tiết kiệm, vừa tránh được khả năng đi sai nét.\n\nNét ngòi 0.5mm rõ, mịn và đẹp sẽ khiến các tác phẩm vẽ, các bài viết của bạn trông sạch và gọn mắt hơn rất nhiều.", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Bút Chì Bấm Jedo Bium 0.5 mm - Morning Glory 32000-89749", 49, 2 },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Băng Keo Trong VP Dày\n\nBăng keo có màu trong suốt vừa giúp bạn dễ dàng quan sát vừa đảm bảo tính thẩm mỹ khi dán lên các đồ vật khác nhau.\n\nKhi dán các đồ vật có kích thước nhỏ hơn bề mặt băng dính, bạn cũng có thể cắt thành nhiều miếng nhỏ rất tiện lợi.\n\nSản phẩm được sử dụng rộng rãi trong cuộc sống, từ trường học, văn phòng, gia đình đến các cơ sở sản xuất thủ công\n\nThành phần không chứa các hóa chất độc hại, đảm bảo an toàn cho sức khỏe người sử dụng.", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Băng Keo Trong Dày", 86, 2 },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Băng Keo Xốp Đen Cao Cấp\n\nCó độ bám dính cao và trong thời gian dài, có thể cắt ra sử dụng dễ dàng. Băng keo 2 mặt giúp bạn dán các vật dụng lại với nhau một cách dễ dàng.\n\nBăng keo có lớp xốp màu đen khi tháo mặt giấy ra thuận tiện cho việc sử dụng và bảo quản.\n\nSản phẩm được sử dụng rộng rãi trong cuộc sống, từ trường học, văn phòng, gia đình đến các cơ sở sản xuất thủ công...\n\nThành phần không chứa các hóa chất độc hại, đảm bảo an toàn cho sức khỏe người sử dụn", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Băng Keo Mouse - Màu Đỏ - Đen", 86, 2 },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Băng Keo Điện Cỡ Đại Nano\n\nCó khả năng chịu mài mòn cao, chịu được trong môi trường acid\n\nCó khả năng chịu được điện thế cao (600V)\n\nChống cháy\n\nCó thể dùng ngoài trời và trong nhà.\n\nThích nghi mọi thay đổi thời tiết\n\nỨng dụng: Đánh dấu pha, đánh dấu màu cho hệ thống dẫn điện...\n\nMã hàng	1503010390462\nNhà Cung Cấp	Cty TM Hạnh Thuận\nThương Hiệu	Nano\nXuất Xứ Thương Hiệu	Trung Quốc\nNơi Gia Công & Sản Xuất	Việt Nam\nMàu sắc	Đen\nChất liệu	Nhựa\nTrọng lượng (gr)	55\nKích Thước Bao Bì	6.5 x 6.5 x 2 cm\nSản phẩm bán chạy nhất	Top 100 sản phẩm Băng Keo - Cắt Băng Keo bán chạy của tháng\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\nBăng Keo Điện Cỡ Đại Nano\n\nCó khả năng chịu mài mòn cao, chịu được trong môi trường acid\n\nCó khả năng chịu được điện thế cao (600V)\n\nChống cháy\n\nCó thể dùng ngoài trời và trong nhà.\n\nThích nghi mọi thay đổi thời tiết\n\nỨng dụng: Đánh dấu pha, đánh dấu màu cho hệ thống dẫn điện...", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Băng Keo Điện Cỡ Lớn - Nano - Màu Đen", 86, 2 },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Băng Keo Đục 5F\n\nLà loại băng keo dán thùng OPP được làm từ màng BOPP và được phủ dưới dạng sữa dựa vào chất acrylic adhesive.\n\nChúng được biểu thị đặc điểm có độ bền cao và có thể kéo dãn, trọng lượng nhẹ mà lại kinh tế cho nên băng keo được sử dụng rộng rãi trong công nghiệp tự động hóa đóng gói thùng carton.\n\nBăng keo đã trở thành vật liệu chính trong nguyên liệu đóng gói cho thùng carton nói chung hoặc dán thùng carton hay ngành đóng gói.", true, false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Băng Keo Đục 5F - Màu Vàng", 86, 2 }
                });

            migrationBuilder.InsertData(
                table: "Skus",
                columns: new[] { "SkuId", "Dimension_Height", "Dimension_Length", "Dimension_Width", "Barcode", "Comment", "CreatedBy", "CreatedWhen", "DeletedWhen", "DiscontinuedWhen", "IsActive", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "Quantity", "RecommendedRetailPrice", "Status", "Tags", "TaxRate", "UnitPrice", "ValidFrom", "ValidTo", "Weight", "SkuValue_Value" },
                values: new object[,]
                {
                    { 9, 17.0m, 8m, 9.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 15, 89000m, "InStock", " ", 0m, 80100m, new DateTimeOffset(new DateTime(2024, 5, 19, 18, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00009" },
                    { 10, 16.0m, 7m, 8.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 27, 72000m, "InStock", " ", 0m, 64800m, new DateTimeOffset(new DateTime(2024, 5, 19, 18, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 100, "SKU00010" },
                    { 11, 12.0m, 8m, 8.0m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 30, 106000m, "InStock", " ", 0m, 95400m, new DateTimeOffset(new DateTime(2024, 5, 19, 18, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 2, 0, 0)), null, 55, "SKU00011" },
                    { 12, 9.0m, 8m, 9.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 20, 111000m, "InStock", " ", 0m, 99900m, new DateTimeOffset(new DateTime(2024, 5, 19, 18, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 3, 0, 0)), null, 43, "SKU00012" },
                    { 13, 1.0m, 3.5m, 9.0m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 0, 101000m, "OutOfStock", " ", 0m, 90900m, new DateTimeOffset(new DateTime(2024, 5, 19, 18, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 4, 0, 0)), null, 26, "SKU00013" }
                });

            migrationBuilder.InsertData(
                table: "VoucherUsages",
                columns: new[] { "Id", "OrderId", "UsedWhen", "UserId", "VoucherId" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2023, 2, 21, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 0, 0, 0, 0)), 5, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2023, 8, 14, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 0, 0, 0, 0)), 1, 4 },
                    { 3, 3, new DateTimeOffset(new DateTime(2022, 12, 16, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 0, 0, 0, 0)), 1, 2 },
                    { 4, 4, new DateTimeOffset(new DateTime(2024, 4, 13, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 0, 0, 0, 0)), 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookAuthorId", "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "WrittenWhen" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 5, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 6, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 7, 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 8, 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 9, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 10, 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 11, 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 12, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 13, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 14, 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 15, 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "OrderLineId", "CreatedBy", "CreatedWhen", "DiscountVoucherId", "LastEditedBy", "LastEditedWhen", "OrderId", "PickingCompletedWhen", "Quantity", "SkuId", "UnitPrice" },
                values: new object[,]
                {
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 2, 9, 27900m },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 3, 10, 170000m },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 2, 10, 170000m },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 27900m },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(2023, 9, 26, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 8, 9, 27900m },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, new DateTimeOffset(new DateTime(2024, 1, 27, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), 6, 10, 170000m },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 6, 10, 170000m },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, null, 9, 11, 180000m },
                    { 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, null, 3, 10, 170000m },
                    { 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, null, 1, 9, 27900m },
                    { 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, null, 7, 10, 170000m },
                    { 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 6, 9, 27900m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "ProductId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_1-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_1-390x510.jpg" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_2-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_2-390x510.jpg" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_3-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_3-390x510.jpg" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_4-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_4-390x510.jpg" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_7-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/chuyen_con_meo_day_hai_au_bay_tai_ban_2019/2023_01_11_15_38_11_7-390x510.jpg" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_187010.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_187010.jpg" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_1-390x510.jpg?_gl=1*bt98vq*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODM0MjYuMjIuMC4xNTUxNzk5MTUz*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_1-390x510.jpg?_gl=1*bt98vq*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODM0MjYuMjIuMC4xNTUxNzk5MTUz*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg." },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_5-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_5-390x510.jpg" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_9-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_9-390x510.jpg" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_13-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hoang_tu_be_tai_ban_2019/2021_05_11_14_41_34_13-390x510.jpg" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/m/u/mu-7452.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/m/u/mu-7452.jpg" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmnpbm3-1.jpg?_gl=1*1jvulx4*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQxNzQuNTMuMC4xNTUxNzk5MTUz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmnpbm3-1.jpg?_gl=1*1jvulx4*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQxNzQuNTMuMC4xNTUxNzk5MTUz" },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmnpbm3-1.jpg?_gl=1*1jvulx4*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQxNzQuNTMuMC4xNTUxNzk5MTUz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmnpbm3-1.jpg?_gl=1*1jvulx4*_gcl_au*NzI3ODcyODI5LjE3MTQ0NzI5NDg.*_ga*MTkxMDUwNDM0LjE3MTQ0NzI5NDg.*_ga_460L9JMC2G*MTcxODE4MTU2OS4zOC4xLjE3MTgxODQxNzQuNTMuMC4xNTUxNzk5MTUz" },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___bo_me_luon_yeu_con/2023_04_06_11_04_51_1-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___bo_me_luon_yeu_con/2023_04_06_11_04_51_1-390x510.jpg" },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___bo_me_luon_yeu_con/2023_04_06_11_04_51_5-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___bo_me_luon_yeu_con/2023_04_06_11_04_51_5-390x510.jpg" },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___con_tu_dau_toi/2023_04_05_15_26_22_1-390x510.jpg?_gl=1*yoqblq*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___con_tu_dau_toi/2023_04_05_15_26_22_1-390x510.jpg?_gl=1*yoqblq*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU." },
                    { 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___to_dung_cam_noi_khong/2023_04_05_15_26_30_3-390x510.jpg?_gl=1*yoqblq*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___to_dung_cam_noi_khong/2023_04_05_15_26_30_3-390x510.jpg?_gl=1*yoqblq*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU." },
                    { 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___nguy_hiem_day__mau_tranh_xa/2023_03_28_15_33_15_9-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___nguy_hiem_day__mau_tranh_xa/2023_03_28_15_33_15_9-390x510.jpg" },
                    { 28, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___khong_duoc_cham_vao_vung_rieng_tu_cua_to/2023_04_05_15_26_37_7-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___khong_duoc_cham_vao_vung_rieng_tu_cua_to/2023_04_05_15_26_37_7-390x510.jpg" },
                    { 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://salt.tikicdn.com/cache/280x280/ts/product/80/92/94/9d29d86173ca50a4820ae32a6ec2bbdc.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "https://salt.tikicdn.com/cache/280x280/ts/product/80/92/94/9d29d86173ca50a4820ae32a6ec2bbdc.jpg" },
                    { 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_thi_n_t_i_b_n_tr_i_k_i_n_b_n_ph_i_1.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_thi_n_t_i_b_n_tr_i_k_i_n_b_n_ph_i_1.png" },
                    { 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/thien_tai_ben_trai__ke_dien_ben_phai_tai_ban_2021/2021_05_10_08_51_00_3-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/thien_tai_ben_trai__ke_dien_ben_phai_tai_ban_2021/2021_05_10_08_51_00_3-390x510.jpg" },
                    { 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/t/h/thuatthaotung1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "https://cdn0.fahasa.com/media/catalog/product/t/h/thuatthaotung1.jpg" },
                    { 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/t/h/thuatthaotung1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "https://cdn0.fahasa.com/media/catalog/product/t/h/thuatthaotung1.jpg" },
                    { 34, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/t/h/thuatthaotung4.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "https://cdn0.fahasa.com/media/catalog/product/t/h/thuatthaotung4.jpg" },
                    { 35, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/p/h/ph_c-h_a-ch_n-dung-k_-ph_m-t_i.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "https://cdn0.fahasa.com/media/catalog/product/p/h/ph_c-h_a-ch_n-dung-k_-ph_m-t_i.jpg" },
                    { 36, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-tr_cph_c-h_a-ch_n-dung-k_-ph_m-t_i.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-tr_cph_c-h_a-ch_n-dung-k_-ph_m-t_i.jpg" },
                    { 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-sauph_c-h_a-ch_n-dung-k_-ph_m-t_i.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-sauph_c-h_a-ch_n-dung-k_-ph_m-t_i.jpg" },
                    { 38, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/t/h/th_ng-b_o-ph_t-h_nh-_nh-lu_t-murphybia-1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/catalog/product/t/h/th_ng-b_o-ph_t-h_nh-_nh-lu_t-murphybia-1.jpg" },
                    { 39, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/t/h/th_ng-b_o-ph_t-h_nh-_nh-lu_t-murphybia-4.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/catalog/product/t/h/th_ng-b_o-ph_t-h_nh-_nh-lu_t-murphybia-4.jpg" },
                    { 40, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/m/o/mockup-_nh-lu_t-murphy.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/catalog/product/m/o/mockup-_nh-lu_t-murphy.jpg" },
                    { 41, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/dinh_luat_murphy___moi_bi_mat_tam_ly_thao_tung_cuoc_doi_ban/2022_11_24_16_24_26_2-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/dinh_luat_murphy___moi_bi_mat_tam_ly_thao_tung_cuoc_doi_ban/2022_11_24_16_24_26_2-390x510.jpg" },
                    { 42, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/dinh_luat_murphy___moi_bi_mat_tam_ly_thao_tung_cuoc_doi_ban/2022_11_24_16_24_26_3-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/dinh_luat_murphy___moi_bi_mat_tam_ly_thao_tung_cuoc_doi_ban/2022_11_24_16_24_26_3-390x510.jpg" },
                    { 43, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/dinh_luat_murphy___moi_bi_mat_tam_ly_thao_tung_cuoc_doi_ban/2022_11_24_16_24_26_6-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/dinh_luat_murphy___moi_bi_mat_tam_ly_thao_tung_cuoc_doi_ban/2022_11_24_16_24_26_6-390x510.jpg" },
                    { 44, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935086856000.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935086856000.jpg" },
                    { 45, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/ban_dang_nghich_gi_voi_doi_minh/2022_12_13_08_59_38_1-390x510.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/ban_dang_nghich_gi_voi_doi_minh/2022_12_13_08_59_38_1-390x510.png" },
                    { 46, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/ban_dang_nghich_gi_voi_doi_minh/2022_12_13_08_59_38_4-390x510.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/ban_dang_nghich_gi_voi_doi_minh/2022_12_13_08_59_38_4-390x510.png" },
                    { 47, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_1_m_t_i_c_m_t.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_1_m_t_i_c_m_t.png" },
                    { 48, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_4_m_t_i_c_m_t.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_4_m_t_i_c_m_t.png" },
                    { 49, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_tr_c_15.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a_tr_c_15.jpg" },
                    { 50, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-tr_c-manifest.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-tr_c-manifest.jpg" },
                    { 51, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-tr_c-manifest_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, "https://cdn0.fahasa.com/media/catalog/product/b/_/b_a-tr_c-manifest_1.jpg" },
                    { 52, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/m/o/mockup---manifest.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, "https://cdn0.fahasa.com/media/catalog/product/m/o/mockup---manifest.jpg" },
                    { 53, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/4/z4118763446785_cf4bc22d353b065bbb37e686de1f9207.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, "https://cdn0.fahasa.com/media/catalog/product/z/4/z4118763446785_cf4bc22d353b065bbb37e686de1f9207.jpg" },
                    { 54, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/4/z4118763446785_cf4bc22d353b065bbb37e686de1f9207.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, "https://cdn0.fahasa.com/media/catalog/product/z/4/z4118763446785_cf4bc22d353b065bbb37e686de1f9207.jpg" },
                    { 55, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hieu_ve_trai_tim_tai_ban_2023/2023_02_21_08_51_07_6-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/hieu_ve_trai_tim_tai_ban_2023/2023_02_21_08_51_07_6-390x510.jpg" },
                    { 56, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-3.jpg?_gl=1*1hdzk44*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTc1Ny41MC4wLjE4NTAxMzg5ODk.*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-3.jpg?_gl=1*1hdzk44*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTc1Ny41MC4wLjE4NTAxMzg5ODk.*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5" },
                    { 57, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/1/9/1901011315413.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, "https://cdn0.fahasa.com/media/catalog/product/1/9/1901011315413.jpg" },
                    { 58, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/1/9/1901011315413-1.jpg?_gl=1*1ssif82*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTc3OS4yOC4wLjE4NTAxMzg5ODk.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, "https://cdn0.fahasa.com/media/catalog/product/1/9/1901011315413-1.jpg?_gl=1*1ssif82*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTc3OS4yOC4wLjE4NTAxMzg5ODk." },
                    { 59, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_7641.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_7641.jpg" },
                    { 60, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/1/9/1901011550555_2_.jpg?_gl=1*vok620*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTgzOC41OC4wLjE4NTAxMzg5ODk.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, "https://cdn0.fahasa.com/media/catalog/product/1/9/1901011550555_2_.jpg?_gl=1*vok620*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTgzOC41OC4wLjE4NTAxMzg5ODk." },
                    { 61, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_232539.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_232539.jpg" },
                    { 62, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/1/5/1503010390462-_3_.jpg?_gl=1*1l407rd*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTgzOC41OC4wLjE4NTAxMzg5ODk.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19, "https://cdn0.fahasa.com/media/catalog/product/1/5/1503010390462-_3_.jpg?_gl=1*1l407rd*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE5MDc2OS4yLjEuMTcxODE5MTgzOC41OC4wLjE4NTAxMzg5ODk." },
                    { 63, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_42015.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_42015.jpg" },
                    { 64, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_42015.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20, "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_42015.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionValues",
                columns: new[] { "ProductOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "OptionId", "ThumbnailImageUrl", "Value" },
                values: new object[,]
                {
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310124.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310124.jpg", "WHL202" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310117.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240310117.jpg", "WHL201" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240311008.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240311008.jpg", "KSXST" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240318007.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240318007.jpg", "MRTSST" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240311183.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/6/9/6920240311183.jpg", "KPBL" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ProductOptionId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "IsOptionWithImage", "LastEditedBy", "LastEditedWhen", "Name", "ProductId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, true, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tập", 3 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, true, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Phần", 6 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, true, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tập", 7 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, true, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Màu sắc", 16 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeProductValues",
                columns: new[] { "ProductTypeAttributeProductValueId", "AttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 3, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 5, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 6, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 7, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 8, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 9, 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 10, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 11, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 12, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 13, 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 14, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 15, 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 26, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6 },
                    { 27, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6 },
                    { 28, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6 },
                    { 29, 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6 },
                    { 30, 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6 },
                    { 31, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7 },
                    { 32, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7 },
                    { 33, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7 },
                    { 34, 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7 },
                    { 35, 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7 },
                    { 36, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8 },
                    { 37, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8 },
                    { 38, 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8 },
                    { 39, 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8 },
                    { 40, 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8 },
                    { 41, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9 },
                    { 42, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9 },
                    { 43, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9 },
                    { 44, 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9 },
                    { 45, 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9 },
                    { 46, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10 },
                    { 47, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10 },
                    { 48, 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10 },
                    { 49, 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10 },
                    { 50, 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10 },
                    { 51, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11 },
                    { 52, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11 },
                    { 53, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11 },
                    { 54, 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11 },
                    { 55, 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11 },
                    { 56, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12 },
                    { 57, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12 },
                    { 58, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12 },
                    { 59, 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12 },
                    { 60, 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12 },
                    { 61, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13 },
                    { 62, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13 },
                    { 63, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13 },
                    { 64, 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13 },
                    { 65, 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13 },
                    { 66, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14 },
                    { 67, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14 },
                    { 68, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14 },
                    { 69, 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14 },
                    { 70, 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14 },
                    { 71, 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15 },
                    { 72, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15 },
                    { 73, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15 },
                    { 74, 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15 },
                    { 75, 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15 },
                    { 76, 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16 },
                    { 77, 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16 },
                    { 78, 34, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16 },
                    { 79, 35, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16 },
                    { 80, 36, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16 },
                    { 81, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17 },
                    { 82, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17 },
                    { 83, 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17 },
                    { 84, 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17 },
                    { 85, 38, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17 },
                    { 86, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18 },
                    { 87, 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18 },
                    { 88, 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18 },
                    { 89, 39, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18 },
                    { 90, 40, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18 },
                    { 91, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19 },
                    { 92, 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19 },
                    { 93, 39, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19 },
                    { 94, 41, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19 },
                    { 95, 42, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19 },
                    { 96, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20 },
                    { 97, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20 },
                    { 98, 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20 },
                    { 99, 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20 },
                    { 100, 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20 }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "CreatedBy", "CreatedWhen", "CustomerId", "LastEditedBy", "LastEditedWhen", "Quantity", "SkuId" },
                values: new object[,]
                {
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 11 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "Skus",
                columns: new[] { "SkuId", "Dimension_Height", "Dimension_Length", "Dimension_Width", "Barcode", "Comment", "CreatedBy", "CreatedWhen", "DeletedWhen", "DiscontinuedWhen", "IsActive", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "Quantity", "RecommendedRetailPrice", "Status", "Tags", "TaxRate", "UnitPrice", "ValidFrom", "ValidTo", "Weight", "SkuValue_Value" },
                values: new object[,]
                {
                    { 1, 14m, 0.3m, 20.5m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 20, 49000m, "InStock", " ", 0m, 39200m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00001" },
                    { 2, 20.5m, 0.5m, 14.5m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 20, 75000m, "InStock", " ", 0m, 60000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 130, "SKU00002" },
                    { 3, 22.0m, 0.5m, 12.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 20, 25000m, "InStock", " ", 0m, 22500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 80, "SKU00003" },
                    { 4, 22.0m, 0.5m, 12.0m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 20, 25000m, "InStock", " ", 0m, 22500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 80, "SKU00004" },
                    { 5, 22.0m, 0.5m, 12.0m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 0, 25000m, "OutOfStock", " ", 0m, 22500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 80, "SKU00005" },
                    { 6, 22.0m, 0.5m, 12.0m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 0, 25000m, "OutOfStock", " ", 0m, 22500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 80, "SKU00006" },
                    { 7, 22.0m, 0.5m, 12.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 20, 25000m, "InStock", " ", 0m, 22500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 80, "SKU00007" },
                    { 8, 22.0m, 0.5m, 12.0m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 20, 25000m, "InStock", " ", 0m, 22500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 80, "SKU00008" },
                    { 15, 20.5m, 0.4m, 18.5m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 15, 42000m, "InStock", " ", 0m, 33600m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 133, "SKU00015" },
                    { 16, 20.5m, 0.4m, 18.5m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 20, 42000m, "InStock", " ", 0m, 33600m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 133, "SKU00016" },
                    { 17, 20.5m, 0.4m, 18.5m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 10, 42000m, "InStock", " ", 0m, 33600m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 133, "SKU00017" },
                    { 18, 20.5m, 0.4m, 18.5m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 20, 42000m, "InStock", " ", 0m, 33600m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 133, "SKU00018" },
                    { 19, 20.5m, 0.4m, 18.5m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 20, 42000m, "InStock", " ", 0m, 33600m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 133, "SKU00019" },
                    { 20, 17.6m, 1m, 11.3m, " ", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 20, 22000m, "InStock", " ", 0m, 20900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00020" },
                    { 21, 17.6m, 1m, 11.3m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 32, 22000m, "InStock", " ", 0m, 20900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00021" },
                    { 22, 17.6m, 1m, 11.3m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 14, 22000m, "InStock", " ", 0m, 20900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00022" },
                    { 23, 17.6m, 1m, 11.3m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 26, 22000m, "InStock", "", 0m, 20900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00023" },
                    { 24, 17.6m, 1m, 11.3m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 15, 22000m, "InStock", "", 0m, 20900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00024" },
                    { 25, 17.6m, 1m, 11.3m, " ", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 18, 22000m, "InStock", "", 0m, 20900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00025" },
                    { 26, 24.0m, 6.0m, 17.0m, "", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 19, 179000m, "InStock", "", 0m, 125300m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 2500, "SKU00026" },
                    { 27, 24.0m, 2.1m, 16.0m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, 21, 1390000m, "InStock", "", 0m, 97300m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 450, "SKU00027" },
                    { 28, 20.0m, 0.5m, 14.5m, "", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, 10, 145000m, "InStock", "", 0m, 101500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 350, "SKU00028" },
                    { 29, 24.0m, 1.4m, 16.0m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, 5, 119000m, "InStock", "", 0m, 95200m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 300, "SKU00029" },
                    { 30, 20.5m, 1.4m, 14.5m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, 15, 124000m, "InStock", "", 0m, 83080m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 320, "SKU00030" },
                    { 31, 20.0m, 1.6m, 14.5m, "", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, 0, 139000m, "OutOfStock", "", 0m, 97300m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 340, "SKU00031" },
                    { 32, 20.5m, 1m, 14m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, 6, 89000m, "InStock", "", 0m, 62300m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 220, "SKU00032" },
                    { 33, 20.5m, 2.5m, 13.0m, "", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, 8, 158000m, "InStock", "", 0m, 126400m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 415, "SKU00033" },
                    { 34, 10.5m, 1.5m, 5m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16, 50, 33000m, "InStock", "", 0m, 31350m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 10, "SKU00034" },
                    { 35, 10.5m, 1.5m, 5m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16, 40, 33000m, "InStock", "", 0m, 31350m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 10, "SKU00035" },
                    { 36, 10.5m, 1.5m, 5m, "", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16, 25, 33000m, "InStock", "", 0m, 31350m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 10, "SKU00036" },
                    { 37, 5m, 2m, 5m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, 100, 6000m, "InStock", "", 0m, 5400m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 195, "SKU00037" },
                    { 38, 15m, 2m, 2m, "", "Not Bad", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, 50, 37500m, "InStock", "", 0m, 33750m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 54, "SKU00038" },
                    { 39, 6.5m, 2m, 6.5m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19, 40, 13000m, "InStock", "", 0m, 11700m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 55, "SKU00039" },
                    { 40, 8m, 8m, 8m, "", "Good", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20, 15, 16500m, "InStock", "", 0m, 14850m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 1, 0, 0)), null, 250, "SKU00040" }
                });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "OrderLineId", "CreatedBy", "CreatedWhen", "DiscountVoucherId", "LastEditedBy", "LastEditedWhen", "OrderId", "PickingCompletedWhen", "Quantity", "SkuId", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2023, 2, 22, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 3, 4, 31500m },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2023, 2, 22, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 1, 2, 31500m },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2023, 8, 15, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 1, 8, 18000m },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(2022, 12, 17, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 8, 8, 18000m },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(2022, 12, 17, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 5, 2, 31500m },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 6, 2, 31500m },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 18000m },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, null, 5, 8, 18000m },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, null, 6, 5, 18000m },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(2023, 9, 26, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 2, 1, 31500m },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, null, 5, 6, 18000m },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, null, 6, 4, 31500m },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, null, 7, 3, 31500m },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, new DateTimeOffset(new DateTime(2024, 2, 29, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 6, 7, 18000m },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, new DateTimeOffset(new DateTime(2024, 2, 29, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 2, 4, 31500m },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 10, 3, 31500m },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 7, 8, 18000m },
                    { 28, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, new DateTimeOffset(new DateTime(2022, 6, 20, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), 3, 2, 31500m },
                    { 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, new DateTimeOffset(new DateTime(2022, 6, 20, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 2, 4, 31500m },
                    { 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16, new DateTimeOffset(new DateTime(2024, 3, 28, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 6, 2, 31500m },
                    { 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 31500m },
                    { 34, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 1, 6, 18000m },
                    { 35, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, null, 5, 5, 18000m },
                    { 36, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, null, 3, 7, 18000m },
                    { 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19, new DateTimeOffset(new DateTime(2023, 9, 1, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), 7, 4, 31500m },
                    { 38, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20, new DateTimeOffset(new DateTime(2023, 12, 3, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), 9, 3, 31500m }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionValues",
                columns: new[] { "ProductOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "OptionId", "ThumbnailImageUrl", "Value" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/4/z4113307553305_aa5e027926d792b10f.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/z/4/z4113307553305_aa5e027926d792b10f.jpg", "Tập 1" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn-phienbanmoi-tap2-1165_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn-phienbanmoi-tap2-1165_1.jpg", "Tập 2" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmnpbm3.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmnpbm3.jpg", "Tập 3" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn_phienbanmoi_tap4.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn_phienbanmoi_tap4.jpg", "Tập 4" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn_phienbanmoi_bia_tap5.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn_phienbanmoi_bia_tap5.jpg", "Tập 5" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn-phienbanmoi-tap6-364.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/l/h/lhmn-phienbanmoi-tap6-364.jpg", "Tập 6" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/frame-1.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/frame-1.png", "Bố Mẹ Luôn Yêu Con" },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/frame-1.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/frame-1.png", "Con Từ Đâu Tới?" },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___to_dung_cam_noi_khong/2023_04_05_15_26_30_1-390x510.jpg?_gl=1*va315b*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___to_dung_cam_noi_khong/2023_04_05_15_26_30_1-390x510.jpg?_gl=1*va315b*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU.", "Tớ Dũng Cảm Nói “Không\"" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/frame-1.png", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/frame-1.png", "Nguy Hiểm Đấy, Mau Tránh Xa" },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___khong_duoc_cham_vao_vung_rieng_tu_cua_to/2023_04_05_15_26_37_1-390x510.jpg?_gl=1*va315b*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU.", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/giao_duc_dau_doi_cho_tre___nhung_bai_hoc_tu_bao_ve_ban_than___khong_duoc_cham_vao_vung_rieng_tu_cua_to/2023_04_05_15_26_37_1-390x510.jpg?_gl=1*va315b*_gcl_au*MTIyMzg2NjI3OS4xNzE4MTg1MzA5*_ga*ODQ0Njg1MjIwLjE3MTgxODUzMDk.*_ga_460L9JMC2G*MTcxODE4NTMwOS4xLjEuMTcxODE4NjA3MS40My4wLjIxMTI2OTY3NzU.", "Không Được Chạm Vào Vùng Riêng Tư Của Tớ" },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet_tap-1_tb-2023.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet_tap-1_tb-2023.jpg", "Tập 1" },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet---tap-2---tb-2023.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet---tap-2---tb-2023.jpg", "Tập 2" },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet---tap-3---tb-2023.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet---tap-3---tb-2023.jpg", "Tập 3" },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet_tap-4_tb-2023.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet_tap-4_tb-2023.jpg", "Tập 4" },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935244890648_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935244890648_1.jpg", "Tập 5" },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet---tap-6---tb-2023.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/d/o/doi-quan-doraemon-dac-biet---tap-6---tb-2023.jpg", "Tập 6" },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-mau1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-mau1.jpg", "Vàng" },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-mau3.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-mau3.jpg", "Nâu" },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-mau1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/8/8/8801237897492-mau1.jpg", "Trắng" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingId", "Comment", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductId", "RatingValue", "ReportedCount", "Response", "SkuId", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "Good Product!", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 5, 0, "Thank you for your compliment!", 1, "Approved", 4 },
                    { 2, "Amazing thing, but need some upgrades", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 4, 0, "Thank you for your compliment!", 1, "Approved", 5 },
                    { 3, "Good Product!", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 5, 0, "Thank you for your compliment!", 2, "Approved", 4 },
                    { 4, "Hơi tệ một chút", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 1, 0, "Thank you for your compliment!", 3, "Approved", 5 },
                    { 5, "Good Product!", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, 5, 0, "Thank you for your compliment!", 4, "Approved", 4 }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "CreatedBy", "CreatedWhen", "CustomerId", "LastEditedBy", "LastEditedWhen", "Quantity", "SkuId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 3 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "SkuOptionValues",
                columns: new[] { "SkuOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "OptionValueId", "SkuId" },
                values: new object[,]
                {
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 7, 9 },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 8, 10 },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 9, 11 },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 10, 12 },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 11, 13 }
                });

            migrationBuilder.InsertData(
                table: "RatingLikes",
                columns: new[] { "RatingLikeId", "Liked", "LikedWhen", "RatingId", "UserId" },
                values: new object[,]
                {
                    { 1, true, new DateTimeOffset(new DateTime(2024, 5, 27, 7, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1, 5 },
                    { 2, true, new DateTimeOffset(new DateTime(2024, 5, 26, 7, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "SkuOptionValues",
                columns: new[] { "SkuOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "OptionValueId", "SkuId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 1, 3 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, 4 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, 5 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 4, 6 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 5, 7 },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 6, 8 },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 12, 15 },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 13, 16 },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 14, 17 },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 15, 18 },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 16, 19 },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 17, 20 },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 18, 21 },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 19, 22 },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 20, 23 },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 21, 24 },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 22, 25 },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 23, 34 },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 24, 35 },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 25, 36 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedBy",
                table: "AspNetUsers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LastEditedBy",
                table: "AspNetUsers",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedBy",
                table: "Authors",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_LastEditedBy",
                table: "Authors",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_CreatedBy",
                table: "BookAuthors",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_LastEditedBy",
                table: "BookAuthors",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_ProductId_AuthorId",
                table: "BookAuthors",
                columns: new[] { "ProductId", "AuthorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMethods_CreatedBy",
                table: "DeliveryMethods",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMethods_LastEditedBy",
                table: "DeliveryMethods",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMethods_Name",
                table: "DeliveryMethods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_CreatedBy",
                table: "DiscountApplyToProductTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_DiscountVoucherId",
                table: "DiscountApplyToProductTypes",
                column: "DiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_DiscountVoucherId1",
                table: "DiscountApplyToProductTypes",
                column: "DiscountVoucherId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_LastEditedBy",
                table: "DiscountApplyToProductTypes",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_ProductTypeId",
                table: "DiscountApplyToProductTypes",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_CreatedBy",
                table: "DiscountVouchers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_LastEditedBy",
                table: "DiscountVouchers",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_CreatedBy",
                table: "OrderLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_DiscountVoucherId",
                table: "OrderLines",
                column: "DiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_LastEditedBy",
                table: "OrderLines",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_SkuId",
                table: "OrderLines",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LastEditedBy",
                table: "Orders",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PriceDiscountVoucherId",
                table: "Orders",
                column: "PriceDiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingDiscountVoucherId",
                table: "Orders",
                column: "ShippingDiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_CreatedBy",
                table: "PaymentMethods",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_LastEditedBy",
                table: "PaymentMethods",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Name",
                table: "PaymentMethods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CreatedBy",
                table: "ProductImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_LastEditedBy",
                table: "ProductImages",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_CreatedBy",
                table: "ProductOptions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_LastEditedBy",
                table: "ProductOptions",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_ProductId",
                table: "ProductOptions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValues_CreatedBy",
                table: "ProductOptionValues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValues_LastEditedBy",
                table: "ProductOptionValues",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValues_OptionId",
                table: "ProductOptionValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistories_CreatedBy",
                table: "ProductPriceHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistories_LastEditedBy",
                table: "ProductPriceHistories",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistories_SkuId",
                table: "ProductPriceHistories",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LastEditedBy",
                table: "Products",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitMeasureId",
                table: "Products",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_CreatedBy",
                table: "ProductTypeAttributeMappings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_LastEditedBy",
                table: "ProductTypeAttributeMappings",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_ProductAttributeId",
                table: "ProductTypeAttributeMappings",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_ProductTypeId",
                table: "ProductTypeAttributeMappings",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValues_AttributeValueId",
                table: "ProductTypeAttributeProductValues",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValues_CreatedBy",
                table: "ProductTypeAttributeProductValues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValues_LastEditedBy",
                table: "ProductTypeAttributeProductValues",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValues_ProductId_AttributeValueId",
                table: "ProductTypeAttributeProductValues",
                columns: new[] { "ProductId", "AttributeValueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_CreatedBy",
                table: "ProductTypeAttributes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_LastEditedBy",
                table: "ProductTypeAttributes",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_Name",
                table: "ProductTypeAttributes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_CreatedBy",
                table: "ProductTypeAttributeValues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_LastEditedBy",
                table: "ProductTypeAttributeValues",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_ProductTypeAttributeId_Value",
                table: "ProductTypeAttributeValues",
                columns: new[] { "ProductTypeAttributeId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_CreatedBy",
                table: "ProductTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_DisplayName",
                table: "ProductTypes",
                column: "DisplayName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_LastEditedBy",
                table: "ProductTypes",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ParentProductTypeId",
                table: "ProductTypes",
                column: "ParentProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingLikes_RatingId",
                table: "RatingLikes",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingLikes_UserId",
                table: "RatingLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CreatedBy",
                table: "Ratings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_LastEditedBy",
                table: "Ratings",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SkuId",
                table: "Ratings",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefAddressTypes_CreatedBy",
                table: "RefAddressTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefAddressTypes_LastEditedBy",
                table: "RefAddressTypes",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefAddressTypes_Name",
                table: "RefAddressTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_AddressTypeId",
                table: "ShippingAddresses",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_CreatedBy",
                table: "ShippingAddresses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_LastEditedBy",
                table: "ShippingAddresses",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CreatedBy",
                table: "ShoppingCartItems",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CustomerId",
                table: "ShoppingCartItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_LastEditedBy",
                table: "ShoppingCartItems",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_SkuId",
                table: "ShoppingCartItems",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_CreatedBy",
                table: "SkuOptionValues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_LastEditedBy",
                table: "SkuOptionValues",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_OptionId",
                table: "SkuOptionValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_OptionValueId",
                table: "SkuOptionValues",
                column: "OptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuOptionValues_SkuId_OptionId_OptionValueId",
                table: "SkuOptionValues",
                columns: new[] { "SkuId", "OptionId", "OptionValueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skus_CreatedBy",
                table: "Skus",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Skus_LastEditedBy",
                table: "Skus",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Skus_ProductId",
                table: "Skus",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasures_CreatedBy",
                table: "UnitMeasures",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasures_LastEditedBy",
                table: "UnitMeasures",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasures_Name",
                table: "UnitMeasures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsages_OrderId",
                table: "VoucherUsages",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsages_UserId",
                table: "VoucherUsages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsages_VoucherId",
                table: "VoucherUsages",
                column: "VoucherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "DiscountApplyToProductTypes");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductPriceHistories");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributeMappings");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributeProductValues");

            migrationBuilder.DropTable(
                name: "RatingLikes");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "SkuOptionValues");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "VoucherUsages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributeValues");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ProductOptionValues");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributes");

            migrationBuilder.DropTable(
                name: "Skus");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "DiscountVouchers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RefAddressTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "UnitMeasures");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
