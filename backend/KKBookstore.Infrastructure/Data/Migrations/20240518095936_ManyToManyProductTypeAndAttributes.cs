using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyProductTypeAndAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttribute_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttribute_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttribute_ProductTypes_ProductTypeId",
                table: "ProductTypeAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValue_ProductTypeAttributeValue_ProductTypeAttributeValueId",
                table: "ProductTypeAttributeProductValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValue_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValue_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValue_ProductTypeAttribute_ProductTypeAttributeId",
                table: "ProductTypeAttributeValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypeAttributeValue",
                table: "ProductTypeAttributeValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypeAttribute",
                table: "ProductTypeAttribute");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeAttribute_ProductTypeId",
                table: "ProductTypeAttribute");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "ProductTypeAttribute");

            migrationBuilder.RenameTable(
                name: "ProductTypeAttributeValue",
                newName: "ProductTypeAttributeValues");

            migrationBuilder.RenameTable(
                name: "ProductTypeAttribute",
                newName: "ProductTypeAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeValue_ProductTypeAttributeId",
                table: "ProductTypeAttributeValues",
                newName: "IX_ProductTypeAttributeValues_ProductTypeAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeValue_LastEditedByUserId",
                table: "ProductTypeAttributeValues",
                newName: "IX_ProductTypeAttributeValues_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeValue_CreatedByUserId",
                table: "ProductTypeAttributeValues",
                newName: "IX_ProductTypeAttributeValues_CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttribute_LastEditedByUserId",
                table: "ProductTypeAttributes",
                newName: "IX_ProductTypeAttributes_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttribute_CreatedByUserId",
                table: "ProductTypeAttributes",
                newName: "IX_ProductTypeAttributes_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypeAttributeValues",
                table: "ProductTypeAttributeValues",
                column: "ProductTypeAttributeValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypeAttributes",
                table: "ProductTypeAttributes",
                column: "ProductTypeAttributeId");

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributeMappings",
                columns: table => new
                {
                    ProductTypeAttributeMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributeMappings", x => x.ProductTypeAttributeMappingId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_AspNetUsers_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_ProductTypeAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductTypeAttributes",
                        principalColumn: "ProductTypeAttributeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeMappings_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_CreatedByUserId",
                table: "ProductTypeAttributeMappings",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_LastEditedByUserId",
                table: "ProductTypeAttributeMappings",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_ProductAttributeId",
                table: "ProductTypeAttributeMappings",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeMappings_ProductTypeId",
                table: "ProductTypeAttributeMappings",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValue_ProductTypeAttributeValues_ProductTypeAttributeValueId",
                table: "ProductTypeAttributeProductValue",
                column: "ProductTypeAttributeValueId",
                principalTable: "ProductTypeAttributeValues",
                principalColumn: "ProductTypeAttributeValueId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
                table: "ProductTypeAttributeValues",
                column: "ProductTypeAttributeId",
                principalTable: "ProductTypeAttributes",
                principalColumn: "ProductTypeAttributeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeProductValue_ProductTypeAttributeValues_ProductTypeAttributeValueId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeAttributeValues_ProductTypeAttributes_ProductTypeAttributeId",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributeMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypeAttributeValues",
                table: "ProductTypeAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypeAttributes",
                table: "ProductTypeAttributes");

            migrationBuilder.RenameTable(
                name: "ProductTypeAttributeValues",
                newName: "ProductTypeAttributeValue");

            migrationBuilder.RenameTable(
                name: "ProductTypeAttributes",
                newName: "ProductTypeAttribute");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeValues_ProductTypeAttributeId",
                table: "ProductTypeAttributeValue",
                newName: "IX_ProductTypeAttributeValue_ProductTypeAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeValues_LastEditedByUserId",
                table: "ProductTypeAttributeValue",
                newName: "IX_ProductTypeAttributeValue_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributeValues_CreatedByUserId",
                table: "ProductTypeAttributeValue",
                newName: "IX_ProductTypeAttributeValue_CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributes_LastEditedByUserId",
                table: "ProductTypeAttribute",
                newName: "IX_ProductTypeAttribute_LastEditedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeAttributes_CreatedByUserId",
                table: "ProductTypeAttribute",
                newName: "IX_ProductTypeAttribute_CreatedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "ProductTypeAttribute",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypeAttributeValue",
                table: "ProductTypeAttributeValue",
                column: "ProductTypeAttributeValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypeAttribute",
                table: "ProductTypeAttribute",
                column: "ProductTypeAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttribute_ProductTypeId",
                table: "ProductTypeAttribute",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttribute_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttribute",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttribute_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttribute",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttribute_ProductTypes_ProductTypeId",
                table: "ProductTypeAttribute",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeProductValue_ProductTypeAttributeValue_ProductTypeAttributeValueId",
                table: "ProductTypeAttributeProductValue",
                column: "ProductTypeAttributeValueId",
                principalTable: "ProductTypeAttributeValue",
                principalColumn: "ProductTypeAttributeValueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValue_AspNetUsers_CreatedByUserId",
                table: "ProductTypeAttributeValue",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValue_AspNetUsers_LastEditedByUserId",
                table: "ProductTypeAttributeValue",
                column: "LastEditedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeAttributeValue_ProductTypeAttribute_ProductTypeAttributeId",
                table: "ProductTypeAttributeValue",
                column: "ProductTypeAttributeId",
                principalTable: "ProductTypeAttribute",
                principalColumn: "ProductTypeAttributeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
