/* eslint-disable react/prop-types */
import { useState } from "react"
import { NavLink } from "react-router-dom"
import { Collapse } from "@mui/material"

const ButtonCollapse = ({ icon, title, items }) => {
  const [isExpand, setIsExpand] = useState(true)

  const handleExpand = () => {
    setIsExpand((prev) => !prev)
  }

  return (
    <div className='select-none '>
      <div
        className='group flex items-center justify-between gap-2 rounded 
        border-[1px] bg-white px-3 py-2 text-sm font-medium
        text-ct-black-300 hover:cursor-pointer hover:text-green-600'
        onClick={handleExpand}
      >
        <div className='flex items-center gap-2'>
          <div className=''>{icon}</div>
          <div className='title'>{title}</div>
        </div>

        <div className=''>
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
      </div>
      <Collapse in={isExpand}>
        <div className='mt-1 flex flex-col justify-center gap-1'>
          {items.map((item, index) => (
            <NavLink
              key={index}
              to={item.path}
              className={({ isActive }) => `${
                isActive
                  ? "border-ct-green-200 bg-green-50 text-ct-green-500"
                  : "bg-gray-50 text-gray-600"
              } 
                rounded border-[1px] px-3 py-2 text-sm 
                font-medium hover:text-ct-green-500`}
            >
              {item.name}
            </NavLink>
          ))}
        </div>
      </Collapse>
    </div>
  )
}

export default ButtonCollapse
