/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from "react"
import { Divider, CircularProgress, Pagination } from "@mui/material"
import { ChevronLeftIcon, ChevronRightIcon } from "@heroicons/react/20/solid"
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

import * as productTypeServices from "../../apiServices/productTypeServices"
import * as productServices from "../../apiServices/productServices"

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

  const [products, setProducts] = useState(null)

  const [totalPages, setTotalPages] = useState(0)

  const [loading, setLoading] = useState(false)

  const [productTypes, setProductTypes] = useState(null)

  // CATALOG
  const [currentProductType, setCurrentProductType] = useState(null)
  const [parents, setParents] = useState(null)
  const [child, setChild] = useState(null)
  const [equalLevel, setEqualLevel] = useState(null)

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
    console.log("go")
    if (sortBy && sortDirection) {
      setSelected({})

      getListProduct({
        ...objectSearch,
        sortBy: [sortBy],
        sortDirection: [sortDirection],
      })
    } else {
      setSelected(value)
      navigate(
        "/user/catalog" +
          objectToQueryString({
            ...objectSearch,
            sortBy: [value.sortBy],
            sortDirection: [value.sortDirection],
          })
      )
    }
  }

  const [hightLight, setHightLight] = useState(0)

  const [notFound, setNotFound] = useState(false)

  const getListProduct = async (objectSearch) => {
    console.log(objectToQueryString(objectSearch))

    const response = await productServices
      .getProductList(objectToQueryString(objectSearch))
      .catch((error) => {
        if (error.response) {
          if (error.response.status === 404) {
            setNotFound(true)
          }
        }
      })

    if (response) {
      setNotFound(false)
      setProducts(response.items)
      setTotalPages(response.totalPages)
    }
  }

  const getListProduct2 = async (objectSearch) => {
    console.log(objectToQueryString(objectSearch))

    const response = await productServices
      .getProductList2(objectToQueryString(objectSearch))
      .catch((error) => {
        if (error.response) {
          if (error.response.status === 404) {
            setNotFound(true)
          }
        }
      })

    if (response) {
      console.log(response)
      setNotFound(false)
      setProducts(response.products)
      setTotalPages(response.totalPages)
    }
  }

  useEffect(() => {
    const fetch = async () => {
      const objectSearch = searchParamsToObject(
        new URLSearchParams(location.search)
      )

      if (!(objectSearch?.sortBy && objectSearch?.sortBy[0] === "Price")) {
        setSelected({})
      }

      console.log(objectSearch)

      if (objectSearch.sortBy) {
        if (objectSearch.sortBy[0] === "BestSeller") {
          setHightLight(1)
        } else if (objectSearch.sortBy[0] === "CreatedWhen") {
          setHightLight(2)
        } else {
          setHightLight(3)
        }
      }

      if (objectSearch.SearchText) {
        getListProduct2(objectSearch)
      } else {
        getListProduct(objectSearch)
      }

      setObjectSearch(objectSearch)
      handleCatalog(objectSearch)
    }

    fetch()
  }, [location.search])

  useEffect(() => {
    getAllProductTypes()
  }, [])

  const handleChange = (e, page) => {
    getListProduct({ ...objectSearch, pageNumber: page })
  }

  const getAllProductTypes = async () => {
    const response = await productTypeServices
      .getAllProductTypes()
      .catch((error) => {
        console.log("error", error)
      })

    if (response) {
      // console.log(response)
      setProductTypes(response.listItem)
    }
  }

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
                  {currentProductType && (
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
              {parents?.map((breadcrumb, index) => (
                <li key={index}>
                  <div className='flex items-center'>
                    <Link
                      to={"/user/catalog?ProductTypeIds=" + breadcrumb.id}
                      className='mx-2 text-sm font-medium text-green-900'
                    >
                      {breadcrumb.displayName}
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
              ))}
              {currentProductType && (
                <li>
                  <div className='flex items-center'>
                    <Link
                      to={"/"}
                      className='ml-2 text-sm font-medium text-green-900'
                    >
                      {currentProductType?.displayName}
                    </Link>
                  </div>
                </li>
              )}
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
                    to={"/user/catalog"}
                    className={`${currentProductType === null && "text-ct-green-400"}
                        my-1 font-medium hover:cursor-pointer hover:text-ct-green-400`}
                    onClick={() => {
                      setParents(null)
                      setCurrentProductType(null)
                    }}
                  >
                    Tất cả
                  </NavLink>

                  {currentProductType?.parentProductTypeId &&
                    parents.map((item) => (
                      <Link
                        key={item.id}
                        to={"/user/catalog?ProductTypeIds=" + item.id}
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
                      to={
                        "/user/catalog?ProductTypeIds=" + currentProductType.id
                      }
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
                        to={"/user/catalog?ProductTypeIds=" + item.id}
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
                        to={"/user/catalog?ProductTypeIds=" + item.id}
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
                    productTypes &&
                    productTypes.map((item, index) => {
                      if (item.level > 1) return null

                      return (
                        <Link
                          key={index}
                          to={"/user/catalog?ProductTypeIds=" + item.id}
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
                     hightLight === 1
                       ? ""
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
                     hightLight === 2
                       ? ""
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
                {notFound ? (
                  <div className='flex flex-col items-center justify-center gap-y-4'>
                    <div className='text-xl font-medium text-gray-400'>
                      Không có kết quả
                    </div>
                    <div className='logo'>
                      <svg
                        xmlns='http://www.w3.org/2000/svg'
                        viewBox='0 0 24 24'
                        fill='currentColor'
                        className='size-20 text-gray-300'
                      >
                        <path
                          fillRule='evenodd'
                          d='M10.5 3.75a6.75 6.75 0 1 0 0 13.5 6.75 6.75 0 0 0 0-13.5ZM2.25 10.5a8.25 8.25 0 1 1 14.59 5.28l4.69 4.69a.75.75 0 1 1-1.06 1.06l-4.69-4.69A8.25 8.25 0 0 1 2.25 10.5Z'
                          clipRule='evenodd'
                        />
                      </svg>
                    </div>
                  </div>
                ) : products === null ? (
                  <CircularProgress sx={{ color: "green" }} size={50} />
                ) : (
                  <div className='grid h-full w-full grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5'>
                    {products &&
                      products.map((item, index) => (
                        <ProductItem key={index} index={index} item={item} />
                      ))}
                  </div>
                )}
              </div>

              <div className='flex items-center gap-3 rounded-md bg-white p-2 shadow '>
                <Pagination
                  count={totalPages}
                  variant='outlined'
                  color='primary'
                  size='large'
                  onChange={handleChange}
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Catalog
