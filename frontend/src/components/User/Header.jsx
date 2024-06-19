/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { Fragment, useState, useRef, useEffect, memo } from "react"
import { Link, useNavigate } from "react-router-dom"
import {
  PopoverButton,
  Popover,
  PopoverPanel,
  Transition,
  MenuItem,
} from "@headlessui/react"
import { ChevronDownIcon } from "@heroicons/react/20/solid"

import CombinationLogo from "../../assets/images/combination-logo.svg"
import MyMenu from "../MyMenu"
import PopUp from "../PopUp"
import VND from "../vnd"
import useClickOutside from "../../hooks/useClickOutside"
import noImage from "../../assets/images/no-photo.png"

import useToast from "../../hooks/useToast"

// const inCart = [
//   {
//     name: "Vở Crabit Kẻ Ngang, Cornell, Ô Vuông 80 120 Trang, Vở Học Sinh Studygram",
//     image:
//       "https://down-vn.img.susercontent.com/file/vn-11134207-7qukw-lh9zpatio75e8e",
//     price: 46000,
//   },
//   {
//     name: "Giá đỡ điện thoại iPhone, máy tính bảng iPad chân xoay 360 độ tiện lợi bằng hợp kim nhôm Macbox",
//     image:
//       "https://down-vn.img.susercontent.com/file/vn-11134207-7qukw-lfyas92mk4fe4a",
//     price: 80000,
//   },
//   {
//     name: "Khăn Tắm Cotton Lapyarn Mollis BM8V 60x120cm dày dặn mềm mại thấm hút không đổ lông dùng cho gia đình spa",
//     image:
//       "https://down-vn.img.susercontent.com/file/vn-11134201-7r98o-lsc4tpiputm677",
//     price: 192765,
//   },
//   {
//     name: "Áo sơ mi nam cổ bẻ dài tay chất liệu LINEN cao cấp, phom dáng thoải mái thấm hút cực tốt - LUCIINON",
//     image:
//       "https://down-vn.img.susercontent.com/file/f8022403d8fce9db8acd0701507cbf32",
//     price: 319000,
//   },
//   {
//     name: "Mũ bảo hiểm 3/4 đầu Lót Màu Cao Cấp,Nút Đồng Chống Rĩ,Nón 3/4 tặng kèm lưỡi trai-Đạt Chuản CR",
//     image:
//       "https://down-vn.img.susercontent.com/file/4c54b802a801203697c5cac490035bd0",
//     price: 135000,
//   },
// ]

const searchHistory = [
  {
    text: "tuổi trẻ đáng giá bao nhiêu",
  },
  {
    text: "doraemon",
  },
  {
    text: "sách giáo khoa lớp 8",
  },
  {
    text: "bút chì kim",
  },
  {
    text: "quà lưu niệm",
  },
]

const notiItems = [
  {
    id: 3352,
    status: "Đang vận chuyển",
    price: 46000,
  },
  {
    id: 1203,
    status: "Đang gói hàng",
    price: 46000,
  },
  {
    id: 9635,
    status: "Đã giao hàng",
    price: 46000,
  },
]

import * as localStorage from "../../store/localStorage"

