using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingCartItemId1",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsOptionWithImage",
                table: "ProductOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductOptions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8272), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(7925), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAELoVgjo9dofDJlmYzd4dhjD+lD3oC6kLh2JAcjGc+hZ2B1kzYoos/k/a3OOtIRRsVw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8798), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8794), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEIfN6G+EabOALQ+w/gdXZ2UBW2Cnn9Xa6u2STejp5E5cQCDNxswLzORtotvN3LnUxA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8799), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8799), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEHzGaLpu/pl3AXxTJyZ4LBDEE6wXP/XJphD0kj1h56Dq3lsyx5CyZ72Gg/kzsScRhQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8801), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8800), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAECrl0dnHdFzeeptHddGa+3uQW6d8oArnbSfD4pvSMd/0mlBX7B+7D0Xk7XpqyYld1w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8802), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 791, DateTimeKind.Unspecified).AddTicks(8802), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(3629), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(3661), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(3664), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(3665), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(5905), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(5913), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(5916), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(5916), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionId",
                keyValue: 1,
                columns: new[] { "IsOptionWithImage", "ProductId1" },
                values: new object[] { true, null });

            migrationBuilder.UpdateData(
                table: "ProductOptions",
                keyColumn: "ProductOptionId",
                keyValue: 2,
                columns: new[] { "IsOptionWithImage", "ProductId1" },
                values: new object[] { true, null });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(7151), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(7157), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(7222), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 13, 3, 0, 746, DateTimeKind.Unspecified).AddTicks(7223), new TimeSpan(0, 7, 0, 0, 0)) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOptions_Products_ProductId1",
                table: "ProductOptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductOptions_ProductId1",
                table: "ProductOptions");

            migrationBuilder.DropColumn(
                name: "IsOptionWithImage",
                table: "ProductOptions");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductOptions");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartItemId1",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3007), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(2677), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEHQaoHEMQVEH/qjlWW/O9BGZnwoDNG6EVFTDCv3HKIow9U5vYEMMl0qupqy/qdlJHQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3553), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3548), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEMTcI3vU09hi1/kGE0O6c8pDdCGGk5o8stlAtublrbdvC5zHjVLYjaH1vgtWVpbHfg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3555), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3554), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEM5LNDzxoNJkMvgmwBhKEx4P55H9KiMu315NychC+Zjx1omBzpfZP0xo1VKxLpSQ6Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreatedWhen", "LastEditedWhen", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3557), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3556), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEC9VwMExDNtN5XlQXvAG59ODtlcxL2YLZGF5HcrbMChOLX/nJ51inLM2a8cMI81+Sw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3558), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 882, DateTimeKind.Unspecified).AddTicks(3557), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(8992), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(9142), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(9146), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 851, DateTimeKind.Unspecified).AddTicks(9147), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2423), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2437), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2440), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4221), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4229), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2,
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4232), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 3, 12, 43, 852, DateTimeKind.Unspecified).AddTicks(4233), new TimeSpan(0, 7, 0, 0, 0)) });
        }
    }
}
