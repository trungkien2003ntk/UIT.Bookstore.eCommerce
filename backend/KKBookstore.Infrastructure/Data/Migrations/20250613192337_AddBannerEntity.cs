using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class AddBannerEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Banners",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                TargetUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                ProductTypeId = table.Column<int>(type: "int", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatorId = table.Column<int>(type: "int", nullable: true),
                LastModifierId = table.Column<int>(type: "int", nullable: true),
                LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Banners", x => x.Id);
                table.ForeignKey(
                    name: "FK_Banners_AspNetUsers_CreatorId",
                    column: x => x.CreatorId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Banners_AspNetUsers_LastModifierId",
                    column: x => x.LastModifierId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Banners_ProductTypes_ProductTypeId",
                    column: x => x.ProductTypeId,
                    principalTable: "ProductTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Banners_CreatorId",
            table: "Banners",
            column: "CreatorId");

        migrationBuilder.CreateIndex(
            name: "IX_Banners_IsActive",
            table: "Banners",
            column: "IsActive");

        migrationBuilder.CreateIndex(
            name: "IX_Banners_LastModifierId",
            table: "Banners",
            column: "LastModifierId");

        migrationBuilder.CreateIndex(
            name: "IX_Banners_ProductTypeId",
            table: "Banners",
            column: "ProductTypeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Banners");
    }
}
