using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_StockAdjustment_20250521_013600 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_WarehouseId",
                table: "StockTransactions",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionDetails_VariantId",
                table: "StockTransactionDetails",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionDetails_ProductVariants_VariantId",
                table: "StockTransactionDetails",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Branchs_WarehouseId",
                table: "StockTransactions",
                column: "WarehouseId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionDetails_ProductVariants_VariantId",
                table: "StockTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Branchs_WarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_WarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactionDetails_VariantId",
                table: "StockTransactionDetails");
        }
    }
}
