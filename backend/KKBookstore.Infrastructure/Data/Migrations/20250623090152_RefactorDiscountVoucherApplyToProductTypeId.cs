using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class RefactorDiscountVoucherApplyToProductTypeId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
            table: "DiscountVouchers");

        migrationBuilder.DropIndex(
            name: "IX_DiscountVouchers_ApplyToProductTypeId",
            table: "DiscountVouchers");

        migrationBuilder.DropColumn(
            name: "ApplyToProductTypeId",
            table: "DiscountVouchers");

        migrationBuilder.AddColumn<string>(
            name: "ApplyToProductTypeIds",
            table: "DiscountVouchers",
            type: "nvarchar(2000)",
            maxLength: 2000,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ApplyToProductTypeIds",
            table: "DiscountVouchers");

        migrationBuilder.AddColumn<int>(
            name: "ApplyToProductTypeId",
            table: "DiscountVouchers",
            type: "int",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_DiscountVouchers_ApplyToProductTypeId",
            table: "DiscountVouchers",
            column: "ApplyToProductTypeId");

        migrationBuilder.AddForeignKey(
            name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
            table: "DiscountVouchers",
            column: "ApplyToProductTypeId",
            principalTable: "ProductTypes",
            principalColumn: "Id",
            onDelete: ReferentialAction.SetNull);
    }
}