const Header = ({ category, isLogin, user, inCart, onKeyDown, onChange, value }) => {
  const navigate = useNavigate()
  const toast = useToast()

  const wrapperRef = useRef(null)

  const [catePop, setCatePop] = useState(category[0])

  // const [isFocusedInput, setIsFocusedInput] = useState(false)
  const [isOpenHistory, setIsOpenHistory] = useState(false)

  const handleCategory = (index) => {
    setCatePop(category[index])
  }

  const handleDeleteHistory = () => {}

  useClickOutside(wrapperRef, () => setIsOpenHistory(false))

  return (
    <header className='relative max-h-max min-h-14 w-full bg-white shadow'>
      <div className='mx-auto flex w-full max-w-screen-xl items-center justify-center gap-0 px-1 py-2 xs:gap-4 xs:p-4 lg:px-8'>
        <Link to={"/"} className='logo hidden md:block md:basis-1/5'>
          <img className='w-[150px]' src={CombinationLogo} alt='svg' />
        </Link>

        <div className='flex items-center xs:basis-4/6 xs:justify-around xs:gap-2 md:basis-3/5'>
          <div className='category'>
            <div className='p-1 hover:cursor-pointer hover:rounded hover:bg-gray-200'>
              <Popover>
                <PopoverButton className='flex select-none items-center outline-none'>
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={2}
                    stroke='currentColor'
                    className='h-8 w-8 cursor-pointer text-ct-green-400'
                  >
                    <path
                      strokeLinecap='round'
                      strokeLinejoin='round'
                      d='M3.75 6A2.25 2.25 0 0 1 6 3.75h2.25A2.25 2.25 0 0 1 10.5 6v2.25a2.25 2.25 0 0 1-2.25 2.25H6a2.25 2.25 0 0 1-2.25-2.25V6ZM3.75 15.75A2.25 2.25 0 0 1 6 13.5h2.25a2.25 2.25 0 0 1 2.25 2.25V18a2.25 2.25 0 0 1-2.25 2.25H6A2.25 2.25 0 0 1 3.75 18v-2.25ZM13.5 6a2.25 2.25 0 0 1 2.25-2.25H18A2.25 2.25 0 0 1 20.25 6v2.25A2.25 2.25 0 0 1 18 10.5h-2.25a2.25 2.25 0 0 1-2.25-2.25V6ZM13.5 15.75a2.25 2.25 0 0 1 2.25-2.25H18a2.25 2.25 0 0 1 2.25 2.25V18A2.25 2.25 0 0 1 18 20.25h-2.25A2.25 2.25 0 0 1 13.5 18v-2.25Z'
                    />
                  </svg>
                  <ChevronDownIcon
                    className='h-5 w-5 text-ct-green-400'
                    aria-hidden='true'
                  />
                </PopoverButton>

                <Transition
                  as={Fragment}
                  enter='transition ease-out duration-200'
                  enterFrom='opacity-0 translate-y-1'
                  enterTo='opacity-100 translate-y-0'
                  leave='transition ease-in duration-150'
                  leaveFrom='opacity-100 translate-y-0'
                  leaveTo='opacity-0 translate-y-1'
                >
                  <PopoverPanel
                    className='absolute left-1/2 top-full z-10 mt-2 flex max-h-fit w-full max-w-screen-xl 
                    -translate-x-1/2 hover:cursor-default'
                  >
                    <div
                      className='max-w-screen-xl flex-auto overflow-hidden rounded-3xl 
                      bg-white text-sm leading-6 shadow-lg ring-1 ring-gray-900/5'
                    >
                      <div className='grid grid-cols-4 divide-x-2 p-4'>
                        <div className='pr-4'>
                          <div className='title mb-4 inline-block pl-2 text-xl font-semibold text-ct-green-400'>
                            Danh mục sản phẩm
                          </div>

                          <div
                            className='list-cate col-span-1 flex h-full max-h-[450px] flex-col gap-2 
                            overflow-auto scrollbar-thin'
                          >
                            {category.map((item, index) => (
                              <div
                                key={index}
                                className='group flex justify-between rounded p-2 font-medium 
                                hover:cursor-pointer hover:bg-[#EDFFEB] hover:font-semibold'
                                onMouseOver={() => handleCategory(index)}
                              >
                                <div className='flex items-center justify-center'>
                                  <div className='icon mr-2'>{item.icon}</div>
                                  <div className='name'>{item.name}</div>
                                </div>

                                <div className='arrow'>
                                  <svg
                                    xmlns='http://www.w3.org/2000/svg'
                                    fill='none'
                                    viewBox='0 0 24 24'
                                    strokeWidth={1}
                                    stroke='currentColor'
                                    className='h-6 w-6 group-hover:stroke-2'
                                  >
                                    <path
                                      strokeLinecap='round'
                                      strokeLinejoin='round'
                                      d='m8.25 4.5 7.5 7.5-7.5 7.5'
                                    />
                                  </svg>
                                </div>
                              </div>
                            ))}
                          </div>
                        </div>

                        <div className='col-span-3 pl-4'>
                          <div className='header mb-4 flex items-center'>
                            <div className='icon'>{catePop.icon}</div>
                            <div className='title ml-2 text-xl font-semibold text-ct-black-300'>
                              {catePop.name}
                            </div>
                          </div>

                          <div
                            className='grid max-h-[450px] grid-cols-4 gap-x-5 gap-y-5 overflow-auto
                          scrollbar-thin'
                          >
                            {catePop.children.map((child, index) => (
                              <div key={index}>
                                <div className='title font-bold uppercase'>
                                  {child.name}
                                </div>

                                <div className='content flex flex-col'>
                                  {child.children.map(
                                    (smallChild, smallIndex) => (
                                      <Link
                                        key={smallIndex}
                                        className='line-clamp-1 transition-all
                                        duration-200 ease-in-out hover:cursor-pointer 
                                        hover:text-[#F95A51]'
                                      >
                                        {smallChild.name}
                                      </Link>
                                    )
                                  )}

                                  <Link
                                    className='more text-blue-500 hover:cursor-pointer 
                                    hover:font-semibold'
                                  >
                                    Xem tất cả
                                  </Link>
                                </div>
                              </div>
                            ))}
                          </div>
                        </div>
                      </div>
                    </div>
                  </PopoverPanel>
                </Transition>
              </Popover>
            </div>
          </div>

          <div className='search-bar relative min-w-0 xs:flex-1'>
            <div
              className='flex w-full items-center justify-center
              rounded-lg border-[1px] border-slate-300 px-2 py-2 shadow'
              onClick={() => setIsOpenHistory(true)}
            >
              <input
                className='placeholder:font-sm box-border w-36 px-1 font-medium
                leading-normal text-ct-black-300 outline-none placeholder:text-sm 2xs:w-full'
                placeholder='Tìm kiếm ...'
                onChange={onChange}
                onKeyDown={onKeyDown}
              />
              <div className='icon'>
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='h-6 w-6 text-ct-green-400 hover:cursor-pointer'
                >
                  <path
                    fillRule='evenodd'
                    d='M10.5 3.75a6.75 6.75 0 1 0 0 13.5 6.75 6.75 0 0 0 0-13.5ZM2.25 10.5a8.25 8.25 0 1 1 14.59 5.28l4.69 4.69a.75.75 0 1 1-1.06 1.06l-4.69-4.69A8.25 8.25 0 0 1 2.25 10.5Z'
                    clipRule='evenodd'
                  />
                </svg>
              </div>
              {/* <div
                ref={wrapperRef}
                className={`absolute left-0 top-full z-10 mt-2 w-full animate-fadeIn 
                rounded border-[1px] bg-white shadow-lg ${isOpenHistory ? "" : "hidden"}`}
              >
                <div className='flex flex-col justify-center text-sm'>
                  {searchHistory.map((item, index) => (
                    <Link
                      key={index}
                      className='flex items-center justify-between px-3 py-2 hover:cursor-pointer
                    hover:bg-gray-100'
                    >
                      <div className='text line-clamp-2'>{item.text}</div>
                      <div
                        className='icon text-gray-400 hover:cursor-pointer hover:text-red-400'
                        onClick={handleDeleteHistory}
                      >
                        <svg
                          xmlns='http://www.w3.org/2000/svg'
                          viewBox='0 0 24 24'
                          fill='currentColor'
                          className='h-6 w-6'
                        >
                          <path
                            fillRule='evenodd'
                            d='M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25Zm-1.72 6.97a.75.75 0 1 0-1.06 1.06L10.94 12l-1.72 1.72a.75.75 0 1 0 1.06 1.06L12 13.06l1.72 1.72a.75.75 0 1 0 1.06-1.06L13.06 12l1.72-1.72a.75.75 0 1 0-1.06-1.06L12 10.94l-1.72-1.72Z'
                            clipRule='evenodd'
                          />
                        </svg>
                      </div>
                    </Link>
                  ))}
                </div>
              </div> */}
            </div>
          </div>
        </div>

        <ul className='flex items-center justify-around xs:basis-2/6 md:basis-1/5'>
          <li className='notifications flex items-center justify-center'>
            <PopUp
              direction='right'
              button={
                <div
                  className='notifications relative rounded p-2 text-ct-green-400 hover:cursor-pointer
                hover:bg-gray-200'
                >
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={2}
                    stroke='currentColor'
                    className='h-6 w-6 '
                  >
                    <path
                      strokeLinecap='round'
                      strokeLinejoin='round'
                      d='M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0'
                    />
                  </svg>
                  <div className='absolute right-3 top-2 h-2 w-2 rounded bg-orange-500'></div>
                </div>
              }
            >
              <div
                className='w-screen max-w-sm flex-auto 
                flex-col justify-center divide-y-[0.5px] divide-gray-200 p-3 text-gray-700'
              >
                <div className='title pb-2 text-start text-sm font-medium uppercase'>
                  Thông báo mới
                </div>
                <div className='flex flex-col items-center justify-center py-2'>
                  {notiItems.map((item, index) => (
                    <div
                      key={index}
                      className='flex w-full items-center gap-2 rounded p-2 hover:cursor-pointer hover:bg-gray-100'
                    >
                      <svg
                        xmlns='http://www.w3.org/2000/svg'
                        viewBox='0 0 24 24'
                        fill='currentColor'
                        className='size-10 text-green-700'
                      >
                        <path d='M3.375 3C2.339 3 1.5 3.84 1.5 4.875v.75c0 1.036.84 1.875 1.875 1.875h17.25c1.035 0 1.875-.84 1.875-1.875v-.75C22.5 3.839 21.66 3 20.625 3H3.375Z' />
                        <path
                          fillRule='evenodd'
                          d='m3.087 9 .54 9.176A3 3 0 0 0 6.62 21h10.757a3 3 0 0 0 2.995-2.824L20.913 9H3.087Zm6.163 3.75A.75.75 0 0 1 10 12h4a.75.75 0 0 1 0 1.5h-4a.75.75 0 0 1-.75-.75Z'
                          clipRule='evenodd'
                        />
                      </svg>

                      <div className='flex flex-1 flex-col'>
                        <div className='flex gap-1'>
                          <div className='text-sm font-medium leading-6'>
                            Đơn hàng
                          </div>

                          <div className='text-sm font-bold leading-6 text-ct-green-400'>
                            {item.id}
                          </div>
                        </div>

                        <div className='line-clamp-1 text-sm leading-6'>
                          {item.status}
                        </div>
                      </div>

                      <VND
                        className={`font-medium text-ct-green-400`}
                        number={item.price}
                      />
                    </div>
                  ))}
                </div>
                <div className='flex items-center justify-end pt-2'>
                  <div
                    className='rounded bg-ct-green-400 px-3 py-1 text-sm text-white hover:cursor-pointer
                    hover:bg-green-600'
                  >
                    Xem thông báo
                  </div>
                </div>
              </div>
            </PopUp>
          </li>

          <li className='cart flex items-center justify-center'>
            <PopUp
              direction='right'
              button={
                <div className='cart relative rounded p-2 text-ct-green-400 hover:bg-gray-200'>
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={2}
                    stroke='currentColor'
                    className='h-6 w-6'
                  >
                    <path
                      strokeLinecap='round'
                      strokeLinejoin='round'
                      d='M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z'
                    />
                  </svg>
                  <div className='absolute left-6 top-0 z-20 rounded-full bg-orange-500 px-[5px] py-[2px] text-xs text-white'>
                    18
                  </div>
                </div>
              }
            >
              <div
                className='w-screen max-w-md flex-auto 
                flex-col justify-center divide-y-[0.5px] divide-gray-200 p-3 text-gray-700'
              >
                <div className='title pb-2 text-start text-sm font-medium uppercase'>
                  Sản phẩm mới thêm
                </div>
                <div className='flex flex-col items-center justify-center py-2'>
                  {inCart.map((item, index) => {
                    if (index > 5) return

                    return (
                      <div
                        key={index}
                        className='flex w-full items-center gap-2 rounded p-2 hover:cursor-pointer hover:bg-gray-100'
                      >
                        <img
                          className='h-14 w-14'
                          alt='img'
                          src={item.imageUrl}
                        />
                        <div className='line-clamp-2 flex-1 text-sm leading-6'>
                          {item.productName}
                        </div>
                        <VND
                          className={`font-medium text-ct-green-400`}
                          number={item.unitPrice}
                        />
                      </div>
                    )
                  })}
                </div>
                <div className='flex items-center justify-end pt-2'>
                  {/* <div className='text-sm'>13 sản phẩm trong giỏ</div> */}
                  <Link
                    to={"/user/cart"}
                    className='rounded bg-ct-green-400 px-3 py-1 text-sm text-white hover:cursor-pointer
                    hover:bg-green-600'
                  >
                    Xem giỏ hàng
                  </Link>
                </div>
              </div>
            </PopUp>
          </li>

          <li className='account flex items-center justify-center'>
            <MyMenu
              button={
                <div className='account rounded p-2 text-ct-green-400 hover:bg-gray-200'>
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={2}
                    stroke='currentColor'
                    className='h-6 w-6'
                  >
                    <path
                      strokeLinecap='round'
                      strokeLinejoin='round'
                      d='M15.75 6a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0ZM4.501 20.118a7.5 7.5 0 0 1 14.998 0A17.933 17.933 0 0 1 12 21.75c-2.676 0-5.216-.584-7.499-1.632Z'
                    />
                  </svg>
                </div>
              }
            >
              {isLogin || (
                <div className='py-1'>
                  <MenuItem>
                    <Link
                      to={"/user/login"}
                      className='group flex items-center justify-start gap-3 px-4
                      py-2 text-sm hover:bg-gray-100'
                    >
                      <div className='icon text-gray-400 group-hover:text-ct-green-300'>
                        <svg
                          xmlns='http://www.w3.org/2000/svg'
                          viewBox='0 0 24 24'
                          fill='currentColor'
                          className='h-6 w-6 '
                        >
                          <path
                            fillRule='evenodd'
                            d='M7.5 3.75A1.5 1.5 0 0 0 6 5.25v13.5a1.5 1.5 0 0 0 1.5 1.5h6a1.5 1.5 0 0 0 1.5-1.5V15a.75.75 0 0 1 1.5 0v3.75a3 3 0 0 1-3 3h-6a3 3 0 0 1-3-3V5.25a3 3 0 0 1 3-3h6a3 3 0 0 1 3 3V9A.75.75 0 0 1 15 9V5.25a1.5 1.5 0 0 0-1.5-1.5h-6Zm5.03 4.72a.75.75 0 0 1 0 1.06l-1.72 1.72h10.94a.75.75 0 0 1 0 1.5H10.81l1.72 1.72a.75.75 0 1 1-1.06 1.06l-3-3a.75.75 0 0 1 0-1.06l3-3a.75.75 0 0 1 1.06 0Z'
                            clipRule='evenodd'
                          />
                        </svg>
                      </div>
                      <div className='title'>Đăng nhập</div>
                    </Link>
                  </MenuItem>
                  <MenuItem>
                    <Link
                      to={"/user/sign-up"}
                      className='group flex items-center justify-start gap-3 px-4
                        py-2 text-sm hover:bg-gray-100'
                    >
                      <div className='icon text-gray-400 group-hover:text-ct-green-300'>
                        <svg
                          xmlns='http://www.w3.org/2000/svg'
                          viewBox='0 0 24 24'
                          fill='currentColor'
                          className='h-6 w-6 '
                        >
                          <path d='M11.47 1.72a.75.75 0 0 1 1.06 0l3 3a.75.75 0 0 1-1.06 1.06l-1.72-1.72V7.5h-1.5V4.06L9.53 5.78a.75.75 0 0 1-1.06-1.06l3-3ZM11.25 7.5V15a.75.75 0 0 0 1.5 0V7.5h3.75a3 3 0 0 1 3 3v9a3 3 0 0 1-3 3h-9a3 3 0 0 1-3-3v-9a3 3 0 0 1 3-3h3.75Z' />
                        </svg>
                      </div>
                      <div className='title'>Đăng ký</div>
                    </Link>
                  </MenuItem>
                </div>
              )}

              {isLogin && (
                <div className='py-1'>
                  <MenuItem>
                    <Link
                      to={"/user/account/profile"}
                      className='group flex items-center justify-start gap-3 px-4
                          py-2 text-sm font-semibold hover:bg-gray-100'
                    >
                      <div
                        className='icon object-none object-center text-gray-400
                          group-hover:text-gray-600'
                      >
                        <img
                          alt='avt'
                          src={user?.avatar || noImage}
                          className='h-10 w-10 rounded-full object-cover'
                        />
                      </div>
                      <div className='title'>{user?.fullName}</div>
                    </Link>
                  </MenuItem>
                </div>
              )}

              {isLogin && (
                <div className='py-1'>
                  <MenuItem>
                    <Link
                      to={"user/orders"}
                      className='group flex items-center justify-start gap-3 px-4
                      py-2 text-sm hover:bg-gray-100'
                    >
                      <div className='icon text-gray-400 group-hover:text-ct-green-300'>
                        <svg
                          xmlns='http://www.w3.org/2000/svg'
                          viewBox='0 0 24 24'
                          fill='currentColor'
                          className='h-6 w-6'
                        >
                          <path
                            fillRule='evenodd'
                            d='M6.912 3a3 3 0 0 0-2.868 2.118l-2.411 7.838a3 3 0 0 0-.133.882V18a3 3 0 0 0 3 3h15a3 3 0 0 0 3-3v-4.162c0-.299-.045-.596-.133-.882l-2.412-7.838A3 3 0 0 0 17.088 3H6.912Zm13.823 9.75-2.213-7.191A1.5 1.5 0 0 0 17.088 4.5H6.912a1.5 1.5 0 0 0-1.434 1.059L3.265 12.75H6.11a3 3 0 0 1 2.684 1.658l.256.513a1.5 1.5 0 0 0 1.342.829h3.218a1.5 1.5 0 0 0 1.342-.83l.256-.512a3 3 0 0 1 2.684-1.658h2.844Z'
                            clipRule='evenodd'
                          />
                        </svg>
                      </div>
                      <div className='title'>Đơn hàng</div>
                    </Link>
                  </MenuItem>
                </div>
              )}

              {isLogin && (
                <div className='py-1'>
                  <MenuItem>
                    <div
                      className='group flex items-center justify-start gap-3 px-4
                      py-2 text-sm hover:cursor-pointer hover:bg-gray-100'
                      onClick={() => {
                        localStorage.setIsCustomerLogin(false)
                        localStorage.setCustomerAccessToken(null)
                        localStorage.setCustomerRefreshToken(null)
                        toast("success", "Đăng xuất thành công")

                        navigate("/")
                      }}
                    >
                      <div className='icon text-gray-400 group-hover:text-ct-green-300'>
                        <svg
                          xmlns='http://www.w3.org/2000/svg'
                          viewBox='0 0 24 24'
                          fill='currentColor'
                          className='h-6 w-6'
                        >
                          <path
                            fillRule='evenodd'
                            d='M16.5 3.75a1.5 1.5 0 0 1 1.5 1.5v13.5a1.5 1.5 0 0 1-1.5 1.5h-6a1.5 1.5 0 0 1-1.5-1.5V15a.75.75 0 0 0-1.5 0v3.75a3 3 0 0 0 3 3h6a3 3 0 0 0 3-3V5.25a3 3 0 0 0-3-3h-6a3 3 0 0 0-3 3V9A.75.75 0 1 0 9 9V5.25a1.5 1.5 0 0 1 1.5-1.5h6ZM5.78 8.47a.75.75 0 0 0-1.06 0l-3 3a.75.75 0 0 0 0 1.06l3 3a.75.75 0 0 0 1.06-1.06l-1.72-1.72H15a.75.75 0 0 0 0-1.5H4.06l1.72-1.72a.75.75 0 0 0 0-1.06Z'
                            clipRule='evenodd'
                          />
                        </svg>
                      </div>
                      <div className='title'>Đăng xuất</div>
                    </div>
                  </MenuItem>
                </div>
              )}
            </MyMenu>
          </li>
        </ul>
      </div>
    </header>
  )
}

export default Header
