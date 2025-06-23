using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Branch_Order_20250622_163700 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RecommendedRetailPrice",
                table: "OrderLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Branchs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedRetailPrice",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Branchs");
        }
    }
}
