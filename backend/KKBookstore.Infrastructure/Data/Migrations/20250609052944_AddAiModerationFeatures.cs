using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAiModerationFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropIndex(
            //     name: "IX_AspNetUsers_FullName_FullText",
            //     table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AiModerationCategory",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AiModerationDate",
                table: "Ratings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AiModerationExplanation",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AiModerationScore",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAiModerated",
                table: "Ratings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // migrationBuilder.AlterColumn<string>(
            //     name: "FullName",
            //     table: "AspNetUsers",
            //     type: "nvarchar(max)",
            //     nullable: false,
            //     computedColumnSql: "[FirstName] + ' ' + [LastName]",
            //     stored: true,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)",
            //     oldComputedColumnSql: "[FirstName] + ' ' + [LastName]",
            //     oldStored: true,
            //     oldCollation: "SQL_Latin1_General_CP1_CI_AI");

            migrationBuilder.CreateTable(
                name: "ModerationAuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeratorId = table.Column<int>(type: "int", nullable: true),
                    AiScore = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    LastModifierId = table.Column<int>(type: "int", nullable: true),
                    LastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModerationAuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModerationAuditLogs_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModerationAuditLogs_AspNetUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModerationAuditLogs_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModerationAuditLogs_CreatorId",
                table: "ModerationAuditLogs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModerationAuditLogs_LastModifierId",
                table: "ModerationAuditLogs",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModerationAuditLogs_RatingId",
                table: "ModerationAuditLogs",
                column: "RatingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModerationAuditLogs");

            migrationBuilder.DropColumn(
                name: "AiModerationCategory",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AiModerationDate",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AiModerationExplanation",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AiModerationScore",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsAiModerated",
                table: "Ratings");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "[FirstName] + ' ' + [LastName]",
                stored: true,
                collation: "SQL_Latin1_General_CP1_CI_AI",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "[FirstName] + ' ' + [LastName]",
                oldStored: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FullName_FullText",
                table: "AspNetUsers",
                column: "FullName");
        }
    }
}
