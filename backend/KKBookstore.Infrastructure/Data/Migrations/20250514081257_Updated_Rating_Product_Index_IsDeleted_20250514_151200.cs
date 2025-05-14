using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class Updated_Rating_Product_Index_IsDeleted_20250514_151200 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "ProductId",
            table: "Ratings",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "UnitMeasures",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "ProductVariants",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "ProductVariantOptionValues",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "ProductTypes",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "Products",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "ProductOptionValues",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "ProductOptions",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "ProductImages",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "PaymentMethods",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "DiscountVouchers",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "DiscountApplyToProductTypes",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "DeliveryMethods",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "CustomerTypes",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "Branchs",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "BookAuthors",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "Authors",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "Addresses",
            column: "IsDeleted");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "UnitMeasures");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "ProductVariants");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "ProductVariantOptionValues");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "ProductTypes");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "Products");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "ProductOptionValues");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "ProductOptions");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "ProductImages");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "PaymentMethods");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "DiscountVouchers");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "DiscountApplyToProductTypes");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "DeliveryMethods");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "CustomerTypes");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "Branchs");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "BookAuthors");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "Authors");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "Addresses");

        migrationBuilder.AlterColumn<int>(
            name: "ProductId",
            table: "Ratings",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");
    }
}
