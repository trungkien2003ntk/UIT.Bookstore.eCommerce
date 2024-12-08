using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Users_20241208_15573005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_CreatorId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_DeleterId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_LastModifierId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Users_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_CreatorId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_DeleterId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_LastModifierId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Users_CreatorId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Users_DeleterId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Users_LastModifierId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Users_CreatorId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Users_DeleterId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Users_LastModifierId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_Users_CreatorId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_Users_DeleterId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_Users_LastModifierId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMethods_Users_CreatorId",
                table: "DeliveryMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMethods_Users_DeleterId",
                table: "DeliveryMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMethods_Users_LastModifierId",
                table: "DeliveryMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountApplyToProductTypes_Users_CreatorId",
                table: "DiscountApplyToProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountApplyToProductTypes_Users_DeleterId",
                table: "DiscountApplyToProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountApplyToProductTypes_Users_LastModifierId",
                table: "DiscountApplyToProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_Users_CreatorId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_Users_DeleterId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_Users_LastModifierId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_CreatorId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_DeleterId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_LastModifierId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Users_CreatorId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Users_LastModifierId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_LastModifierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Users_CreatorId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Users_DeleterId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Users_LastModifierId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_CreatorId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_DeleterId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_LastModifierId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Users_CreatorId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Users_DeleterId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Users_LastModifierId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_Users_CreatorId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_Users_DeleterId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_Users_LastModifierId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_Users_CreatorId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_Users_LastModifierId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_DeleterId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_LastModifierId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_Users_CreatorId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_Users_LastModifierId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValues_Users_CreatorId",
                table: "ProductTypeAttributeProductValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValues_Users_LastModifierId",
                table: "ProductTypeAttributeProductValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_Users_CreatorId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_Users_LastModifierId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_Users_CreatorId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_Users_LastModifierId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Users_CreatorId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Users_DeleterId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Users_LastModifierId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_Users_CreatorId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_Users_DeleterId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_Users_LastModifierId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Users_CreatorId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Users_DeleterId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Users_LastModifierId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Users_CreatorId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Users_CustomerId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Users_LastModifierId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_CreatorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_CustomerId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_LastModifierId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_Users_CustomerId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Users_CreatorId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Users_CustomerId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Users_LastModifierId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_Users_CreatorId",
                table: "UnitMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_Users_DeleterId",
                table: "UnitMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_Users_LastModifierId",
                table: "UnitMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CustomerTypes_CustomerTypeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_Users_CreatorId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_Users_CustomerId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_Users_LastModifierId",
                table: "VoucherUsages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CustomerTypeId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CustomerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorId",
                table: "Addresses",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_DeleterId",
                table: "Addresses",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifierId",
                table: "Addresses",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CustomerTypes_CustomerTypeId",
                table: "AspNetUsers",
                column: "CustomerTypeId",
                principalTable: "CustomerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_CreatorId",
                table: "Authors",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_DeleterId",
                table: "Authors",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_LastModifierId",
                table: "Authors",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_AspNetUsers_CreatorId",
                table: "BookAuthors",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_AspNetUsers_DeleterId",
                table: "BookAuthors",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_AspNetUsers_LastModifierId",
                table: "BookAuthors",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_AspNetUsers_CreatorId",
                table: "Branchs",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_AspNetUsers_DeleterId",
                table: "Branchs",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_AspNetUsers_LastModifierId",
                table: "Branchs",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_CreatorId",
                table: "CustomerTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_DeleterId",
                table: "CustomerTypes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_LastModifierId",
                table: "CustomerTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_CreatorId",
                table: "DeliveryMethods",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_DeleterId",
                table: "DeliveryMethods",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_LastModifierId",
                table: "DeliveryMethods",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountApplyToProductTypes_AspNetUsers_CreatorId",
                table: "DiscountApplyToProductTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountApplyToProductTypes_AspNetUsers_DeleterId",
                table: "DiscountApplyToProductTypes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountApplyToProductTypes_AspNetUsers_LastModifierId",
                table: "DiscountApplyToProductTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_CreatorId",
                table: "DiscountVouchers",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_DeleterId",
                table: "DiscountVouchers",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_LastModifierId",
                table: "DiscountVouchers",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_CreatorId",
                table: "Inventory",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_DeleterId",
                table: "Inventory",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_LastModifierId",
                table: "Inventory",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_AspNetUsers_CreatorId",
                table: "OrderLines",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_AspNetUsers_LastModifierId",
                table: "OrderLines",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CreatorId",
                table: "Orders",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_LastModifierId",
                table: "Orders",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_CreatorId",
                table: "PaymentMethods",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_DeleterId",
                table: "PaymentMethods",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_LastModifierId",
                table: "PaymentMethods",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_CreatorId",
                table: "ProductImages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_DeleterId",
                table: "ProductImages",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_LastModifierId",
                table: "ProductImages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_CreatorId",
                table: "ProductOptions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_DeleterId",
                table: "ProductOptions",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_LastModifierId",
                table: "ProductOptions",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_CreatorId",
                table: "ProductOptionValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_DeleterId",
                table: "ProductOptionValues",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_LastModifierId",
                table: "ProductOptionValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_AspNetUsers_CreatorId",
                table: "ProductPriceHistories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_AspNetUsers_LastModifierId",
                table: "ProductPriceHistories",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatorId",
                table: "Products",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_DeleterId",
                table: "Products",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_LastModifierId",
                table: "Products",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeMappings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeMappings",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeProductValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeProductValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatorId",
                table: "ProductTypeAttributes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_AspNetUsers_CreatorId",
                table: "ProductTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_AspNetUsers_DeleterId",
                table: "ProductTypes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_AspNetUsers_LastModifierId",
                table: "ProductTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_CreatorId",
                table: "ProductVariantOptionValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_DeleterId",
                table: "ProductVariantOptionValues",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_LastModifierId",
                table: "ProductVariantOptionValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_CreatorId",
                table: "ProductVariants",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_DeleterId",
                table: "ProductVariants",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_LastModifierId",
                table: "ProductVariants",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CreatorId",
                table: "RatingLikes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CustomerId",
                table: "RatingLikes",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_LastModifierId",
                table: "RatingLikes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatorId",
                table: "Ratings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_CustomerId",
                table: "Ratings",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_LastModifierId",
                table: "Ratings",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_CustomerId",
                table: "ShippingAddresses",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_CreatorId",
                table: "ShoppingCartItems",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_CustomerId",
                table: "ShoppingCartItems",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_LastModifierId",
                table: "ShoppingCartItems",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_CreatorId",
                table: "UnitMeasures",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_DeleterId",
                table: "UnitMeasures",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_LastModifierId",
                table: "UnitMeasures",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CreatorId",
                table: "VoucherUsages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CustomerId",
                table: "VoucherUsages",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_LastModifierId",
                table: "VoucherUsages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_DeleterId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifierId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CustomerTypes_CustomerTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_CreatorId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_DeleterId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_LastModifierId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_AspNetUsers_CreatorId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_AspNetUsers_DeleterId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_AspNetUsers_LastModifierId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_AspNetUsers_CreatorId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_AspNetUsers_DeleterId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_AspNetUsers_LastModifierId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_CreatorId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_DeleterId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_LastModifierId",
                table: "CustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_CreatorId",
                table: "DeliveryMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_DeleterId",
                table: "DeliveryMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_LastModifierId",
                table: "DeliveryMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountApplyToProductTypes_AspNetUsers_CreatorId",
                table: "DiscountApplyToProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountApplyToProductTypes_AspNetUsers_DeleterId",
                table: "DiscountApplyToProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountApplyToProductTypes_AspNetUsers_LastModifierId",
                table: "DiscountApplyToProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_CreatorId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_DeleterId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_LastModifierId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_CreatorId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_DeleterId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_LastModifierId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AspNetUsers_CreatorId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AspNetUsers_LastModifierId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreatorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_LastModifierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_CreatorId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_DeleterId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_LastModifierId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_AspNetUsers_CreatorId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_AspNetUsers_DeleterId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_AspNetUsers_LastModifierId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_AspNetUsers_CreatorId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_AspNetUsers_DeleterId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_AspNetUsers_LastModifierId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_CreatorId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_DeleterId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_LastModifierId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_AspNetUsers_CreatorId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_AspNetUsers_LastModifierId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreatorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_DeleterId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_LastModifierId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValues_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeProductValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValues_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeProductValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatorId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_AspNetUsers_CreatorId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_AspNetUsers_DeleterId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_AspNetUsers_LastModifierId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_CreatorId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_DeleterId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_LastModifierId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_AspNetUsers_CreatorId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_AspNetUsers_DeleterId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_AspNetUsers_LastModifierId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CreatorId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CustomerId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_LastModifierId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CustomerId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_LastModifierId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_CustomerId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_CreatorId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_CustomerId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_LastModifierId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_CreatorId",
                table: "UnitMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_DeleterId",
                table: "UnitMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_LastModifierId",
                table: "UnitMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CreatorId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CustomerId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_LastModifierId",
                table: "VoucherUsages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CustomerTypeId",
                table: "Users",
                newName: "IX_Users_CustomerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_CreatorId",
                table: "Addresses",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_DeleterId",
                table: "Addresses",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_LastModifierId",
                table: "Addresses",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Users_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_CreatorId",
                table: "Authors",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_DeleterId",
                table: "Authors",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_LastModifierId",
                table: "Authors",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Users_CreatorId",
                table: "BookAuthors",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Users_DeleterId",
                table: "BookAuthors",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Users_LastModifierId",
                table: "BookAuthors",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Users_CreatorId",
                table: "Branchs",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Users_DeleterId",
                table: "Branchs",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Users_LastModifierId",
                table: "Branchs",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_Users_CreatorId",
                table: "CustomerTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_Users_DeleterId",
                table: "CustomerTypes",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_Users_LastModifierId",
                table: "CustomerTypes",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_Users_CreatorId",
                table: "DeliveryMethods",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_Users_DeleterId",
                table: "DeliveryMethods",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_Users_LastModifierId",
                table: "DeliveryMethods",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountApplyToProductTypes_Users_CreatorId",
                table: "DiscountApplyToProductTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountApplyToProductTypes_Users_DeleterId",
                table: "DiscountApplyToProductTypes",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountApplyToProductTypes_Users_LastModifierId",
                table: "DiscountApplyToProductTypes",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_Users_CreatorId",
                table: "DiscountVouchers",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_Users_DeleterId",
                table: "DiscountVouchers",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_Users_LastModifierId",
                table: "DiscountVouchers",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_CreatorId",
                table: "Inventory",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_DeleterId",
                table: "Inventory",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_LastModifierId",
                table: "Inventory",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Users_CreatorId",
                table: "OrderLines",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Users_LastModifierId",
                table: "OrderLines",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatorId",
                table: "Orders",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_LastModifierId",
                table: "Orders",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_CreatorId",
                table: "PaymentMethods",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_DeleterId",
                table: "PaymentMethods",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_LastModifierId",
                table: "PaymentMethods",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_CreatorId",
                table: "ProductImages",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_DeleterId",
                table: "ProductImages",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_LastModifierId",
                table: "ProductImages",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Users_CreatorId",
                table: "ProductOptions",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Users_DeleterId",
                table: "ProductOptions",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Users_LastModifierId",
                table: "ProductOptions",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_Users_CreatorId",
                table: "ProductOptionValues",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_Users_DeleterId",
                table: "ProductOptionValues",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_Users_LastModifierId",
                table: "ProductOptionValues",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_Users_CreatorId",
                table: "ProductPriceHistories",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_Users_LastModifierId",
                table: "ProductPriceHistories",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatorId",
                table: "Products",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_DeleterId",
                table: "Products",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_LastModifierId",
                table: "Products",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_Users_CreatorId",
                table: "ProductTypeAttributeMappings",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_Users_LastModifierId",
                table: "ProductTypeAttributeMappings",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_Users_CreatorId",
                table: "ProductTypeAttributeProductValues",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_Users_LastModifierId",
                table: "ProductTypeAttributeProductValues",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_Users_CreatorId",
                table: "ProductTypeAttributes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_Users_LastModifierId",
                table: "ProductTypeAttributes",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_Users_CreatorId",
                table: "ProductTypeAttributeValues",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_Users_LastModifierId",
                table: "ProductTypeAttributeValues",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Users_CreatorId",
                table: "ProductTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Users_DeleterId",
                table: "ProductTypes",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Users_LastModifierId",
                table: "ProductTypes",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_Users_CreatorId",
                table: "ProductVariantOptionValues",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_Users_DeleterId",
                table: "ProductVariantOptionValues",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_Users_LastModifierId",
                table: "ProductVariantOptionValues",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Users_CreatorId",
                table: "ProductVariants",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Users_DeleterId",
                table: "ProductVariants",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Users_LastModifierId",
                table: "ProductVariants",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_Users_CreatorId",
                table: "RatingLikes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_Users_CustomerId",
                table: "RatingLikes",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_Users_LastModifierId",
                table: "RatingLikes",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_CreatorId",
                table: "Ratings",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_CustomerId",
                table: "Ratings",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_LastModifierId",
                table: "Ratings",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_Users_CustomerId",
                table: "ShippingAddresses",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Users_CreatorId",
                table: "ShoppingCartItems",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Users_CustomerId",
                table: "ShoppingCartItems",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Users_LastModifierId",
                table: "ShoppingCartItems",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_Users_CreatorId",
                table: "UnitMeasures",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_Users_DeleterId",
                table: "UnitMeasures",
                column: "DeleterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_Users_LastModifierId",
                table: "UnitMeasures",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CustomerTypes_CustomerTypeId",
                table: "Users",
                column: "CustomerTypeId",
                principalTable: "CustomerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_Users_CreatorId",
                table: "VoucherUsages",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_Users_CustomerId",
                table: "VoucherUsages",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_Users_LastModifierId",
                table: "VoucherUsages",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
