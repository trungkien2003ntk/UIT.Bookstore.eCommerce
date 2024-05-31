using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 5 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(5607), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(5235), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEBk9sNh5Iiu0Pr4cxVt/MU4EpLx+ShX30Na4R9UZ0PACeQ+Njuh8kCEwI/Av+3Yycw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6176), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6172), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEOH5hZ0eTEh66/HWr1bRrj7EDAwxTTY6Y08ZUUuYh5Xh6875ey8eXg5iSxdO5kTQmA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6179), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6178), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEDnZZOgCfEFJggME740YsekxU3jXwFUABOioY6ys5dl7bz3/96We+2WT5KZIKsU4Og==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6180), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6179), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEMr4OxbuPLOJKo2ZCknTXrJsiQzp4bwUkKBuRBTt8bx5JhMgy3UlIIqZn0YmonYmWg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6181), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 529, DateTimeKind.Unspecified).AddTicks(6181), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEJnPZSEzBJDTQoDSNMAVlqzC5WUHceKZFfzqKs0rjuE58mXebj43slD+7aF7+xJFwA==" });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(5955), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(5983), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(5986), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(5987), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(8280), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(8287), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(8290), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(8291), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(9592), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(9599), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(9602), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 46, 17, 860, DateTimeKind.Unspecified).AddTicks(9603), new TimeSpan(0, 7, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 5 });

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
    }
}
