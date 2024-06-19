/* eslint-disable react/prop-types */
import { Swiper, SwiperSlide } from "swiper/react"
import DiscountItem from "./DiscountItem"
import "swiper/css"

const DiscountSwiper = ({ slides }) => {
  return (
    <Swiper spaceBetween={6} slidesPerView={3.5} className='hover:cursor-grab'>
      {slides?.map((obj, index) => (
        <SwiperSlide key={index}>
          <DiscountItem obj={obj} />
        </SwiperSlide>
      ))}
    </Swiper>
  )
}

export default DiscountSwiper
