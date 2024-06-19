/* eslint-disable no-unused-vars */
import { useState } from "react"
import { Divider } from "@mui/material"
import VND from "../../components/vnd"
import Button from "../../components/Button"
import Dropdown from "../../components/Dropdown"

const status = [
  {
    text: "Đang giao dịch",
  },
  {
    text: "Ngừng giao dịch",
  },
]

const CustomerDetail = () => {
  const [selected, setSelected] = useState(status[0])

  return (
    <div className='flex flex-col gap-3 overflow-auto'>
      <div className='flex items-center justify-between rounded bg-white p-3 shadow'>
        <div className='flex items-center gap-3'>
          <div className='text-gray-600'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-10'
            >
              <path
                fillRule='evenodd'
                d='M7.502 6h7.128A3.375 3.375 0 0 1 18 9.375v9.375a3 3 0 0 0 3-3V6.108c0-1.505-1.125-2.811-2.664-2.94a48.972 48.972 0 0 0-.673-.05A3 3 0 0 0 15 1.5h-1.5a3 3 0 0 0-2.663 1.618c-.225.015-.45.032-.673.05C8.662 3.295 7.554 4.542 7.502 6ZM13.5 3A1.5 1.5 0 0 0 12 4.5h4.5A1.5 1.5 0 0 0 15 3h-1.5Z'
                clipRule='evenodd'
              />
              <path
                fillRule='evenodd'
                d='M3 9.375C3 8.339 3.84 7.5 4.875 7.5h9.75c1.036 0 1.875.84 1.875 1.875v11.25c0 1.035-.84 1.875-1.875 1.875h-9.75A1.875 1.875 0 0 1 3 20.625V9.375ZM6 12a.75.75 0 0 1 .75-.75h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H6.75a.75.75 0 0 1-.75-.75V12Zm2.25 0a.75.75 0 0 1 .75-.75h3.75a.75.75 0 0 1 0 1.5H9a.75.75 0 0 1-.75-.75ZM6 15a.75.75 0 0 1 .75-.75h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H6.75a.75.75 0 0 1-.75-.75V15Zm2.25 0a.75.75 0 0 1 .75-.75h3.75a.75.75 0 0 1 0 1.5H9a.75.75 0 0 1-.75-.75ZM6 18a.75.75 0 0 1 .75-.75h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H6.75a.75.75 0 0 1-.75-.75V18Zm2.25 0a.75.75 0 0 1 .75-.75h3.75a.75.75 0 0 1 0 1.5H9a.75.75 0 0 1-.75-.75Z'
                clipRule='evenodd'
              />
            </svg>
          </div>
          <div className='text-lg font-medium text-gray-500'>
            Chi tiết khách hàng
          </div>
        </div>

        <Dropdown
          selected={selected}
          setSelected={(value) => setSelected(value)}
          placeholder={"Trạng thái"}
          items={status}
        ></Dropdown>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-5 shadow'>
        <div className='flex items-center gap-2'>
          <div className='text-orange-500'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6'
            >
              <path
                fillRule='evenodd'
                d='M7.5 6a4.5 4.5 0 1 1 9 0 4.5 4.5 0 0 1-9 0ZM3.751 20.105a8.25 8.25 0 0 1 16.498 0 .75.75 0 0 1-.437.695A18.683 18.683 0 0 1 12 22.5c-2.786 0-5.433-.608-7.812-1.7a.75.75 0 0 1-.437-.695Z'
                clipRule='evenodd'
              />
            </svg>
          </div>
          <div className='text-lg font-medium text-ct-black-300'>
            Thông tin khách hàng
          </div>
        </div>

        <div className='flex flex-col justify-center gap-1'>
          <div className='flex items-center'>
            <div className='min-w-40'>Mã khách hàng:</div>
            <div className='font-medium text-blue-500 hover:cursor-pointer hover:font-semibold'>
              3350
            </div>
          </div>
          <div className='flex items-center'>
            <div className='min-w-40'>Tên khách hàng:</div>
            <div className='font-medium'>Phạm Tuấn Kiệt</div>
          </div>
          <div className='flex items-center'>
            <div className='min-w-40'>Số điện thoại:</div>
            <div className='font-medium'>0835489396</div>
          </div>
          <div className='flex items-center'>
            <div className='min-w-40'>Email:</div>
            <div className='font-medium'>kietphamkb@gmail.com</div>
          </div>
        </div>
      </div>

      <div className='flex items-center justify-end gap-5'>
        <Button
          className={`min-w-24 border-[1px] !border-error bg-white !text-red-500 hover:!bg-red-50`}
        >
          Xóa
        </Button>
        <Button className={`min-w-24`}>Lưu</Button>
      </div>
    </div>
  )
}

export default CustomerDetail
