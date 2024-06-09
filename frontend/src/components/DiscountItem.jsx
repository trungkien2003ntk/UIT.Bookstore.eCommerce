/* eslint-disable react/prop-types */
import { useState } from "react"
import Modal from "./Modal"

const DiscountItem = ({ obj }) => {
  const [openModal, setOpenModal] = useState(false)

  const handleModal = () => {
    setOpenModal((prev) => !prev)
  }

  return (
    <div className='flex min-w-80 rounded border-[1px] bg-white shadow'>
      <div
        className={`${obj?.type === "freeShipping" ? "bg-blue-400" : "bg-[#FFB323]"} 
        flex min-h-24 min-w-24 items-center justify-center rounded-l`}
      >
        {obj?.type === "freeShipping" ? (
          <img
            className='h-10 w-10'
            alt='img'
            src='https://down-vn.img.susercontent.com/file/sg-11134004-22120-4cskiffs0olvc3'
          />
        ) : (
          <img
            className='h-10 w-10'
            alt='img'
            src='https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/event_cart_2/ico_promotion.svg?q=105685'
          />
        )}
      </div>
      <div className='flex min-w-56 flex-col justify-between p-3'>
        <div className='line-clamp-2 text-sm font-medium uppercase'>
          {obj?.title}
        </div>

        <div className='flex items-center justify-between gap-1'>
          <div className='text-xs text-gray-500'>Hết hạn: {obj?.expired}</div>
          <div
            className='text-xs font-medium text-blue-500 
            hover:cursor-pointer hover:text-blue-700'
            onClick={handleModal}
          >
            Xem thêm
          </div>
        </div>
      </div>

      <Modal
        discount={obj?.type === "discount"}
        freeShipping={obj?.type === "freeShipping"}
        open={openModal}
        setOpen={handleModal}
        title={obj?.title}
        contentComp={<div>{obj?.description}</div>}
        isCancelButton
      />
    </div>
  )
}

export default DiscountItem
