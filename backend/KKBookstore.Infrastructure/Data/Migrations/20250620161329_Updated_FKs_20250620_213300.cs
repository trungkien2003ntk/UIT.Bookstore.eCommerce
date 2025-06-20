using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_FKs_20250620_213300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CustomerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_DeleterId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifierId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

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
                name: "FK_Banners_AspNetUsers_CreatorId",
                table: "Banners");

            migrationBuilder.DropForeignKey(
                name: "FK_Banners_AspNetUsers_LastModifierId",
                table: "Banners");

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
                name: "FK_Branchs_Addresses_AddressId",
                table: "Branchs");

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
                name: "FK_DiscountVouchers_AspNetUsers_CreatorId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_DeleterId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_LastModifierId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_DeleterId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_LastModifierId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Branchs_WarehouseId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_ProductVariants_ProductVariantId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_CreatorId",
                table: "ModerationAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_LastModifierId",
                table: "ModerationAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationAuditLogs_Ratings_RatingId",
                table: "ModerationAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_CreatorId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_LastModifierId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_Orders_OrderId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineAllocations_OrderFulfillments_OrderFulfillmentId",
                table: "OrderLineAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineAllocations_OrderLines_OrderLineId",
                table: "OrderLineAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AspNetUsers_CreatorId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AspNetUsers_LastModifierId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_ProductVariants_ProductVariantId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreatorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_LastModifierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountVouchers_PriceDiscountVoucherId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountVouchers_ShippingDiscountVoucherId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
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
                name: "FK_ProductImages_Products_ProductId",
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
                name: "FK_ProductOptions_Products_ProductId",
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
                name: "FK_ProductOptionValues_ProductOptions_OptionId",
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
                name: "FK_ProductTypeAttributeProductValues_Products_ProductId",
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
                name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
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
                name: "FK_ProductVariantOptionValues_ProductVariants_ProductVariantId",
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
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_CreatorId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_DeleterId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_LastModifierId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_Ratings_RatingId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CreatorId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CustomerId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_DeleterId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_LastModifierId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Ratings_RatingId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_CreatorId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_CustomerId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_LastModifierId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_Ratings_RatingId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CustomerId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_DeleterId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_LastModifierId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_ProductVariants_ProductVariantId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

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
                name: "FK_StockTransactionDetails_AspNetUsers_CreatorId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_DeleterId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_LastModifierId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_ProductVariants_VariantId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_StockTransactions_StockTransactionId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_AspNetUsers_CreatorId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_AspNetUsers_DeleterId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_AspNetUsers_LastModifierId",
                table: "StockTransactions");

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
                name: "FK_VoucherCustomerTypes_AspNetUsers_CreatorId",
                table: "VoucherCustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherCustomerTypes_AspNetUsers_LastModifierId",
                table: "VoucherCustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherCustomerTypes_DiscountVouchers_VoucherId",
                table: "VoucherCustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CreatorId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_LastModifierId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_DiscountVouchers_VoucherId",
                table: "VoucherUsages");

            migrationBuilder.DropIndex(
                name: "IX_IsDeleted",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_DeleterId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductImages");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Ratings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RatingReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVariantId",
                table: "OrderLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorId",
                table: "Addresses",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_DeleterId",
                table: "Addresses",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifierId",
                table: "Addresses",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_CreatorId",
                table: "Authors",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_DeleterId",
                table: "Authors",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_LastModifierId",
                table: "Authors",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_AspNetUsers_CreatorId",
                table: "Banners",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_AspNetUsers_LastModifierId",
                table: "Banners",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_AspNetUsers_CreatorId",
                table: "BookAuthors",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_AspNetUsers_DeleterId",
                table: "BookAuthors",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_AspNetUsers_LastModifierId",
                table: "BookAuthors",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Addresses_AddressId",
                table: "Branchs",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_AspNetUsers_CreatorId",
                table: "Branchs",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_AspNetUsers_DeleterId",
                table: "Branchs",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_AspNetUsers_LastModifierId",
                table: "Branchs",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_CreatorId",
                table: "CustomerTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_DeleterId",
                table: "CustomerTypes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTypes_AspNetUsers_LastModifierId",
                table: "CustomerTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_CreatorId",
                table: "DeliveryMethods",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_DeleterId",
                table: "DeliveryMethods",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMethods_AspNetUsers_LastModifierId",
                table: "DeliveryMethods",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_CreatorId",
                table: "DiscountVouchers",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_DeleterId",
                table: "DiscountVouchers",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_LastModifierId",
                table: "DiscountVouchers",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
                table: "DiscountVouchers",
                column: "ApplyToProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_DeleterId",
                table: "Inventories",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_LastModifierId",
                table: "Inventories",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Branchs_WarehouseId",
                table: "Inventories",
                column: "WarehouseId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_ProductVariants_ProductVariantId",
                table: "Inventories",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_CreatorId",
                table: "ModerationAuditLogs",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_LastModifierId",
                table: "ModerationAuditLogs",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationAuditLogs_Ratings_RatingId",
                table: "ModerationAuditLogs",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_CreatorId",
                table: "OrderFulfillments",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_LastModifierId",
                table: "OrderFulfillments",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_Orders_OrderId",
                table: "OrderFulfillments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineAllocations_OrderFulfillments_OrderFulfillmentId",
                table: "OrderLineAllocations",
                column: "OrderFulfillmentId",
                principalTable: "OrderFulfillments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineAllocations_OrderLines_OrderLineId",
                table: "OrderLineAllocations",
                column: "OrderLineId",
                principalTable: "OrderLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_AspNetUsers_CreatorId",
                table: "OrderLines",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_AspNetUsers_LastModifierId",
                table: "OrderLines",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines",
                column: "DiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_ProductVariants_ProductVariantId",
                table: "OrderLines",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CreatorId",
                table: "Orders",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_LastModifierId",
                table: "Orders",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountVouchers_PriceDiscountVoucherId",
                table: "Orders",
                column: "PriceDiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountVouchers_ShippingDiscountVoucherId",
                table: "Orders",
                column: "ShippingDiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_CreatorId",
                table: "PaymentMethods",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_DeleterId",
                table: "PaymentMethods",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_LastModifierId",
                table: "PaymentMethods",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_CreatorId",
                table: "ProductImages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_LastModifierId",
                table: "ProductImages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_CreatorId",
                table: "ProductOptions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_DeleterId",
                table: "ProductOptions",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_LastModifierId",
                table: "ProductOptions",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Products_ProductId",
                table: "ProductOptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_CreatorId",
                table: "ProductOptionValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_DeleterId",
                table: "ProductOptionValues",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_LastModifierId",
                table: "ProductOptionValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_ProductOptions_OptionId",
                table: "ProductOptionValues",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_AspNetUsers_CreatorId",
                table: "ProductPriceHistories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_AspNetUsers_LastModifierId",
                table: "ProductPriceHistories",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatorId",
                table: "Products",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_DeleterId",
                table: "Products",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_LastModifierId",
                table: "Products",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeMappings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeMappings",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeProductValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeProductValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValues_Products_ProductId",
                table: "ProductTypeAttributeProductValues",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatorId",
                table: "ProductTypeAttributes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatorId",
                table: "ProductTypeAttributeValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastModifierId",
                table: "ProductTypeAttributeValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
                table: "ProductTypeAttributeValues",
                column: "ProductTypeAttributeId",
                principalTable: "ProductTypeAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_AspNetUsers_CreatorId",
                table: "ProductTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_AspNetUsers_DeleterId",
                table: "ProductTypes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_AspNetUsers_LastModifierId",
                table: "ProductTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_CreatorId",
                table: "ProductVariantOptionValues",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_DeleterId",
                table: "ProductVariantOptionValues",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_LastModifierId",
                table: "ProductVariantOptionValues",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_ProductVariants_ProductVariantId",
                table: "ProductVariantOptionValues",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_CreatorId",
                table: "ProductVariants",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_DeleterId",
                table: "ProductVariants",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_LastModifierId",
                table: "ProductVariants",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_CreatorId",
                table: "RatingImages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_DeleterId",
                table: "RatingImages",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_LastModifierId",
                table: "RatingImages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_Ratings_RatingId",
                table: "RatingImages",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CreatorId",
                table: "RatingLikes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CustomerId",
                table: "RatingLikes",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_DeleterId",
                table: "RatingLikes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_LastModifierId",
                table: "RatingLikes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_Ratings_RatingId",
                table: "RatingLikes",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_CreatorId",
                table: "RatingReports",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_CustomerId",
                table: "RatingReports",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_LastModifierId",
                table: "RatingReports",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_Ratings_RatingId",
                table: "RatingReports",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatorId",
                table: "Ratings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_CustomerId",
                table: "Ratings",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_DeleterId",
                table: "Ratings",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_LastModifierId",
                table: "Ratings",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_ProductVariants_ProductVariantId",
                table: "Ratings",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_CreatorId",
                table: "ShoppingCartItems",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_CustomerId",
                table: "ShoppingCartItems",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_AspNetUsers_LastModifierId",
                table: "ShoppingCartItems",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_CreatorId",
                table: "StockTransactionDetails",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_DeleterId",
                table: "StockTransactionDetails",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_LastModifierId",
                table: "StockTransactionDetails",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_ProductVariants_VariantId",
                table: "StockTransactionDetails",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_StockTransactions_StockTransactionId",
                table: "StockTransactionDetails",
                column: "StockTransactionId",
                principalTable: "StockTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_AspNetUsers_CreatorId",
                table: "StockTransactions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_AspNetUsers_DeleterId",
                table: "StockTransactions",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_AspNetUsers_LastModifierId",
                table: "StockTransactions",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_CreatorId",
                table: "UnitMeasures",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_DeleterId",
                table: "UnitMeasures",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitMeasures_AspNetUsers_LastModifierId",
                table: "UnitMeasures",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherCustomerTypes_AspNetUsers_CreatorId",
                table: "VoucherCustomerTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherCustomerTypes_AspNetUsers_LastModifierId",
                table: "VoucherCustomerTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherCustomerTypes_DiscountVouchers_VoucherId",
                table: "VoucherCustomerTypes",
                column: "VoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CreatorId",
                table: "VoucherUsages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_LastModifierId",
                table: "VoucherUsages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_DiscountVouchers_VoucherId",
                table: "VoucherUsages",
                column: "VoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CustomerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_DeleterId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_LastModifierId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

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
                name: "FK_Banners_AspNetUsers_CreatorId",
                table: "Banners");

            migrationBuilder.DropForeignKey(
                name: "FK_Banners_AspNetUsers_LastModifierId",
                table: "Banners");

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
                name: "FK_Branchs_Addresses_AddressId",
                table: "Branchs");

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
                name: "FK_DiscountVouchers_AspNetUsers_CreatorId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_DeleterId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_AspNetUsers_LastModifierId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_DeleterId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_LastModifierId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Branchs_WarehouseId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_ProductVariants_ProductVariantId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_CreatorId",
                table: "ModerationAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_LastModifierId",
                table: "ModerationAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ModerationAuditLogs_Ratings_RatingId",
                table: "ModerationAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_CreatorId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_LastModifierId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_Orders_OrderId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineAllocations_OrderFulfillments_OrderFulfillmentId",
                table: "OrderLineAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineAllocations_OrderLines_OrderLineId",
                table: "OrderLineAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AspNetUsers_CreatorId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AspNetUsers_LastModifierId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_ProductVariants_ProductVariantId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreatorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_LastModifierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountVouchers_PriceDiscountVoucherId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiscountVouchers_ShippingDiscountVoucherId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
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
                name: "FK_ProductImages_AspNetUsers_LastModifierId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
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
                name: "FK_ProductOptions_Products_ProductId",
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
                name: "FK_ProductOptionValues_ProductOptions_OptionId",
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
                name: "FK_ProductTypeAttributeProductValues_Products_ProductId",
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
                name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
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
                name: "FK_ProductVariantOptionValues_ProductVariants_ProductVariantId",
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
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_CreatorId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_DeleterId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_LastModifierId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_Ratings_RatingId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CreatorId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_CustomerId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_DeleterId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_LastModifierId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Ratings_RatingId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_CreatorId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_CustomerId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_LastModifierId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_Ratings_RatingId",
                table: "RatingReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CreatorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_CustomerId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_DeleterId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_LastModifierId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_ProductVariants_ProductVariantId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

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
                name: "FK_StockTransactionDetails_AspNetUsers_CreatorId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_DeleterId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_LastModifierId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_ProductVariants_VariantId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_StockTransactions_StockTransactionId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_AspNetUsers_CreatorId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_AspNetUsers_DeleterId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_AspNetUsers_LastModifierId",
                table: "StockTransactions");

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
                name: "FK_VoucherCustomerTypes_AspNetUsers_CreatorId",
                table: "VoucherCustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherCustomerTypes_AspNetUsers_LastModifierId",
                table: "VoucherCustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherCustomerTypes_DiscountVouchers_VoucherId",
                table: "VoucherCustomerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_CreatorId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_AspNetUsers_LastModifierId",
                table: "VoucherUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherUsages_DiscountVouchers_VoucherId",
                table: "VoucherUsages");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RatingReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleterId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionTime",
                table: "ProductImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProductVariantId",
                table: "OrderLines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IsDeleted",
                table: "ProductImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_DeleterId",
                table: "ProductImages",
                column: "DeleterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CreatorId",
                table: "Addresses",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CustomerId",
                table: "Addresses",
                column: "CustomerId",
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
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
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
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
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
                name: "FK_Banners_AspNetUsers_CreatorId",
                table: "Banners",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_AspNetUsers_LastModifierId",
                table: "Banners",
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
                name: "FK_Branchs_Addresses_AddressId",
                table: "Branchs",
                column: "AddressId",
                principalTable: "Addresses",
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
                name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
                table: "DiscountVouchers",
                column: "ApplyToProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_DeleterId",
                table: "Inventories",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_LastModifierId",
                table: "Inventories",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Branchs_WarehouseId",
                table: "Inventories",
                column: "WarehouseId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_ProductVariants_ProductVariantId",
                table: "Inventories",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_CreatorId",
                table: "ModerationAuditLogs",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationAuditLogs_AspNetUsers_LastModifierId",
                table: "ModerationAuditLogs",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModerationAuditLogs_Ratings_RatingId",
                table: "ModerationAuditLogs",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_CreatorId",
                table: "OrderFulfillments",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_AspNetUsers_LastModifierId",
                table: "OrderFulfillments",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_Orders_OrderId",
                table: "OrderFulfillments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineAllocations_OrderFulfillments_OrderFulfillmentId",
                table: "OrderLineAllocations",
                column: "OrderFulfillmentId",
                principalTable: "OrderFulfillments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineAllocations_OrderLines_OrderLineId",
                table: "OrderLineAllocations",
                column: "OrderLineId",
                principalTable: "OrderLines",
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
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines",
                column: "DiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_ProductVariants_ProductVariantId",
                table: "OrderLines",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "Addresses",
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
                name: "FK_Orders_AspNetUsers_LastModifierId",
                table: "Orders",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountVouchers_PriceDiscountVoucherId",
                table: "Orders",
                column: "PriceDiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiscountVouchers_ShippingDiscountVoucherId",
                table: "Orders",
                column: "ShippingDiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
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
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
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
                name: "FK_ProductOptions_Products_ProductId",
                table: "ProductOptions",
                column: "ProductId",
                principalTable: "Products",
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
                name: "FK_ProductOptionValues_ProductOptions_OptionId",
                table: "ProductOptionValues",
                column: "OptionId",
                principalTable: "ProductOptions",
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
                name: "FK_ProductTypeAttributeProductValues_Products_ProductId",
                table: "ProductTypeAttributeProductValues",
                column: "ProductId",
                principalTable: "Products",
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
                name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
                table: "ProductTypeAttributeValues",
                column: "ProductTypeAttributeId",
                principalTable: "ProductTypeAttributes",
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
                name: "FK_ProductVariantOptionValues_ProductVariants_ProductVariantId",
                table: "ProductVariantOptionValues",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
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
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_CreatorId",
                table: "RatingImages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_DeleterId",
                table: "RatingImages",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_LastModifierId",
                table: "RatingImages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_Ratings_RatingId",
                table: "RatingImages",
                column: "RatingId",
                principalTable: "Ratings",
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
                name: "FK_RatingLikes_AspNetUsers_DeleterId",
                table: "RatingLikes",
                column: "DeleterId",
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
                name: "FK_RatingLikes_Ratings_RatingId",
                table: "RatingLikes",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_CreatorId",
                table: "RatingReports",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_CustomerId",
                table: "RatingReports",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_LastModifierId",
                table: "RatingReports",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_Ratings_RatingId",
                table: "RatingReports",
                column: "RatingId",
                principalTable: "Ratings",
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
                name: "FK_Ratings_AspNetUsers_DeleterId",
                table: "Ratings",
                column: "DeleterId",
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
                name: "FK_Ratings_ProductVariants_ProductVariantId",
                table: "Ratings",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings",
                column: "ProductId",
                principalTable: "Products",
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
                name: "FK_StockTransactionDetails_AspNetUsers_CreatorId",
                table: "StockTransactionDetails",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_DeleterId",
                table: "StockTransactionDetails",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_AspNetUsers_LastModifierId",
                table: "StockTransactionDetails",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_ProductVariants_VariantId",
                table: "StockTransactionDetails",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_StockTransactions_StockTransactionId",
                table: "StockTransactionDetails",
                column: "StockTransactionId",
                principalTable: "StockTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_AspNetUsers_CreatorId",
                table: "StockTransactions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_AspNetUsers_DeleterId",
                table: "StockTransactions",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_AspNetUsers_LastModifierId",
                table: "StockTransactions",
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
                name: "FK_VoucherCustomerTypes_AspNetUsers_CreatorId",
                table: "VoucherCustomerTypes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherCustomerTypes_AspNetUsers_LastModifierId",
                table: "VoucherCustomerTypes",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherCustomerTypes_DiscountVouchers_VoucherId",
                table: "VoucherCustomerTypes",
                column: "VoucherId",
                principalTable: "DiscountVouchers",
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
                name: "FK_VoucherUsages_AspNetUsers_LastModifierId",
                table: "VoucherUsages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherUsages_DiscountVouchers_VoucherId",
                table: "VoucherUsages",
                column: "VoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
