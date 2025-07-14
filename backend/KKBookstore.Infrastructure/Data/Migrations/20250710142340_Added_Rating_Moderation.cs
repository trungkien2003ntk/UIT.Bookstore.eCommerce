using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Rating_Moderation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModerationLevel",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentimentLabel",
                table: "Ratings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SentimentScore",
                table: "Ratings",
                type: "decimal(5,4)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModerationLevel",
                table: "ModerationAuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThresholdUsed",
                table: "ModerationAuditLogs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationLevel",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "SentimentLabel",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "SentimentScore",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ModerationLevel",
                table: "ModerationAuditLogs");

            migrationBuilder.DropColumn(
                name: "ThresholdUsed",
                table: "ModerationAuditLogs");
        }
    }
}
