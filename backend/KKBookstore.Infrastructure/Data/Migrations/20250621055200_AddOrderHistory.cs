using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines");

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    FromStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TriggeredByUserId = table.Column<int>(type: "int", nullable: true),
                    ExternalReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    LastModifierId = table.Column<int>(type: "int", nullable: true),
                    LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_TriggeredByUserId",
                        column: x => x.TriggeredByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_CreatorId",
                table: "OrderHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_FromStatus",
                table: "OrderHistories",
                column: "FromStatus");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_LastModifierId",
                table: "OrderHistories",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderId",
                table: "OrderHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderId_Timestamp",
                table: "OrderHistories",
                columns: new[] { "OrderId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_Timestamp",
                table: "OrderHistories",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_ToStatus",
                table: "OrderHistories",
                column: "ToStatus");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_TriggeredByUserId",
                table: "OrderHistories",
                column: "TriggeredByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines",
                column: "DiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_DiscountVouchers_DiscountVoucherId",
                table: "OrderLines",
                column: "DiscountVoucherId",
                principalTable: "DiscountVouchers",
                principalColumn: "Id");
        }
    }
}
