using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations;

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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Authors", x => x.AuthorId);
                table.ForeignKey(
                    name: "FK_Authors_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Authors_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DeliveryMethods", x => x.DeliveryMethodId);
                table.ForeignKey(
                    name: "FK_DeliveryMethods_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_DeliveryMethods_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DiscountVouchers", x => x.DiscountVoucherId);
                table.ForeignKey(
                    name: "FK_DiscountVouchers_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_DiscountVouchers_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                table.ForeignKey(
                    name: "FK_PaymentMethods_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PaymentMethods_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTypeAttributes", x => x.ProductTypeAttributeId);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributes_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributes_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeId);
                table.ForeignKey(
                    name: "FK_ProductTypes_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductTypes_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefAddressTypes", x => x.RefAddressTypeId);
                table.ForeignKey(
                    name: "FK_RefAddressTypes_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_RefAddressTypes_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UnitMeasures", x => x.UnitMeasureId);
                table.ForeignKey(
                    name: "FK_UnitMeasures_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UnitMeasures_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTypeAttributeValues", x => x.ProductTypeAttributeValueId);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributeValues_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("DiscountApplyToProductTypeId", x => x.Id);
                table.ForeignKey(
                    name: "FK_DiscountApplyToProductTypes_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_DiscountApplyToProductTypes_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTypeAttributeMappings", x => x.ProductTypeAttributeMappingId);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ShippingAddresses", x => x.ShippingAddressId);
                table.ForeignKey(
                    name: "FK_ShippingAddresses_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ShippingAddresses_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.ProductId);
                table.ForeignKey(
                    name: "FK_Products_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Products_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.OrderId);
                table.ForeignKey(
                    name: "FK_Orders_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
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
                    name: "FK_Orders_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookAuthors", x => x.BookAuthorId);
                table.ForeignKey(
                    name: "FK_BookAuthors_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_BookAuthors_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                table.ForeignKey(
                    name: "FK_ProductImages_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductImages_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductOptions", x => x.ProductOptionId);
                table.ForeignKey(
                    name: "FK_ProductOptions_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductOptions_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTypeAttributeProductValues", x => x.ProductTypeAttributeProductValueId);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributeProductValues_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductTypeAttributeProductValues_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Skus", x => x.SkuId);
                table.ForeignKey(
                    name: "FK_Skus_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Skus_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                UsedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("VoucherUsageId", x => x.Id);
                table.ForeignKey(
                    name: "FK_VoucherUsages_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_VoucherUsages_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductOptionValues", x => x.ProductOptionValueId);
                table.ForeignKey(
                    name: "FK_ProductOptionValues_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductOptionValues_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderLines", x => x.OrderLineId);
                table.ForeignKey(
                    name: "FK_OrderLines_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderLines_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductPriceHistories", x => x.ProductPriceHistoryId);
                table.ForeignKey(
                    name: "FK_ProductPriceHistories_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductPriceHistories_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ratings", x => x.RatingId);
                table.ForeignKey(
                    name: "FK_Ratings_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Ratings_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                table.ForeignKey(
                    name: "FK_ShoppingCartItems_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
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
                    name: "FK_ShoppingCartItems_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SkuOptionValues", x => x.SkuOptionValueId);
                table.ForeignKey(
                    name: "FK_SkuOptionValues_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_SkuOptionValues_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
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
                LikedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RatingLikes", x => x.RatingLikeId);
                table.ForeignKey(
                    name: "FK_RatingLikes_AspNetUsers_CreatedByUserId",
                    column: x => x.CreatedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_RatingLikes_AspNetUsers_LastEditedByUserId",
                    column: x => x.LastEditedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Restrict);
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
            name: "IX_Authors_CreatedByUserId",
            table: "Authors",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Authors_LastEditedByUserId",
            table: "Authors",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_BookAuthors_AuthorId",
            table: "BookAuthors",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_BookAuthors_CreatedByUserId",
            table: "BookAuthors",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_BookAuthors_LastEditedByUserId",
            table: "BookAuthors",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_BookAuthors_ProductId_AuthorId",
            table: "BookAuthors",
            columns: new[] { "ProductId", "AuthorId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DeliveryMethods_CreatedByUserId",
            table: "DeliveryMethods",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DeliveryMethods_LastEditedByUserId",
            table: "DeliveryMethods",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DeliveryMethods_Name",
            table: "DeliveryMethods",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DiscountApplyToProductTypes_CreatedByUserId",
            table: "DiscountApplyToProductTypes",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DiscountApplyToProductTypes_DiscountVoucherId",
            table: "DiscountApplyToProductTypes",
            column: "DiscountVoucherId");

        migrationBuilder.CreateIndex(
            name: "IX_DiscountApplyToProductTypes_LastEditedByUserId",
            table: "DiscountApplyToProductTypes",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DiscountApplyToProductTypes_ProductTypeId",
            table: "DiscountApplyToProductTypes",
            column: "ProductTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_DiscountVouchers_CreatedByUserId",
            table: "DiscountVouchers",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DiscountVouchers_LastEditedByUserId",
            table: "DiscountVouchers",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_CreatedByUserId",
            table: "OrderLines",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_DiscountVoucherId",
            table: "OrderLines",
            column: "DiscountVoucherId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_LastEditedByUserId",
            table: "OrderLines",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_OrderId",
            table: "OrderLines",
            column: "OrderId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_SkuId",
            table: "OrderLines",
            column: "SkuId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_CreatedByUserId",
            table: "Orders",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_CustomerId",
            table: "Orders",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_DeliveryMethodId",
            table: "Orders",
            column: "DeliveryMethodId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_LastEditedByUserId",
            table: "Orders",
            column: "LastEditedByUserId");

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
            name: "IX_PaymentMethods_CreatedByUserId",
            table: "PaymentMethods",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_PaymentMethods_LastEditedByUserId",
            table: "PaymentMethods",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_PaymentMethods_Name",
            table: "PaymentMethods",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductImages_CreatedByUserId",
            table: "ProductImages",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductImages_LastEditedByUserId",
            table: "ProductImages",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductImages_ProductId",
            table: "ProductImages",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductOptions_CreatedByUserId",
            table: "ProductOptions",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductOptions_LastEditedByUserId",
            table: "ProductOptions",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductOptions_ProductId",
            table: "ProductOptions",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductOptionValues_CreatedByUserId",
            table: "ProductOptionValues",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductOptionValues_LastEditedByUserId",
            table: "ProductOptionValues",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductOptionValues_OptionId",
            table: "ProductOptionValues",
            column: "OptionId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductPriceHistories_CreatedByUserId",
            table: "ProductPriceHistories",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductPriceHistories_LastEditedByUserId",
            table: "ProductPriceHistories",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductPriceHistories_SkuId",
            table: "ProductPriceHistories",
            column: "SkuId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_CreatedByUserId",
            table: "Products",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_LastEditedByUserId",
            table: "Products",
            column: "LastEditedByUserId");

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
            name: "IX_ProductTypeAttributeMappings_CreatedByUserId",
            table: "ProductTypeAttributeMappings",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributeMappings_LastEditedByUserId",
            table: "ProductTypeAttributeMappings",
            column: "LastEditedByUserId");

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
            name: "IX_ProductTypeAttributeProductValues_CreatedByUserId",
            table: "ProductTypeAttributeProductValues",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributeProductValues_LastEditedByUserId",
            table: "ProductTypeAttributeProductValues",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributeProductValues_ProductId_AttributeValueId",
            table: "ProductTypeAttributeProductValues",
            columns: new[] { "ProductId", "AttributeValueId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributes_CreatedByUserId",
            table: "ProductTypeAttributes",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributes_LastEditedByUserId",
            table: "ProductTypeAttributes",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributes_Name",
            table: "ProductTypeAttributes",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributeValues_CreatedByUserId",
            table: "ProductTypeAttributeValues",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributeValues_LastEditedByUserId",
            table: "ProductTypeAttributeValues",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypeAttributeValues_ProductTypeAttributeId_Value",
            table: "ProductTypeAttributeValues",
            columns: new[] { "ProductTypeAttributeId", "Value" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypes_CreatedByUserId",
            table: "ProductTypes",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypes_DisplayName",
            table: "ProductTypes",
            column: "DisplayName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypes_LastEditedByUserId",
            table: "ProductTypes",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTypes_ParentProductTypeId",
            table: "ProductTypes",
            column: "ParentProductTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_RatingLikes_CreatedByUserId",
            table: "RatingLikes",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_RatingLikes_LastEditedByUserId",
            table: "RatingLikes",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_RatingLikes_RatingId",
            table: "RatingLikes",
            column: "RatingId");

        migrationBuilder.CreateIndex(
            name: "IX_RatingLikes_UserId",
            table: "RatingLikes",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Ratings_CreatedByUserId",
            table: "Ratings",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Ratings_LastEditedByUserId",
            table: "Ratings",
            column: "LastEditedByUserId");

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
            name: "IX_RefAddressTypes_CreatedByUserId",
            table: "RefAddressTypes",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_RefAddressTypes_LastEditedByUserId",
            table: "RefAddressTypes",
            column: "LastEditedByUserId");

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
            name: "IX_ShippingAddresses_CreatedByUserId",
            table: "ShippingAddresses",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ShippingAddresses_LastEditedByUserId",
            table: "ShippingAddresses",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ShippingAddresses_UserId",
            table: "ShippingAddresses",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ShoppingCartItems_CreatedByUserId",
            table: "ShoppingCartItems",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ShoppingCartItems_CustomerId",
            table: "ShoppingCartItems",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_ShoppingCartItems_LastEditedByUserId",
            table: "ShoppingCartItems",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ShoppingCartItems_SkuId",
            table: "ShoppingCartItems",
            column: "SkuId");

        migrationBuilder.CreateIndex(
            name: "IX_SkuOptionValues_CreatedByUserId",
            table: "SkuOptionValues",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_SkuOptionValues_LastEditedByUserId",
            table: "SkuOptionValues",
            column: "LastEditedByUserId");

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
            name: "IX_Skus_CreatedByUserId",
            table: "Skus",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Skus_LastEditedByUserId",
            table: "Skus",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Skus_ProductId",
            table: "Skus",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_OrderId",
            table: "Transactions",
            column: "OrderId");

        migrationBuilder.CreateIndex(
            name: "IX_UnitMeasures_CreatedByUserId",
            table: "UnitMeasures",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_UnitMeasures_LastEditedByUserId",
            table: "UnitMeasures",
            column: "LastEditedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_UnitMeasures_Name",
            table: "UnitMeasures",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_VoucherUsages_CreatedByUserId",
            table: "VoucherUsages",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_VoucherUsages_LastEditedByUserId",
            table: "VoucherUsages",
            column: "LastEditedByUserId");

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
