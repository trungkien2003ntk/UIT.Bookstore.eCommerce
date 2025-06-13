using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class AddVoucherCustomerType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "VoucherCustomerTypes",
            columns: table => new
            {
                VoucherId = table.Column<int>(type: "int", nullable: false),
                CustomerTypeId = table.Column<int>(type: "int", nullable: false),
                Id = table.Column<int>(type: "int", nullable: false),
                CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatorId = table.Column<int>(type: "int", nullable: true),
                LastModifierId = table.Column<int>(type: "int", nullable: true),
                LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VoucherCustomerTypes", x => new { x.VoucherId, x.CustomerTypeId });
                table.ForeignKey(
                    name: "FK_VoucherCustomerTypes_AspNetUsers_CreatorId",
                    column: x => x.CreatorId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_VoucherCustomerTypes_AspNetUsers_LastModifierId",
                    column: x => x.LastModifierId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_VoucherCustomerTypes_CustomerTypes_CustomerTypeId",
                    column: x => x.CustomerTypeId,
                    principalTable: "CustomerTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_VoucherCustomerTypes_DiscountVouchers_VoucherId",
                    column: x => x.VoucherId,
                    principalTable: "DiscountVouchers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_VoucherCustomerTypes_CreatorId",
            table: "VoucherCustomerTypes",
            column: "CreatorId");

        migrationBuilder.CreateIndex(
            name: "IX_VoucherCustomerTypes_CustomerTypeId",
            table: "VoucherCustomerTypes",
            column: "CustomerTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_VoucherCustomerTypes_LastModifierId",
            table: "VoucherCustomerTypes",
            column: "LastModifierId");

        migrationBuilder.CreateIndex(
            name: "IX_VoucherCustomerTypes_VoucherId",
            table: "VoucherCustomerTypes",
            column: "VoucherId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "VoucherCustomerTypes");
    }
}
