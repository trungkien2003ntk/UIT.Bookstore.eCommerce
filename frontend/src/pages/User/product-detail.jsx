import { useState, useEffect } from "react"
import { useParams } from "react-router-dom"
import { Divider, Pagination, CircularProgress } from "@mui/material"

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

import * as productServices from "../../apiServices/productServices"
import * as ratingServices from "../../apiServices/ratingServices"
import * as cartServices from "../../apiServices/cartServices"
import useToast from "../../hooks/useToast"
import Modal from "../../components/Modal"

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

const ProductDetail = () => {
  const { id } = useParams()
  const toast = useToast()
  const [obj, setObj] = useState(null)
  const [ratings, setRatings] = useState(null)

  // MODAL
  const [openModal, setOpenModal] = useState(false)
  const handleModal = () => {
    setOpenModal((prev) => !prev)
  }

  // IMAGE
  const [image, setImage] = useState()
  const [listImage, setListImage] = useState([])

  // OPTIONS
  const [myOptions, setMyOptions] = useState()
  const [selectedOptions, setSelectedOptions] = useState([])
  const handleSelectedOptions = (newOption) => {
    setSelectedOptions((prev) => {
      if (prev.some((option) => option.title === newOption.title)) {
        return prev.map((option) =>
          option.title === newOption.title ? newOption : option
        )
      }

      return [...prev, newOption]
    })

    console.log("newOption", newOption)

    console.log(money)

    setMoney({
      price: newOption.selected.recommendedRetailPrice,
      discountRate: newOption.selected.basicDiscountRate,
      actualPrice: newOption.selected.unitPrice,
    })

    setRestQuantity(newOption.selected.quantity)
    setImage(newOption.selected.image)
  }

  const [money, setMoney] = useState({
    price: 0,
    discountRate: 0,
    actualPrice: 0,
  })

  const [restQuantity, setRestQuantity] = useState(0)

  const [quantity, setQuantity] = useState(1)

  // DESCRIPTION
  const [collapsedDesc, setCollapsedDesc] = useState(true)

  // RATINGS PAGINATION
  const handleChange = (e, pageNumber) => {
    getProductRatings(pageNumber)
  }

  const getProduct = async () => {
    const response = await productServices.getProduct(id).catch((error) => {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log("Error", error.message)
      }
      console.log(error.config)

      toast("error", "Có lỗi xảy ra!")
    })

    if (response) {
      setObj(response)
      setMoney({
        price: response.minUnitPrice,
        discountRate: 0,
        actualPrice: response.minRecommendedRetailPrice,
      })
      setMyOptions(skusToOptions(response.skus))

      let listImage = response.largeImageUrls
      response.skus.forEach((item) => {
        if (item.largeImageUrl) {
          listImage.push(item.largeImageUrl)
        }
      })
      setListImage(listImage)

      setImage(
        response.largeImageUrls.concat(
          response.skus.map((item) => item.largeImageUrl)
        )[0]
      )

      console.log(response)
    }
  }

  const getProductLike = async () => {}

  const getProductRatings = async (pageNumber) => {
    const response = await ratingServices
      .getProductRatings(id, {
        Statuses: "Approved",
        pageNumber: pageNumber,
        pageSize: 5,
      })
      .catch((error) => {
        if (error.response) {
          if (error.response.status === 404) {
            setRatings({
              averageRating: 0,
              totalApprovedRating: 0,
              totalRating: 0,
              total5StarRating: 0,
              total4StarRating: 0,
              total3StarRating: 0,
              total2StarRating: 0,
              total1StarRating: 0,
              totalRatingWithComment: 0,
              totalRatingWithImage: 0,
              ratings: {
                items: [],
                totalCount: 0,
                pageSize: 5,
                pageNumber: 1,
                totalPages: 1,
              },
            })
          }
        }
      })

    if (response) {
      console.log(response)
      setRatings(response)
    }
  }

  useEffect(() => {
    getProduct()
    getProductRatings(1)
  }, [])

  const addProductToCart = async (obj) => {
    const response = await cartServices.addShoppingCart(obj).catch((error) => {
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
      toast("success", "Thêm vào giỏ hàng thành công!")
    }
  }

  const handleAddCart = () => {
    if (myOptions.length > 0) {
      if (selectedOptions.length > 0) {
        addProductToCart({
          skuId: selectedOptions[0].selected.id,
          quantity: quantity,
        })
      } else {
        toast("error", "Vui lòng chọn phân loại sản phẩm!")
      }
    } else {
      addProductToCart({ skuId: obj.skus[0].id, quantity: quantity })
    }
  }

  const [selectedStar, setSelectedStar] = useState(0)

  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
        gap-5 p-4 lg:px-8'
      >
        {obj === null ? (
          <div className='flex w-full items-center justify-center rounded bg-white p-5 shadow'>
            <CircularProgress sx={{ color: "green" }} />
          </div>
        ) : (
          <div className='flex w-full justify-center gap-10 rounded bg-white p-5 shadow'>
            <div className='w-[40%] '>
              <img
                alt='img'
                className='h-[500px] w-full object-contain'
                src={image}
              />
              <Slider
                className='mt-2 object-cover'
                setHoverImage={(img) => setImage(img)}
                slides={listImage}
              />
            </div>

            <div className='flex h-full w-[60%] flex-col'>
              <div className='text-2xl font-semibold'>{obj.name}</div>

              <div className='mt-3 flex items-center gap-4'>
                {ratings && (
                  <div className='flex items-center gap-1'>
                    <div className='text-sm font-medium text-yellow-500'>
                      {ratings.averageRating.toFixed(1)}
                    </div>
                    <FiveStar
                      rating={ratings.averageRating}
                      classNameForSize='h-4 w-4'
                    />
                  </div>
                )}

                <div className='h-5 w-[1px] bg-gray-300'></div>

                {ratings && (
                  <div className='flex items-center gap-1'>
                    <div className='text-sm font-medium '>
                      {ratings?.totalApprovedRating} Đánh giá
                    </div>
                  </div>
                )}

                <div className='h-5 w-[1px] bg-gray-300'></div>

                <div className='flex items-center gap-1'>
                  <div className='text-sm font-medium '>823 Đã bán</div>
                </div>
              </div>

              <div className='mt-14 flex items-center gap-5'>
                <VND
                  className={
                    "mt-2 text-xl font-medium text-gray-500 line-through"
                  }
                  number={money.price}
                ></VND>
                <VND
                  className={"text-3xl font-bold text-ct-green-400"}
                  number={money.actualPrice}
                />
                {money.discountRate > 0 && (
                  <div className='rounded bg-red-100 px-2 py-1 font-medium text-red-600'>
                    {money.discountRate}%
                  </div>
                )}
              </div>

              <div className='mt-14 flex w-full items-center justify-center'>
                <div className='w-[30%] text-gray-500'>Chính sách đổi trả</div>
                <div className='flex w-[70%] gap-2'>
                  <div>Đổi trả sản phẩm trong 7 ngày</div>
                  <div
                    className='font-medium text-ct-green-400 hover:cursor-pointer 
                    hover:text-ct-green-300'
                    onClick={handleModal}
                  >
                    Xem thêm
                  </div>
                </div>

                <Modal
                  open={openModal}
                  setOpen={handleModal}
                  title={"CHÍNH SÁCH ĐỔI TRẢ "}
                  contentComp={
                    <div>
                      Đổi trả sản phẩm trong 7 ngày kể từ ngày nhận hàng
                    </div>
                  }
                  isCancelButton
                ></Modal>
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
                  <div className='flex items-center gap-10'>
                    <ButtonNumber
                      quantity={quantity}
                      setQuantity={setQuantity}
                      min={1}
                      max={restQuantity}
                    />
                    {restQuantity > 0 && (
                      <div className='text-gray-500'>
                        {restQuantity} sản phẩm
                      </div>
                    )}
                  </div>
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
                  onClick={handleAddCart}
                >
                  Thêm vào giỏ hàng
                </Button>

                <Button className={`w-72 !py-4`}>Mua ngay</Button>
              </div>
            </div>
          </div>
        )}

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

        {/* <BlockWrapper
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
        </BlockWrapper> */}

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
            {obj === null ? (
              <CircularProgress />
            ) : (
              <div className='info flex flex-col gap-3'>
                <div className='flex w-full justify-center'>
                  <div className='w-[25%]'>Tên sản phẩm</div>
                  <div className='w-[75%]'>{obj?.name}</div>
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
            )}
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
          {obj === null ? (
            <CircularProgress />
          ) : (
            <div className='w-full'>
              <textarea
                className={`${collapsedDesc ? "max-h-[200px]" : "max-h-fit"}  
                w-full resize-none overflow-hidden outline-none transition-all ease-in-out`}
                readOnly
                value={obj.description}
                rows={15}
              ></textarea>

              <Button
                className='mt-5'
                onClick={() => setCollapsedDesc(!collapsedDesc)}
              >
                {collapsedDesc ? "Xem thêm" : "Thu gọn"}
              </Button>
            </div>
          )}
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
          {ratings === null ? (
            <CircularProgress />
          ) : (
            <div className='w-full'>
              <div
                className='flex items-center gap-5
              rounded border-[1px] border-yellow-500 p-3'
              >
                <div className='flex w-fit flex-col items-center gap-2'>
                  {ratings && (
                    <div className='text-2xl font-medium text-yellow-500'>
                      {ratings?.averageRating?.toFixed(1)}/5
                    </div>
                  )}

                  <FiveStar
                    rating={ratings?.averageRating && ratings?.averageRating}
                    classNameForSize={`w-8 h-8`}
                  />
                </div>

                <div className='flex items-center justify-center gap-3'>
                  <div
                    className={`
                    ${selectedStar === 0 && "border-yellow-500 text-yellow-500"} 
                    rounded border-[1px] px-5 py-1 hover:cursor-pointer
                  hover:border-yellow-500 hover:text-yellow-500`}
                    onClick={() => setSelectedStar(0)}
                  >
                    Tất cả {"(" + ratings?.totalApprovedRating + ")"}
                  </div>
                  <div
                    className={`
                    ${selectedStar === 5 && "border-yellow-500 text-yellow-500"} 
                    rounded border-[1px] px-5 py-1 hover:cursor-pointer
                  hover:border-yellow-500 hover:text-yellow-500`}
                    onClick={() => setSelectedStar(5)}
                  >
                    5 Sao {"(" + ratings?.total5StarRating + ")"}
                  </div>
                  <div
                    className={`
                    ${selectedStar === 4 && "border-yellow-500 text-yellow-500"} 
                    rounded border-[1px] px-5 py-1 hover:cursor-pointer
                  hover:border-yellow-500 hover:text-yellow-500`}
                    onClick={() => setSelectedStar(4)}
                  >
                    4 Sao {"(" + ratings?.total4StarRating + ")"}
                  </div>
                  <div
                    className={`
                    ${selectedStar === 3 && "border-yellow-500 text-yellow-500"} 
                    rounded border-[1px] px-5 py-1 hover:cursor-pointer
                  hover:border-yellow-500 hover:text-yellow-500`}
                    onClick={() => setSelectedStar(3)}
                  >
                    3 Sao {"(" + ratings?.total3StarRating + ")"}
                  </div>
                  <div
                    className={`
                    ${selectedStar === 2 && "border-yellow-500 text-yellow-500"} 
                    rounded border-[1px] px-5 py-1 hover:cursor-pointer
                  hover:border-yellow-500 hover:text-yellow-500`}
                    onClick={() => setSelectedStar(2)}
                  >
                    2 Sao {"(" + ratings?.total2StarRating + ")"}
                  </div>
                  <div
                    className={`
                    ${selectedStar === 1 && "border-yellow-500 text-yellow-500"} 
                    rounded border-[1px] px-5 py-1 hover:cursor-pointer
                    hover:border-yellow-500 hover:text-yellow-500`}
                    onClick={() => setSelectedStar(1)}
                  >
                    1 Sao {"(" + ratings?.total1StarRating + ")"}
                  </div>
                </div>
              </div>

              <div className='mt-5 flex flex-col gap-5 p-5'>
                {ratings?.ratings?.items.map((item, index) => (
                  <div key={index} className='flex flex-col gap-5'>
                    <div className='flex gap-x-3'>
                      <img
                        className='h-10 w-10 rounded-full'
                        alt='img'
                        src='https://down-vn.img.susercontent.com/file/sg-11134004-7qvef-lfjhk3o78hho40_tn'
                      />

                      <div className='flex flex-col gap-1'>
                        <div className='text-sm font-medium'>
                          {item.userName}
                        </div>
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
                    count={ratings.ratings.totalPages}
                    variant='outlined'
                    color='primary'
                    size='large'
                    onChange={handleChange}
                  />
                </div>
              </div>
            </div>
          )}
        </BlockWrapper>
      </div>
    </div>
  )
}

export default ProductDetail
