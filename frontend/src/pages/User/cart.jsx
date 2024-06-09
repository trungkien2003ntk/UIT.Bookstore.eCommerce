import { useState } from "react"
import { Checkbox } from "@mui/material"
import { green } from "@mui/material/colors"

import ProductItemCart from "../../components/Cart/ProductItemCart"
import VND from "../../components/vnd"
import Button from "../../components/Button"
import Modal from "../../components/Modal"
import VoucherRadio from "../../components/Checkout/VoucherRadio"

const items = [
  {
    productId: 1,
    skuId: 1,
    skuName: "Một ngày của tớ và bố",
    productName: "Gia Đình Thương Yêu",
    productTypeId: 13,
    unitPrice: 31500,
    recommendedRetailPrice: 35000,
    quantity: 6,
    availableQuantity: 20,
    totalQuantity: 20,
    imageUrl:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg",
    description:
      "Gia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.\r\n\r\nMã hàng\t8935212367677\r\nNgày Dự Kiến Phát Hành\t20/05/2024\r\nTên Nhà Cung Cấp\tĐinh Tị\r\nTác giả\tAlison Ritchie, Alison Edgson\r\nNgười Dịch\tThành Đạt\r\nNXB\tHà Nội\r\nNăm XB\t2024\r\nTrọng lượng (gr)\t100\r\nKích Thước Bao Bì\t25 x 20.5 x 0.2 cm\r\nSố trang\t28\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nGia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.",
    createdWhen: "2024-05-20T00:00:00.0003843+00:00",
    skuVariations: [
      {
        productId: 1,
        skuValue: "SKU00001",
        skuName: "Một ngày của tớ và bố",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [0],
        id: 1,
      },
      {
        productId: 1,
        skuValue: "SKU00003",
        skuName: "Một ngày của tớ và ông",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [2],
        id: 3,
      },
      {
        productId: 1,
        skuValue: "SKU00002",
        skuName: "Một ngày của tớ và mẹ",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [1],
        id: 2,
      },
      {
        productId: 1,
        skuValue: "SKU00004",
        skuName: "Một ngày của tớ và bà",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [3],
        id: 4,
      },
    ],
    productOptions: [
      {
        name: "Phần",
        values: [
          "Một ngày của tớ và bố",
          "Một ngày của tớ và mẹ",
          "Một ngày của tớ và ông",
          "Một ngày của tớ và bà",
        ],
        images: [
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg",
        ],
      },
    ],
    id: 1,
  },
  {
    productId: 1,
    skuId: 3,
    skuName: "Một ngày của tớ và ông",
    productName: "Gia Đình Thương Yêu",
    productTypeId: 13,
    unitPrice: 31500,
    recommendedRetailPrice: 35000,
    quantity: 2,
    availableQuantity: 20,
    totalQuantity: 20,
    imageUrl:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg",
    description:
      "Gia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.\r\n\r\nMã hàng\t8935212367677\r\nNgày Dự Kiến Phát Hành\t20/05/2024\r\nTên Nhà Cung Cấp\tĐinh Tị\r\nTác giả\tAlison Ritchie, Alison Edgson\r\nNgười Dịch\tThành Đạt\r\nNXB\tHà Nội\r\nNăm XB\t2024\r\nTrọng lượng (gr)\t100\r\nKích Thước Bao Bì\t25 x 20.5 x 0.2 cm\r\nSố trang\t28\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nGia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.",
    createdWhen: "2024-05-20T00:00:00.0003843+00:00",
    skuVariations: [
      {
        productId: 1,
        skuValue: "SKU00001",
        skuName: "Một ngày của tớ và bố",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [0],
        id: 1,
      },
      {
        productId: 1,
        skuValue: "SKU00003",
        skuName: "Một ngày của tớ và ông",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [2],
        id: 3,
      },
      {
        productId: 1,
        skuValue: "SKU00002",
        skuName: "Một ngày của tớ và mẹ",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [1],
        id: 2,
      },
      {
        productId: 1,
        skuValue: "SKU00004",
        skuName: "Một ngày của tớ và bà",
        unitPrice: 31500,
        recommendedRetailPrice: 35000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Phần"],
        optionIndex: [3],
        id: 4,
      },
    ],
    productOptions: [
      {
        name: "Phần",
        values: [
          "Một ngày của tớ và bố",
          "Một ngày của tớ và mẹ",
          "Một ngày của tớ và ông",
          "Một ngày của tớ và bà",
        ],
        images: [
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg",
        ],
      },
    ],
    id: 2,
  },
  {
    productId: 2,
    skuId: 5,
    skuName: "Tập 1 - Mùa Xuân",
    productName: "Tô Màu Bốn Mùa",
    productTypeId: 14,
    unitPrice: 18000,
    recommendedRetailPrice: 20000,
    quantity: 1,
    availableQuantity: 20,
    totalQuantity: 20,
    imageUrl:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg",
    description:
      "Tô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.\r\n\r\nMã hàng\t8936071294357\r\nTên Nhà Cung Cấp\tCty Phan Thị\r\nTác giả\tPhan Thị\r\nNXB\tVăn Học\r\nNăm XB\t2024\r\nTrọng lượng (gr)\t150\r\nKích Thước Bao Bì\t24 x 16 x 0.5 cm\r\nSố trang\t30\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Tô màu, luyện chữ bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nTô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.",
    createdWhen: "2024-05-20T00:00:00.0003843+00:00",
    skuVariations: [
      {
        productId: 2,
        skuValue: "SKU00005",
        skuName: "Tập 1 - Mùa Xuân",
        unitPrice: 18000,
        recommendedRetailPrice: 20000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Tập"],
        optionIndex: [0],
        id: 5,
      },
      {
        productId: 2,
        skuValue: "SKU00006",
        skuName: "Tập 2 - Mùa Hạ",
        unitPrice: 18000,
        recommendedRetailPrice: 20000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Tập"],
        optionIndex: [1],
        id: 6,
      },
      {
        productId: 2,
        skuValue: "SKU00007",
        skuName: "Tập 3 - Mùa Thu",
        unitPrice: 18000,
        recommendedRetailPrice: 20000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Tập"],
        optionIndex: [2],
        id: 7,
      },
      {
        productId: 2,
        skuValue: "SKU00008",
        skuName: "Tập 4 - Mùa Đông",
        unitPrice: 18000,
        recommendedRetailPrice: 20000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: ["Tập"],
        optionIndex: [3],
        id: 8,
      },
    ],
    productOptions: [
      {
        name: "Tập",
        values: [
          "Tập 1 - Mùa Xuân",
          "Tập 2 - Mùa Hạ",
          "Tập 3 - Mùa Thu",
          "Tập 4 - Mùa Đông",
        ],
        images: [
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg",
          "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg",
        ],
      },
    ],
    id: 3,
  },
  {
    productId: 3,
    skuId: 9,
    skuName: "",
    productName:
      "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)",
    productTypeId: 15,
    unitPrice: 27900,
    recommendedRetailPrice: 31000,
    quantity: 8,
    availableQuantity: 20,
    totalQuantity: 20,
    imageUrl: "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg",
    description:
      "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)\r\n\r\nBản đồ là phương tiện giảng dạy và học tập điạ lý không thể thiếu trong nhà trường phổ thông. Cùng với sách giáo khoa, Atlat địa lí Việt Nam là nguồn cung cấp kiến thức, thông tin tổng hợp và hệ thống giúp giáo viên đổi mới phương pháp dạy học, hỗ trợ học.\r\n\r\nĐể đáp ứng nhu cầu bức thiết đó, NXB Giáo dục Việt Nam trân trọng giới thiệu tập Atlat địa lý Việt Nam gồm 21 bản đồ, được in màu rõ nét, liên quan đến các lĩnh vực kinh tế - xã hội. Các bản đồ được xây dựng theo nguồn số liệu của Nhà xuất bản thống kê - Tổng cục thống kê. Đây là tài liệu được phép mang vào phòng thi tốt nghiệp THPT môn Địa lý do Bộ Giáo dục và Đào tạo quy định.\r\n\r\nNội dung gồm có:\r\n\r\n1. Kí hiệu chung\r\n\r\n2. Hành chính 4. Hình thể\r\n\r\n4. Địa khoáng sản\r\n\r\n5. Khí hậu\r\n\r\n6. Các hệ thống sông\r\n\r\n7. Các nhóm và các loại đât chính\r\n\r\n8. Thực vật và động vật\r\n\r\n9. Các miền tự nhiên\r\n\r\n11. Dân số\r\n\r\n11. Dân tộc\r\n\r\n12. Kinh tế chung\r\n\r\n14. Nông nghiệp chung\r\n\r\n14. Lâm nông và thuỷ sản\r\n\r\n15. Công nghiệp chung\r\n\r\n16. Các ngành công nghiệp trọng điểm\r\n\r\n17. Giao thông\r\n\r\n18. Thương mại\r\n\r\n19. Du lịch\r\n\r\n21. Vùng trunh du và miền núi Bắc Bộ, vùn Đồng Bằng Sông Hồng\r\n\r\n21. Vùng Bắc Trung Bộ\r\n\r\n22. Vùng Duyên Hải Nam Trung Bộ, Vùng Tây Nguyên\r\n\r\n24. Vùng Đông Nam Bộ, Vùng Đồng Bằng Sông Cửu Long\r\n\r\n25. Các vùng kinh tế trọng điểm\r\n\r\nNgoài ra, NXB Giáo dục Việt Nam đã biên soạn cuốn “Hướng dẫn sử dụng Atlat Địa lý Việt Nam” dùng cho học sinh THCS và THPT, ôn tập thi tốt nghiệp THPT, thi ĐH, CĐ và ôn luyện thi học sinh giỏi quốc gia.\r\n\r\nNội dung sách gồm ba phần:\r\n\r\nPhần 1: Một số kiến thức về phương pháp sử dụng bản đồ giáo khoa;\r\n\r\nPhần 2: Giới thiệu về Atlat Địa lý Việt Nam.\r\n\r\nPhần 4: Hướng dẫn sử dụng Atlat Địa lý Việt Nam.",
    createdWhen: "2024-05-29T09:43:56.1077851+00:00",
    skuVariations: [
      {
        productId: 3,
        skuValue: "SKU00009",
        skuName: "",
        unitPrice: 27900,
        recommendedRetailPrice: 31000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: [],
        optionIndex: [],
        id: 9,
      },
    ],
    productOptions: [],
    id: 8,
  },
  {
    productId: 5,
    skuId: 11,
    skuName: "",
    productName:
      "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (Chuẩn) (Không Tin Học)",
    productTypeId: 16,
    unitPrice: 180000,
    recommendedRetailPrice: 180000,
    quantity: 2,
    availableQuantity: 20,
    totalQuantity: 20,
    imageUrl:
      "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg",
    description:
      "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT\tTên hàng\r\n1\tCông nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2\tGiáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3\tGiáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4\tHoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5\tKhoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6\tÂm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7\tToán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8\tToán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9\tLịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10\tNgữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11\tNgữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12\tMĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)\r\nMã hàng\t3300000027333\r\nCấp Độ/ Lớp\tLớp 7\r\nCấp Học\tCấp 2\r\nNhà Cung Cấp\tNhà xuất bản Giáo Dục\r\nTác giả\tBộ Giáo Dục Và Đào Tạo\r\nNXB\tGiáo Dục Việt Nam\r\nNăm XB\t2023\r\nNgôn Ngữ\tTiếng Việt\r\nTrọng lượng (gr)\t2500\r\nKích Thước Bao Bì\t24 x 17 x 6 cm\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT\tTên hàng\r\n1\tCông nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2\tGiáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3\tGiáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4\tHoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5\tKhoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6\tÂm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7\tToán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8\tToán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9\tLịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10\tNgữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11\tNgữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12\tMĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)",
    createdWhen: "2024-05-29T09:54:57.3744884+00:00",
    skuVariations: [
      {
        productId: 5,
        skuValue: "SKU00011",
        skuName: "",
        unitPrice: 180000,
        recommendedRetailPrice: 180000,
        availableQuantity: 20,
        totalQuantity: 20,
        status: "InStock",
        optionNames: [],
        optionIndex: [],
        id: 11,
      },
    ],
    productOptions: [],
    id: 9,
  },
]

