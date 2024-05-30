import { useEffect, useState } from "react"
import { Divider, CircularProgress } from "@mui/material"
import {
  ChevronLeftIcon,
  ChevronRightIcon,
  MinusIcon,
  PlusIcon,
} from "@heroicons/react/20/solid"
import {
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
} from "@headlessui/react"
import { NavLink, Link, useLocation, useNavigate } from "react-router-dom"

import Dropdown from "../../components/Dropdown"
import Button from "../../components/Button"
import {
  objectToQueryString,
  searchParamsToObject,
} from "../../components/funcApi"
import { findParent } from "../../components/funcCatalog"
import ProductItem from "../../components/User/ProductItem"
import MyFilter from "../../components/MyFilter"

const breadcrumbs = [
  {
    href: "/",
    name: "Sách tiếng việt",
  },
]

const productTypes = [
  {
    productTypeCode: "sach-tieng-viet",
    level: 1,
    displayName: "SáchTiếngViệt",
    description: "Các loại sách viết bằng tiếng Việt",
    parentProductTypeId: null,
    childProductTypes: null,
    id: 1,
  },
  {
    productTypeCode: "van-phong-pham",
    level: 1,
    displayName: "VănPhòngPhẩm",
    description: "Các loại văn phòng phẩm như bút, viết, tập vở",
    parentProductTypeId: null,
    childProductTypes: null,
    id: 2,
  },
  {
    productTypeCode: "do-choi",
    level: 1,
    displayName: "ĐồChơi",
    description: "Đồ chơi các loại",
    parentProductTypeId: null,
    childProductTypes: null,
    id: 3,
  },
  {
    productTypeCode: "thieu-nhi",
    level: 2,
    displayName: "ThiếuNhi",
    description: "Các loại sách Thiếu Nhi",
    parentProductTypeId: 1,
    childProductTypes: null,
    id: 4,
  },
  {
    productTypeCode: "giao-khoa-tham-khao",
    level: 2,
    displayName: "GiáoKhoa-ThamKhảo",
    description: "Các loại SGK và sách tham khảo",
    parentProductTypeId: 1,
    childProductTypes: null,
    id: 5,
  },
  {
    productTypeCode: "manga-comic",
    level: 2,
    displayName: "Manga-Comic",
    description: "Các loại sách Manga và Comic",
    parentProductTypeId: 1,
    childProductTypes: null,
    id: 6,
  },
  {
    productTypeCode: "but-viet",
    level: 2,
    displayName: "Bút-Viết",
    description: "Các loại bút viết",
    parentProductTypeId: 2,
    childProductTypes: null,
    id: 7,
  },
  {
    productTypeCode: "dung-cu-hoc-sinh",
    level: 2,
    displayName: "DụngCụHọcSinh",
    description: "Các loại dụng cụ học sinh dùng ở trường",
    parentProductTypeId: 2,
    childProductTypes: null,
    id: 8,
  },
  {
    productTypeCode: "san-pham-ve-giay",
    level: 2,
    displayName: "SảnPhẩmVềGiấy",
    description: "Các loại bút viết",
    parentProductTypeId: 2,
    childProductTypes: null,
    id: 9,
  },
  {
    productTypeCode: "bup-be-thu-bong",
    level: 2,
    displayName: "BúpBê-ThúBông",
    description: "Các loại thú bông và búp bê",
    parentProductTypeId: 3,
    childProductTypes: null,
    id: 10,
  },
  {
    productTypeCode: "mo-hinh",
    level: 2,
    displayName: "MôHình",
    description: "Các loại mô hình lắp ghép",
    parentProductTypeId: 3,
    childProductTypes: null,
    id: 11,
  },
  {
    productTypeCode: "board-game",
    level: 2,
    displayName: "BoardGame",
    description: "Các loại board game",
    parentProductTypeId: 3,
    childProductTypes: null,
    id: 12,
  },
  {
    productTypeCode: "truyen-thieu-nhi",
    level: 3,
    displayName: "TruyệnThiếuNhi",
    description: "Các loại truyện thiếu nhi",
    parentProductTypeId: 4,
    childProductTypes: null,
    id: 13,
  },
  {
    productTypeCode: "to-mau-luyen-chu",
    level: 3,
    displayName: "TôMàu-LuyệnChữ",
    description: "Các loại sách tô màu và luyện chữ cho trẻ em",
    parentProductTypeId: 4,
    childProductTypes: null,
    id: 14,
  },
  {
    productTypeCode: "sach-tham-khao",
    level: 3,
    displayName: "SáchThamKhảo",
    description: "Các loại sách tham khảo dành cho học sinh, sinh viên",
    parentProductTypeId: 5,
    childProductTypes: null,
    id: 15,
  },
  {
    productTypeCode: "sach-giao-khoa",
    level: 3,
    displayName: "SáchGiáoKhoa",
    description: "Các loại sách giáo khoa dành cho học sinh",
    parentProductTypeId: 5,
    childProductTypes: null,
    id: 16,
  },
  {
    productTypeCode: "manga",
    level: 3,
    displayName: "Manga",
    description: "Các loại sách Manga",
    parentProductTypeId: 6,
    childProductTypes: null,
    id: 17,
  },
  {
    productTypeCode: "comic",
    level: 3,
    displayName: "Comic",
    description: "Các loại sách Comic",
    parentProductTypeId: 6,
    childProductTypes: null,
    id: 18,
  },
  {
    productTypeCode: "but-chi",
    level: 3,
    displayName: "BútChì",
    description: "Các loại bút chì",
    parentProductTypeId: 7,
    childProductTypes: null,
    id: 19,
  },
  {
    productTypeCode: "but-muc-but-may",
    level: 3,
    displayName: "BútMực-BútMáy",
    description: "Các loại bút mực và bút máy",
    parentProductTypeId: 7,
    childProductTypes: null,
    id: 20,
  },
  {
    productTypeCode: "gom-tay",
    level: 3,
    displayName: "Gôm-Tẩy",
    description: "Các loại gôm và tẩy",
    parentProductTypeId: 8,
    childProductTypes: null,
    id: 21,
  },
  {
    productTypeCode: "thu-bong",
    level: 3,
    displayName: "ThúBông",
    description: "Các loại thú bông",
    parentProductTypeId: 10,
    childProductTypes: null,
    id: 22,
  },
  {
    productTypeCode: "bup-be",
    level: 3,
    displayName: "BúpBê",
    description: "Các loại búp bê",
    parentProductTypeId: 10,
    childProductTypes: null,
    id: 23,
  },
]

