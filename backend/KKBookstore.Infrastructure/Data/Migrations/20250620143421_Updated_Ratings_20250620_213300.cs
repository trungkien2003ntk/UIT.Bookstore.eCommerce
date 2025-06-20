using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Ratings_20250620_213300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeleterId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionTime",
                table: "Ratings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ratings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DeleterId",
                table: "RatingLikes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionTime",
                table: "RatingLikes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RatingLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "RatingImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "RatingImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleterId",
                table: "RatingImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionTime",
                table: "RatingImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RatingImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModificationTime",
                table: "RatingImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifierId",
                table: "RatingImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IsDeleted",
                table: "Ratings",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DeleterId",
                table: "Ratings",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_IsDeleted",
                table: "RatingLikes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RatingLikes_DeleterId",
                table: "RatingLikes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingImages_CreatorId",
                table: "RatingImages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingImages_DeleterId",
                table: "RatingImages",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingImages_LastModifierId",
                table: "RatingImages",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_CreatorId",
                table: "RatingImages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_DeleterId",
                table: "RatingImages",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingImages_AspNetUsers_LastModifierId",
                table: "RatingImages",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_AspNetUsers_DeleterId",
                table: "RatingLikes",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_DeleterId",
                table: "Ratings",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_CreatorId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_DeleterId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingImages_AspNetUsers_LastModifierId",
                table: "RatingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_AspNetUsers_DeleterId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_DeleterId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_IsDeleted",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_DeleterId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_IsDeleted",
                table: "RatingLikes");

            migrationBuilder.DropIndex(
                name: "IX_RatingLikes_DeleterId",
                table: "RatingLikes");

            migrationBuilder.DropIndex(
                name: "IX_RatingImages_CreatorId",
                table: "RatingImages");

            migrationBuilder.DropIndex(
                name: "IX_RatingImages_DeleterId",
                table: "RatingImages");

            migrationBuilder.DropIndex(
                name: "IX_RatingImages_LastModifierId",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RatingLikes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RatingLikes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RatingLikes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "RatingImages");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "RatingImages");
        }
    }
}
