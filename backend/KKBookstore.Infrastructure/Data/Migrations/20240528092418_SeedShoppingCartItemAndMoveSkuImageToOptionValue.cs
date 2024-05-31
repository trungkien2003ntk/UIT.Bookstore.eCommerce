using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedShoppingCartItemAndMoveSkuImageToOptionValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkuImages");

            migrationBuilder.AddColumn<string>(
                name: "LargeImageUrl",
                table: "ProductOptionValues",
                type: "varchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImageUrl",
                table: "ProductOptionValues",
                type: "varchar(MAX)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "CreatedWhen", "LastEditedWhen" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6054), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 28, 16, 24, 15, 915, DateTimeKind.Unspecified).AddTicks(6053), new TimeSpan(0, 7, 0, 0, 0)) });

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

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "ProductId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 1,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 2,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 3,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 4,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 5,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 6,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 7,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 8,
                columns: new[] { "LargeImageUrl", "ThumbnailImageUrl" },
                values: new object[] { "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg", "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg" });

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

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "CreatedBy", "CreatedWhen", "CustomerId", "LastEditedBy", "LastEditedWhen", "Quantity", "SkuId" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, 3 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 5 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, 11 },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ProductImageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ProductImageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ProductImageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "LargeImageUrl",
                table: "ProductOptionValues");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageUrl",
                table: "ProductOptionValues");

            migrationBuilder.CreateTable(
                name: "SkuImages",
                columns: table => new
                {
                    SkuImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LargeImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ThumbnailImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuImages", x => x.SkuImageId);
                    table.ForeignKey(
                        name: "FK_SkuImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuImages_AspNetUsers_LastEditedBy",
                        column: x => x.LastEditedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkuImages_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "SkuId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "SkuImages",
                columns: new[] { "SkuImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "SkuId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg" },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg" },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg" },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 7, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 8, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg" },
                    { 9, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 9, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg" },
                    { 10, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 10, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg" },
                    { 11, 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg", 2, new DateTimeOffset(new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(3843), new TimeSpan(0, 0, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkuImages_CreatedBy",
                table: "SkuImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SkuImages_LastEditedBy",
                table: "SkuImages",
                column: "LastEditedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SkuImages_SkuId",
                table: "SkuImages",
                column: "SkuId");
        }
    }
}