const vouchers = [
  {
    id: 1,
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    id: 2,
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
]

const Cart = () => {
  // VOUCHER
  const [openVoucher, setOpenVoucher] = useState(false)
  const handleVoucher = () => {
    setOpenVoucher((prev) => !prev)
  }
  // VOUCHER RADIO
  const [selectedVoucher, setSelectedVoucher] = useState(null)
  const handleChangeVoucher = (event) => {
    setSelectedVoucher(event.target.value)
  }

  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col justify-between 
        gap-y-5 p-4 lg:px-8'
      >
        <div className='flex items-center gap-3 rounded bg-white p-5 shadow'>
          <div className='text-orange-500'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-14 '
            >
              <path
                fillRule='evenodd'
                d='M7.5 6v.75H5.513c-.96 0-1.764.724-1.865 1.679l-1.263 12A1.875 1.875 0 0 0 4.25 22.5h15.5a1.875 1.875 0 0 0 1.865-2.071l-1.263-12a1.875 1.875 0 0 0-1.865-1.679H16.5V6a4.5 4.5 0 1 0-9 0ZM12 3a3 3 0 0 0-3 3v.75h6V6a3 3 0 0 0-3-3Zm-3 8.25a3 3 0 1 0 6 0v-.75a.75.75 0 0 1 1.5 0v.75a4.5 4.5 0 1 1-9 0v-.75a.75.75 0 0 1 1.5 0v.75Z'
                clipRule='evenodd'
              />
            </svg>
          </div>

          <span className='text-2xl font-medium text-orange-500'>Giỏ hàng</span>
        </div>

        <div className='flex items-center rounded bg-white p-2 font-medium shadow'>
          <Checkbox
            sx={{
              color: green[500],
              "&.Mui-checked": {
                color: green[500],
              },
            }}
          />

          <span className='flex-1'>Sản phẩm</span>

          <span className='w-44 text-center'></span>
          <span className='w-44 text-center'>Đơn giá</span>
          <span className='w-44 text-center'>Số lượng</span>
          <span className='w-44 text-center'>Số tiền</span>
          <span className='w-44 text-center'>Thao tác</span>
        </div>

        <div className='flex max-h-[1000px] w-full flex-col gap-y-5 overflow-auto bg-transparent'>
          {items.map((item, index) => (
            <ProductItemCart key={index} item={item} />
          ))}
        </div>

        <div
          className={`flex items-center justify-between rounded bg-white px-3 py-3 shadow`}
        >
          <div className='flex items-center gap-5'>
            <Checkbox
              sx={{
                color: green[500],
                "&.Mui-checked": {
                  color: green[500],
                },
              }}
            />

            <div className='hover:cursor-pointer hover:text-ct-green-400'>
              Chọn tất cả (12)
            </div>

            <div className='hover:cursor-pointer hover:text-red-500'>Xóa</div>

            <div
              className='hover:cursor-pointer hover:text-blue-500'
              onClick={handleVoucher}
            >
              Chọn voucher
            </div>

            <Modal
              open={openVoucher}
              setOpen={handleVoucher}
              title={"VOUCHER"}
              contentComp={
                <div>
                  <VoucherRadio
                    options={vouchers}
                    onChange={handleChangeVoucher}
                    value={selectedVoucher}
                  />
                </div>
              }
              actionComp={<Button onClick={handleVoucher}>Xác nhận</Button>}
              isCancelButton
            ></Modal>
          </div>

          <div className='flex items-center gap-5'>
            <div>Tổng thanh toán (0 sản phẩm):</div>
            <div>
              {selectedVoucher && (
                <VND
                  className={`font-medium text-gray-400 line-through`}
                  number={350000}
                />
              )}
              <VND
                className={`text-xl font-medium text-ct-green-400`}
                number={200000}
              />
            </div>
            <Button className={`min-w-52`}>Mua hàng</Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Cart
