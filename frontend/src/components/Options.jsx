/* eslint-disable react/prop-types */
import { useState } from "react"
const Options = ({ className, option, onOptionSelect }) => {
  const [selectedIndex, setSelectedIndex] = useState(null)

  const handleSelect = (item, index) => {
    onOptionSelect({
      title: option?.title,
      selected: item,
    })
    setSelectedIndex(index)
  }

  return (
    <div className={`${className} flex w-full items-start justify-center`}>
      <div className='w-[30%] text-gray-500'>{option.title}</div>
      <div className='flex w-[70%] flex-wrap gap-2'>
        {option?.items?.map((item, index) => (
          <div
            key={index}
            className={`${selectedIndex === index && "border-[2px] border-ct-green-400"} 
            relative flex items-center gap-2 rounded border-[1px]
            px-3 py-2 text-gray-600 hover:cursor-pointer hover:border-ct-green-400`}
            onClick={() => handleSelect(item, index)}
          >
            {item.image && <img className='w-6' alt='img' src={item.image} />}
            <div className=''>{item.name}</div>

            {selectedIndex === index && (
              <div
                className='absolute bottom-0 right-0 h-0 w-0 
                border-b-[12px] border-l-[12px] border-b-ct-green-400 border-l-transparent'
              ></div>
            )}
          </div>
        ))}
      </div>
    </div>
  )
}

export default Options
