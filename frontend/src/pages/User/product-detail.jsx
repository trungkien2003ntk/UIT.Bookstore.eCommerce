import { useState } from "react"
import { useParams } from "react-router-dom"
import { Divider, Pagination } from "@mui/material"

import FiveStar from "../../components/User/FiveStar"
import Slider from "../../components/Slider"
import VND from "../../components/vnd"
import Options from "../../components/Options"
import ButtonNumber from "../../components/ButtonNumber"
import Button from "../../components/Button"
import BlockWrapper from "../../components/BlockWrapper"
import DiscountSwiper from "../../components/DiscountSwiper"
import MySwiper from "../../components/MySwiper"
import { skusToOptions } from "../../components/funcProductDetail"

// const options = [
//   {
//     title: "Màu sắc",
//     items: [
//       {
//         name: "xanh hải quân",
//         image:
//           "https://down-vn.img.susercontent.com/file/sg-11134201-7qvdg-lhfvlcfyl3f794",
//       },
//       {
//         name: "quả mơ",
//         image:
//           "https://down-vn.img.susercontent.com/file/sg-11134201-7qvdt-lggahbq2v4lce9",
//       },
//       {
//         name: "xanh xám",
//         image:
//           "https://down-vn.img.susercontent.com/file/sg-11134201-7qvei-lhfvlf2apmr10c",
//       },
//     ],
//   },
//   {
//     title: "Kích thước",
//     items: [
//       {
//         name: "S",
//       },
//       {
//         name: "M",
//       },
//       {
//         name: "L",
//       },
//       {
//         name: "XL",
//       },
//     ],
//   },
// ]

