using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_ProductRelatedFields_20241222_003940 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_CreatorId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_DeleterId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_LastModifierId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Branchs_WarehouseId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariants_ProductVariantId",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Inventories");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_WarehouseId",
                table: "Inventories",
                newName: "IX_Inventories_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ProductVariantId",
                table: "Inventories",
                newName: "IX_Inventories_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_LastModifierId",
                table: "Inventories",
                newName: "IX_Inventories_LastModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_DeleterId",
                table: "Inventories",
                newName: "IX_Inventories_DeleterId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_CreatorId",
                table: "Inventories",
                newName: "IX_Inventories_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_DeleterId",
                table: "Inventories",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_LastModifierId",
                table: "Inventories",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Branchs_WarehouseId",
                table: "Inventories",
                column: "WarehouseId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_ProductVariants_ProductVariantId",
                table: "Inventories",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_CreatorId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_DeleterId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_LastModifierId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Branchs_WarehouseId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_ProductVariants_ProductVariantId",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Inventory");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventory",
                newName: "IX_Inventory_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_ProductVariantId",
                table: "Inventory",
                newName: "IX_Inventory_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_LastModifierId",
                table: "Inventory",
                newName: "IX_Inventory_LastModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_DeleterId",
                table: "Inventory",
                newName: "IX_Inventory_DeleterId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_CreatorId",
                table: "Inventory",
                newName: "IX_Inventory_CreatorId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockStatus",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_CreatorId",
                table: "Inventory",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_DeleterId",
                table: "Inventory",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_LastModifierId",
                table: "Inventory",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Branchs_WarehouseId",
                table: "Inventory",
                column: "WarehouseId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariants_ProductVariantId",
                table: "Inventory",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
