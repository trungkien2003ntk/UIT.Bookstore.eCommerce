/* eslint-disable react/prop-types */
/* eslint-disable react-refresh/only-export-components */
import { memo } from "react"
import MyDataGrid from "./MyDataGrid"
import Filter from "./Filter"

const DataTable = ({ columns, rows, height, filterChildren, ...rest }) => {
  return (
    <div className='rounded bg-white p-3 shadow'>
      <div className='flex items-center gap-2'>
        <div
          className='flex w-full items-center justify-center
            rounded-lg border-[1px] border-slate-300 px-2 py-2 shadow'
        >
          <input
            className='placeholder:font-sm box-border w-36 px-1 font-medium
                    leading-normal text-ct-black-300 outline-none placeholder:text-sm 2xs:w-full'
            placeholder='Tìm kiếm ...'
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
        </div>

        <Filter>{filterChildren}</Filter>
      </div>

      <div className='mt-5'>
        <MyDataGrid
          {...rest}
          height={height}
          columns={columns}
          rows={rows}
        ></MyDataGrid>
      </div>
    </div>
  )
}

export default memo(DataTable)
