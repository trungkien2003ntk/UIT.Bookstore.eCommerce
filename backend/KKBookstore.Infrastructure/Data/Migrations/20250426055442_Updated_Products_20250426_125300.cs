using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Products_20250426_125300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                maxLength: -1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "SourceType",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: -1);

            migrationBuilder.AlterColumn<string>(
                name: "SourceType",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "None");
        }
    }
}
