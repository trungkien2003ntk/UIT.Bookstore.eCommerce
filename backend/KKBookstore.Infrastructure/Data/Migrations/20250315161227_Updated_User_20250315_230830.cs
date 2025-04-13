using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class Updated_User_20250315_230830 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Gender",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "Male");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Gender",
            table: "AspNetUsers");
    }
}
