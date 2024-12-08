using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Address_2024120821413098 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Addresses",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Addresses",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_CustomerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Addresses",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_Addresses_Id",
                        column: x => x.Id,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_CustomerId",
                table: "ShippingAddresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
