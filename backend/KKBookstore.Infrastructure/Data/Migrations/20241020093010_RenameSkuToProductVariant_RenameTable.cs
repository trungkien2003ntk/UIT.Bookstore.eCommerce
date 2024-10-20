using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameSkuToProductVariant_RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Skus_ProductVariantId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_Skus_ProductVariantId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Skus_ProductVariantId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Skus_ProductVariantId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_AspNetUsers_CreatedByUserId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_AspNetUsers_LastEditedByUserId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_ProductOptionValues_OptionValueId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_ProductOptions_OptionId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_Skus_ProductVariantId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Skus_AspNetUsers_CreatedByUserId",
                table: "Skus");

            migrationBuilder.DropForeignKey(
                name: "FK_Skus_AspNetUsers_LastEditedByUserId",
                table: "Skus");

            migrationBuilder.DropForeignKey(
                name: "FK_Skus_Products_ProductId",
                table: "Skus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skus",
                table: "Skus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkuOptionValues",
                table: "SkuOptionValues");

            migrationBuilder.RenameTable(
                name: "Skus",
                newName: "ProductVariants");

            migrationBuilder.RenameTable(
                name: "SkuOptionValues",
                newName: "ProductVariantOptionValues");

            migrationBuilder.RenameIndex(
                name: "IX_Skus_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Skus_LastEditedByUserId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Skus_CreatedByUserId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_ProductVariantId_OptionId_OptionValueId",
                table: "ProductVariantOptionValues",
                newName: "IX_ProductVariantOptionValues_ProductVariantId_OptionId_OptionValueId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_OptionValueId",
                table: "ProductVariantOptionValues",
                newName: "IX_ProductVariantOptionValues_OptionValueId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_OptionId",
                table: "ProductVariantOptionValues",
                newName: "IX_ProductVariantOptionValues_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_LastEditedByUserId",
                table: "ProductVariantOptionValues",
                newName: "IX_ProductVariantOptionValues_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_CreatedByUserId",
                table: "ProductVariantOptionValues",
                newName: "IX_ProductVariantOptionValues_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "ProductVariantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantOptionValues",
                table: "ProductVariantOptionValues",
                column: "ProductVariantOptionValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_ProductVariants_ProductVariantId",
                table: "OrderLines",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_ProductVariants_ProductVariantId",
                table: "ProductPriceHistories",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_CreatedByUserId",
                table: "ProductVariantOptionValues",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_LastEditedByUserId",
                table: "ProductVariantOptionValues",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_ProductOptionValues_OptionValueId",
                table: "ProductVariantOptionValues",
                column: "OptionValueId",
                principalTable: "ProductOptionValues",
                principalColumn: "ProductOptionValueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_ProductOptions_OptionId",
                table: "ProductVariantOptionValues",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "ProductOptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOptionValues_ProductVariants_ProductVariantId",
                table: "ProductVariantOptionValues",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_CreatedByUserId",
                table: "ProductVariants",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_AspNetUsers_LastEditedByUserId",
                table: "ProductVariants",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_ProductVariants_ProductVariantId",
                table: "Ratings",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ProductVariants_ProductVariantId",
                table: "ShoppingCartItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_ProductVariants_ProductVariantId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_ProductVariants_ProductVariantId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_CreatedByUserId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_AspNetUsers_LastEditedByUserId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_ProductOptionValues_OptionValueId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_ProductOptions_OptionId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOptionValues_ProductVariants_ProductVariantId",
                table: "ProductVariantOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_AspNetUsers_CreatedByUserId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_AspNetUsers_LastEditedByUserId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_ProductVariants_ProductVariantId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ProductVariants_ProductVariantId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantOptionValues",
                table: "ProductVariantOptionValues");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "Skus");

            migrationBuilder.RenameTable(
                name: "ProductVariantOptionValues",
                newName: "SkuOptionValues");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "Skus",
                newName: "IX_Skus_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_LastEditedByUserId",
                table: "Skus",
                newName: "IX_Skus_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_CreatedByUserId",
                table: "Skus",
                newName: "IX_Skus_CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantOptionValues_ProductVariantId_OptionId_OptionValueId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_ProductVariantId_OptionId_OptionValueId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantOptionValues_OptionValueId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_OptionValueId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantOptionValues_OptionId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantOptionValues_LastEditedByUserId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantOptionValues_CreatedByUserId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skus",
                table: "Skus",
                column: "ProductVariantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkuOptionValues",
                table: "SkuOptionValues",
                column: "ProductVariantOptionValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Skus_ProductVariantId",
                table: "OrderLines",
                column: "ProductVariantId",
                principalTable: "Skus",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_Skus_ProductVariantId",
                table: "ProductPriceHistories",
                column: "ProductVariantId",
                principalTable: "Skus",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Skus_ProductVariantId",
                table: "Ratings",
                column: "ProductVariantId",
                principalTable: "Skus",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Skus_ProductVariantId",
                table: "ShoppingCartItems",
                column: "ProductVariantId",
                principalTable: "Skus",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_AspNetUsers_CreatedByUserId",
                table: "SkuOptionValues",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_AspNetUsers_LastEditedByUserId",
                table: "SkuOptionValues",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_ProductOptionValues_OptionValueId",
                table: "SkuOptionValues",
                column: "OptionValueId",
                principalTable: "ProductOptionValues",
                principalColumn: "ProductOptionValueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_ProductOptions_OptionId",
                table: "SkuOptionValues",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "ProductOptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_Skus_ProductVariantId",
                table: "SkuOptionValues",
                column: "ProductVariantId",
                principalTable: "Skus",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skus_AspNetUsers_CreatedByUserId",
                table: "Skus",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skus_AspNetUsers_LastEditedByUserId",
                table: "Skus",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skus_Products_ProductId",
                table: "Skus",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
