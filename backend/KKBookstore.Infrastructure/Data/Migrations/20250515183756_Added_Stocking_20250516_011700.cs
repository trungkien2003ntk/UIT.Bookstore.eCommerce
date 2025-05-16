using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Stocking_20250516_011700 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    SourceWarehouseId = table.Column<int>(type: "int", nullable: true),
                    DestinationWarehouseId = table.Column<int>(type: "int", nullable: true),
                    DeparturedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ArrivalDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TransferDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TransferStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    LastModifierId = table.Column<int>(type: "int", nullable: true),
                    LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterId = table.Column<int>(type: "int", nullable: true),
                    DeletionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactions_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactions_AspNetUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactions_AspNetUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Branchs_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Branchs_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    StockTransactionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    AdjustmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    LastModifierId = table.Column<int>(type: "int", nullable: true),
                    LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterId = table.Column<int>(type: "int", nullable: true),
                    DeletionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactionDetails_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactionDetails_AspNetUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactionDetails_AspNetUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransactionDetails_StockTransactions_StockTransactionId",
                        column: x => x.StockTransactionId,
                        principalTable: "StockTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IsDeleted",
                table: "StockTransactionDetails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionDetails_CreatorId",
                table: "StockTransactionDetails",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionDetails_DeleterId",
                table: "StockTransactionDetails",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionDetails_LastModifierId",
                table: "StockTransactionDetails",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionDetails_StockTransactionId",
                table: "StockTransactionDetails",
                column: "StockTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_IsDeleted",
                table: "StockTransactions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_CreatorId",
                table: "StockTransactions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_DeleterId",
                table: "StockTransactions",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_DestinationWarehouseId",
                table: "StockTransactions",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_LastModifierId",
                table: "StockTransactions",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_SourceWarehouseId",
                table: "StockTransactions",
                column: "SourceWarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransactionDetails");

            migrationBuilder.DropTable(
                name: "StockTransactions");
        }
    }
}
