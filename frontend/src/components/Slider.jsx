/* eslint-disable react/prop-types */
import { Swiper, SwiperSlide } from "swiper/react"
import ButtonForSlider from "./ButtonForSlider"
import "swiper/css"

import PropTypes from "prop-types"

const Slider = ({ slides, className, setHoverImage }) => {
  return (
    <Swiper
      className={`${className} relative select-none`}
      spaceBetween={1}
      slidesPerView={5}
    >
      {slides?.map((slide, index) => (
        <SwiperSlide key={index} onMouseOver={() => setHoverImage(slide)}>
          <img
            className='w-24 border-[1px] border-transparent hover:cursor-pointer hover:border-ct-green-400'
            alt='img'
            src={slide}
          />
        </SwiperSlide>
      ))}

      <ButtonForSlider direction={"prev"} />
      <ButtonForSlider direction={"next"} />
    </Swiper>
  )
}

Slider.propTypes = {
  slides: PropTypes.array.isRequired,
}

export default Slider
