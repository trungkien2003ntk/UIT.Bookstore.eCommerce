using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameSkuToProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Skus_SkuId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceHistories_Skus_SkuId",
                table: "ProductPriceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Skus_SkuId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Skus_SkuId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_Skus_SkuId",
                table: "SkuOptionValues");

            migrationBuilder.RenameColumn(
                name: "SkuId",
                table: "Skus",
                newName: "ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "SkuOptionValueId",
                table: "SkuOptionValues",
                newName: "ProductVariantOptionValueId");

            migrationBuilder.RenameColumn(
                name: "SkuId",
                table: "SkuOptionValues",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_SkuId_OptionId_OptionValueId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_ProductVariantId_OptionId_OptionValueId");

            migrationBuilder.RenameColumn(
                name: "SkuId",
                table: "ShoppingCartItems",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_SkuId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "SkuId",
                table: "Ratings",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_SkuId",
                table: "Ratings",
                newName: "IX_Ratings_ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "SkuId",
                table: "ProductPriceHistories",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPriceHistories_SkuId",
                table: "ProductPriceHistories",
                newName: "IX_ProductPriceHistories_ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "SkuId",
                table: "OrderLines",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_SkuId",
                table: "OrderLines",
                newName: "IX_OrderLines_ProductVariantId");

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
                name: "FK_SkuOptionValues_Skus_ProductVariantId",
                table: "SkuOptionValues",
                column: "ProductVariantId",
                principalTable: "Skus",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_SkuOptionValues_Skus_ProductVariantId",
                table: "SkuOptionValues");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "Skus",
                newName: "SkuId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantOptionValueId",
                table: "SkuOptionValues",
                newName: "SkuOptionValueId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "SkuOptionValues",
                newName: "SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuOptionValues_ProductVariantId_OptionId_OptionValueId",
                table: "SkuOptionValues",
                newName: "IX_SkuOptionValues_SkuId_OptionId_OptionValueId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "ShoppingCartItems",
                newName: "SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ProductVariantId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_SkuId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "Ratings",
                newName: "SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ProductVariantId",
                table: "Ratings",
                newName: "IX_Ratings_SkuId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "ProductPriceHistories",
                newName: "SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPriceHistories_ProductVariantId",
                table: "ProductPriceHistories",
                newName: "IX_ProductPriceHistories_SkuId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "OrderLines",
                newName: "SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_ProductVariantId",
                table: "OrderLines",
                newName: "IX_OrderLines_SkuId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Skus_SkuId",
                table: "OrderLines",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceHistories_Skus_SkuId",
                table: "ProductPriceHistories",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Skus_SkuId",
                table: "Ratings",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Skus_SkuId",
                table: "ShoppingCartItems",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_Skus_SkuId",
                table: "SkuOptionValues",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
