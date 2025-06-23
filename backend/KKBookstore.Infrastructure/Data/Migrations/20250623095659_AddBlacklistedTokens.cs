using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class AddBlacklistedTokens : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BlacklistedTokens",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Token = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                ExpireAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlacklistedTokens", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_BlacklistedTokens_ExpireAt",
            table: "BlacklistedTokens",
            column: "ExpireAt");

        migrationBuilder.CreateIndex(
            name: "IX_BlacklistedTokens_Token",
            table: "BlacklistedTokens",
            column: "Token",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BlacklistedTokens");
    }
}
