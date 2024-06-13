using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderSkuId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2012), new TimeSpan(0, 7, 0, 0, 0)), "https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg", new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(1418), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEHg7FYtwsveYIRCZe4aeuvSgfNSgPB4xvkn5pbUACdL789nF0sRSZMNrbj2BAIWNLQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2789), new TimeSpan(0, 7, 0, 0, 0)), "https://i.pinimg.com/originals/c0/4b/01/c04b017b6b9d1c189e15e6559aeb3ca8.png", new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2783), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEP1dwVNxTBRTmXZTXZppsR/Y940hBAwUIh/YDnmOfuGU2IEyDFnCksUM6GOKvz44UA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2792), new TimeSpan(0, 7, 0, 0, 0)), "https://i.pinimg.com/originals/c0/4b/01/c04b017b6b9d1c189e15e6559aeb3ca8.png", new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2791), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEFZ8/Y7WuX/Nfw4g3/j1MfA4FPw6u2a8+NY4i07S1Ofoa6IbcYCB5JKAW6amU4s69Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2793), new TimeSpan(0, 7, 0, 0, 0)), "https://i.pinimg.com/originals/c0/4b/01/c04b017b6b9d1c189e15e6559aeb3ca8.png", new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2793), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEMO/GGhLALVKlyfrl1HvD6P0plawdCp2lmk6+54ea0AAAte9Nyb7q0fDgrswbHis1A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2795), new TimeSpan(0, 7, 0, 0, 0)), "https://pngimg.com/d/avatar_PNG9.png", new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 9, 628, DateTimeKind.Unspecified).AddTicks(2794), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEHuC64Yssb2Tg7Smz6tvR5QKIVfUZ1YbXQ9XVApJ1/CmVTnqlFa6H08O86+TVtNfUQ==" });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 130, DateTimeKind.Unspecified).AddTicks(7995), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 130, DateTimeKind.Unspecified).AddTicks(8028), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 130, DateTimeKind.Unspecified).AddTicks(8031), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 130, DateTimeKind.Unspecified).AddTicks(8032), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 1,
                column: "SkuId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 2,
                column: "SkuId",
                value: 39);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 3,
                column: "SkuId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 4,
                column: "SkuId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 5,
                column: "SkuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 6,
                column: "SkuId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 7,
                column: "SkuId",
                value: 34);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 8,
                column: "SkuId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 9,
                column: "SkuId",
                value: 30);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 10,
                column: "SkuId",
                value: 37);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 12,
                column: "SkuId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 13,
                column: "SkuId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 14,
                column: "SkuId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 15,
                column: "SkuId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 16,
                column: "SkuId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 17,
                column: "SkuId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 18,
                column: "SkuId",
                value: 33);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 19,
                column: "SkuId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 20,
                column: "SkuId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 21,
                column: "SkuId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 22,
                column: "SkuId",
                value: 37);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 23,
                column: "SkuId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 24,
                column: "SkuId",
                value: 34);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 25,
                column: "SkuId",
                value: 33);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 26,
                column: "SkuId",
                value: 40);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 27,
                column: "SkuId",
                value: 36);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 28,
                column: "SkuId",
                value: 28);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 29,
                column: "SkuId",
                value: 38);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 30,
                column: "SkuId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 31,
                column: "SkuId",
                value: 34);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 32,
                column: "SkuId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 33,
                column: "SkuId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 34,
                column: "SkuId",
                value: 30);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 35,
                column: "SkuId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 36,
                column: "SkuId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 37,
                column: "SkuId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 38,
                column: "SkuId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(368), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(376), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(379), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(380), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 15,
                column: "Value",
                value: "Nguy Hiểm Đấy Mau Tránh Xa");

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(1825), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(1833), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(1836), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 13, 2, 49, 10, 131, DateTimeKind.Unspecified).AddTicks(1837), new TimeSpan(0, 7, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(7853), new TimeSpan(0, 7, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(7279), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEEIKN6vV/0mlNmxO0VAUK1PUVBIETwq7pFFBVqSOhUdsCy/rACUH16k7pU77EMEchA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8428), new TimeSpan(0, 7, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8424), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEF3Dv5BYiFDwz0G+4bE/7sAA0A9cr2zC4R4hnOqIU9qp/pEq7T6bb3U7kN5R/ly3WQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8430), new TimeSpan(0, 7, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8430), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAENO5MYaGeFBzdpz+hIsTQUJEo8Ndv+uDhpXUV1/R90LDOuYkqn9WiP9MLe3+bni2AA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8432), new TimeSpan(0, 7, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8431), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEHL+HWZWSNUhbj9S3uGIpVCkX3hQEk3TJa8hvR4NOURPDG8zOWxoTzvx1uahdyfP3Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "ImageUrl", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8433), new TimeSpan(0, 7, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 26, 938, DateTimeKind.Unspecified).AddTicks(8433), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEP7Z79V6Kd7SAB0gZq308v6+CYs4Y8SAbup3pmohkAIKupRy42X2CRHUA3WT3ZJOkQ==" });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3569), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3596), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3598), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(3599), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 1,
                column: "SkuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 2,
                column: "SkuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 3,
                column: "SkuId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 4,
                column: "SkuId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 5,
                column: "SkuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 6,
                column: "SkuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 7,
                column: "SkuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 8,
                column: "SkuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 9,
                column: "SkuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 10,
                column: "SkuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 12,
                column: "SkuId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 13,
                column: "SkuId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 14,
                column: "SkuId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 15,
                column: "SkuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 16,
                column: "SkuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 17,
                column: "SkuId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 18,
                column: "SkuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 19,
                column: "SkuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 20,
                column: "SkuId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 21,
                column: "SkuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 22,
                column: "SkuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 23,
                column: "SkuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 24,
                column: "SkuId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 25,
                column: "SkuId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 26,
                column: "SkuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 27,
                column: "SkuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 28,
                column: "SkuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 29,
                column: "SkuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 30,
                column: "SkuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 31,
                column: "SkuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 32,
                column: "SkuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 33,
                column: "SkuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 34,
                column: "SkuId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 35,
                column: "SkuId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 36,
                column: "SkuId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 37,
                column: "SkuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 38,
                column: "SkuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5826), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5838), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5841), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(5842), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 15,
                column: "Value",
                value: "Nguy Hiểm Đấy, Mau Tránh Xa");

            migrationBuilder.InsertData(
                table: "ProductTypeAttributes",
                columns: new[] { "ProductTypeAttributeId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[] { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), "Tác giả" });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7136), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7142), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7145), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 12, 22, 5, 27, 246, DateTimeKind.Unspecified).AddTicks(7146), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeMappings",
                columns: new[] { "ProductTypeAttributeMappingId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductAttributeId", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 1 },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 33 }
                });
        }
    }
}
