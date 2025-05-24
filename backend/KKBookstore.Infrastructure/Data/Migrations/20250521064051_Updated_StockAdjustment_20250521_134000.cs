using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class Updated_StockAdjustment_20250521_134000 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "UnitCost",
            table: "Inventories",
            type: "decimal(18,2)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "UnitCost",
            table: "Inventories",
            type: "int",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)");
    }
}
