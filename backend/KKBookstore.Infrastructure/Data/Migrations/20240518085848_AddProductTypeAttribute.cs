using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTypeAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "DeliveryMethodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OptionValues",
                keyColumn: "OptionValueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OptionValues",
                keyColumn: "OptionValueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UnitMeasures",
                keyColumn: "UnitMeasureId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UnitMeasures",
                keyColumn: "UnitMeasureId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<decimal>(
                name: "Dimension_Height",
                table: "Skus",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dimension_Length",
                table: "Skus",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dimension_Width",
                table: "Skus",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Skus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductTypeAttribute",
                columns: table => new
                {
                    ProductTypeOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttribute", x => x.ProductTypeOptionId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttribute_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttribute_AspNetUsers_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttribute_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributeValue",
                columns: table => new
                {
                    ProductTypeOptionValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductTypeAttributeId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributeValue", x => x.ProductTypeOptionValueId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeValue_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeValue_AspNetUsers_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeValue_ProductTypeAttribute_ProductTypeAttributeId",
                        column: x => x.ProductTypeAttributeId,
                        principalTable: "ProductTypeAttribute",
                        principalColumn: "ProductTypeOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAttributeProductValue",
                columns: table => new
                {
                    ProductTypeAttributeProductValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeAttributeValueId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastEditedWhen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LastEditedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAttributeProductValue", x => x.ProductTypeAttributeProductValueId);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValue_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValue_AspNetUsers_LastEditedByUserId",
                        column: x => x.LastEditedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValue_ProductTypeAttributeValue_ProductTypeAttributeValueId",
                        column: x => x.ProductTypeAttributeValueId,
                        principalTable: "ProductTypeAttributeValue",
                        principalColumn: "ProductTypeOptionValueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypeAttributeProductValue_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttribute_CreatedByUserId",
                table: "ProductTypeAttribute",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttribute_LastEditedByUserId",
                table: "ProductTypeAttribute",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttribute_ProductTypeId",
                table: "ProductTypeAttribute",
                column: "ProductTypeId");

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
                name: "IX_ProductTypeAttributeProductValue_ProductTypeAttributeValueId",
                table: "ProductTypeAttributeProductValue",
                column: "ProductTypeAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValue_CreatedByUserId",
                table: "ProductTypeAttributeValue",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValue_LastEditedByUserId",
                table: "ProductTypeAttributeValue",
                column: "LastEditedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAttributeValue_ProductTypeAttributeId",
                table: "ProductTypeAttributeValue",
                column: "ProductTypeAttributeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTypeAttributeProductValue");

            migrationBuilder.DropTable(
                name: "ProductTypeAttributeValue");

            migrationBuilder.DropTable(
                name: "ProductTypeAttribute");

            migrationBuilder.DropColumn(
                name: "Dimension_Height",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "Dimension_Length",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "Dimension_Width",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(3816), new TimeSpan(0, 7, 0, 0, 0)), null, "Author description 1", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(3866), new TimeSpan(0, 7, 0, 0, 0)), "Author 1" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(3869), new TimeSpan(0, 7, 0, 0, 0)), null, "Author description 2", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(3870), new TimeSpan(0, 7, 0, 0, 0)), "Author 2" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "DeliveryMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5154), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng tiêu chuẩn", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5155), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng tiêu chuẩn" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5156), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng nhanh", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5157), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng nhanh" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5162), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán khi nhận hàng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5163), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán khi nhận hàng" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5164), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán qua thẻ", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5165), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán qua thẻ" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4218), new TimeSpan(0, 7, 0, 0, 0)), null, "Comic books", "Comic", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4220), new TimeSpan(0, 7, 0, 0, 0)), 0, null, "comic" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4222), new TimeSpan(0, 7, 0, 0, 0)), null, "Literature books", "Literature", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4223), new TimeSpan(0, 7, 0, 0, 0)), 0, null, "literature" }
                });

            migrationBuilder.InsertData(
                table: "RefAddressTypes",
                columns: new[] { "RefAddressTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5169), new TimeSpan(0, 7, 0, 0, 0)), null, "Nhà riêng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5170), new TimeSpan(0, 7, 0, 0, 0)), "Nhà riêng" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5171), new TimeSpan(0, 7, 0, 0, 0)), null, "Văn phòng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5172), new TimeSpan(0, 7, 0, 0, 0)), "Văn phòng" }
                });

            migrationBuilder.InsertData(
                table: "UnitMeasures",
                columns: new[] { "UnitMeasureId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4127), new TimeSpan(0, 7, 0, 0, 0)), null, "Dùng cho các loại sách", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4129), new TimeSpan(0, 7, 0, 0, 0)), "quyển" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4130), new TimeSpan(0, 7, 0, 0, 0)), null, "Dùng cho các loại văn phòng phẩm", false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4131), new TimeSpan(0, 7, 0, 0, 0)), "cái" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsActive", "IsBook", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductTypeId", "UnitMeasureId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4297), new TimeSpan(0, 7, 0, 0, 0)), null, "Book description 1", true, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4298), new TimeSpan(0, 7, 0, 0, 0)), "Book 1", 1, 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4301), new TimeSpan(0, 7, 0, 0, 0)), null, "Bút chì Thiên Long, Thiên Thiên Long Long", true, false, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4302), new TimeSpan(0, 7, 0, 0, 0)), "Bút chì Thiên Long", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookAuthorId", "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "WrittenWhen" },
                values: new object[,]
                {
                    { 1, 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4887), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4889), new TimeSpan(0, 7, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4891), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4892), new TimeSpan(0, 7, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductId" },
                values: new object[] { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4963), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4965), new TimeSpan(0, 7, 0, 0, 0)), "Màu sắc", 2 });

            migrationBuilder.InsertData(
                table: "Skus",
                columns: new[] { "SkuId", "Barcode", "Comment", "CreatedBy", "CreatedWhen", "DeletedWhen", "DiscontinuedWhen", "IsActive", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "Quantity", "RecommendedRetailPrice", "Status", "Tags", "TaxRate", "UnitPrice", "ValidFrom", "ValidTo", "SkuValue_Value" },
                values: new object[,]
                {
                    { 1, "0000000000001", "", 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4410), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4411), new TimeSpan(0, 7, 0, 0, 0)), 1, 20, 15000m, "InStock", "", 0m, 10000m, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4379), new TimeSpan(0, 7, 0, 0, 0)), null, "Sku01" },
                    { 2, "0000000000002", "", 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4417), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4418), new TimeSpan(0, 7, 0, 0, 0)), 2, 50, 7000m, "InStock", "", 0m, 5000m, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(4415), new TimeSpan(0, 7, 0, 0, 0)), null, "Sku02" }
                });

            migrationBuilder.InsertData(
                table: "OptionValues",
                columns: new[] { "OptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "Value" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5005), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5023), new TimeSpan(0, 7, 0, 0, 0)), 1, "Đỏ" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5066), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5068), new TimeSpan(0, 7, 0, 0, 0)), 1, "Đỏ" }
                });

            migrationBuilder.InsertData(
                table: "SkuOptionValues",
                columns: new[] { "SkuOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "OptionValueId", "SkuId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5108), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5109), new TimeSpan(0, 7, 0, 0, 0)), 1, 1, 2 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5111), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 15, 16, 46, 0, 366, DateTimeKind.Unspecified).AddTicks(5112), new TimeSpan(0, 7, 0, 0, 0)), 1, 2, 2 }
                });
        }
    }
}
