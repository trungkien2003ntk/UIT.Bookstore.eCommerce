using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KKBookstore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(8440), new TimeSpan(0, 7, 0, 0, 0)), null, "Alison Ritchie", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(8930), new TimeSpan(0, 7, 0, 0, 0)), "Alison Ritchie" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9117), new TimeSpan(0, 7, 0, 0, 0)), null, "Alison Edgson", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9120), new TimeSpan(0, 7, 0, 0, 0)), "Alison Edgson" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9121), new TimeSpan(0, 7, 0, 0, 0)), null, "Phan Thị", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9122), new TimeSpan(0, 7, 0, 0, 0)), "Phan Thị" },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9124), new TimeSpan(0, 7, 0, 0, 0)), null, "Alison Edgson", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9125), new TimeSpan(0, 7, 0, 0, 0)), "Alison Edgson" },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9126), new TimeSpan(0, 7, 0, 0, 0)), null, "Nhiều tác giả", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9126), new TimeSpan(0, 7, 0, 0, 0)), "Nhiều tác giả" },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9127), new TimeSpan(0, 7, 0, 0, 0)), null, "Bộ Giáo Dục Và Đào Tạo", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 569, DateTimeKind.Unspecified).AddTicks(9128), new TimeSpan(0, 7, 0, 0, 0)), "Bộ Giáo Dục Và Đào Tạo" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "DeliveryMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(2318), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng tiêu chuẩn", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(2327), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng tiêu chuẩn" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(2329), new TimeSpan(0, 7, 0, 0, 0)), null, "Giao hàng nhanh", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(2330), new TimeSpan(0, 7, 0, 0, 0)), "Giao hàng nhanh" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(4731), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán khi nhận hàng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(4739), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán khi nhận hàng" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(4742), new TimeSpan(0, 7, 0, 0, 0)), null, "Thanh toán qua thẻ", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(4743), new TimeSpan(0, 7, 0, 0, 0)), "Thanh toán qua thẻ" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributes",
                columns: new[] { "ProductTypeAttributeId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2138), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2161), new TimeSpan(0, 7, 0, 0, 0)), "Hình thức bìa" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2162), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2163), new TimeSpan(0, 7, 0, 0, 0)), "NXB" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2164), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2165), new TimeSpan(0, 7, 0, 0, 0)), "Năm xuất bản" },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2165), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2166), new TimeSpan(0, 7, 0, 0, 0)), "Ngôn ngữ" },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2167), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2168), new TimeSpan(0, 7, 0, 0, 0)), "Số trang" },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2168), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2169), new TimeSpan(0, 7, 0, 0, 0)), "Thương hiệu" },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2170), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2170), new TimeSpan(0, 7, 0, 0, 0)), "Nơi sản xuất" },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2171), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2172), new TimeSpan(0, 7, 0, 0, 0)), "Chất liệu" },
                    { 9, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2172), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2173), new TimeSpan(0, 7, 0, 0, 0)), "Độ tuổi" },
                    { 10, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2174), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2175), new TimeSpan(0, 7, 0, 0, 0)), "Lớp" },
                    { 11, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2175), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(2176), new TimeSpan(0, 7, 0, 0, 0)), "Cấp học" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9038), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách viết bằng tiếng Việt", "SáchTiếngViệt", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9047), new TimeSpan(0, 7, 0, 0, 0)), 1, null, "sach-tieng-viet" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9049), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại văn phòng phẩm như bút, viết, tập vở", "VănPhòngPhẩm", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9050), new TimeSpan(0, 7, 0, 0, 0)), 1, null, "van-phong-pham" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9051), new TimeSpan(0, 7, 0, 0, 0)), null, "Đồ chơi các loại", "ĐồChơi", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9051), new TimeSpan(0, 7, 0, 0, 0)), 1, null, "do-choi" }
                });

            migrationBuilder.InsertData(
                table: "RefAddressTypes",
                columns: new[] { "RefAddressTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(6202), new TimeSpan(0, 7, 0, 0, 0)), null, "Nhà riêng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(6210), new TimeSpan(0, 7, 0, 0, 0)), "Nhà riêng" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(6213), new TimeSpan(0, 7, 0, 0, 0)), null, "Văn phòng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 603, DateTimeKind.Unspecified).AddTicks(6214), new TimeSpan(0, 7, 0, 0, 0)), "Văn phòng" }
                });

            migrationBuilder.InsertData(
                table: "UnitMeasures",
                columns: new[] { "UnitMeasureId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 571, DateTimeKind.Unspecified).AddTicks(2645), new TimeSpan(0, 7, 0, 0, 0)), null, "Dùng cho các loại sách", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 571, DateTimeKind.Unspecified).AddTicks(2655), new TimeSpan(0, 7, 0, 0, 0)), "quyển" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 571, DateTimeKind.Unspecified).AddTicks(2656), new TimeSpan(0, 7, 0, 0, 0)), null, "Dùng cho các loại đồ dùng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 571, DateTimeKind.Unspecified).AddTicks(2657), new TimeSpan(0, 7, 0, 0, 0)), "cái" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 571, DateTimeKind.Unspecified).AddTicks(2658), new TimeSpan(0, 7, 0, 0, 0)), null, "Dùng cho các loại đồ dùng", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 571, DateTimeKind.Unspecified).AddTicks(2659), new TimeSpan(0, 7, 0, 0, 0)), "chiếc" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeMappings",
                columns: new[] { "ProductTypeAttributeMappingId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductAttributeId", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9774), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9784), new TimeSpan(0, 7, 0, 0, 0)), 1, 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9786), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9787), new TimeSpan(0, 7, 0, 0, 0)), 2, 1 },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9788), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9788), new TimeSpan(0, 7, 0, 0, 0)), 3, 1 },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9789), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9790), new TimeSpan(0, 7, 0, 0, 0)), 4, 1 },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9791), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9791), new TimeSpan(0, 7, 0, 0, 0)), 5, 1 },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9792), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9793), new TimeSpan(0, 7, 0, 0, 0)), 6, 2 },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9794), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9794), new TimeSpan(0, 7, 0, 0, 0)), 7, 2 },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9795), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9796), new TimeSpan(0, 7, 0, 0, 0)), 8, 2 },
                    { 9, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9796), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9797), new TimeSpan(0, 7, 0, 0, 0)), 6, 3 },
                    { 10, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9798), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9798), new TimeSpan(0, 7, 0, 0, 0)), 7, 3 },
                    { 11, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9800), new TimeSpan(0, 7, 0, 0, 0)), 8, 3 },
                    { 12, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9801), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9801), new TimeSpan(0, 7, 0, 0, 0)), 9, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeValues",
                columns: new[] { "ProductTypeAttributeValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductTypeAttributeId", "Value" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7687), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7698), new TimeSpan(0, 7, 0, 0, 0)), 1, "Bìa mềm" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7699), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7700), new TimeSpan(0, 7, 0, 0, 0)), 2, "Hà Nội" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7701), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7702), new TimeSpan(0, 7, 0, 0, 0)), 3, "2019" },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7702), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7703), new TimeSpan(0, 7, 0, 0, 0)), 4, "Tiếng Việt" },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7704), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7704), new TimeSpan(0, 7, 0, 0, 0)), 5, "28" },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7706), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7706), new TimeSpan(0, 7, 0, 0, 0)), 2, "Văn Học" },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7707), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7708), new TimeSpan(0, 7, 0, 0, 0)), 5, "30" },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7708), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7709), new TimeSpan(0, 7, 0, 0, 0)), 2, "Đại Học Quốc Gia Hà Nội" },
                    { 9, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7710), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7710), new TimeSpan(0, 7, 0, 0, 0)), 3, "2024" },
                    { 10, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7711), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7712), new TimeSpan(0, 7, 0, 0, 0)), 5, "178" },
                    { 11, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7712), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7713), new TimeSpan(0, 7, 0, 0, 0)), 2, "Giáo Dục Việt Nam" },
                    { 12, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7714), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7714), new TimeSpan(0, 7, 0, 0, 0)), 3, "2023" },
                    { 13, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7715), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7716), new TimeSpan(0, 7, 0, 0, 0)), 10, "7" },
                    { 14, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7717), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7717), new TimeSpan(0, 7, 0, 0, 0)), 11, "Cấp 2" },
                    { 15, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7718), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 582, DateTimeKind.Unspecified).AddTicks(7719), new TimeSpan(0, 7, 0, 0, 0)), 5, "0" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ProductTypeId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "DisplayName", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Level", "ParentProductTypeId", "ProductTypeCode" },
                values: new object[,]
                {
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9052), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách Thiếu Nhi", "ThiếuNhi", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9053), new TimeSpan(0, 7, 0, 0, 0)), 2, 1, "thieu-nhi" },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9053), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại SGK và sách tham khảo", "GiáoKhoa-ThamKhảo", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9054), new TimeSpan(0, 7, 0, 0, 0)), 2, 1, "giao-khoa-tham-khao" },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9055), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách Manga và Comic", "Manga-Comic", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9058), new TimeSpan(0, 7, 0, 0, 0)), 2, 1, "manga-comic" },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9082), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại bút viết", "Bút-Viết", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9083), new TimeSpan(0, 7, 0, 0, 0)), 2, 2, "but-viet" },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9084), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại dụng cụ học sinh dùng ở trường", "DụngCụHọcSinh", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9085), new TimeSpan(0, 7, 0, 0, 0)), 2, 2, "dung-cu-hoc-sinh" },
                    { 9, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9086), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại bút viết", "SảnPhẩmVềGiấy", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9086), new TimeSpan(0, 7, 0, 0, 0)), 2, 2, "san-pham-ve-giay" },
                    { 10, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9087), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại thú bông và búp bê", "BúpBê-ThúBông", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9088), new TimeSpan(0, 7, 0, 0, 0)), 2, 3, "bup-be-thu-bong" },
                    { 11, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9089), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại mô hình lắp ghép", "MôHình", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9089), new TimeSpan(0, 7, 0, 0, 0)), 2, 3, "mo-hinh" },
                    { 12, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9090), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại board game", "BoardGame", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9091), new TimeSpan(0, 7, 0, 0, 0)), 2, 3, "board-game" },
                    { 13, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9092), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại truyện thiếu nhi", "TruyệnThiếuNhi", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9092), new TimeSpan(0, 7, 0, 0, 0)), 3, 4, "truyen-thieu-nhi" },
                    { 14, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9093), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách tô màu và luyện chữ cho trẻ em", "TôMàu-LuyệnChữ", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9094), new TimeSpan(0, 7, 0, 0, 0)), 3, 4, "to-mau-luyen-chu" },
                    { 15, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9094), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách tham khảo dành cho học sinh, sinh viên", "SáchThamKhảo", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9095), new TimeSpan(0, 7, 0, 0, 0)), 3, 5, "sach-tham-khao" },
                    { 16, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9096), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách giáo khoa dành cho học sinh", "SáchGiáoKhoa", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9096), new TimeSpan(0, 7, 0, 0, 0)), 3, 5, "sach-giao-khoa" },
                    { 17, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9097), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách Manga", "Manga", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9098), new TimeSpan(0, 7, 0, 0, 0)), 3, 6, "manga" },
                    { 18, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9098), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại sách Comic", "Comic", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9099), new TimeSpan(0, 7, 0, 0, 0)), 3, 6, "comic" },
                    { 19, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9100), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại bút chì", "BútChì", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9100), new TimeSpan(0, 7, 0, 0, 0)), 3, 7, "but-chi" },
                    { 20, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9101), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại bút mực và bút máy", "BútMực-BútMáy", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9102), new TimeSpan(0, 7, 0, 0, 0)), 3, 7, "but-muc-but-may" },
                    { 21, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9102), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại gôm và tẩy", "Gôm-Tẩy", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9103), new TimeSpan(0, 7, 0, 0, 0)), 3, 8, "gom-tay" },
                    { 22, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9104), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại thú bông", "ThúBông", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9104), new TimeSpan(0, 7, 0, 0, 0)), 3, 10, "thu-bong" },
                    { 23, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9105), new TimeSpan(0, 7, 0, 0, 0)), null, "Các loại búp bê", "BúpBê", false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 572, DateTimeKind.Unspecified).AddTicks(9106), new TimeSpan(0, 7, 0, 0, 0)), 3, 10, "bup-be" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeMappings",
                columns: new[] { "ProductTypeAttributeMappingId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductAttributeId", "ProductTypeId" },
                values: new object[,]
                {
                    { 13, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9802), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9802), new TimeSpan(0, 7, 0, 0, 0)), 10, 16 },
                    { 14, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9803), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 581, DateTimeKind.Unspecified).AddTicks(9804), new TimeSpan(0, 7, 0, 0, 0)), 11, 16 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedBy", "CreatedWhen", "DeletedWhen", "Description", "IsActive", "IsBook", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductTypeId", "UnitMeasureId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2133), new TimeSpan(0, 7, 0, 0, 0)), null, "Gia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.\r\n\r\nMã hàng	8935212367677\r\nNgày Dự Kiến Phát Hành	20/05/2024\r\nTên Nhà Cung Cấp	Đinh Tị\r\nTác giả	Alison Ritchie, Alison Edgson\r\nNgười Dịch	Thành Đạt\r\nNXB	Hà Nội\r\nNăm XB	2024\r\nTrọng lượng (gr)	100\r\nKích Thước Bao Bì	25 x 20.5 x 0.2 cm\r\nSố trang	28\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nGia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.", true, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2143), new TimeSpan(0, 7, 0, 0, 0)), "Gia Đình Thương Yêu", 13, 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2144), new TimeSpan(0, 7, 0, 0, 0)), null, "Tô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.\r\n\r\nMã hàng	8936071294357\r\nTên Nhà Cung Cấp	Cty Phan Thị\r\nTác giả	Phan Thị\r\nNXB	Văn Học\r\nNăm XB	2024\r\nTrọng lượng (gr)	150\r\nKích Thước Bao Bì	24 x 16 x 0.5 cm\r\nSố trang	30\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Tô màu, luyện chữ bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nTô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.", true, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2145), new TimeSpan(0, 7, 0, 0, 0)), "Tô Màu Bốn Mùa", 14, 1 },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2146), new TimeSpan(0, 7, 0, 0, 0)), null, "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)\r\n\r\nBản đồ là phương tiện giảng dạy và học tập điạ lý không thể thiếu trong nhà trường phổ thông. Cùng với sách giáo khoa, Atlat địa lí Việt Nam là nguồn cung cấp kiến thức, thông tin tổng hợp và hệ thống giúp giáo viên đổi mới phương pháp dạy học, hỗ trợ học.\r\n\r\nĐể đáp ứng nhu cầu bức thiết đó, NXB Giáo dục Việt Nam trân trọng giới thiệu tập Atlat địa lý Việt Nam gồm 21 bản đồ, được in màu rõ nét, liên quan đến các lĩnh vực kinh tế - xã hội. Các bản đồ được xây dựng theo nguồn số liệu của Nhà xuất bản thống kê - Tổng cục thống kê. Đây là tài liệu được phép mang vào phòng thi tốt nghiệp THPT môn Địa lý do Bộ Giáo dục và Đào tạo quy định.\r\n\r\nNội dung gồm có:\r\n\r\n1. Kí hiệu chung\r\n\r\n2. Hành chính 4. Hình thể\r\n\r\n4. Địa khoáng sản\r\n\r\n5. Khí hậu\r\n\r\n6. Các hệ thống sông\r\n\r\n7. Các nhóm và các loại đât chính\r\n\r\n8. Thực vật và động vật\r\n\r\n9. Các miền tự nhiên\r\n\r\n11. Dân số\r\n\r\n11. Dân tộc\r\n\r\n12. Kinh tế chung\r\n\r\n14. Nông nghiệp chung\r\n\r\n14. Lâm nông và thuỷ sản\r\n\r\n15. Công nghiệp chung\r\n\r\n16. Các ngành công nghiệp trọng điểm\r\n\r\n17. Giao thông\r\n\r\n18. Thương mại\r\n\r\n19. Du lịch\r\n\r\n21. Vùng trunh du và miền núi Bắc Bộ, vùn Đồng Bằng Sông Hồng\r\n\r\n21. Vùng Bắc Trung Bộ\r\n\r\n22. Vùng Duyên Hải Nam Trung Bộ, Vùng Tây Nguyên\r\n\r\n24. Vùng Đông Nam Bộ, Vùng Đồng Bằng Sông Cửu Long\r\n\r\n25. Các vùng kinh tế trọng điểm\r\n\r\nNgoài ra, NXB Giáo dục Việt Nam đã biên soạn cuốn “Hướng dẫn sử dụng Atlat Địa lý Việt Nam” dùng cho học sinh THCS và THPT, ôn tập thi tốt nghiệp THPT, thi ĐH, CĐ và ôn luyện thi học sinh giỏi quốc gia.\r\n\r\nNội dung sách gồm ba phần:\r\n\r\nPhần 1: Một số kiến thức về phương pháp sử dụng bản đồ giáo khoa;\r\n\r\nPhần 2: Giới thiệu về Atlat Địa lý Việt Nam.\r\n\r\nPhần 4: Hướng dẫn sử dụng Atlat Địa lý Việt Nam.", true, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2147), new TimeSpan(0, 7, 0, 0, 0)), "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)", 15, 1 },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2148), new TimeSpan(0, 7, 0, 0, 0)), null, "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (2023)\r\n\r\nSTT	Tên hàng\r\n1	Bài tập Mĩ thuật 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n2	Bài tập Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n3	Bài tập Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Bài tập Lịch sử và Địa lí 7 (Phần Địa lí) (Chân Trời Sáng Tạo) (2023)\r\n5	Bài tập Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Bài tập Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Bài tập Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n8	Bài tập Lịch sử và Địa lí 7 (Phần Lịch sử) (Chân Trời Sáng Tạo) (2023)\r\n9	Bài tập Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n10	Bài tập Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n11	Bài tập Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n12	Bài tập Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\nMã hàng	3300000027357\r\nCấp Độ/ Lớp	Lớp 7\r\nCấp Học	Cấp 2\r\nNhà Cung Cấp	Nhà xuất bản Giáo Dục\r\nTác giả	Bộ Giáo Dục Và Đào Tạo\r\nNXB	Giáo Dục Việt Nam\r\nNăm XB	2023\r\nNgôn Ngữ	Tiếng Việt\r\nTrọng lượng (gr)	1200\r\nKích Thước Bao Bì	24 x 17 x 3 cm\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (2023)\r\n\r\nSTT	Tên hàng\r\n1	Bài tập Mĩ thuật 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n2	Bài tập Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n3	Bài tập Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Bài tập Lịch sử và Địa lí 7 (Phần Địa lí) (Chân Trời Sáng Tạo) (2023)\r\n5	Bài tập Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Bài tập Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Bài tập Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n8	Bài tập Lịch sử và Địa lí 7 (Phần Lịch sử) (Chân Trời Sáng Tạo) (2023)\r\n9	Bài tập Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n10	Bài tập Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n11	Bài tập Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n12	Bài tập Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)", true, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2148), new TimeSpan(0, 7, 0, 0, 0)), "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (Chuẩn)", 16, 1 },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2149), new TimeSpan(0, 7, 0, 0, 0)), null, "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT	Tên hàng\r\n1	Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2	Giáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3	Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5	Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8	Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9	Lịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10	Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11	Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12	Mĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)\r\nMã hàng	3300000027333\r\nCấp Độ/ Lớp	Lớp 7\r\nCấp Học	Cấp 2\r\nNhà Cung Cấp	Nhà xuất bản Giáo Dục\r\nTác giả	Bộ Giáo Dục Và Đào Tạo\r\nNXB	Giáo Dục Việt Nam\r\nNăm XB	2023\r\nNgôn Ngữ	Tiếng Việt\r\nTrọng lượng (gr)	2500\r\nKích Thước Bao Bì	24 x 17 x 6 cm\r\nHình thức	Bìa Mềm\r\nSản phẩm bán chạy nhất	Top 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT	Tên hàng\r\n1	Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2	Giáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3	Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4	Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5	Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6	Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7	Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8	Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9	Lịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10	Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11	Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12	Mĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)", true, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 585, DateTimeKind.Unspecified).AddTicks(2150), new TimeSpan(0, 7, 0, 0, 0)), "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (Chuẩn) (Không Tin Học)", 16, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookAuthorId", "AuthorId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "WrittenWhen" },
                values: new object[,]
                {
                    { 1, 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3739), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3749), new TimeSpan(0, 7, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3750), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3751), new TimeSpan(0, 7, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3752), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3753), new TimeSpan(0, 7, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3753), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3754), new TimeSpan(0, 7, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 5, 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3755), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 600, DateTimeKind.Unspecified).AddTicks(3756), new TimeSpan(0, 7, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ProductOptionId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "Name", "ProductId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(1354), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(1364), new TimeSpan(0, 7, 0, 0, 0)), "Phần", 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(1365), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(1365), new TimeSpan(0, 7, 0, 0, 0)), "Tập", 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeAttributeProductValue",
                columns: new[] { "ProductTypeAttributeProductValueId", "CreatedBy", "CreatedWhen", "LastEditedBy", "LastEditedWhen", "ProductId", "ProductTypeAttributeValueId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5479), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5489), new TimeSpan(0, 7, 0, 0, 0)), 1, 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5491), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5491), new TimeSpan(0, 7, 0, 0, 0)), 1, 2 },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5492), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5493), new TimeSpan(0, 7, 0, 0, 0)), 1, 9 },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5494), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5494), new TimeSpan(0, 7, 0, 0, 0)), 1, 4 },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5495), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5496), new TimeSpan(0, 7, 0, 0, 0)), 1, 5 },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5497), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5498), new TimeSpan(0, 7, 0, 0, 0)), 2, 1 },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5498), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5499), new TimeSpan(0, 7, 0, 0, 0)), 2, 6 },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5500), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5501), new TimeSpan(0, 7, 0, 0, 0)), 2, 9 },
                    { 9, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5501), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5502), new TimeSpan(0, 7, 0, 0, 0)), 2, 4 },
                    { 10, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5503), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5503), new TimeSpan(0, 7, 0, 0, 0)), 2, 7 },
                    { 11, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5504), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5505), new TimeSpan(0, 7, 0, 0, 0)), 3, 1 },
                    { 12, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5505), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5506), new TimeSpan(0, 7, 0, 0, 0)), 3, 8 },
                    { 13, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5507), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5507), new TimeSpan(0, 7, 0, 0, 0)), 3, 4 },
                    { 14, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5508), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5509), new TimeSpan(0, 7, 0, 0, 0)), 3, 10 },
                    { 15, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5510), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5510), new TimeSpan(0, 7, 0, 0, 0)), 4, 1 },
                    { 16, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5511), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5512), new TimeSpan(0, 7, 0, 0, 0)), 4, 11 },
                    { 17, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5512), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5513), new TimeSpan(0, 7, 0, 0, 0)), 4, 13 },
                    { 18, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5514), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5514), new TimeSpan(0, 7, 0, 0, 0)), 4, 14 },
                    { 19, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5515), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5515), new TimeSpan(0, 7, 0, 0, 0)), 4, 12 },
                    { 20, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5516), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5517), new TimeSpan(0, 7, 0, 0, 0)), 4, 4 },
                    { 21, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5517), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5518), new TimeSpan(0, 7, 0, 0, 0)), 4, 15 },
                    { 22, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5519), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5519), new TimeSpan(0, 7, 0, 0, 0)), 5, 1 },
                    { 23, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5520), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5521), new TimeSpan(0, 7, 0, 0, 0)), 5, 11 },
                    { 24, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5521), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5522), new TimeSpan(0, 7, 0, 0, 0)), 5, 13 },
                    { 25, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5523), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5523), new TimeSpan(0, 7, 0, 0, 0)), 5, 14 },
                    { 26, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5524), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5524), new TimeSpan(0, 7, 0, 0, 0)), 5, 12 },
                    { 27, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5525), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5526), new TimeSpan(0, 7, 0, 0, 0)), 5, 4 },
                    { 28, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5527), new TimeSpan(0, 7, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 583, DateTimeKind.Unspecified).AddTicks(5527), new TimeSpan(0, 7, 0, 0, 0)), 5, 15 }
                });

            migrationBuilder.InsertData(
                table: "Skus",
                columns: new[] { "SkuId", "Dimension_Height", "Dimension_Length", "Dimension_Width", "Barcode", "Comment", "CreatedBy", "CreatedWhen", "DeletedWhen", "DiscontinuedWhen", "IsActive", "IsDeleted", "LastEditedBy", "LastEditedWhen", "ProductId", "Quantity", "RecommendedRetailPrice", "Status", "Tags", "TaxRate", "UnitPrice", "ValidFrom", "ValidTo", "Weight", "SkuValue_Value" },
                values: new object[,]
                {
                    { 1, 25m, 0.2m, 20.5m, "0765083359063", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(3964), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(3999), new TimeSpan(0, 7, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00001" },
                    { 2, 25m, 0.2m, 20.5m, "0462639494097", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4000), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4001), new TimeSpan(0, 7, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2020), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00002" },
                    { 3, 25m, 0.2m, 20.5m, "1482949057379", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4002), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4002), new TimeSpan(0, 7, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2110), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00003" },
                    { 4, 25m, 0.2m, 20.5m, "6267047946549", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4003), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4004), new TimeSpan(0, 7, 0, 0, 0)), 1, 20, 35000m, "InStock", "", 0m, 31500m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2180), new TimeSpan(0, 0, 0, 0, 0)), null, 100, "SKU00004" },
                    { 5, 24.0m, 0.5m, 16.0m, "9606726677252", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4005), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4005), new TimeSpan(0, 7, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2510), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00005" },
                    { 6, 24.0m, 0.5m, 16.0m, "8379760183413", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4006), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4007), new TimeSpan(0, 7, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00006" },
                    { 7, 24.0m, 0.5m, 16.0m, "4501110187232", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4007), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4008), new TimeSpan(0, 7, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2740), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00007" },
                    { 8, 24.0m, 0.5m, 16.0m, "1262570326893", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4009), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4009), new TimeSpan(0, 7, 0, 0, 0)), 2, 20, 20000m, "InStock", "", 0m, 18000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2850), new TimeSpan(0, 0, 0, 0, 0)), null, 150, "SKU00008" },
                    { 9, 24.0m, 0.5m, 16.0m, "2875627948270", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4010), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4011), new TimeSpan(0, 7, 0, 0, 0)), 3, 20, 31000m, "InStock", "", 0m, 27900m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(2980), new TimeSpan(0, 0, 0, 0, 0)), null, 190, "SKU00009" },
                    { 10, 24.0m, 3.0m, 17.0m, "5273035092419", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4012), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4012), new TimeSpan(0, 7, 0, 0, 0)), 4, 20, 170000m, "InStock", "", 0m, 170000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(3120), new TimeSpan(0, 0, 0, 0, 0)), null, 1200, "SKU00010" },
                    { 11, 24.0m, 6.0m, 17.0m, "3169651688303", "", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4013), new TimeSpan(0, 7, 0, 0, 0)), null, null, true, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 597, DateTimeKind.Unspecified).AddTicks(4014), new TimeSpan(0, 7, 0, 0, 0)), 5, 20, 180000m, "InStock", "", 0m, 180000m, new DateTimeOffset(new DateTime(2024, 5, 18, 16, 0, 46, 140, DateTimeKind.Unspecified).AddTicks(3210), new TimeSpan(0, 0, 0, 0, 0)), null, 2500, "SKU00011" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionValues",
                columns: new[] { "ProductOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "Value" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8522), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8532), new TimeSpan(0, 7, 0, 0, 0)), 1, "Một ngày của tớ và bố" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8533), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8534), new TimeSpan(0, 7, 0, 0, 0)), 1, "Một ngày của tớ và mẹ" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8535), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8535), new TimeSpan(0, 7, 0, 0, 0)), 1, "Một ngày của tớ và ông" },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8536), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8537), new TimeSpan(0, 7, 0, 0, 0)), 1, "Một ngày của tớ và bà" },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8538), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8538), new TimeSpan(0, 7, 0, 0, 0)), 2, "Tập 1 - Mùa Xuân" },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8539), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8540), new TimeSpan(0, 7, 0, 0, 0)), 2, "Tập 2 - Mùa Hạ" },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8541), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8541), new TimeSpan(0, 7, 0, 0, 0)), 2, "Tập 3 - Mùa Thu" },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8542), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 601, DateTimeKind.Unspecified).AddTicks(8542), new TimeSpan(0, 7, 0, 0, 0)), 2, "Tập 4 - Mùa Đông" }
                });

            migrationBuilder.InsertData(
                table: "SkuImages",
                columns: new[] { "SkuImageId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LargeImageUrl", "LastEditedBy", "LastEditedWhen", "SkuId", "ThumbnailImageUrl" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6229), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6239), new TimeSpan(0, 7, 0, 0, 0)), 1, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg" },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6240), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6241), new TimeSpan(0, 7, 0, 0, 0)), 2, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg" },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6242), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6242), new TimeSpan(0, 7, 0, 0, 0)), 3, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg" },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6243), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6244), new TimeSpan(0, 7, 0, 0, 0)), 4, "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg" },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6245), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6245), new TimeSpan(0, 7, 0, 0, 0)), 5, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg" },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6246), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6246), new TimeSpan(0, 7, 0, 0, 0)), 6, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg" },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6247), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6248), new TimeSpan(0, 7, 0, 0, 0)), 7, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg" },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6249), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6249), new TimeSpan(0, 7, 0, 0, 0)), 8, "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg" },
                    { 9, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6250), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6251), new TimeSpan(0, 7, 0, 0, 0)), 9, "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg" },
                    { 10, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6252), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6252), new TimeSpan(0, 7, 0, 0, 0)), 10, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg" },
                    { 11, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6253), new TimeSpan(0, 7, 0, 0, 0)), null, false, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg", 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 599, DateTimeKind.Unspecified).AddTicks(6254), new TimeSpan(0, 7, 0, 0, 0)), 11, "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg" }
                });

            migrationBuilder.InsertData(
                table: "SkuOptionValues",
                columns: new[] { "SkuOptionValueId", "CreatedBy", "CreatedWhen", "DeletedWhen", "IsDeleted", "LastEditedBy", "LastEditedWhen", "OptionId", "OptionValueId", "SkuId" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6379), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6389), new TimeSpan(0, 7, 0, 0, 0)), 1, 1, 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6390), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6391), new TimeSpan(0, 7, 0, 0, 0)), 1, 2, 2 },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6392), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6392), new TimeSpan(0, 7, 0, 0, 0)), 1, 3, 3 },
                    { 4, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6393), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6394), new TimeSpan(0, 7, 0, 0, 0)), 1, 4, 4 },
                    { 5, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6395), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6396), new TimeSpan(0, 7, 0, 0, 0)), 2, 5, 5 },
                    { 6, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6397), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6397), new TimeSpan(0, 7, 0, 0, 0)), 2, 6, 6 },
                    { 7, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6398), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6399), new TimeSpan(0, 7, 0, 0, 0)), 2, 7, 7 },
                    { 8, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6399), new TimeSpan(0, 7, 0, 0, 0)), null, false, 9, new DateTimeOffset(new DateTime(2024, 5, 19, 2, 27, 19, 602, DateTimeKind.Unspecified).AddTicks(6400), new TimeSpan(0, 7, 0, 0, 0)), 2, 8, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "BookAuthorId",
                keyValue: 5);

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
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeMappings",
                keyColumn: "ProductTypeAttributeMappingId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeProductValue",
                keyColumn: "ProductTypeAttributeProductValueId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RefAddressTypes",
                keyColumn: "RefAddressTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SkuImages",
                keyColumn: "SkuImageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SkuOptionValues",
                keyColumn: "SkuOptionValueId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UnitMeasures",
                keyColumn: "UnitMeasureId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UnitMeasures",
                keyColumn: "UnitMeasureId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductOptionValues",
                keyColumn: "ProductOptionValueId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributeValues",
                keyColumn: "ProductTypeAttributeValueId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skus",
                keyColumn: "SkuId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "ProductOptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductTypeAttributes",
                keyColumn: "ProductTypeAttributeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UnitMeasures",
                keyColumn: "UnitMeasureId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "ProductTypeId",
                keyValue: 1);
        }
    }
}