const products = [
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

const filters = [
  {
    id: "color",
    name: "Color",
    options: [
      { value: "white", label: "White", checked: false },
      { value: "beige", label: "Beige", checked: false },
      { value: "blue", label: "Blue", checked: true },
      { value: "brown", label: "Brown", checked: false },
      { value: "green", label: "Green", checked: false },
      { value: "purple", label: "Purple", checked: false },
    ],
  },
  {
    id: "category",
    name: "Category",
    options: [
      { value: "new-arrivals", label: "New Arrivals", checked: false },
      { value: "sale", label: "Sale", checked: false },
      { value: "travel", label: "Travel", checked: true },
      { value: "organization", label: "Organization", checked: false },
      { value: "accessories", label: "Accessories", checked: false },
    ],
  },
  {
    id: "size",
    name: "Size",
    options: [
      { value: "2l", label: "2L", checked: false },
      { value: "6l", label: "6L", checked: false },
      { value: "12l", label: "12L", checked: false },
      { value: "18l", label: "18L", checked: false },
      { value: "20l", label: "20L", checked: false },
      { value: "40l", label: "40L", checked: true },
    ],
  },
]

const Catalog = () => {
  const navigate = useNavigate()
  const location = useLocation()
  const [objectSearch, setObjectSearch] = useState({})

  // CATALOG
  const [currentProductType, setCurrentProductType] = useState(null)
  const [parents, setParents] = useState([])
  const [child, setChild] = useState(null)
  const [equalLevel, setEqualLevel] = useState([])

  const handleCatalog = async (objectSearch) => {
    if (objectSearch?.ProductTypeIds?.length === 1) {
      const idToFind = Number(objectSearch.ProductTypeIds[0])

      const current = productTypes.find((element) => element.id === idToFind)

      setCurrentProductType(current)

      if (current?.parentProductTypeId) {
        setParents(findParent(productTypes, idToFind))
      }
      setChild(
        productTypes.filter((item) => item.parentProductTypeId === idToFind)
      )
      if (current.level === 3) {
        setEqualLevel(
          productTypes.filter(
            (item) => item.parentProductTypeId === current.parentProductTypeId
          )
        )
      }
    }
  }

  // SORT
  const [selected, setSelected] = useState({})

  const handleSort = (value, sortBy, sortDirection) => {
    if (sortBy && sortDirection) {
      setSelected({})
      navigate(
        "/catalog" +
          objectToQueryString({
            ...objectSearch,
            sortBy: [sortBy],
            sortDirection: [sortDirection],
          })
      )
    } else {
      setSelected(value)
      navigate(
        "/catalog" +
          objectToQueryString({
            ...objectSearch,
            sortBy: [value.sortBy],
            sortDirection: [value.sortDirection],
          })
      )
    }
  }

  // PAGINATION
  const [totalPages, setTotalPages] = useState(10)
  const [pageNumber, setPageNumber] = useState(1)

  useEffect(() => {
    const fetch = async () => {
      const objectSearch = searchParamsToObject(
        new URLSearchParams(location.search)
      )

      if (!(objectSearch?.sortBy && objectSearch?.sortBy[0] === "Price")) {
        setSelected({})
      }

      console.log(objectSearch)

      setObjectSearch(objectSearch)

      handleCatalog(objectSearch)
    }

    fetch()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [location.search])

  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
        gap-y-5 p-4 lg:px-8'
      >
        <div className='flex w-full flex-col gap-4'>
          <nav aria-label='breadcrumbs'>
            <ol className='flex items-center'>
              <li>
                <div className='flex items-center'>
                  <Link
                    to={"/"}
                    className='mr-2 text-sm font-medium text-green-900'
                  >
                    Trang chủ
                  </Link>
                  <svg
                    width={16}
                    height={20}
                    viewBox='0 0 16 20'
                    fill='currentColor'
                    aria-hidden='true'
                    className='h-5 w-4 text-gray-300'
                  >
                    <path d='M5.697 4.34L8.98 16.532h1.327L7.025 4.341H5.697z' />
                  </svg>
                </div>
              </li>
              {breadcrumbs.map((breadcrumb, index) => (
                <li key={index}>
                  <div className='flex items-center'>
                    <a
                      href={breadcrumb.href}
                      className='mr-2 text-sm font-medium text-green-900'
                    >
                      {breadcrumb.name}
                    </a>
                    {index < breadcrumb.length && (
                      <svg
                        width={16}
                        height={20}
                        viewBox='0 0 16 20'
                        fill='currentColor'
                        aria-hidden='true'
                        className='h-5 w-4 text-gray-300'
                      >
                        <path d='M5.697 4.34L8.98 16.532h1.327L7.025 4.341H5.697z' />
                      </svg>
                    )}
                  </div>
                </li>
              ))}
            </ol>
          </nav>

          <div className='flex items-start gap-4'>
            <div className='flex min-h-[500px] min-w-60 flex-col justify-start rounded-md bg-white p-3 shadow'>
              <div className='category'>
                <div className='mb-2 flex items-center gap-3'>
                  <div className='icon'>
                    <svg
                      xmlns='http://www.w3.org/2000/svg'
                      viewBox='0 0 24 24'
                      fill='currentColor'
                      className='size-7 text-green-700'
                    >
                      <path
                        fillRule='evenodd'
                        d='M3 6.75A.75.75 0 0 1 3.75 6h16.5a.75.75 0 0 1 0 1.5H3.75A.75.75 0 0 1 3 6.75ZM3 12a.75.75 0 0 1 .75-.75h16.5a.75.75 0 0 1 0 1.5H3.75A.75.75 0 0 1 3 12Zm0 5.25a.75.75 0 0 1 .75-.75h16.5a.75.75 0 0 1 0 1.5H3.75a.75.75 0 0 1-.75-.75Z'
                        clipRule='evenodd'
                      />
                    </svg>
                  </div>
                  <div className='title font-semibold text-green-700'>
                    Danh mục sản phẩm
                  </div>
                </div>

                <Divider />

                <div className='my-2 flex flex-col'>
                  <NavLink
                    to={"/catalog"}
                    className={`${currentProductType === null && "text-ct-green-400"}
                        my-1 font-medium hover:cursor-pointer hover:text-ct-green-400`}
                    onClick={() => setCurrentProductType(null)}
                  >
                    Tất cả
                  </NavLink>

                  {currentProductType?.parentProductTypeId &&
                    parents.map((item) => (
                      <Link
                        key={item.id}
                        to={"/catalog?ProductTypeIds=" + item.id}
                        className={`
                          ${item.level === 1 && "ml-5"} 
                          ${item.level === 2 && "ml-10"} 
                          my-1 hover:cursor-pointer`}
                      >
                        {item.displayName}
                      </Link>
                    ))}

                  {currentProductType && currentProductType.level !== 3 && (
                    <Link
                      key={currentProductType.id}
                      to={"/catalog?ProductTypeIds=" + currentProductType.id}
                      className={`
                      ${currentProductType.level === 1 && "ml-5"} 
                      ${currentProductType.level === 2 && "ml-10"} 
                      my-1 font-medium text-ct-green-400 hover:cursor-pointer`}
                    >
                      {currentProductType.displayName}
                    </Link>
                  )}

                  {currentProductType &&
                    currentProductType.level === 3 &&
                    equalLevel.map((item) => (
                      <Link
                        key={item.id}
                        to={"/catalog?ProductTypeIds=" + item.id}
                        className={`${currentProductType.id === item.id && "font-medium text-ct-green-400"} 
                        my-1 ml-14 hover:cursor-pointer`}
                      >
                        {item.displayName}
                      </Link>
                    ))}

                  {currentProductType &&
                    child?.map((item) => (
                      <Link
                        key={item.id}
                        to={"/catalog?ProductTypeIds=" + item.id}
                        className={`
                      ${item.level === 1 && "ml-5"} 
                      ${item.level === 2 && "ml-10"} 
                      ${item.level === 3 && "ml-14"} 
                      my-1 hover:cursor-pointer`}
                      >
                        {item.displayName}
                      </Link>
                    ))}

                  {currentProductType === null &&
                    productTypes.map((item, index) => {
                      if (item.level > 1) return null

                      return (
                        <Link
                          key={index}
                          to={"/catalog?ProductTypeIds=" + item.id}
                          className={`
                            ${item.level === 1 && "ml-5"} 
                            ${item.level === 2 && "ml-10"} 
                            ${item.level === 3 && "ml-14"} 
                            my-1 hover:cursor-pointer`}
                        >
                          {item.displayName}
                        </Link>
                      )
                    })}
                </div>
              </div>

              <div className=''>
                {filters.map((section, index) => (
                  <MyFilter key={index} section={section} />
                ))}
              </div>
            </div>

            <div className='flex min-h-[500px] flex-1 flex-col items-center justify-center gap-3'>
              <div className='flex w-full items-center gap-3 rounded-md bg-white px-4 py-2 shadow '>
                <div className='text-sm font-medium text-gray-900'>
                  Sắp xếp theo
                </div>
                <Button
                  className={`
                   ${
                     objectSearch?.sortBy
                       ? objectSearch?.sortBy[0] === "BestSeller"
                         ? ""
                         : "bg-white !text-gray-900 hover:!bg-gray-100"
                       : "bg-white !text-gray-900 hover:!bg-gray-100"
                   } 
                    border-[1px] text-sm`}
                  onClick={() => handleSort("button", "BestSeller", "desc")}
                >
                  Bán chạy
                </Button>
                <Button
                  className={`
                   ${
                     objectSearch?.sortBy
                       ? objectSearch?.sortBy[0] === "CreatedWhen"
                         ? ""
                         : "bg-white !text-gray-900 hover:!bg-gray-100"
                       : "bg-white !text-gray-900 hover:!bg-gray-100"
                   } 
                     border-[1px] text-sm`}
                  onClick={() => handleSort("button", "CreatedWhen", "desc")}
                >
                  Mới nhất
                </Button>
                <Dropdown
                  selected={selected}
                  setSelected={handleSort}
                  placeholder={"Giá"}
                  items={[
                    {
                      sortBy: "Price",
                      text: "Giá: từ thấp đến cao",
                      sortDirection: "asc",
                    },
                    {
                      sortBy: "Price",
                      text: "Giá: từ cao đến thấp",
                      sortDirection: "desc",
                    },
                  ]}
                ></Dropdown>
              </div>

              <div className='flex w-full flex-1 items-center justify-center rounded-md bg-white p-3 shadow'>
                {/* <CircularProgress sx={{ color: "green" }} size={50} /> */}
                <div className='grid h-full w-full grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5'>
                  {products.map((item, index) => (
                    <ProductItem key={index} index={index} item={item} />
                  ))}
                </div>
              </div>

              <div className='flex items-center gap-3 rounded-md bg-white p-2 shadow '>
                <nav
                  className='isolate inline-flex -space-x-px rounded-md shadow-sm'
                  aria-label='Pagination'
                >
                  <Link className='relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0'>
                    <span className='sr-only'>Previous</span>
                    <ChevronLeftIcon className='h-5 w-5' aria-hidden='true' />
                  </Link>
                  {pageNumber > 4 && (
                    <Link className='relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0'>
                      ...
                    </Link>
                  )}
                  {pageNumber - 2 > 0 && (
                    <Link
                      className='relative inline-flex items-center px-4 py-2 text-sm font-semibold 
                        text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 
                        focus:outline-offset-0'
                    >
                      {pageNumber - 2}
                    </Link>
                  )}
                  {pageNumber - 1 > 0 && (
                    <Link
                      className='relative inline-flex items-center px-4 py-2 text-sm font-semibold 
                        text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 
                        focus:outline-offset-0'
                    >
                      {pageNumber - 1}
                    </Link>
                  )}
                  <Link
                    aria-current='page'
                    className='relative z-10 inline-flex items-center bg-green-600 px-4 py-2 text-sm font-semibold text-white focus:z-20 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600'
                  >
                    {pageNumber}
                  </Link>

                  {pageNumber + 1 <= totalPages && (
                    <Link
                      className='relative inline-flex items-center px-4 py-2 text-sm font-semibold 
                        text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 
                        focus:outline-offset-0'
                    >
                      {pageNumber + 1}
                    </Link>
                  )}

                  {pageNumber + 2 <= totalPages && (
                    <Link
                      className='relative inline-flex items-center px-4 py-2 text-sm font-semibold 
                        text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 
                        focus:outline-offset-0'
                    >
                      {pageNumber + 2}
                    </Link>
                  )}

                  {totalPages - (pageNumber + 2) > 0 && (
                    <Link className='relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0'>
                      ...
                    </Link>
                  )}
                  <Link className='relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0'>
                    <span className='sr-only'>Next</span>
                    <ChevronRightIcon className='h-5 w-5' aria-hidden='true' />
                  </Link>
                </nav>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Catalog
