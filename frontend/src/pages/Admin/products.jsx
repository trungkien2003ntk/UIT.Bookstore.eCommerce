import { useState } from "react"
import DataTable from "../../components/Admin/DataTable"
import CellItem from "../../components/Admin/CellItem"
import VND from "../../components/vnd"
import Button from "../../components/Button"

const columns = [
  {
    field: "id",
    headerName: "Mã sản phẩm",
    width: 130,
    headerAlign: "center",
    align: "center",
  },
  {
    field: "thumbnailImageUrl",
    headerName: "Ảnh",
    width: 100,
    headerAlign: "center",
    align: "center",
    sortable: false,
    renderCell: (row) => {
      return (
        <div className='flex h-full w-full items-center justify-center'>
          <img className='h-20 w-20' alt='img' src={row.value} />
        </div>
      )
    },
  },
  {
    field: "name",
    headerName: "Tên sản phẩm",
    width: 370,
    sortable: false,
    renderCell: (row) => {
      return (
        <div className='flex h-full w-full items-center'>
          <div>{row.value}</div>
        </div>
      )
    },
  },
  {
    field: "isActived",
    headerName: "Trạng Thái",
    width: 200,
    sortable: false,
    renderCell: (row) => {
      return (
        <CellItem
          type={row.value}
          text={row.value ? "Đang giao dịch" : "Ngừng giao dịch"}
          iconCheck={row.value}
          iconX={!row.value}
        />
      )
    },
  },
  {
    field: "productTypeName",
    headerName: "Loại sản phẩm",
    width: 200,
    sortable: false,
    renderCell: (row) => {
      return <CellItem type={"ProductType"} text={row.value} />
    },
  },
  {
    field: "minUnitPrice",
    headerName: "Giá bán",
    width: 150,
    renderCell: (row) => {
      return (
        <VND className={`font-medium text-ct-green-400`} number={row.value} />
      )
    },
  },
]

