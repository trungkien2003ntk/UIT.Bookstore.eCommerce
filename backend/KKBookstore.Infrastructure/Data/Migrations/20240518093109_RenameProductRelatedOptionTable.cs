using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductRelatedOptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductTypeOptionValueId",
                table: "ProductTypeAttributeValue",
                newName: "ProductTypeAttributeValueId");

            migrationBuilder.RenameColumn(
                name: "ProductTypeOptionId",
                table: "ProductTypeAttribute",
                newName: "ProductTypeAttributeId");

            migrationBuilder.RenameColumn(
                name: "OptionValueId",
                table: "OptionValues",
                newName: "ProductOptionValueId");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "Options",
                newName: "ProductOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductTypeAttributeValueId",
                table: "ProductTypeAttributeValue",
                newName: "ProductTypeOptionValueId");

            migrationBuilder.RenameColumn(
                name: "ProductTypeAttributeId",
                table: "ProductTypeAttribute",
                newName: "ProductTypeOptionId");

            migrationBuilder.RenameColumn(
                name: "ProductOptionValueId",
                table: "OptionValues",
                newName: "OptionValueId");

            migrationBuilder.RenameColumn(
                name: "ProductOptionId",
                table: "Options",
                newName: "OptionId");
        }
    }
}
