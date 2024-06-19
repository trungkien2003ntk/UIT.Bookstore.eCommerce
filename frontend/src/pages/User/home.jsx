import { useEffect, useState } from "react"
import { Link, useNavigate } from "react-router-dom"
import BlockWrapper from "../../components/BlockWrapper"
import ImageSlider from "../../components/User/ImageSlider"
import ProductItem from "../../components/User/ProductItem"
import MySwiper from "../../components/MySwiper"

import * as productServices from "../../apiServices/productServices"

const slides = [
  "https://img.freepik.com/free-vector/flat-social-media-cover-template-world-book-day-celebration_23-2150201450.jpg?t=st=1715839011~exp=1715842611~hmac=6bf816776980806cc41a27ea418ac1b2b5ec2d7b940d142057e66b6055da6bf5&w=1380",
  "https://images.unsplash.com/photo-1457369804613-52c61a468e7d?q=80&w=1770&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
  "https://images.unsplash.com/photo-1575773462156-bc243c6d3adf?q=80&w=1770&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
  "https://images.unsplash.com/photo-1613837433006-7cfce42658c6?q=80&w=1772&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
]

const cate = [
  {
    name: "Đồ chơi",
    image:
      "https://cdn0.fahasa.com/media/wysiwyg/Huyen-KD/3900000187138-2-removebg-preview.png",
  },
  {
    name: "Văn học",
    image:
      "https://cdn0.fahasa.com/media/wysiwyg/tuan-test-css/thuong-resize.jpg",
  },
  {
    name: "Tâm lý",
    image:
      "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/TLKN.png",
  },
  {
    name: "Ngoại ngữ",
    image:
      "https://cdn0.fahasa.com/media/wysiwyg/hieu_kd/2023-08-frame/b1_-HNN.png",
  },
  {
    name: "Bộ màu vẽ",
    image:
      "https://cdn0.fahasa.com/media/wysiwyg/Huyen-KD/9328577021909-1-removebg-preview.png",
  },
  {
    name: "Capypara",
    image:
      "https://cdn0.fahasa.com/media/catalog/product/2/0/2000214266450.jpg",
  },
  {
    name: "Bình nước",
    image:
      "https://cdn0.fahasa.com/media/catalog/product/6/9/6922111456726.jpg",
  },
  {
    name: "Gấu bông",
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8936134263122_1.jpg",
  },
  {
    name: "Lắp ghép",
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935256622237.jpg",
  },
  {
    name: "IELTS",
    image:
      "https://cdn0.fahasa.com/media/catalog/product/9/7/9781009470377_1.jpg",
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

const products2 = [
  {
    name: "Bút Bi Nước Roller Pen 0.5 mm - Stacom RP2025 - Mực Xanh",
    price: 10500,
    discount_rate: 5,
    actual_price: 9975,
    sale_quantity: 1200,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8904106862611.jpg",
  },
  {
    name: "Bút Bi Bấm Pentonic Switch 0.4 mm - Linc 4029-B",
    price: 0,
    discount_rate: 0,
    actual_price: 5000,
    sale_quantity: 82,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935318307294.jpg",
  },
  {
    name: "Kệ Rổ 1 Ngăn Ageless - Xanh Dương",
    price: 179000,
    discount_rate: 20,
    actual_price: 143200,
    sale_quantity: 3000,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/i/m/image_244718_1_1685.jpg",
  },
  {
    name: "Bìa Tài Liệu 5 Ngăn A4 Friendly Rabbit - Classmate CL-CBA32 - Màu Xanh Dương",
    price: 190000,
    discount_rate: 35,
    actual_price: 123500,
    sale_quantity: 100,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8936212221839-mau3.jpg",
  },
  {
    name: "Gỡ Kim Số 10 Officetex OT-SR003 (Mẫu Màu Giao Ngẫu Nhiên)",
    price: 336000,
    discount_rate: 10,
    actual_price: 302000,
    sale_quantity: 6000,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/i/m/image_244718_1_187.jpg",
  },
  {
    name: "Kẹp Bướm Echo 32mm",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 82,
    rating: 4,
    image: "https://cdn0.fahasa.com/media/catalog/product/i/m/image_220690.jpg",
  },
]

const products3 = [
  {
    name: "A Man Called Ove",
    price: 10500,
    discount_rate: 5,
    actual_price: 9975,
    sale_quantity: 1200,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/9/7/9781476738024_1.jpg",
  },
  {
    name: "Little Fires Everywhere",
    price: 0,
    discount_rate: 0,
    actual_price: 5000,
    sale_quantity: 82,
    rating: 4.5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_1713.jpg",
  },
  {
    name: "Little Fires Everywhere (Movie Tie-In)",
    price: 179000,
    discount_rate: 20,
    actual_price: 143200,
    sale_quantity: 3000,
    rating: 5,
    image:
      "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_55518.jpg",
  },
  {
    name: "The true story of the 3 little pigs",
    price: 190000,
    discount_rate: 35,
    actual_price: 123500,
    sale_quantity: 100,
    rating: 5,
    image: "https://cdn0.fahasa.com/media/catalog/product/i/m/image_173168.jpg",
  },
  {
    name: "Entwined With You: Crossfire Book 3",
    price: 336000,
    discount_rate: 10,
    actual_price: 302000,
    sale_quantity: 6000,
    rating: 4.5,
    image: "https://cdn0.fahasa.com/media/catalog/product/1/1/111_2.jpg",
  },
  {
    name: "The Upside of Irrationality : The Unexpected Benefits of Defying Logic",
    price: 100000,
    discount_rate: 20,
    actual_price: 80000,
    sale_quantity: 82,
    rating: 4,
    image: "https://cdn0.fahasa.com/media/catalog/product/i/m/image_105529.jpg",
  },
]

const cateBlock = [
  {
    name: "Sách giáo khoa - Tham khảo",
    primary:
      "https://cdn0.fahasa.com/media/wysiwyg/Thang-02-2024/CATE_BackToSchool_266x156.jpg",
    child: [
      "https://cdn0.fahasa.com/media/catalog/product/8/7/8794069304118.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/3/3/3300000027449.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/9/7/9786040288080.jpg",
    ],
  },
  {
    name: "Manga - Comic",
    primary:
      "https://cdn0.fahasa.com/media/wysiwyg/Thang-02-2024/CATE_Manga_266x156.jpg",
    child: [
      "https://cdn0.fahasa.com/media/catalog/product/s/h/shadow_house_bia_tap_1.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/b/o/bocchi_the_rock_bia_tap_2.jpg",
      "https://cdn0.fahasa.com/media/wysiwyg/Thang-02-2024/frieren_phap_su_tien_tang_bia_tap_6_resize.jpg",
    ],
  },
  {
    name: "Ngoại văn",
    primary:
      "https://cdn0.fahasa.com/media/wysiwyg/Thang-02-2024/CATE_Findyour_266x156.jpg",
    child: [
      "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_39171.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/9/7/9780593189641.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/i/m/image_195509_1_39154.jpg",
    ],
  },
  {
    name: "Đồ chơi",
    primary:
      "https://cdn0.fahasa.com/media/wysiwyg/Thang-02-2024/CATE_DoChoi_266x156.jpg",
    child: [
      "https://cdn0.fahasa.com/media/catalog/product/5/7/5702017419626.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/z/4/z4876354661921_09b9c396ffd40da9720f172bbf9a7115.jpg",
      "https://cdn0.fahasa.com/media/catalog/product/8/9/8935337508153.jpg",
    ],
  },
]

const Home = () => {
  const navigate = useNavigate()

  const [trendyProducts, setTrendyProducts] = useState([])
  const [vietBooks, setVietBooks] = useState([])

  const getTrendyProducts = async () => {
    const response = await productServices
      .getTrendyProducts()
      .catch((error) => {
        if (error.response) {
          // The request was made and the server responded with a status code
          // that falls out of the range of 2xx
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          // The request was made but no response was received
          // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
          // http.ClientRequest in node.js
          console.log(error.request)
        } else {
          // Something happened in setting up the request that triggered an Error
          console.log("Error", error.message)
        }
        console.log(error.config)
      })

    if (response) {
      console.log(response.items)
      setTrendyProducts(response.items)
    }
  }

  const getVietBooks = async () => {
    const response = await productServices
      .getProductList("?ProductTypeIds=1")
      .catch((error) => {
        if (error.response) {
          // The request was made and the server responded with a status code
          // that falls out of the range of 2xx
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          // The request was made but no response was received
          // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
          // http.ClientRequest in node.js
          console.log(error.request)
        } else {
          // Something happened in setting up the request that triggered an Error
          console.log("Error", error.message)
        }
        console.log(error.config)
      })

    if (response) {
      console.log(response)
      setVietBooks(response.items)
    }
  }

  useEffect(() => {
    getTrendyProducts()
    getVietBooks()
  }, [])

  return (
    <div>
      <main className='relative w-full'>
        <div
          className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
              gap-y-5 p-4 lg:px-8'
        >
          <ImageSlider slides={slides} />

          <BlockWrapper
            title={"Danh mục sản phẩm"}
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                fill='none'
                viewBox='0 0 24 24'
                strokeWidth={1.5}
                stroke='currentColor'
                className='h-6 w-6 text-ct-green-400'
              >
                <path
                  strokeLinecap='round'
                  strokeLinejoin='round'
                  d='M3.75 6A2.25 2.25 0 0 1 6 3.75h2.25A2.25 2.25 0 0 1 10.5 6v2.25a2.25 2.25 0 0 1-2.25 2.25H6a2.25 2.25 0 0 1-2.25-2.25V6ZM3.75 15.75A2.25 2.25 0 0 1 6 13.5h2.25a2.25 2.25 0 0 1 2.25 2.25V18a2.25 2.25 0 0 1-2.25 2.25H6A2.25 2.25 0 0 1 3.75 18v-2.25ZM13.5 6a2.25 2.25 0 0 1 2.25-2.25H18A2.25 2.25 0 0 1 20.25 6v2.25A2.25 2.25 0 0 1 18 10.5h-2.25a2.25 2.25 0 0 1-2.25-2.25V6ZM13.5 15.75a2.25 2.25 0 0 1 2.25-2.25H18a2.25 2.25 0 0 1 2.25 2.25V18A2.25 2.25 0 0 1 18 20.25h-2.25A2.25 2.25 0 0 1 13.5 18v-2.25Z'
                />
              </svg>
            }
          >
            <div className='grid grid-cols-2 xs:grid-cols-4 sm:grid-cols-6 md:grid-cols-8 lg:grid-cols-10'>
              {cate.map((item, index) => (
                <Link
                  key={index}
                  className='group flex h-auto w-[120px] flex-col items-center rounded-md
                      border-[1px] border-transparent p-2 hover:cursor-pointer hover:border-gray-200'
                >
                  <div
                    className='img h-[70px] w-full bg-contain bg-center bg-no-repeat'
                    style={{
                      backgroundImage: `url('${item.image}')`,
                    }}
                  ></div>
                  <div className='name mt-2 line-clamp-1 font-medium group-hover:text-green-700'>
                    {item.name}
                  </div>
                </Link>
              ))}
            </div>
          </BlockWrapper>
        </div>
      </main>

      <main className='relative mt-4 w-full bg-gradient-to-b from-red-400 to-red-500'>
        <div
          className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
              gap-5 p-4 py-10 lg:px-8'
        >
          <BlockWrapper
            title={"Xu hướng mua sắm"}
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='h-6 w-6 text-red-600'
              >
                <path
                  fillRule='evenodd'
                  d='M15.22 6.268a.75.75 0 0 1 .968-.431l5.942 2.28a.75.75 0 0 1 .431.97l-2.28 5.94a.75.75 0 1 1-1.4-.537l1.63-4.251-1.086.484a11.2 11.2 0 0 0-5.45 5.173.75.75 0 0 1-1.199.19L9 12.312l-6.22 6.22a.75.75 0 0 1-1.06-1.061l6.75-6.75a.75.75 0 0 1 1.06 0l3.606 3.606a12.695 12.695 0 0 1 5.68-4.974l1.086-.483-4.251-1.632a.75.75 0 0 1-.432-.97Z'
                  clipRule='evenodd'
                />
              </svg>
            }
          >
            <MySwiper slides={trendyProducts}></MySwiper>
          </BlockWrapper>
        </div>
      </main>

      <main className='relative mt-4 w-full'>
        <div
          className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
              gap-5 p-4 lg:px-8'
        >
          <div className='grid h-full w-full grid-cols-2 gap-4 lg:grid-cols-4'>
            {cateBlock.map((item, index) => (
              <Link
                key={index}
                className='group flex select-none flex-col justify-center gap-2 rounded bg-white px-3 py-3 shadow'
              >
                <div
                  className='header flex items-center justify-between font-semibold 
                    group-hover:text-ct-green-400'
                >
                  <div className='title '>{item.name}</div>
                  <div className='icon'>
                    <svg
                      xmlns='http://www.w3.org/2000/svg'
                      viewBox='0 0 24 24'
                      fill='currentColor'
                      className='h-6 w-6'
                    >
                      <path
                        fillRule='evenodd'
                        d='M16.28 11.47a.75.75 0 0 1 0 1.06l-7.5 7.5a.75.75 0 0 1-1.06-1.06L14.69 12 7.72 5.03a.75.75 0 0 1 1.06-1.06l7.5 7.5Z'
                        clipRule='evenodd'
                      />
                    </svg>
                  </div>
                </div>
                <div className=''>
                  <div
                    className='img h-44 w-full bg-cover bg-center bg-no-repeat'
                    style={{ backgroundImage: `url(${item.primary})` }}
                  ></div>
                  <div className='mt-2 grid grid-cols-3 gap-1'>
                    {item.child.map((childItem, childIndex) => (
                      <div
                        key={childIndex}
                        className='img h-24 w-full bg-cover bg-center bg-no-repeat'
                        style={{ backgroundImage: `url(${childItem})` }}
                      ></div>
                    ))}
                  </div>
                </div>
              </Link>
            ))}
          </div>
        </div>
      </main>

      {/* <main className='relative w-full'>
        <div
          className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
              gap-5 p-4 lg:px-8'
        >
          <BlockWrapper
            title={"Văn phòng phẩm nổi bật"}
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='h-6 w-6 text-blue-400'
              >
                <path d='M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157 3.712 3.712 1.157-1.157a2.625 2.625 0 0 0 0-3.712ZM19.513 8.199l-3.712-3.712-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32L19.513 8.2Z' />
              </svg>
            }
          >
            <div className='grid h-full w-full grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6'>
              {products2.map((item, index) => (
                <ProductItem key={index} index={index} item={item} />
              ))}
            </div>
          </BlockWrapper>
        </div>
      </main> */}

      <main className='relative w-full'>
        <div
          className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
              gap-5 p-4 lg:px-8'
        >
          <BlockWrapper
            title={"Sách tiếng Việt nổi bật"}
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='h-6 w-6 text-orange-500'
              >
                <path d='M11.25 4.533A9.707 9.707 0 0 0 6 3a9.735 9.735 0 0 0-3.25.555.75.75 0 0 0-.5.707v14.25a.75.75 0 0 0 1 .707A8.237 8.237 0 0 1 6 18.75c1.995 0 3.823.707 5.25 1.886V4.533ZM12.75 20.636A8.214 8.214 0 0 1 18 18.75c.966 0 1.89.166 2.75.47a.75.75 0 0 0 1-.708V4.262a.75.75 0 0 0-.5-.707A9.735 9.735 0 0 0 18 3a9.707 9.707 0 0 0-5.25 1.533v16.103Z' />
              </svg>
            }
          >
            <div className='grid h-full w-full grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6'>
              {vietBooks.map((item, index) => (
                <ProductItem key={index} index={index} item={item} />
              ))}
            </div>
          </BlockWrapper>
        </div>
      </main>

      <main className='relative w-full'>
        <div
          className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
              gap-5 p-4 lg:px-8'
        >
          <BlockWrapper
            title={"Văn phòng phẩm nổi bật"}
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='h-6 w-6 text-orange-500'
              >
                <path d='M11.25 4.533A9.707 9.707 0 0 0 6 3a9.735 9.735 0 0 0-3.25.555.75.75 0 0 0-.5.707v14.25a.75.75 0 0 0 1 .707A8.237 8.237 0 0 1 6 18.75c1.995 0 3.823.707 5.25 1.886V4.533ZM12.75 20.636A8.214 8.214 0 0 1 18 18.75c.966 0 1.89.166 2.75.47a.75.75 0 0 0 1-.708V4.262a.75.75 0 0 0-.5-.707A9.735 9.735 0 0 0 18 3a9.707 9.707 0 0 0-5.25 1.533v16.103Z' />
              </svg>
            }
          >
            <div className='grid h-full w-full grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6'>
              {vietBooks.map((item, index) => (
                <ProductItem key={index} index={index} item={item} />
              ))}
            </div>
          </BlockWrapper>
        </div>
      </main>
    </div>
  )
}

export default Home
