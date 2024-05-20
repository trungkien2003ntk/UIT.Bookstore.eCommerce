using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeValues_CreatedByUserId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeValues_LastEditedByUserId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_CreatedByUserId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_LastEditedByUserId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeProductValue_CreatedByUserId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeProductValue_LastEditedByUserId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeProductValue_ProductId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeMappings_CreatedByUserId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeMappings_LastEditedByUserId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropColumn(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_CreatedBy",
                table: "ProductTypeAttributeValues",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_LastEditedBy",
                table: "ProductTypeAttributeValues",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_Value",
                table: "ProductTypeAttributeValues",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_CreatedBy",
                table: "ProductTypeAttributes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_LastEditedBy",
                table: "ProductTypeAttributes",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_Name",
                table: "ProductTypeAttributes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValue_CreatedBy",
                table: "ProductTypeAttributeProductValue",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValue_LastEditedBy",
                table: "ProductTypeAttributeProductValue",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValue_ProductId_ProductTypeAttributeValueId",
                table: "ProductTypeAttributeProductValue",
                columns: new[] { "ProductId", "ProductTypeAttributeValueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_CreatedBy",
                table: "ProductTypeAttributeMappings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_LastEditedBy",
                table: "ProductTypeAttributeMappings",
                column: "LastEditedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributeMappings",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributeMappings",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributeProductValue",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributeProductValue",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributes",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributes",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributeValues",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributeValues",
                column: "LastEditedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatedBy",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastEditedBy",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeValues_CreatedBy",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeValues_LastEditedBy",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeValues_Value",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_CreatedBy",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_LastEditedBy",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributes_Name",
                table: "ProductTypeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeProductValue_CreatedBy",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeProductValue_LastEditedBy",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeProductValue_ProductId_ProductTypeAttributeValueId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeMappings_CreatedBy",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttributeMappings_LastEditedBy",
                table: "ProductTypeAttributeMappings");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ProductTypeAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ProductTypeAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ProductTypeAttributeProductValue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributeProductValue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ProductTypeAttributeMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastEditedByUserId",
                table: "ProductTypeAttributeMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_CreatedByUserId",
                table: "ProductTypeAttributeValues",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValues_LastEditedByUserId",
                table: "ProductTypeAttributeValues",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_CreatedByUserId",
                table: "ProductTypeAttributes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributes_LastEditedByUserId",
                table: "ProductTypeAttributes",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValue_CreatedByUserId",
                table: "ProductTypeAttributeProductValue",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValue_LastEditedByUserId",
                table: "ProductTypeAttributeProductValue",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeProductValue_ProductId",
                table: "ProductTypeAttributeProductValue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_CreatedByUserId",
                table: "ProductTypeAttributeMappings",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_LastEditedByUserId",
                table: "ProductTypeAttributeMappings",
                column: "LastEditedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeMappings",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeMappings",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeProductValue",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValue_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeProductValue",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributes",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributes_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributes",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeValues",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeValues",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
