using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_DiscountStatus_And_OneToOne_ProductType_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountApplyToProductTypes");

            migrationBuilder.AddColumn<int>(
                name: "ApplyToProductTypeId",
                table: "DiscountVouchers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DiscountVouchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RatingReports_CustomerId",
                table: "RatingReports",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVouchers_ApplyToProductTypeId",
                table: "DiscountVouchers",
                column: "ApplyToProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
                table: "DiscountVouchers",
                column: "ApplyToProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_AspNetUsers_CustomerId",
                table: "RatingReports",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVouchers_ProductTypes_ApplyToProductTypeId",
                table: "DiscountVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_AspNetUsers_CustomerId",
                table: "RatingReports");

            migrationBuilder.DropIndex(
                name: "IX_RatingReports_CustomerId",
                table: "RatingReports");

            migrationBuilder.DropIndex(
                name: "IX_DiscountVouchers_ApplyToProductTypeId",
                table: "DiscountVouchers");

            migrationBuilder.DropColumn(
                name: "ApplyToProductTypeId",
                table: "DiscountVouchers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DiscountVouchers");

            migrationBuilder.CreateTable(
                name: "DiscountApplyToProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    DeleterId = table.Column<int>(type: "int", nullable: true),
                    DiscountVoucherId = table.Column<int>(type: "int", nullable: false),
                    LastModifierId = table.Column<int>(type: "int", nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountApplyToProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_AspNetUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_AspNetUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_DiscountVouchers_DiscountVoucherId",
                        column: x => x.DiscountVoucherId,
                        principalTable: "DiscountVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountApplyToProductTypes_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_CreatorId",
                table: "DiscountApplyToProductTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_DeleterId",
                table: "DiscountApplyToProductTypes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_DiscountVoucherId",
                table: "DiscountApplyToProductTypes",
                column: "DiscountVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_LastModifierId",
                table: "DiscountApplyToProductTypes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountApplyToProductTypes_ProductTypeId",
                table: "DiscountApplyToProductTypes",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IsDeleted",
                table: "DiscountApplyToProductTypes",
                column: "IsDeleted");
        }
    }
}
