/* eslint-disable react/prop-types */
import { useSwiper } from "swiper/react"

const ButtonForSlider = ({ direction }) => {
  const swiper = useSwiper()

  const handleSlide = () => {
    if (direction === "next") {
      swiper.slideNext()
    } else {
      swiper.slidePrev()
    }
  }

  return (
    <div
      className={`${direction === "next" ? "right-0" : "left-0"} 
      } absolute top-0 z-20 flex h-full w-4
        items-center justify-center bg-black opacity-35 hover:cursor-pointer`}
      onClick={handleSlide}
    >
      <div className='text-white'>
        {direction === "prev" && (
          <svg
            xmlns='http://www.w3.org/2000/svg'
            viewBox='0 0 24 24'
            fill='currentColor'
            className='size-6'
          >
            <path
              fillRule='evenodd'
              d='M7.72 12.53a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 1 1 1.06 1.06L9.31 12l6.97 6.97a.75.75 0 1 1-1.06 1.06l-7.5-7.5Z'
              clipRule='evenodd'
            />
          </svg>
        )}
        {direction === "next" && (
          <svg
            xmlns='http://www.w3.org/2000/svg'
            viewBox='0 0 24 24'
            fill='currentColor'
            className='size-6'
          >
            <path
              fillRule='evenodd'
              d='M16.28 11.47a.75.75 0 0 1 0 1.06l-7.5 7.5a.75.75 0 0 1-1.06-1.06L14.69 12 7.72 5.03a.75.75 0 0 1 1.06-1.06l7.5 7.5Z'
              clipRule='evenodd'
            />
          </svg>
        )}
      </div>
    </div>
  )
}

export default ButtonForSlider
