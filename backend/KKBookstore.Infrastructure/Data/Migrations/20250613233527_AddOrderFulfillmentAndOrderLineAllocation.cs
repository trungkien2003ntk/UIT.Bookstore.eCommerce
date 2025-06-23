using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations;

/// <inheritdoc />
public partial class AddOrderFulfillmentAndOrderLineAllocation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "OrderFulfillments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderId = table.Column<int>(type: "int", nullable: false),
                BranchId = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DistanceFromCustomer = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                IsSelectedForPackaging = table.Column<bool>(type: "bit", nullable: false),
                PackagingStartedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                PackagingCompletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatorId = table.Column<int>(type: "int", nullable: true),
                LastModifierId = table.Column<int>(type: "int", nullable: true),
                LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderFulfillments", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderFulfillments_AspNetUsers_CreatorId",
                    column: x => x.CreatorId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderFulfillments_AspNetUsers_LastModifierId",
                    column: x => x.LastModifierId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderFulfillments_Branchs_BranchId",
                    column: x => x.BranchId,
                    principalTable: "Branchs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderFulfillments_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "OrderLineAllocations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderLineId = table.Column<int>(type: "int", nullable: false),
                OrderFulfillmentId = table.Column<int>(type: "int", nullable: false),
                ProductVariantId = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                InventoryId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderLineAllocations", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderLineAllocations_Inventories_InventoryId",
                    column: x => x.InventoryId,
                    principalTable: "Inventories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderLineAllocations_OrderFulfillments_OrderFulfillmentId",
                    column: x => x.OrderFulfillmentId,
                    principalTable: "OrderFulfillments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderLineAllocations_OrderLines_OrderLineId",
                    column: x => x.OrderLineId,
                    principalTable: "OrderLines",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderLineAllocations_ProductVariants_ProductVariantId",
                    column: x => x.ProductVariantId,
                    principalTable: "ProductVariants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_OrderFulfillments_BranchId",
            table: "OrderFulfillments",
            column: "BranchId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderFulfillments_CreatorId",
            table: "OrderFulfillments",
            column: "CreatorId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderFulfillments_LastModifierId",
            table: "OrderFulfillments",
            column: "LastModifierId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderFulfillments_OrderId",
            table: "OrderFulfillments",
            column: "OrderId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderFulfillments_OrderId_BranchId",
            table: "OrderFulfillments",
            columns: new[] { "OrderId", "BranchId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_OrderLineAllocations_InventoryId",
            table: "OrderLineAllocations",
            column: "InventoryId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLineAllocations_OrderFulfillmentId",
            table: "OrderLineAllocations",
            column: "OrderFulfillmentId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLineAllocations_OrderLineId",
            table: "OrderLineAllocations",
            column: "OrderLineId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLineAllocations_ProductVariantId",
            table: "OrderLineAllocations",
            column: "ProductVariantId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OrderLineAllocations");

        migrationBuilder.DropTable(
            name: "OrderFulfillments");
    }
}
