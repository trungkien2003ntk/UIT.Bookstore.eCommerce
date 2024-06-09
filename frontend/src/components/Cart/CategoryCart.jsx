/* eslint-disable react/display-name */
/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import { useState, memo } from "react"
import PopUp from "../PopUp"
import Options from "../Options"

const CategoryCart = memo(({ item }) => {
  const [myOptions, setMyOptions] = useState(
    item.productOptions.map((section) => ({
      title: section.name,
      items: section.values.map((value, index) => ({
        name: value,
        image: section.images[index],
      })),
    }))
  )

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
  }

  return (
    <PopUp
      direction='right'
      button={
        <div
          className='flex min-w-44 max-w-44 flex-col items-start justify-start 
          rounded bg-slate-100 p-3 hover:cursor-pointer'
        >
          <div className='flex items-center gap-1 text-sm'>
            <span className='font-medium'>Phân loại hàng</span>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-4'
            >
              <path
                fillRule='evenodd'
                d='M12.53 16.28a.75.75 0 0 1-1.06 0l-7.5-7.5a.75.75 0 0 1 1.06-1.06L12 14.69l6.97-6.97a.75.75 0 1 1 1.06 1.06l-7.5 7.5Z'
                clipRule='evenodd'
              />
            </svg>
          </div>
          <div className='text-start text-sm'>{item?.skuName}</div>
        </div>
      }
    >
      <div
        className='w-screen max-w-sm flex-auto 
        flex-col justify-center divide-y-[0.5px] divide-gray-200 p-3 text-gray-700'
      >
        <div className='title pb-2 text-start text-sm font-medium uppercase'>
          Phân loại hàng
        </div>

        {myOptions.map((option, index) => (
          <Options
            key={index}
            className={"py-5"}
            option={option}
            onOptionSelect={handleSelectedOptions}
          />
        ))}

        <div className='flex items-center justify-end pt-2'>
          <div
            className='rounded bg-ct-green-400 px-3 py-1 text-sm text-white hover:cursor-pointer
            hover:bg-green-600'
          >
            Xác nhận
          </div>
        </div>
      </div>
    </PopUp>
  )
})

export default CategoryCart