const object = {
  items: [
    {
      name: "Gia Đình Thương Yêu",
      productTypeId: 13,
      productTypeName: "Sách",
      description:
        "Gia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.\r\n\r\nMã hàng\t8935212367677\r\nNgày Dự Kiến Phát Hành\t20/05/2024\r\nTên Nhà Cung Cấp\tĐinh Tị\r\nTác giả\tAlison Ritchie, Alison Edgson\r\nNgười Dịch\tThành Đạt\r\nNXB\tHà Nội\r\nNăm XB\t2024\r\nTrọng lượng (gr)\t100\r\nKích Thước Bao Bì\t25 x 20.5 x 0.2 cm\r\nSố trang\t28\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Truyện Đọc Thiếu Nhi bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nGia Đình Thương Yêu - Một Ngày Của Tớ Và Bà\r\n\r\nBộ sách Gia đình thương yêu, với lời thơ hết sức ngọt ngào, tình cảm và đáng yêu không chỉ giúp các bạn nhỏ mở rộng từ vựng, biết cách diễn đạt câu mà còn học được cách yêu thương, thể hiện tình cảm đối với ông bà, bố mẹ của mình.\r\n\r\nGia đình thương yêu gồm 4 tập:\r\n\r\n- Một ngày của tớ và bố\r\n\r\n- Một ngày của tớ và mẹ\r\n\r\n- Một ngày của tớ và ông\r\n\r\n- Một ngày của tớ và bà\r\n\r\nMỗi tập kể về câu chuyện của bạn Gấu Con với những trải nghiệm đáng nhớ cũng gia đình của mình. Bên cạnh vần thơ đáng yêu, bộ sách cũng gây ấn tượng bởi hình ảnh minh hoạ đẹp, kích thích thị giác và trí tưởng tượng cho trẻ.",
      isBook: true,
      thumbnailImageUrl:
        "https://cdn0.fahasa.com/media/flashmagazine/images/page_images/gia_dinh_thuong_yeu___mot_ngay_cua_to_va_bo/2024_05_20_17_11_53_1-390x510.jpg?_gl=1*10nhn5q*_ga*NjQ1ODI5NTU4LjE3MTU4NTM2MTM.*_ga_460L9JMC2G*MTcxNjUzNTA4NC40LjAuMTcxNjUzNTA4NC42MC4wLjIwNDc4NjYxODI.*_gcl_au*MjAzMDgwNzcxOS4xNzE1ODUzNjEz",
      minUnitPrice: 31500,
      minRecommendedRetailPrice: 35000,
      minDiscountRate: 10,
      averageRating: 0,
      isActived: true,
      id: 1,
    },
    {
      name: "Tô Màu Bốn Mùa",
      productTypeId: 14,
      productTypeName: "Sách",
      description:
        "Tô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.\r\n\r\nMã hàng\t8936071294357\r\nTên Nhà Cung Cấp\tCty Phan Thị\r\nTác giả\tPhan Thị\r\nNXB\tVăn Học\r\nNăm XB\t2024\r\nTrọng lượng (gr)\t150\r\nKích Thước Bao Bì\t24 x 16 x 0.5 cm\r\nSố trang\t30\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Tô màu, luyện chữ bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nTô Màu Bốn Mùa - Xuân - Tập 1\r\n\r\nSách Tô Màu Bốn Mùa giúp bé thỏa sức sáng tạo, tô màu để tạo nên những sắc màu bốn mùa Xuân - Hạ - Thu - Đông sinh động, vui tươi theo sở thích và hiểu biết của riêng mình.\r\n\r\nCác tranh trong sách được tô sẵn một số chi tiết để khơi gợi cảm hứng, tư duy mỹ thuật, phát huy trí tưởng tượng của các em. Không chỉ là sách tô màu, các hình ảnh sinh động trong sách còn giúp các em nhận biết, quan sát các hiện tượng, sự việc, sự kiện xảy ra trong từng mùa như:\r\n\r\n- Mùa xuân hoa lá đâm chồi nảy lộc\r\n\r\n- Mùa hè toả nắng yêu thương\r\n\r\n- Mùa thu lá vàng ánh lên niềm hạnh phúc\r\n\r\n- Mùa đông sắc màu giáng sinh\r\n\r\nĐồng thời tô màu là hoạt động tăng khả năng biểu đạt cảm xúc của trẻ thông qua các màu sắc từ những tập sách tô màu. Vì vậy, tô màu không chỉ là một trò chơi mà còn là môn học cần thiết dành cho bé.",
      isBook: true,
      thumbnailImageUrl:
        "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg",
      minUnitPrice: 18000,
      minRecommendedRetailPrice: 20000,
      minDiscountRate: 10,
      averageRating: 0,
      isActived: true,
      id: 2,
    },
    {
      name: "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)",
      productTypeId: 15,
      productTypeName: "Sách",
      description:
        "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)\r\n\r\nBản đồ là phương tiện giảng dạy và học tập điạ lý không thể thiếu trong nhà trường phổ thông. Cùng với sách giáo khoa, Atlat địa lí Việt Nam là nguồn cung cấp kiến thức, thông tin tổng hợp và hệ thống giúp giáo viên đổi mới phương pháp dạy học, hỗ trợ học.\r\n\r\nĐể đáp ứng nhu cầu bức thiết đó, NXB Giáo dục Việt Nam trân trọng giới thiệu tập Atlat địa lý Việt Nam gồm 21 bản đồ, được in màu rõ nét, liên quan đến các lĩnh vực kinh tế - xã hội. Các bản đồ được xây dựng theo nguồn số liệu của Nhà xuất bản thống kê - Tổng cục thống kê. Đây là tài liệu được phép mang vào phòng thi tốt nghiệp THPT môn Địa lý do Bộ Giáo dục và Đào tạo quy định.\r\n\r\nNội dung gồm có:\r\n\r\n1. Kí hiệu chung\r\n\r\n2. Hành chính 4. Hình thể\r\n\r\n4. Địa khoáng sản\r\n\r\n5. Khí hậu\r\n\r\n6. Các hệ thống sông\r\n\r\n7. Các nhóm và các loại đât chính\r\n\r\n8. Thực vật và động vật\r\n\r\n9. Các miền tự nhiên\r\n\r\n11. Dân số\r\n\r\n11. Dân tộc\r\n\r\n12. Kinh tế chung\r\n\r\n14. Nông nghiệp chung\r\n\r\n14. Lâm nông và thuỷ sản\r\n\r\n15. Công nghiệp chung\r\n\r\n16. Các ngành công nghiệp trọng điểm\r\n\r\n17. Giao thông\r\n\r\n18. Thương mại\r\n\r\n19. Du lịch\r\n\r\n21. Vùng trunh du và miền núi Bắc Bộ, vùn Đồng Bằng Sông Hồng\r\n\r\n21. Vùng Bắc Trung Bộ\r\n\r\n22. Vùng Duyên Hải Nam Trung Bộ, Vùng Tây Nguyên\r\n\r\n24. Vùng Đông Nam Bộ, Vùng Đồng Bằng Sông Cửu Long\r\n\r\n25. Các vùng kinh tế trọng điểm\r\n\r\nNgoài ra, NXB Giáo dục Việt Nam đã biên soạn cuốn “Hướng dẫn sử dụng Atlat Địa lý Việt Nam” dùng cho học sinh THCS và THPT, ôn tập thi tốt nghiệp THPT, thi ĐH, CĐ và ôn luyện thi học sinh giỏi quốc gia.\r\n\r\nNội dung sách gồm ba phần:\r\n\r\nPhần 1: Một số kiến thức về phương pháp sử dụng bản đồ giáo khoa;\r\n\r\nPhần 2: Giới thiệu về Atlat Địa lý Việt Nam.\r\n\r\nPhần 4: Hướng dẫn sử dụng Atlat Địa lý Việt Nam.",
      isBook: true,
      thumbnailImageUrl:
        "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg",
      minUnitPrice: 27900,
      minRecommendedRetailPrice: 31000,
      minDiscountRate: 10,
      averageRating: 0,
      isActived: false,
      id: 3,
    },
    {
      name: "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (Chuẩn)",
      productTypeId: 16,
      productTypeName: "Sách",
      description:
        "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (2023)\r\n\r\nSTT\tTên hàng\r\n1\tBài tập Mĩ thuật 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n2\tBài tập Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n3\tBài tập Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n4\tBài tập Lịch sử và Địa lí 7 (Phần Địa lí) (Chân Trời Sáng Tạo) (2023)\r\n5\tBài tập Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n6\tBài tập Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n7\tBài tập Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n8\tBài tập Lịch sử và Địa lí 7 (Phần Lịch sử) (Chân Trời Sáng Tạo) (2023)\r\n9\tBài tập Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n10\tBài tập Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n11\tBài tập Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n12\tBài tập Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\nMã hàng\t3300000027357\r\nCấp Độ/ Lớp\tLớp 7\r\nCấp Học\tCấp 2\r\nNhà Cung Cấp\tNhà xuất bản Giáo Dục\r\nTác giả\tBộ Giáo Dục Và Đào Tạo\r\nNXB\tGiáo Dục Việt Nam\r\nNăm XB\t2023\r\nNgôn Ngữ\tTiếng Việt\r\nTrọng lượng (gr)\t1200\r\nKích Thước Bao Bì\t24 x 17 x 3 cm\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (2023)\r\n\r\nSTT\tTên hàng\r\n1\tBài tập Mĩ thuật 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n2\tBài tập Hoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n3\tBài tập Công nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n4\tBài tập Lịch sử và Địa lí 7 (Phần Địa lí) (Chân Trời Sáng Tạo) (2023)\r\n5\tBài tập Giáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n6\tBài tập Khoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n7\tBài tập Âm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n8\tBài tập Lịch sử và Địa lí 7 (Phần Lịch sử) (Chân Trời Sáng Tạo) (2023)\r\n9\tBài tập Toán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n10\tBài tập Toán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n11\tBài tập Ngữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n12\tBài tập Ngữ văn 7/2 (Chân Trời Sáng Tạo) (2023)",
      isBook: true,
      thumbnailImageUrl:
        "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944359096_c109dffd7f8004e1b78aa31f65526f08_1.jpg",
      minUnitPrice: 170000,
      minRecommendedRetailPrice: 170000,
      minDiscountRate: 0,
      averageRating: 0,
      isActived: false,
      id: 4,
    },
    {
      name: "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (Chuẩn) (Không Tin Học)",
      productTypeId: 16,
      productTypeName: "Sách",
      description:
        "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT\tTên hàng\r\n1\tCông nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2\tGiáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3\tGiáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4\tHoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5\tKhoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6\tÂm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7\tToán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8\tToán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9\tLịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10\tNgữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11\tNgữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12\tMĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)\r\nMã hàng\t3300000027333\r\nCấp Độ/ Lớp\tLớp 7\r\nCấp Học\tCấp 2\r\nNhà Cung Cấp\tNhà xuất bản Giáo Dục\r\nTác giả\tBộ Giáo Dục Và Đào Tạo\r\nNXB\tGiáo Dục Việt Nam\r\nNăm XB\t2023\r\nNgôn Ngữ\tTiếng Việt\r\nTrọng lượng (gr)\t2500\r\nKích Thước Bao Bì\t24 x 17 x 6 cm\r\nHình thức\tBìa Mềm\r\nSản phẩm bán chạy nhất\tTop 100 sản phẩm Giáo Khoa Lớp 7 bán chạy của tháng\r\nGiá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành. Bên cạnh đó, tuỳ vào loại sản phẩm, hình thức và địa chỉ giao hàng mà có thể phát sinh thêm chi phí khác như Phụ phí đóng gói, phí vận chuyển, phụ phí hàng cồng kềnh,...\r\nChính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc\r\nSách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (2023) (Không Tin Học)\r\n\r\nBao gồm:\r\n\r\nSTT\tTên hàng\r\n1\tCông nghệ 7 (Chân Trời Sáng Tạo) (2023)\r\n2\tGiáo dục thể chất 7 (Chân Trời Sáng Tạo) (2023)\r\n3\tGiáo dục công dân 7 (Chân Trời Sáng Tạo) (2023)\r\n4\tHoạt động trải nghiệm, hướng nghiệp 7 (Bản 1) (Chân Trời Sáng Tạo) (2023)\r\n5\tKhoa học tự nhiên 7 (Chân Trời Sáng Tạo) (2023)\r\n6\tÂm nhạc 7 (Chân Trời Sáng Tạo) (2023)\r\n7\tToán 7/1 (Chân Trời Sáng Tạo) (2023)\r\n8\tToán 7/2 (Chân Trời Sáng Tạo) (2023)\r\n9\tLịch sử và Địa lí 7 (Chân Trời Sáng Tạo) (2023)\r\n10\tNgữ văn 7/1 (Chân Trời Sáng Tạo) (2023)\r\n11\tNgữ văn 7/2 (Chân Trời Sáng Tạo) (2023)\r\n12\tMĩ thuật 7 (bản 1) (Chân Trời Sáng Tạo) (2023)",
      isBook: true,
      thumbnailImageUrl:
        "https://cdn0.fahasa.com/media/catalog/product/z/3/z3543944350145_ec66f22a86880daac11b61bc47e36387_1_1.jpg",
      minUnitPrice: 180000,
      minRecommendedRetailPrice: 180000,
      minDiscountRate: 0,
      averageRating: 0,
      isActived: false,

      id: 5,
    },
  ],
  totalCount: 5,
  pageSize: 12,
  pageNumber: 1,
  totalPages: 1,
}

