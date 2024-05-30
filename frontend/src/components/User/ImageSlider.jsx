/* eslint-disable react/prop-types */
import { useState, useEffect } from "react"

const ImageSlider = ({ slides }) => {
  const [currentItem, setCurrentItem] = useState({ item: slides[0], index: 0 })

  const handleChangeItem = (direction) => {
    if (direction == "next") {
      if (currentItem.index == slides.length - 1) {
        setCurrentItem(() => ({
          item: slides[0],
          index: 0,
        }))
      } else {
        setCurrentItem((prev) => ({
          item: slides[prev.index + 1],
          index: prev.index + 1,
        }))
      }
    } else if (currentItem.index == 0) {
      setCurrentItem(() => ({
        item: slides[slides.length - 1],
        index: slides.length - 1,
      }))
    } else {
      setCurrentItem((prev) => ({
        item: slides[prev.index - 1],
        index: prev.index - 1,
      }))
    }
  }

  useEffect(() => {
    const intervalImage = setInterval(() => {
      handleChangeItem("next")
    }, 3000)

    return () => clearInterval(intervalImage)
  }, [currentItem])

  return (
    <div className='slider group relative h-[50vh] max-h-[400px] min-h-52 w-full min-w-80 transition-all duration-1000 ease-in-out'>
      <div
        style={{ backgroundImage: `url(${currentItem.item})` }}
        className='h-full w-full rounded-2xl bg-cover bg-center bg-no-repeat transition-all duration-500
        ease-in-out'
      ></div>

      <div
        className='absolute left-2 top-1/2 hidden -translate-y-1/2 animate-fadeIn rounded-full 
        bg-white p-2 transition-all duration-200 ease-in-out group-hover:inline-block 
        hover:cursor-pointer hover:bg-gray-100'
        onClick={() => handleChangeItem("prev")}
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 24 24'
          fill='currentColor'
          className='h-6 w-6'
        >
          <path
            fillRule='evenodd'
            d='M11.03 3.97a.75.75 0 0 1 0 1.06l-6.22 6.22H21a.75.75 0 0 1 0 1.5H4.81l6.22 6.22a.75.75 0 1 1-1.06 1.06l-7.5-7.5a.75.75 0 0 1 0-1.06l7.5-7.5a.75.75 0 0 1 1.06 0Z'
            clipRule='evenodd'
          />
        </svg>
      </div>

      <div
        className='absolute right-2 top-1/2 hidden -translate-y-1/2 animate-fadeIn rounded-full bg-white p-2 
        transition-all duration-200 ease-in-out group-hover:inline-block hover:cursor-pointer
        hover:bg-gray-100'
        onClick={() => handleChangeItem("next")}
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 24 24'
          fill='currentColor'
          className='h-6 w-6'
        >
          <path
            fillRule='evenodd'
            d='M12.97 3.97a.75.75 0 0 1 1.06 0l7.5 7.5a.75.75 0 0 1 0 1.06l-7.5 7.5a.75.75 0 1 1-1.06-1.06l6.22-6.22H3a.75.75 0 0 1 0-1.5h16.19l-6.22-6.22a.75.75 0 0 1 0-1.06Z'
            clipRule='evenodd'
          />
        </svg>
      </div>

      <div className='absolute bottom-0 left-1/2 mb-2 flex -translate-x-1/2 gap-2'>
        {slides.map((item, index) => (
          <div
            key={index}
            className={`h-2 w-2 -translate-x-1/2 rounded-full  
               ${currentItem.index == index ? "bg-ct-green-400" : "bg-white"}`}
          ></div>
        ))}
      </div>
    </div>
  )
}

export default ImageSlider
