using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class Updated_Product_20250413_170700 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "PurchasePrice",
            table: "Inventories",
            newName: "UnitCost");

        migrationBuilder.AddColumn<string>(
            name: "Sku_Value",
            table: "Products",
            type: "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "InitialQuantity",
            table: "Inventories",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "OriginalCreatedDate",
            table: "Inventories",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
            name: "SourceType",
            table: "Inventories",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "None");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Sku_Value",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "InitialQuantity",
            table: "Inventories");

        migrationBuilder.DropColumn(
            name: "OriginalCreatedDate",
            table: "Inventories");

        migrationBuilder.DropColumn(
            name: "SourceType",
            table: "Inventories");

        migrationBuilder.RenameColumn(
            name: "UnitCost",
            table: "Inventories",
            newName: "PurchasePrice");
    }
}
