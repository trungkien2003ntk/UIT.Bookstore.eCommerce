using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Address_20241222_144020 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Branchs_AddressId",
                table: "Branchs");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branchs_AddressId",
                table: "Branchs",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Branchs_AddressId",
                table: "Branchs");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Branchs_AddressId",
                table: "Branchs",
                column: "AddressId");
        }
    }
}
