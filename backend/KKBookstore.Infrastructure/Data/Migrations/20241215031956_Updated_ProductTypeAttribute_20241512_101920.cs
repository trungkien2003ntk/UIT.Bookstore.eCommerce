using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_ProductTypeAttribute_20241512_101920 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_ProductTypeAttributes_ProductAttributeId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                table: "ProductTypeAttributeMappings",
                newName: "ProductTypeAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeMappings_ProductAttributeId",
                table: "ProductTypeAttributeMappings",
                newName: "IX_ProductTypeAttributeMappings_ProductTypeAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_ProductTypeAttributes_ProductTypeAttributeId",
                table: "ProductTypeAttributeMappings",
                column: "ProductTypeAttributeId",
                principalTable: "ProductTypeAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_ProductTypeAttributes_ProductTypeAttributeId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.RenameColumn(
                name: "ProductTypeAttributeId",
                table: "ProductTypeAttributeMappings",
                newName: "ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeMappings_ProductTypeAttributeId",
                table: "ProductTypeAttributeMappings",
                newName: "IX_ProductTypeAttributeMappings_ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_ProductTypeAttributes_ProductAttributeId",
                table: "ProductTypeAttributeMappings",
                column: "ProductAttributeId",
                principalTable: "ProductTypeAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