const rows = object.items

const Products = () => {
  const [itemsDelete, setItemsDelete] = useState()

  return (
    <div className='flex flex-col gap-3'>
      <div className='flex items-center justify-between rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Quản lý sản phẩm
        </div>

        <div className='flex gap-5'>
          {itemsDelete && (
            <Button
              className={`border-[1px] bg-white !text-red-500 hover:border-red-500
            hover:!bg-red-50`}
              leftIcon={
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='size-6'
                >
                  <path
                    fillRule='evenodd'
                    d='M16.5 4.478v.227a48.816 48.816 0 0 1 3.878.512.75.75 0 1 1-.256 1.478l-.209-.035-1.005 13.07a3 3 0 0 1-2.991 2.77H8.084a3 3 0 0 1-2.991-2.77L4.087 6.66l-.209.035a.75.75 0 0 1-.256-1.478A48.567 48.567 0 0 1 7.5 4.705v-.227c0-1.564 1.213-2.9 2.816-2.951a52.662 52.662 0 0 1 3.369 0c1.603.051 2.815 1.387 2.815 2.951Zm-6.136-1.452a51.196 51.196 0 0 1 3.273 0C14.39 3.05 15 3.684 15 4.478v.113a49.488 49.488 0 0 0-6 0v-.113c0-.794.609-1.428 1.364-1.452Zm-.355 5.945a.75.75 0 1 0-1.5.058l.347 9a.75.75 0 1 0 1.499-.058l-.346-9Zm5.48.058a.75.75 0 1 0-1.498-.058l-.347 9a.75.75 0 0 0 1.5.058l.345-9Z'
                    clipRule='evenodd'
                  />
                </svg>
              }
            >
              Xóa sản phẩm
            </Button>
          )}

          <Button
            leftIcon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6'
              >
                <path
                  fillRule='evenodd'
                  d='M12 3.75a.75.75 0 0 1 .75.75v6.75h6.75a.75.75 0 0 1 0 1.5h-6.75v6.75a.75.75 0 0 1-1.5 0v-6.75H4.5a.75.75 0 0 1 0-1.5h6.75V4.5a.75.75 0 0 1 .75-.75Z'
                  clipRule='evenodd'
                />
              </svg>
            }
          >
            Thêm sản phẩm
          </Button>
        </div>
      </div>

      <DataTable
        height={600}
        columns={columns}
        rows={rows}
        getRowHeight={() => 100}
        checkboxSelection
        onRowClick={(value) => {
          console.log({ ...value.row })
        }}
      />
    </div>
  )
}

export default Products
