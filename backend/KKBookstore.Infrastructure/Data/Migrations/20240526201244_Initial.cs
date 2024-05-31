using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                name: "DiscountVouchers",
                columns: table => new
                {
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    DiscountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityRange_MinApplyQuantity = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: false),
                    QuantityRange_MaxApplyQuantity = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: true),
                    ValueRange_MinValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValueRange_MaxValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StartWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_DiscountVouchers_Products_ProductId",
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
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "ProductOptionValues",
                columns: table => new
                {
                    ProductOptionValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    ShoppingCartItemId1 = table.Column<int>(type: "int", nullable: false),
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
                name: "SkuImages",
                columns: table => new
                {
                    SkuImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SkuImages", x => x.SkuImageId);
                    table.ForeignKey(
                        name: "FK_SkuImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuImages_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuImages_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
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
                    { 1, 0, "c9f837c2-b1d4-4297-c035-e92856db3g51", null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3007), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "admin@example.com", true, "Admin", null, true, true, false, false, null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(2677), new TimeSpan(0, 7, 0, 0, 0)), "Admin", true, null, "Email", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHQaoHEMQVEH/qjlWW/O9BGZnwoDNG6EVFTDCv3HKIow9U5vYEMMl0qupqy/qdlJHQ==", "1234567890", true, "XJH4QW2N8GZRMLV5KF3Y7PCT9USB6ADO", "Active", false, "admin@example.com", null },
                    { 2, 0, "d3e126c2-b2d3-4398-b024-e97372db4f60", null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3553), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1985, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "salesstaff@example.com", true, "Sales", null, true, false, false, true, null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3548), new TimeSpan(0, 7, 0, 0, 0)), "Staff", true, null, "Email", "SALESSTAFF@EXAMPLE.COM", "SALESSTAFF@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMTcI3vU09hi1/kGE0O6c8pDdCGGk5o8stlAtublrbdvC5zHjVLYjaH1vgtWVpbHfg==", "0987654321", true, "LV9Q8G6ZRN2KM4PSTXJY5W3FB7HOCD1U", "Active", false, "salesstaff@example.com", null },
                    { 3, 0, "e4f036c3-b2d3-4398-c034-e98272db4f61", null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3555), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1992, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "customercare@example.com", true, "Customer Care", null, true, false, false, true, null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3554), new TimeSpan(0, 7, 0, 0, 0)), "Staff", true, null, "Email", "CUSTOMERCARE@EXAMPLE.COM", "CUSTOMERCARE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEM5LNDzxoNJkMvgmwBhKEx4P55H9KiMu315NychC+Zjx1omBzpfZP0xo1VKxLpSQ6Q==", "1122334455", true, "P5YHXJLZMO9V3G2KTRN4S8QWCFUB7D1A", "Active", false, "customercare@example.com", null },
                    { 4, 0, "f4e026b2-b1d3-4198-a024-e97272da3f50", null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3557), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2003, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "trungkien2003ntk@gmail.com", true, "Trung Kiên", null, true, true, false, false, null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3556), new TimeSpan(0, 7, 0, 0, 0)), "Nguyễn", true, null, "Email", "TRUNGKIEN2003NTK@GMAIL.COM", "TRUNGKIEN2003NTK@GMAIL.COM", "AQAAAAIAAYagAAAAEC9VwMExDNtN5XlQXvAG59ODtlcxL2YLZGF5HcrbMChOLX/nJ51inLM2a8cMI81+Sw==", "0866919340", true, "SENTMLQ2I6NCSBGSCLVXP4UOHGJF4G66", "Active", false, "trungkien2003ntk@gmail.com", null },
                    { 5, 0, "f0klsm1m-b1d6-4496-d036-e96378ef3g67", null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3558), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2003, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "truykichtk20031@gmail.com", true, "Tiêm Kích", null, true, true, false, false, null, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3557), new TimeSpan(0, 7, 0, 0, 0)), "Trần", true, null, "Email", "TRUYKICHTK20031@GMAIL.COM", "TRUYKICHTK20031@GMAIL.COM", null, "0939199946", true, "MGJSHVQ2IJ28SKMVCLVXP4LAM8J2KJ5O", "Active", false, "truykichtk20031@gmail.com", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Alison Ritchie", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Alison Ritchie" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Alison Edgson", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Alison Edgson" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Phan Thị", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Phan Thị" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Alison Edgson", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Alison Edgson" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Nhiều tác giả", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Nhiều tác giả" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Bộ Giáo Dục Và Đào Tạo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Bộ Giáo Dục Và Đào Tạo" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "DeliveryMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(8992), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng tiêu chuẩn", false, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(9142), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng tiêu chuẩn" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(9146), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng nhanh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(9147), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng nhanh" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2423), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán khi nhận hàng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2437), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán khi nhận hàng" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2440), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán qua thẻ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán qua thẻ" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributes",
                columns: new[] { "ProductTypeAttributeId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Hình thức bìa" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "NXB" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Năm xuất bản" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Ngôn ngữ" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Số trang" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Thương hiệu" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Nơi sản xuất" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Chất liệu" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Độ tuổi" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Lớp" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Cấp học" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách viết bằng tiếng Việt", "Sách Tiếng Việt", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "sach-tieng-viet" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại văn phòng phẩm như bút, viết, tập vở", "Văn Phòng Phẩm", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "van-phong-pham" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Đồ chơi các loại", "Đồ Chơi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, null, "do-choi" }
                });

            migrationBuilder.InsertData(
                table: "RefAddressTypes",
                columns: new[] { "RefAddressTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4221), new TimeSpan(0, 7, 0, 0, 0)), null, "Nhà riêng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4229), new TimeSpan(0, 7, 0, 0, 0)), "Nhà riêng" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4232), new TimeSpan(0, 7, 0, 0, 0)), null, "Văn phòng", false, 2, new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4233), new TimeSpan(0, 7, 0, 0, 0)), "Văn phòng" }
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
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 2 },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 2 },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 2 },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, 3 },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, 3 },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, 3 },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeValues",
                columns: new[] { "ProductTypeAttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductTypeAttributeId", "Value" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "Bìa mềm" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Hà Nội" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2019" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "Tiếng Việt" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "28" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Văn Học" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "30" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Đại Học Quốc Gia Hà Nội" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2024" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "178" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Giáo Dục Việt Nam" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "2023" },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "7" },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "Cấp 2" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "0" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách Thiếu Nhi", "Thiếu Nhi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "thieu-nhi" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại SGK và sách tham khảo", "Giáo Khoa - Tham Khảo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "giao-khoa-tham-khao" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách Manga và Comic", "Manga - Comic", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "manga-comic" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại bút viết", "Bút - Viết", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 2, "but-viet" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại dụng cụ học sinh dùng ở trường", "Dụng Cụ Học Sinh", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 2, "dung-cu-hoc-sinh" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại bút viết", "Sản Phẩm Về Giấy", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 2, "san-pham-ve-giay" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại thú bông và búp bê", "Búp Bê - Thú Bông", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 3, "bup-be-thu-bong" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại mô hình lắp ghép", "Mô Hình", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 3, "mo-hinh" },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại board game", "Board Game", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 3, "board-game" }
                });

            migrationBuilder.InsertData(
                table: "ShippingAddresses",
                columns: new[] { "ShippingAddressId", "AddressTypeId", "Commune", "CreatedBy", "CreatedWhen", "DeletedWhen", "DetailAddress", "District", "IsDefault", "IsDeleted", "LastEditedBy", "LastEditedWhen", "PhoneNumber", "Province", "ReceiverName", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Xã Đức Hòa Hạ", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Số nhà 394", "Huyện Đức Hòa", false, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "0866919340", "Long An", "Nguyễn Trung Kiên", 1 },
                    { 2, 2, "Phường Linh Trung", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Ktx khu A, ĐHQG", "Quận Thủ Đức", true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "0866919340", "Hồ Chí Minh", "Nguyễn Trung Kiên", 1 },
                    { 3, 1, "Xã Đức Hòa Thượng", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Chung cư 977", "Huyện Đức Hòa", true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "0866919340", "Long An", "Nguyễn Truy Kích", 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Comment", "ConfirmedDeliveryWhen", "ConfirmedReceivedWhen", "CreatedBy", "CreatedWhen", "CustomerId", "DeliveryInstruction", "DeliveryMethodId", "DiscountVoucherId", "DueWhen", "ExpectedDeliveryWhen", "LastEditedBy", "LastEditedWhen", "OrderNumber", "OrderWhen", "PaidWhen", "PaymentMethodId", "PickingCompletedWhen", "ShippingAddressId", "Status", "Subtotal", "TaxRate" },
                values: new object[,]
                {
                    { 1, "Careful", new DateTimeOffset(new DateTime(2023, 2, 23, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, null, new DateTimeOffset(new DateTime(2023, 2, 26, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-2-21-92ab8", new DateTimeOffset(new DateTime(2023, 2, 21, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2023, 2, 22, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 1, "Delivered", 126000m, 0m },
                    { 2, "Careful", new DateTimeOffset(new DateTime(2023, 8, 16, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 20, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2023, 8, 19, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-8-14-b3e0c", new DateTimeOffset(new DateTime(2023, 8, 14, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2023, 8, 15, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 1, "Received", 18000m, 0m },
                    { 3, "Careful", new DateTimeOffset(new DateTime(2022, 12, 18, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2022, 12, 21, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-12-16-fe174", new DateTimeOffset(new DateTime(2022, 12, 16, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2022, 12, 17, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 1, "Delivered", 301500m, 0m },
                    { 4, "Careful", new DateTimeOffset(new DateTime(2024, 4, 15, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2024, 4, 18, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-4-13-2f823", new DateTimeOffset(new DateTime(2024, 4, 13, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 1, "Delivered", 754800m, 0m },
                    { 5, "Careful", new DateTimeOffset(new DateTime(2022, 12, 13, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, null, new DateTimeOffset(new DateTime(2022, 12, 16, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-12-11-ba604", new DateTimeOffset(new DateTime(2022, 12, 11, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 1, "Shipped", 541600m, 0m },
                    { 6, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2022, 3, 4, 11, 53, 20, 558, DateTimeKind.Unspecified).AddTicks(7420), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-2-27-7b23f", new DateTimeOffset(new DateTime(2022, 2, 27, 11, 53, 20, 558, DateTimeKind.Unspecified).AddTicks(7420), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 1, "Pending", 198000m, 0m },
                    { 7, "Careful", new DateTimeOffset(new DateTime(2023, 9, 27, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 10, 1, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2023, 9, 30, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-9-25-74459", new DateTimeOffset(new DateTime(2023, 9, 25, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2023, 9, 26, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 1, "Received", 286200m, 0m },
                    { 8, "Careful", new DateTimeOffset(new DateTime(2024, 1, 28, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2024, 1, 31, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-1-26-19f7f", new DateTimeOffset(new DateTime(2024, 1, 26, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2024, 1, 27, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), 1, "Delivered", 1020000m, 0m },
                    { 9, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2024, 2, 20, 9, 36, 58, 867, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-2-15-c75f4", new DateTimeOffset(new DateTime(2024, 2, 15, 9, 36, 58, 867, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, 7, 0, 0, 0)), null, 1, null, 1, "Processing", 499500m, 0m },
                    { 10, "Careful", new DateTimeOffset(new DateTime(2024, 3, 1, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2024, 3, 4, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-2-28-44c33", new DateTimeOffset(new DateTime(2024, 2, 28, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2024, 2, 29, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 1, "Delivered", 171000m, 0m },
                    { 11, "Careful", new DateTimeOffset(new DateTime(2023, 7, 11, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 1, null, null, new DateTimeOffset(new DateTime(2023, 7, 14, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-7-9-6de5b", new DateTimeOffset(new DateTime(2023, 7, 9, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 1, "Shipped", 1461000m, 0m },
                    { 12, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, null, new DateTimeOffset(new DateTime(2024, 2, 23, 20, 41, 11, 372, DateTimeKind.Unspecified).AddTicks(2310), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-2-18-7040f", new DateTimeOffset(new DateTime(2024, 2, 18, 20, 41, 11, 372, DateTimeKind.Unspecified).AddTicks(2310), new TimeSpan(0, 7, 0, 0, 0)), null, 2, null, 1, "Processing", 2157900m, 0m },
                    { 13, "Careful", new DateTimeOffset(new DateTime(2022, 6, 21, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, null, new DateTimeOffset(new DateTime(2022, 6, 24, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-6-19-05eb3", new DateTimeOffset(new DateTime(2022, 6, 19, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2022, 6, 20, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), 1, "Shipped", 94500m, 0m },
                    { 14, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2022, 2, 11, 20, 54, 18, 599, DateTimeKind.Unspecified).AddTicks(7440), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-2-6-d0d99", new DateTimeOffset(new DateTime(2022, 2, 6, 20, 54, 18, 599, DateTimeKind.Unspecified).AddTicks(7440), new TimeSpan(0, 7, 0, 0, 0)), null, 2, null, 1, "Pending", 1190000m, 0m },
                    { 15, "Careful", new DateTimeOffset(new DateTime(2022, 6, 21, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2022, 6, 25, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2022, 6, 24, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-6-19-d7f83", new DateTimeOffset(new DateTime(2022, 6, 19, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2022, 6, 20, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 1, "Received", 63000m, 0m },
                    { 16, "Careful", new DateTimeOffset(new DateTime(2024, 3, 29, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 2, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "", 2, null, null, new DateTimeOffset(new DateTime(2024, 4, 1, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2024-3-27-3d9be", new DateTimeOffset(new DateTime(2024, 3, 27, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2024, 3, 28, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 1, "Received", 189000m, 0m },
                    { 17, "Careful", new DateTimeOffset(new DateTime(2022, 1, 14, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2022, 1, 18, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2022, 1, 17, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2022-1-12-aa39a", new DateTimeOffset(new DateTime(2022, 1, 12, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 1, "Received", 311400m, 0m },
                    { 18, "Careful", null, null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 1, null, null, new DateTimeOffset(new DateTime(2023, 10, 18, 5, 51, 13, 33, DateTimeKind.Unspecified).AddTicks(9820), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-10-13-a2695", new DateTimeOffset(new DateTime(2023, 10, 13, 5, 51, 13, 33, DateTimeKind.Unspecified).AddTicks(9820), new TimeSpan(0, 7, 0, 0, 0)), null, 2, null, 1, "Processing", 144000m, 0m },
                    { 19, "Careful", new DateTimeOffset(new DateTime(2023, 9, 2, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2023, 9, 5, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-8-31-e96f3", new DateTimeOffset(new DateTime(2023, 8, 31, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), null, 1, new DateTimeOffset(new DateTime(2023, 9, 1, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), 1, "Delivered", 220500m, 0m },
                    { 20, "Careful", new DateTimeOffset(new DateTime(2023, 12, 4, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "", 2, null, null, new DateTimeOffset(new DateTime(2023, 12, 7, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "2023-12-2-53b91", new DateTimeOffset(new DateTime(2023, 12, 2, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), null, 2, new DateTimeOffset(new DateTime(2023, 12, 3, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), 1, "Shipped", 283500m, 0m }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại truyện thiếu nhi", "Truyện Thiếu Nhi", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 4, "truyen-thieu-nhi" },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách tô màu và luyện chữ cho trẻ em", "Tô Màu - Luyện Chữ", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 4, "to-mau-luyen-chu" },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách tham khảo dành cho học sinh, sinh viên", "Sách Tham Khảo", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 5, "sach-tham-khao" },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách giáo khoa dành cho học sinh", "Sách Giáo Khoa", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 5, "sach-giao-khoa" },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách Manga", "Manga", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 6, "manga" },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại sách Comic", "Comic", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 6, "comic" },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại bút chì", "Bút Chì", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 7, "but-chi" },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại bút mực và bút máy", "Bút Mực - Bút Máy", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 7, "but-muc-but-may" },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại gôm và tẩy", "Gôm - Tẩy", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 8, "gom-tay" },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại thú bông", "Thú Bông", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 10, "thu-bong" },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Các loại búp bê", "Búp Bê", false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 10, "bup-be" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeMappings",
                columns: new[] { "ProductTypeAttributeMappingId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductAttributeId", "ProductTypeId" },
                values: new object[,]
                {
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, 16 },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, 16 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsActive", "IsBook", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductTypeId", "UnitMeasureId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Gia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.\r\n\r\nMã hàng	8935212367677\r\nNgày Dự Kiến Phát Hành	20/05/2024\r\nTên Nhà Cung Cấp	Đinh Tị\r\nTác giả	Alison Ritchie, Alison Edgson\r\nNgười Dịch	Thành Đạt\r\nNXB	Hà Nội\r\nNăm XB	2024\r\nTrọng lượng (gr)	100\r\nKích Thước Bao Bì	25 x 20.5 x 0.2 cm\r\nSố trang	28\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nGia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Gia Đình Thương Yêu", 13, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Tô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.\r\n\r\nMã hàng	8936071294357\r\nTên Nhà Cung Cấp	Cty Phan Thị\r\nTác giả	Phan Thị\r\nNXB	Văn Học\r\nNăm XB	2024\r\nTrọng lượng (gr)	150\r\nKích Thước Bao Bì	24 x 16 x 0.5 cm\r\nSố trang	30\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Tô màu, luyện chữ bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nTô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tô Màu Bốn Mùa", 14, 1 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)\r\n\r\nBản đồ là phương tiện giảng dạy và học tập điạ lý không thể thiếu trong nhà trường phổ thông. Cùng với sách giáo khoa, Atlat địa lí Việt Nam là nguồn cung cấp kiến thức, thông tin tổng hợp và hệ thống giúp giáo viên đổi mới phương pháp dạy học, hỗ trợ học.\r\n\r\nĐể đáp ứng nhu cầu bức thiết đó, NXB Giáo dục Việt Nam trân trọng giới thiệu tập Atlat địa lý Việt Nam gồm 21 bản đồ, được in màu rõ nét, liên quan đến các lĩnh vực kinh tế - xã hội. Các bản đồ được xây dựng theo nguồn số liệu của Nhà xuất bản thống kê - Tổng cục thống kê. Đây là tài liệu được phép mang vào phòng thi tốt nghiệp THPT môn Địa lý do Bộ Giáo dục và Đào tạo quy định.\r\n\r\nNội dung gồm có:\r\n\r\n1. Kí hiệu chung\r\n\r\n2. Hành chính 4. Hình thể\r\n\r\n4. Địa khoáng sản\r\n\r\n5. Khí hậu\r\n\r\n6. Các hệ thống sông\r\n\r\n7. Các nhóm và các loại đât chính\r\n\r\n8. Thực vật và động vật\r\n\r\n9. Các miền tự nhiên\r\n\r\n11. Dân số\r\n\r\n11. Dân tộc\r\n\r\n12. Kinh tế chung\r\n\r\n14. Nông nghiệp chung\r\n\r\n14. Lâm nông và thuỷ sản\r\n\r\n15. Công nghiệp chung\r\n\r\n16. Các ngành công nghiệp trọng điểm\r\n\r\n17. Giao thông\r\n\r\n18. Thương mại\r\n\r\n19. Du lịch\r\n\r\n21. Vùng trunh du và miền núi Bắc Bộ, vùn Đồng Bằng Sông Hồng\r\n\r\n21. Vùng Bắc Trung Bộ\r\n\r\n22. Vùng Duyên Hải Nam Trung Bộ, Vùng Tây Nguyên\r\n\r\n24. Vùng Đông Nam Bộ, Vùng Đồng Bằng Sông Cửu Long\r\n\r\n25. Các vùng kinh tế trọng điểm\r\n\r\nNgoài ra, NXB Giáo dục Việt Nam đã biên soạn cuốn “Hướng dẫn sử dụng Atlat Địa lý Việt Nam” dùng cho học sinh THCS và THPT, ôn tập thi tốt nghiệp THPT, thi ĐH, CĐ và ôn luyện thi học sinh giỏi quốc gia.\r\n\r\nNội dung sách gồm ba phần:\r\n\r\nPhần 1: Một số kiến thức về phương pháp sử dụng bản đồ giáo khoa;\r\n\r\nPhần 2: Giới thiệu về Atlat Địa lý Việt Nam.\r\n\r\nPhần 4: Hướng dẫn sử dụng Atlat Địa lý Việt Nam.", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)", 15, 1 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (2023)\r\n\r\nSTT	Tên hàng\r\n1	Bài tập Mĩ thuật 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n2	Bài tập Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n3	Bài tập Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Bài tập Lịch sử và Địa lí 7 (Phần Địa lí) (Chân Trời Sáng Tạo) (2023)\r\n5	Bài tập Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Bài tập Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Bài tập Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n8	Bài tập Lịch sử và Địa lí 7 (Phần Lịch sử) (Chân Trời Sáng Tạo) (2023)\r\n9	Bài tập Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n10	Bài tập Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n11	Bài tập Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n12	Bài tập Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\nMã hàng	3300000027357\r\nCấp Độ/ Lớp	Lớp 7\r\nCấp Học	Cấp 2\r\nNhà Cung Cấp	Nhà xuất bản Giáo Dục\r\nTác giả	Bộ Giáo Dục Và Đào Tạo\r\nNXB	Giáo Dục Việt Nam\r\nNăm XB	2023\r\nNgôn Ngữ	Tiếng Việt\r\nTrọng lượng (gr)	1200\r\nKích Thước Bao Bì	24 x 17 x 3 cm\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (2023)\r\n\r\nSTT	Tên hàng\r\n1	Bài tập Mĩ thuật 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n2	Bài tập Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n3	Bài tập Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Bài tập Lịch sử và Địa lí 7 (Phần Địa lí) (Chân Trời Sáng Tạo) (2023)\r\n5	Bài tập Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Bài tập Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Bài tập Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n8	Bài tập Lịch sử và Địa lí 7 (Phần Lịch sử) (Chân Trời Sáng Tạo) (2023)\r\n9	Bài tập Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n10	Bài tập Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n11	Bài tập Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n12	Bài tập Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (Chuẩn)", 16, 1 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT	Tên hàng\r\n1	Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2	Giáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3	Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5	Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8	Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9	Lịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10	Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11	Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12	Mĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)\r\nMã hàng	3300000027333\r\nCấp Độ/ Lớp	Lớp 7\r\nCấp Học	Cấp 2\r\nNhà Cung Cấp	Nhà xuất bản Giáo Dục\r\nTác giả	Bộ Giáo Dục Và Đào Tạo\r\nNXB	Giáo Dục Việt Nam\r\nNăm XB	2023\r\nNgôn Ngữ	Tiếng Việt\r\nTrọng lượng (gr)	2500\r\nKích Thước Bao Bì	24 x 17 x 6 cm\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT	Tên hàng\r\n1	Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2	Giáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3	Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5	Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8	Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9	Lịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10	Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11	Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12	Mĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)", true, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (Chuẩn) (Không Tin Học)", 16, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookAuthorId", "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "WrittenWhen" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 5, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "ProductId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_bo/2024_05_20_17_11_53_1-390x510.jpg?_gl=1*10nhn5q*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjAuMTcxNjUzNTA4NC42MC4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_bo/2024_05_20_17_11_53_1-390x510.jpg?_gl=1*10nhn5q*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjAuMTcxNjUzNTA4NC42MC4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_bo/2024_05_20_17_11_53_2-390x510.jpg?_gl=1*10nhn5q*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjAuMTcxNjUzNTA4NC42MC4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_bo/2024_05_20_17_11_53_2-390x510.jpg?_gl=1*10nhn5q*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjAuMTcxNjUzNTA4NC42MC4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_me/2024_05_20_17_11_53_2-390x510.jpg?_gl=1*1v684ny*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjEuMTcxNjUzNTQ2NS41OS4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_me/2024_05_20_17_11_53_2-390x510.jpg?_gl=1*1v684ny*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjEuMTcxNjUzNTQ2NS41OS4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_me/2024_05_20_17_11_53_4-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_me/2024_05_20_17_11_53_4-390x510.jpg" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_ba/2024_05_20_17_11_53_3-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_ba/2024_05_20_17_11_53_3-390x510.jpg" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_ong/2024_05_20_17_11_53_1-390x510.jpg?_gl=1*11n63iw*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjEuMTcxNjUzNTY0Ny41OS4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_ong/2024_05_20_17_11_53_1-390x510.jpg?_gl=1*11n63iw*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjEuMTcxNjUzNTY0Ny41OS4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_ong/2024_05_20_17_11_53_2-390x510.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_ong/2024_05_20_17_11_53_2-390x510.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ProductOptionId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Phần", 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tập", 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeProductValues",
                columns: new[] { "ProductTypeAttributeProductValueId", "AttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 3, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 5, 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 6, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 7, 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 8, 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 9, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 10, 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 11, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 12, 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 13, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 14, 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 15, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 16, 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 17, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 18, 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 19, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 20, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 21, 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 22, 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 23, 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 24, 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 25, 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 26, 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 27, 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 28, 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5 }
                });

            migrationBuilder.InsertData(
                table: "Skus",
                columns: new[] { "SkuId", "Dimension_Height", "Dimension_Length", "Dimension_Width", "Barcode", "Comment", "CreatedBy", "CreatedWhen", "DeletedWhen", "DiscontinuedWhen", "IsActive", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "Quantity", "RecommendedRetailPrice", "Status", "Tags", "TaxRate", "UnitPrice", "ValidFrom", "ValidTo", "Weight", "SkuValue_Value" },
                values: new object[,]
                {
                    { 1, 25m, 0.2m, 20.5m, "0765083359063", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00001" },
                    { 2, 25m, 0.2m, 20.5m, "0462639494097", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2020), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00002" },
                    { 3, 25m, 0.2m, 20.5m, "1482949057379", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2110), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00003" },
                    { 4, 25m, 0.2m, 20.5m, "6267047946549", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2180), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00004" },
                    { 5, 24.0m, 0.5m, 16.0m, "9606726677252", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2510), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00005" },
                    { 6, 24.0m, 0.5m, 16.0m, "8379760183413", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00006" },
                    { 7, 24.0m, 0.5m, 16.0m, "4501110187232", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2740), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00007" },
                    { 8, 24.0m, 0.5m, 16.0m, "1262570326893", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2850), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00008" },
                    { 9, 24.0m, 0.5m, 16.0m, "2875627948270", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 20, 31000m, "InStock", "", 0m, 27900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2980), new TimeSpan(0, 0, 0, 0, 0)), null, 190, "SKU00009" },
                    { 10, 24.0m, 3.0m, 17.0m, "5273035092419", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 20, 170000m, "InStock", "", 0m, 170000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(3120), new TimeSpan(0, 0, 0, 0, 0)), null, 1200, "SKU00010" },
                    { 11, 24.0m, 6.0m, 17.0m, "3169651688303", "", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, null, true, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 20, 180000m, "InStock", "", 0m, 180000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(3210), new TimeSpan(0, 0, 0, 0, 0)), null, 2500, "SKU00011" }
                });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "OrderLineId", "CreatedBy", "CreatedWhen", "Description", "DiscountVoucherId", "LastEditedBy", "LastEditedWhen", "OrderId", "PickingCompletedWhen", "Quantity", "SkuId", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2023, 2, 22, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 3, 4, 31500m },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2023, 2, 22, 20, 4, 55, 286, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 7, 0, 0, 0)), 1, 2, 31500m },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2023, 8, 15, 5, 27, 0, 814, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 7, 0, 0, 0)), 1, 8, 18000m },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(2022, 12, 17, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 8, 8, 18000m },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(2022, 12, 17, 23, 8, 22, 354, DateTimeKind.Unspecified).AddTicks(9620), new TimeSpan(0, 7, 0, 0, 0)), 5, 2, 31500m },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 6, 2, 31500m },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 2, 9, 27900m },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2024, 4, 14, 4, 51, 18, 409, DateTimeKind.Unspecified).AddTicks(1730), new TimeSpan(0, 7, 0, 0, 0)), 3, 10, 170000m },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 2, 10, 170000m },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 27900m },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2022, 12, 12, 18, 26, 47, 381, DateTimeKind.Unspecified).AddTicks(7680), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 18000m },
                    { 12, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, null, 5, 8, 18000m },
                    { 13, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, null, 6, 5, 18000m },
                    { 14, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(2023, 9, 26, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 2, 1, 31500m },
                    { 15, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(2023, 9, 26, 21, 59, 50, 477, DateTimeKind.Unspecified).AddTicks(9000), new TimeSpan(0, 7, 0, 0, 0)), 8, 9, 27900m },
                    { 16, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, new DateTimeOffset(new DateTime(2024, 1, 27, 5, 52, 51, 821, DateTimeKind.Unspecified).AddTicks(5390), new TimeSpan(0, 7, 0, 0, 0)), 6, 10, 170000m },
                    { 17, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, null, 5, 6, 18000m },
                    { 18, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, null, 6, 4, 31500m },
                    { 19, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, null, 7, 3, 31500m },
                    { 20, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, new DateTimeOffset(new DateTime(2024, 2, 29, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 6, 7, 18000m },
                    { 21, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, new DateTimeOffset(new DateTime(2024, 2, 29, 4, 17, 18, 577, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), 2, 4, 31500m },
                    { 22, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 6, 10, 170000m },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 10, 3, 31500m },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2023, 7, 10, 4, 53, 34, 917, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 7, 0, 0, 0)), 7, 8, 18000m },
                    { 25, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, null, 9, 11, 180000m },
                    { 26, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, null, 3, 10, 170000m },
                    { 27, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 12, null, 1, 9, 27900m },
                    { 28, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 13, new DateTimeOffset(new DateTime(2022, 6, 20, 3, 37, 6, 285, DateTimeKind.Unspecified).AddTicks(6660), new TimeSpan(0, 7, 0, 0, 0)), 3, 2, 31500m },
                    { 29, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 14, null, 7, 10, 170000m },
                    { 30, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 15, new DateTimeOffset(new DateTime(2022, 6, 20, 14, 47, 8, 989, DateTimeKind.Unspecified).AddTicks(8270), new TimeSpan(0, 7, 0, 0, 0)), 2, 4, 31500m },
                    { 31, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 16, new DateTimeOffset(new DateTime(2024, 3, 28, 3, 2, 10, 276, DateTimeKind.Unspecified).AddTicks(8450), new TimeSpan(0, 7, 0, 0, 0)), 6, 2, 31500m },
                    { 32, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 31500m },
                    { 33, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 6, 9, 27900m },
                    { 34, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 17, new DateTimeOffset(new DateTime(2022, 1, 13, 12, 23, 40, 995, DateTimeKind.Unspecified).AddTicks(4620), new TimeSpan(0, 7, 0, 0, 0)), 1, 6, 18000m },
                    { 35, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, null, 5, 5, 18000m },
                    { 36, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 18, null, 3, 7, 18000m },
                    { 37, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 19, new DateTimeOffset(new DateTime(2023, 9, 1, 0, 34, 46, 300, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 7, 0, 0, 0)), 7, 4, 31500m },
                    { 38, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "", null, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 20, new DateTimeOffset(new DateTime(2023, 12, 3, 12, 26, 52, 244, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), 9, 3, 31500m }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionValues",
                columns: new[] { "ProductOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "Value" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "Một ngày của tớ và bố" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "Một ngày của tớ và mẹ" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "Một ngày của tớ và ông" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "Một ngày của tớ và bà" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Tập 1 - Mùa Xuân" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Tập 2 - Mùa Hạ" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Tập 3 - Mùa Thu" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "Tập 4 - Mùa Đông" }
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
                table: "SkuImages",
                columns: new[] { "SkuImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "SkuId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg" }
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
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 1, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, 2 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, 3 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 4, 4 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 5, 5 },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 6, 6 },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 7, 7 },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 8, 8 }
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
                name: "IX_DiscountVouchers_CreatedBy",
                table: "DiscountVouchers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_LastEditedBy",
                table: "DiscountVouchers",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_ProductId",
                table: "DiscountVouchers",
                column: "ProductId");

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
                name: "IX_Orders_DiscountVoucherId",
                table: "Orders",
                column: "DiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LastEditedBy",
                table: "Orders",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

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
                name: "IX_ProductTypeAttributeValues_ProductTypeAttributeId",
                table: "ProductTypeAttributeValues",
                column: "ProductTypeAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_Value",
                table: "ProductTypeAttributeValues",
                column: "Value",
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
                name: "IX_SkuImages_CreatedBy",
                table: "SkuImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SkuImages_LastEditedBy",
                table: "SkuImages",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SkuImages_SkuId",
                table: "SkuImages",
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
                name: "SkuImages");

            migrationBuilder.DropTable(
                name: "SkuOptionValues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributeValues");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ProductOptionValues");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "DiscountVouchers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributes");

            migrationBuilder.DropTable(
                name: "Skus");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "RefAddressTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "UnitMeasures");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
