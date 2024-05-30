/* eslint-disable react/prop-types */
import { useRef } from "react"
import { Swiper, SwiperSlide } from "swiper/react"
import "swiper/css"

import ProductItem from "./User/ProductItem"

const MySwiper = ({ slides }) => {
  const swiperRef = useRef()

  const calculateSlidesPerView = () => {
    const screenWidth = window.innerWidth
    if (screenWidth >= 1280) {
      return 6
    } else if (screenWidth >= 1024) {
      return 5
    } else if (screenWidth >= 950) {
      return 5
    } else if (screenWidth >= 800) {
      return 4
    } else if (screenWidth >= 750) {
      return 3.7
    } else if (screenWidth >= 700) {
      return 3.5
    } else if (screenWidth >= 650) {
      return 3.2
    } else if (screenWidth >= 600) {
      return 3
    } else if (screenWidth >= 550) {
      return 2.7
    } else if (screenWidth >= 500) {
      return 2.2
    } else if (screenWidth >= 450) {
      return 2
    } else if (screenWidth >= 400) {
      return 1.8
    } else if (screenWidth >= 375) {
      return 1.7
    }
  }

  return (
    <Swiper
      spaceBetween={1}
      slidesPerView={calculateSlidesPerView()}
      onSwiper={(swiper) => {
        swiperRef.current = swiper
      }}
      className='group relative'
    >
      {slides.map((slide, index) => (
        <SwiperSlide key={index} className='flex max-w-fit'>
          <ProductItem index={index} item={slide} />
        </SwiperSlide>
      ))}

      <div
        className='absolute left-0 top-1/2 z-50 hidden h-10 w-10 -translate-y-1/2 items-center justify-center 
        rounded-full border-[1px] bg-white shadow group-hover:flex hover:cursor-pointer hover:bg-gray-100'
        onClick={() => swiperRef.current.slidePrev()}
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 24 24'
          fill='currentColor'
          className='h-6 w-6'
        >
          <path
            fillRule='evenodd'
            d='M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z'
            clipRule='evenodd'
          />
        </svg>
      </div>

      <div
        className='absolute right-0 top-1/2 z-50 hidden h-10 w-10 -translate-y-1/2 items-center justify-center 
        rounded-full border-[1px] bg-white shadow group-hover:flex hover:cursor-pointer hover:bg-gray-100'
        onClick={() => swiperRef.current.slideNext()}
      >
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
    </Swiper>
  )
}

export default MySwiper
