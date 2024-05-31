using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Products_ProductId1",
                table: "ProductOptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductOptions_ProductId1",
                table: "ProductOptions");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductOptions");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 1, 4 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(2002), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEFz3WBGpH22uTgBP0nDFxkekYlnX2NH1Y2Vb9qcslmnPefV6+Gb0ytwwbIWDudMlTw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3033), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3028), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEEfXZUPcjwqBR7atHwoq9QimUhPwzGx9JbGJ3dhUdBTguzBL0faxYUnS30wY9Fsukg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3035), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3034), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAELSWfG2DrmdIxUfkl6+iEEIkIIEniXXZEU2RDrRXZpw8feJCyM9dSC6xj9JdCGUvtA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3036), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3036), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEN+t9sGU7HJECQzejj2ZeWRPHWkWM8Dq9kjt9gJs3qtFb9/U1GBy3jR3zJW/EfZ2/w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 34, 757, DateTimeKind.Unspecified).AddTicks(3037), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEK+nPg1lCNp7S3SeMWiJIV3k44NCOoWTTcHYvi1R079ngtTTWxNuz3crvmWg7uYNJA==" });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 72, DateTimeKind.Unspecified).AddTicks(8497), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 72, DateTimeKind.Unspecified).AddTicks(8532), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 72, DateTimeKind.Unspecified).AddTicks(8535), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 72, DateTimeKind.Unspecified).AddTicks(8536), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(857), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(866), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(871), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(872), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(2336), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(2343), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(2346), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 40, 35, 73, DateTimeKind.Unspecified).AddTicks(2347), new TimeSpan(0, 7, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductOptions",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(5451), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(5046), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEFNmXTxdS4RR8cRhsrGfNSBBTeO2ZYZb3psOJHBOAtf3Yaxp0L+I/oys6jFuzMFD2A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6048), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6044), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAECEoBZj0us1cWjo9hh4FmjRtqlY4dWVsbHO8ZmKTH0Kc1W4jrk48J/5oHH4QfNaYGw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6051), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6050), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEGQ4ganQ46DGCNSSnaodxafrKLI2D8AoYBf+EWDmV2HWBidbHAccVLGehzuaMx7Oow==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6052), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6052), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEM5kDuPrzEPVsNQAiJWuXSk6t4VPzDq33CdxGiMuRUqzGQOuWQliDrZ8Z99kV7b0GQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6054), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6053), new TimeSpan(0, 7, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(1758), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(1801), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(1802), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(4706), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(4726), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(4730), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(4731), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionId",
                keyValue: 1,
                column: "ProductId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionId",
                keyValue: 2,
                column: "ProductId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(6506), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(6519), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(6523), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 16, 226, DateTimeKind.Unspecified).AddTicks(6524), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_ProductId1",
                table: "ProductOptions",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOptions_Products_ProductId1",
                table: "ProductOptions",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