const discount = [
  {
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
]

const products = [
  {
    name: "Tập Học Sinh Chống Lóa Fluffy Panda - Miền Bắc - 4 Ô Ly - 48 Trang 100gsm - The Sun 03",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 1200,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/c/h/chemta-bino_bia1.jpg",
  },
  {
    name: "Tập Học Sinh Chống Lóa Fluffy Panda - Miền Bắc - 4 Ô Ly - 48 Trang 100gsm - The Sun 03",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 82,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935318307294.jpg",
  },
  {
    name: "Nghệ Thuật Kể Chuyện Bằng Hình Ảnh",
    price: 179000,
    discount_rate: 20,
    actual_price: 143200,
    sale_quantity: 3000,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/n/g/ngh_-thu_t-k_-chuy_n-b_ng-h_nh-_nh-b_a-1.jpg",
  },
  {
    name: "Mindmap English Grammar - Ngữ Pháp Tiếng Anh Bằng Sơ Đồ Tư Duy",
    price: 190000,
    discount_rate: 35,
    actual_price: 123500,
    sale_quantity: 100,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/m/i/mindmap_english_grammar___ngu_phap_tieng_anh_bang_so_do_tu_duy_1_2018_11_21_09_32_32.jpg?_gl=1*avkbv*_ga*MTExMDQwNzk0OS4xNzEyNjgxNDE1*_ga_460L9JMC2G*MTcxNTg0MDA0MS4yMC4xLjE3MTU4NDAyODcuNTEuMC4yMDYwMjM2MDU1*_gcl_au*NzEzNDUwNjcwLjE3MTI2ODE0MTU.",
  },
  {
    name: "Atomic Habits: An Easy & Proven Way To Build Good Habits & Break Bad Ones",
    price: 336000,
    discount_rate: 10,
    actual_price: 302000,
    sale_quantity: 6000,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/9/7/9780593189641.jpg",
  },
  {
    name: "Thrive : The Third Metric to Redefining Success and Creating a Happier Life",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 82,
    rating: 4,
    image: "https://cdn0.fahasa.com/media/catalog/product/i/m/image_55020.jpg",
  },
  {
    name: "Tập Học Sinh Chống Lóa Fluffy Panda - Miền Bắc - 4 Ô Ly - 48 Trang 100gsm - The Sun 03",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 1200,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/c/h/chemta-bino_bia1.jpg",
  },
  {
    name: "Tập Học Sinh Chống Lóa Fluffy Panda - Miền Bắc - 4 Ô Ly - 48 Trang 100gsm - The Sun 03",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 82,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935318307294.jpg",
  },
  {
    name: "Nghệ Thuật Kể Chuyện Bằng Hình Ảnh",
    price: 179000,
    discount_rate: 20,
    actual_price: 143200,
    sale_quantity: 3000,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/n/g/ngh_-thu_t-k_-chuy_n-b_ng-h_nh-_nh-b_a-1.jpg",
  },
  {
    name: "Mindmap English Grammar - Ngữ Pháp Tiếng Anh Bằng Sơ Đồ Tư Duy",
    price: 190000,
    discount_rate: 35,
    actual_price: 123500,
    sale_quantity: 100,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/m/i/mindmap_english_grammar___ngu_phap_tieng_anh_bang_so_do_tu_duy_1_2018_11_21_09_32_32.jpg?_gl=1*avkbv*_ga*MTExMDQwNzk0OS4xNzEyNjgxNDE1*_ga_460L9JMC2G*MTcxNTg0MDA0MS4yMC4xLjE3MTU4NDAyODcuNTEuMC4yMDYwMjM2MDU1*_gcl_au*NzEzNDUwNjcwLjE3MTI2ODE0MTU.",
  },
  {
    name: "Atomic Habits: An Easy & Proven Way To Build Good Habits & Break Bad Ones",
    price: 336000,
    discount_rate: 10,
    actual_price: 302000,
    sale_quantity: 6000,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/9/7/9780593189641.jpg",
  },
  {
    name: "Thrive : The Third Metric to Redefining Success and Creating a Happier Life",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 82,
    rating: 4,
    image: "https://cdn0.fahasa.com/media/catalog/product/i/m/image_55020.jpg",
  },
]

const obj = {
  name: "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)",
  minUnitPrice: 27900,
  maxUnitPrice: 27900,
  minRecommendedRetailPrice: 31000,
  maxRecommendedRetailPrice: 31000,
  unitMeasureName: "quyển",
  description:
    "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)\r\n\r\nBản đồ là phương tiện giảng dạy và học tập điạ lý không thể thiếu trong nhà trường phổ thông. Cùng với sách giáo khoa, Atlat địa lí Việt Nam là nguồn cung cấp kiến thức, thông tin tổng hợp và hệ thống giúp giáo viên đổi mới phương pháp dạy học, hỗ trợ học.\r\n\r\nĐể đáp ứng nhu cầu bức thiết đó, NXB Giáo dục Việt Nam trân trọng giới thiệu tập Atlat địa lý Việt Nam gồm 21 bản đồ, được in màu rõ nét, liên quan đến các lĩnh vực kinh tế - xã hội. Các bản đồ được xây dựng theo nguồn số liệu của Nhà xuất bản thống kê - Tổng cục thống kê. Đây là tài liệu được phép mang vào phòng thi tốt nghiệp THPT môn Địa lý do Bộ Giáo dục và Đào tạo quy định.\r\n\r\nNội dung gồm có:\r\n\r\n1. Kí hiệu chung\r\n\r\n2. Hành chính 4. Hình thể\r\n\r\n4. Địa khoáng sản\r\n\r\n5. Khí hậu\r\n\r\n6. Các hệ thống sông\r\n\r\n7. Các nhóm và các loại đât chính\r\n\r\n8. Thực vật và động vật\r\n\r\n9. Các miền tự nhiên\r\n\r\n11. Dân số\r\n\r\n11. Dân tộc\r\n\r\n12. Kinh tế chung\r\n\r\n14. Nông nghiệp chung\r\n\r\n14. Lâm nông và thuỷ sản\r\n\r\n15. Công nghiệp chung\r\n\r\n16. Các ngành công nghiệp trọng điểm\r\n\r\n17. Giao thông\r\n\r\n18. Thương mại\r\n\r\n19. Du lịch\r\n\r\n21. Vùng trunh du và miền núi Bắc Bộ, vùn Đồng Bằng Sông Hồng\r\n\r\n21. Vùng Bắc Trung Bộ\r\n\r\n22. Vùng Duyên Hải Nam Trung Bộ, Vùng Tây Nguyên\r\n\r\n24. Vùng Đông Nam Bộ, Vùng Đồng Bằng Sông Cửu Long\r\n\r\n25. Các vùng kinh tế trọng điểm\r\n\r\nNgoài ra, NXB Giáo dục Việt Nam đã biên soạn cuốn “Hướng dẫn sử dụng Atlat Địa lý Việt Nam” dùng cho học sinh THCS và THPT, ôn tập thi tốt nghiệp THPT, thi ĐH, CĐ và ôn luyện thi học sinh giỏi quốc gia.\r\n\r\nNội dung sách gồm ba phần:\r\n\r\nPhần 1: Một số kiến thức về phương pháp sử dụng bản đồ giáo khoa;\r\n\r\nPhần 2: Giới thiệu về Atlat Địa lý Việt Nam.\r\n\r\nPhần 4: Hướng dẫn sử dụng Atlat Địa lý Việt Nam.",
  productTypeName: "Sách Tham Khảo",
  isBook: true,
  thumbnailImageUrls: [
    "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg",
  ],
  largeImageUrls: [
    "https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg",
  ],
  productTypeAttributes: [
    {
      productTypeId: 15,
      attributeId: 1,
      attributeValueId: 1,
      name: "Hình thức bìa",
      value: "Bìa mềm",
    },
    {
      productTypeId: 15,
      attributeId: 2,
      attributeValueId: 8,
      name: "NXB",
      value: "Đại Học Quốc Gia Hà Nội",
    },
    {
      productTypeId: 15,
      attributeId: 4,
      attributeValueId: 4,
      name: "Ngôn ngữ",
      value: "Tiếng Việt",
    },
    {
      productTypeId: 15,
      attributeId: 5,
      attributeValueId: 10,
      name: "Số trang",
      value: "178",
    },
  ],
  authors: [
    {
      id: 5,
      name: "Nhiều tác giả",
      description: "Nhiều tác giả",
    },
  ],
  skus: [
    {
      skuValue: "SKU00009",
      unitPrice: 27900,
      recommendedRetailPrice: 31000,
      barcode: "2875627948270",
      quantity: 20,
      status: "InStock",
      weight: 190,
      width: 16,
      height: 24,
      length: 0.5,
      thumbnailImageUrl: null,
      largeImageUrl: null,
      optionValues: [],
      id: 9,
    },
  ],
  id: 3,
}

const ratings = {
  averageRating: 4.0,
  totalApprovedRating: 5,
  totalRating: 5,
  total5StarRating: 3,
  total4StarRating: 1,
  total3StarRating: 0,
  total2StarRating: 0,
  total1StarRating: 1,
  totalRatingWithComment: 5,
  totalRatingWithImage: 0,
  ratings: {
    items: [
      {
        comment: "Good Product!",
        ratingValue: 5,
        userName: "Nguyễn Trung Kiên",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và bố",
        likesCount: 1,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 1,
      },
      {
        comment: "Amazing thing, but need some upgrades",
        ratingValue: 4,
        userName: "Trần Tiêm Kích",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và bố",
        likesCount: 1,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 2,
      },
      {
        comment: "Good Product!",
        ratingValue: 5,
        userName: "Nguyễn Trung Kiên",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và mẹ",
        likesCount: 0,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 3,
      },
      {
        comment: "Hơi tệ một chút",
        ratingValue: 1,
        userName: "Trần Tiêm Kích",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và ông",
        likesCount: 0,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 4,
      },
      {
        comment: "Good Product!",
        ratingValue: 5,
        userName: "Nguyễn Trung Kiên",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và bà",
        likesCount: 0,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 5,
      },
    ],
    totalCount: 5,
    pageSize: 12,
    pageNumber: 1,
    totalPages: 1,
  },
}

const ProductDetail = () => {
  const { id } = useParams()

  // PRIMARY IMAGE
  const [image, setImage] = useState(obj.largeImageUrls[0])

  const [selectedOptions, setSelectedOptions] = useState([])

  const [money, setMoney] = useState({
    price: obj.minUnitPrice,
    discountRate: 10,
    actualPrice: obj.minRecommendedRetailPrice,
  })

  const handleSelectedOptions = (newOption) => {
    setSelectedOptions((prev) => {
      if (prev.some((option) => option.title === newOption.title)) {
        return prev.map((option) =>
          option.title === newOption.title ? newOption : option
        )
      }

      return [...prev, newOption]
    })
  }

  const [myOptions, setMyOptions] = useState(skusToOptions(obj.skus))

  // console.log(myOptions)

  // DESCRIPTION
  const [collapsedDesc, setCollapsedDesc] = useState(true)

  // COMMENTS PAGINATION
  const handleChange = (e, page) => {
    console.log(page)
  }

  const [quantity, setQuantity] = useState(1)

  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
        gap-5 p-4 lg:px-8'
      >
        <div className='flex w-full justify-center gap-10 rounded bg-white p-5 shadow'>
          <div className='w-[40%] '>
            <img alt='img' className='w-full' src={image} />
            <Slider
              className='mt-2 object-cover'
              setHoverImage={(img) => setImage(img)}
              slides={obj.largeImageUrls}
            />
          </div>

          <div className='flex h-full w-[60%] flex-col'>
            <div className='text-2xl font-semibold'>{obj.name}</div>

            <div className='mt-3 flex items-center gap-4'>
              <div className='flex items-center gap-1'>
                <div className='text-sm font-medium text-yellow-500'>
                  {ratings.averageRating.toFixed(1)}
                </div>
                <FiveStar
                  rating={ratings.averageRating}
                  classNameForSize='h-4 w-4'
                />
              </div>

              <div className='h-5 w-[1px] bg-gray-300'></div>

              <div className='flex items-center gap-1'>
                <div className='text-sm font-medium '>
                  {ratings.totalApprovedRating} Đánh giá
                </div>
              </div>

              <div className='h-5 w-[1px] bg-gray-300'></div>

              <div className='flex items-center gap-1'>
                <div className='text-sm font-medium '>823 Đã bán</div>
              </div>
            </div>

            <div className='mt-14 flex items-center gap-5'>
              <VND
                className={"text-3xl font-bold text-ct-green-400"}
                number={money.actualPrice}
              />
              <div className='rounded bg-red-100 px-2 py-1 font-medium text-red-600'>
                {money.discountRate}%
              </div>
            </div>

            <VND
              className={"mt-2 text-xl font-medium text-gray-500 line-through"}
              number={money.price}
            ></VND>

            <div className='mt-14 flex w-full items-center justify-center'>
              <div className='w-[30%] text-gray-500'>Chính sách đổi trả</div>
              <div className='flex w-[70%] gap-2'>
                <div>Đổi trả sản phẩm trong 7 ngày</div>
                <div
                  className='font-medium text-ct-green-400 hover:cursor-pointer 
                hover:text-ct-green-300'
                >
                  Xem thêm
                </div>
              </div>
            </div>

            {myOptions.map((option, index) => (
              <Options
                key={index}
                className={"mt-10"}
                option={option}
                onOptionSelect={handleSelectedOptions}
              />
            ))}

            <div className='mt-10 flex w-full items-center justify-center'>
              <div className='w-[30%] text-gray-500'>Số lượng</div>
              <div className='w-[70%]'>
                <ButtonNumber
                  quantity={quantity}
                  setQuantity={setQuantity}
                  min={1}
                />
              </div>
            </div>

            <div className='mt-10 flex items-center gap-5'>
              <Button
                className={`w-72 border-[1px] border-ct-green-400 bg-green-50 !py-4 
                !text-ct-green-400 hover:border-ct-green-500 hover:!bg-green-100`}
                leftIcon={
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={1.5}
                    stroke='currentColor'
                    className='size-6'
                  >
                    <path
                      strokeLinecap='round'
                      strokeLinejoin='round'
                      d='M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z'
                    />
                  </svg>
                }
              >
                Thêm vào giỏ hàng
              </Button>

              <Button className={`w-72 !py-4`}>Mua ngay</Button>
            </div>
          </div>
        </div>

        <BlockWrapper
          title={"Ưu đãi liên quan"}
          icon={
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-5 text-red-500'
            >
              <path d='M9.375 3a1.875 1.875 0 0 0 0 3.75h1.875v4.5H3.375A1.875 1.875 0 0 1 1.5 9.375v-.75c0-1.036.84-1.875 1.875-1.875h3.193A3.375 3.375 0 0 1 12 2.753a3.375 3.375 0 0 1 5.432 3.997h3.943c1.035 0 1.875.84 1.875 1.875v.75c0 1.036-.84 1.875-1.875 1.875H12.75v-4.5h1.875a1.875 1.875 0 1 0-1.875-1.875V6.75h-1.5V4.875C11.25 3.839 10.41 3 9.375 3ZM11.25 12.75H3v6.75a2.25 2.25 0 0 0 2.25 2.25h6v-9ZM12.75 12.75v9h6.75a2.25 2.25 0 0 0 2.25-2.25v-6.75h-9Z' />
            </svg>
          }
        >
          <div className='flex w-full items-center gap-5 overflow-auto'>
            <DiscountSwiper slides={discount} />
          </div>
        </BlockWrapper>

        <BlockWrapper
          title={"Có thể bạn cũng thích"}
          icon={
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6 text-blue-500'
            >
              <path d='M7.493 18.5c-.425 0-.82-.236-.975-.632A7.48 7.48 0 0 1 6 15.125c0-1.75.599-3.358 1.602-4.634.151-.192.373-.309.6-.397.473-.183.89-.514 1.212-.924a9.042 9.042 0 0 1 2.861-2.4c.723-.384 1.35-.956 1.653-1.715a4.498 4.498 0 0 0 .322-1.672V2.75A.75.75 0 0 1 15 2a2.25 2.25 0 0 1 2.25 2.25c0 1.152-.26 2.243-.723 3.218-.266.558.107 1.282.725 1.282h3.126c1.026 0 1.945.694 2.054 1.715.045.422.068.85.068 1.285a11.95 11.95 0 0 1-2.649 7.521c-.388.482-.987.729-1.605.729H14.23c-.483 0-.964-.078-1.423-.23l-3.114-1.04a4.501 4.501 0 0 0-1.423-.23h-.777ZM2.331 10.727a11.969 11.969 0 0 0-.831 4.398 12 12 0 0 0 .52 3.507C2.28 19.482 3.105 20 3.994 20H4.9c.445 0 .72-.498.523-.898a8.963 8.963 0 0 1-.924-3.977c0-1.708.476-3.305 1.302-4.666.245-.403-.028-.959-.5-.959H4.25c-.832 0-1.612.453-1.918 1.227Z' />
            </svg>
          }
        >
          <MySwiper slides={products}></MySwiper>
        </BlockWrapper>

        <BlockWrapper
          title={"Thông tin sản phẩm"}
          icon={
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6 text-ct-green-400'
            >
              <path
                fillRule='evenodd'
                d='M2.25 12c0-5.385 4.365-9.75 9.75-9.75s9.75 4.365 9.75 9.75-4.365 9.75-9.75 9.75S2.25 17.385 2.25 12Zm8.706-1.442c1.146-.573 2.437.463 2.126 1.706l-.709 2.836.042-.02a.75.75 0 0 1 .67 1.34l-.04.022c-1.147.573-2.438-.463-2.127-1.706l.71-2.836-.042.02a.75.75 0 1 1-.671-1.34l.041-.022ZM12 9a.75.75 0 1 0 0-1.5.75.75 0 0 0 0 1.5Z'
                clipRule='evenodd'
              />
            </svg>
          }
        >
          <div className='w-full'>
            <div className='info flex flex-col gap-3'>
              <div className='flex w-full justify-center'>
                <div className='w-[25%]'>Tên sản phẩm</div>
                <div className='w-[75%]'>{obj.name}</div>
              </div>

              <div className='flex w-full justify-center'>
                <div className='w-[25%]'>Loại sản phẩm</div>
                <div className='w-[75%]'>{obj.productTypeName}</div>
              </div>

              {obj.isBook && (
                <div className='flex w-full justify-center'>
                  <div className='w-[25%]'>Tác giả</div>
                  <div className='flex w-[75%] flex-col'>
                    {obj.authors.map((item, index) => (
                      <div key={index}>{item.name}</div>
                    ))}
                  </div>
                </div>
              )}

              {obj.productTypeAttributes.map((item, index) => (
                <div key={index} className='flex w-full justify-center'>
                  <div className='w-[25%]'>{item.name}</div>
                  <div className='w-[75%]'>{item.value}</div>
                </div>
              ))}
            </div>
          </div>
        </BlockWrapper>

        <BlockWrapper
          title={"Mô tả sản phẩm"}
          icon={
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6'
            >
              <path
                fillRule='evenodd'
                d='M3 9a.75.75 0 0 1 .75-.75h16.5a.75.75 0 0 1 0 1.5H3.75A.75.75 0 0 1 3 9Zm0 6.75a.75.75 0 0 1 .75-.75h16.5a.75.75 0 0 1 0 1.5H3.75a.75.75 0 0 1-.75-.75Z'
                clipRule='evenodd'
              />
            </svg>
          }
        >
          <div className='w-full'>
            <div
              className={`${collapsedDesc ? "max-h-[200px]" : "max-h-fit"}  
              overflow-hidden transition-all ease-in-out`}
            >
              {obj.description.split("\r\n").map((item, index) => (
                <div key={index}>{item}</div>
              ))}
            </div>

            <Button
              className='mt-5'
              onClick={() => setCollapsedDesc(!collapsedDesc)}
            >
              {collapsedDesc ? "Xem thêm" : "Thu gọn"}
            </Button>
          </div>
        </BlockWrapper>

        <BlockWrapper
          title={"Đánh giá sản phẩm"}
          icon={
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6 text-yellow-500'
            >
              <path
                fillRule='evenodd'
                d='M10.788 3.21c.448-1.077 1.976-1.077 2.424 0l2.082 5.006 5.404.434c1.164.093 1.636 1.545.749 2.305l-4.117 3.527 1.257 5.273c.271 1.136-.964 2.033-1.96 1.425L12 18.354 7.373 21.18c-.996.608-2.231-.29-1.96-1.425l1.257-5.273-4.117-3.527c-.887-.76-.415-2.212.749-2.305l5.404-.434 2.082-5.005Z'
                clipRule='evenodd'
              />
            </svg>
          }
        >
          <div className='w-full'>
            <div
              className='flex items-center gap-5
              rounded border-[1px] border-yellow-500 p-3'
            >
              <div className='flex w-fit flex-col items-center gap-2'>
                <div className='text-2xl font-medium text-yellow-500'>
                  {ratings.averageRating.toFixed(1)}/5
                </div>

                <FiveStar
                  rating={ratings.averageRating}
                  classNameForSize={`w-8 h-8`}
                />
              </div>

              <div className='flex items-center justify-center gap-3'>
                <div
                  className='rounded border-[1px] px-5 py-1 hover:cursor-pointer
                hover:border-yellow-500 hover:text-yellow-500'
                >
                  Tất cả
                </div>
                <div
                  className='rounded border-[1px] px-5 py-1 hover:cursor-pointer
                hover:border-yellow-500 hover:text-yellow-500'
                >
                  5 Sao
                </div>
                <div
                  className='rounded border-[1px] px-5 py-1 hover:cursor-pointer
                hover:border-yellow-500 hover:text-yellow-500'
                >
                  4 Sao
                </div>
                <div
                  className='rounded border-[1px] px-5 py-1 hover:cursor-pointer
                hover:border-yellow-500 hover:text-yellow-500'
                >
                  3 Sao
                </div>
                <div
                  className='rounded border-[1px] px-5 py-1 hover:cursor-pointer
                hover:border-yellow-500 hover:text-yellow-500'
                >
                  2 Sao
                </div>
                <div
                  className='rounded border-[1px] px-5 py-1 hover:cursor-pointer
                hover:border-yellow-500 hover:text-yellow-500'
                >
                  1 Sao
                </div>
              </div>
            </div>

            <div className='mt-5 flex flex-col gap-5 p-5'>
              {ratings.ratings.items.map((item, index) => (
                <div key={index} className='flex flex-col gap-5'>
                  <div className='flex gap-x-3'>
                    <img
                      className='h-10 w-10 rounded-full'
                      alt='img'
                      src='https://down-vn.img.susercontent.com/file/sg-11134004-7qvef-lfjhk3o78hho40_tn'
                    />

                    <div className='flex flex-col gap-1'>
                      <div className='text-sm font-medium'>{item.userName}</div>
                      <div>
                        <FiveStar
                          classNameForSize={`w-5 h-5`}
                          rating={item.ratingValue}
                        />
                      </div>
                      <div className='text-sm'>Phân loại: {item.skuName}</div>
                      <div className='mt-3'>{item.comment}</div>
                    </div>
                  </div>

                  <Divider className='mt-5' />
                </div>
              ))}

              <div className='flex items-center justify-center'>
                <Pagination
                  count={10}
                  variant='outlined'
                  color='primary'
                  size='large'
                  onChange={handleChange}
                />
              </div>
            </div>
          </div>
        </BlockWrapper>
      </div>
    </div>
  )
}

export default ProductDetail
