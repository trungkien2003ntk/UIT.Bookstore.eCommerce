using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductRelatedOptionTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_AspNetUsers_CreatedBy",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_AspNetUsers_LastEditedBy",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Products_ProductId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionValues_AspNetUsers_CreatedBy",
                table: "OptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionValues_AspNetUsers_LastEditedBy",
                table: "OptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionValues_Options_OptionId",
                table: "OptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_AspNetUsers_CreatedBy",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_AspNetUsers_LastEditedBy",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Skus_SkuId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_OptionValues_OptionValueId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_Options_OptionId",
                table: "SkuOptionValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionValues",
                table: "OptionValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "SkuImages");

            migrationBuilder.RenameTable(
                name: "OptionValues",
                newName: "ProductOptionValues");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "ProductOptions");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_SkuId",
                table: "SkuImages",
                newName: "IX_SkuImages_SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_LastEditedBy",
                table: "SkuImages",
                newName: "IX_SkuImages_LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_CreatedBy",
                table: "SkuImages",
                newName: "IX_SkuImages_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_OptionValues_OptionId",
                table: "ProductOptionValues",
                newName: "IX_ProductOptionValues_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionValues_LastEditedBy",
                table: "ProductOptionValues",
                newName: "IX_ProductOptionValues_LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_OptionValues_CreatedBy",
                table: "ProductOptionValues",
                newName: "IX_ProductOptionValues_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Options_ProductId",
                table: "ProductOptions",
                newName: "IX_ProductOptions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_LastEditedBy",
                table: "ProductOptions",
                newName: "IX_ProductOptions_LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Options_CreatedBy",
                table: "ProductOptions",
                newName: "IX_ProductOptions_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkuImages",
                table: "SkuImages",
                column: "SkuImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOptionValues",
                table: "ProductOptionValues",
                column: "ProductOptionValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOptions",
                table: "ProductOptions",
                column: "ProductOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_CreatedBy",
                table: "ProductOptions",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_AspNetUsers_LastEditedBy",
                table: "ProductOptions",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Products_ProductId",
                table: "ProductOptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_CreatedBy",
                table: "ProductOptionValues",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_LastEditedBy",
                table: "ProductOptionValues",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptionValues_ProductOptions_OptionId",
                table: "ProductOptionValues",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "ProductOptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuImages_AspNetUsers_CreatedBy",
                table: "SkuImages",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuImages_AspNetUsers_LastEditedBy",
                table: "SkuImages",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuImages_Skus_SkuId",
                table: "SkuImages",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_ProductOptionValues_OptionValueId",
                table: "SkuOptionValues",
                column: "OptionValueId",
                principalTable: "ProductOptionValues",
                principalColumn: "ProductOptionValueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_ProductOptions_OptionId",
                table: "SkuOptionValues",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "ProductOptionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_AspNetUsers_CreatedBy",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_AspNetUsers_LastEditedBy",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Products_ProductId",
                table: "ProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_CreatedBy",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_AspNetUsers_LastEditedBy",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptionValues_ProductOptions_OptionId",
                table: "ProductOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuImages_AspNetUsers_CreatedBy",
                table: "SkuImages");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuImages_AspNetUsers_LastEditedBy",
                table: "SkuImages");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuImages_Skus_SkuId",
                table: "SkuImages");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_ProductOptionValues_OptionValueId",
                table: "SkuOptionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuOptionValues_ProductOptions_OptionId",
                table: "SkuOptionValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkuImages",
                table: "SkuImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOptionValues",
                table: "ProductOptionValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOptions",
                table: "ProductOptions");

            migrationBuilder.RenameTable(
                name: "SkuImages",
                newName: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductOptionValues",
                newName: "OptionValues");

            migrationBuilder.RenameTable(
                name: "ProductOptions",
                newName: "Options");

            migrationBuilder.RenameIndex(
                name: "IX_SkuImages_SkuId",
                table: "ProductImages",
                newName: "IX_ProductImages_SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_SkuImages_LastEditedBy",
                table: "ProductImages",
                newName: "IX_ProductImages_LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SkuImages_CreatedBy",
                table: "ProductImages",
                newName: "IX_ProductImages_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOptionValues_OptionId",
                table: "OptionValues",
                newName: "IX_OptionValues_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOptionValues_LastEditedBy",
                table: "OptionValues",
                newName: "IX_OptionValues_LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOptionValues_CreatedBy",
                table: "OptionValues",
                newName: "IX_OptionValues_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOptions_ProductId",
                table: "Options",
                newName: "IX_Options_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOptions_LastEditedBy",
                table: "Options",
                newName: "IX_Options_LastEditedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOptions_CreatedBy",
                table: "Options",
                newName: "IX_Options_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "SkuImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionValues",
                table: "OptionValues",
                column: "ProductOptionValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "ProductOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_AspNetUsers_CreatedBy",
                table: "Options",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_AspNetUsers_LastEditedBy",
                table: "Options",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Products_ProductId",
                table: "Options",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValues_AspNetUsers_CreatedBy",
                table: "OptionValues",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValues_AspNetUsers_LastEditedBy",
                table: "OptionValues",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValues_Options_OptionId",
                table: "OptionValues",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "ProductOptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_CreatedBy",
                table: "ProductImages",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_AspNetUsers_LastEditedBy",
                table: "ProductImages",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Skus_SkuId",
                table: "ProductImages",
                column: "SkuId",
                principalTable: "Skus",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_OptionValues_OptionValueId",
                table: "SkuOptionValues",
                column: "OptionValueId",
                principalTable: "OptionValues",
                principalColumn: "ProductOptionValueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuOptionValues_Options_OptionId",
                table: "SkuOptionValues",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "ProductOptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
