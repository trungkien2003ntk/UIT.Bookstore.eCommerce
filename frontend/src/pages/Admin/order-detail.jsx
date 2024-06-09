/* eslint-disable no-unused-vars */
import { useState } from "react"
import { Divider } from "@mui/material"
import VND from "../../components/vnd"
import Button from "../../components/Button"
import Dropdown from "../../components/Dropdown"

const status = [
  {
    step: 1,
    text: "Chờ xác nhận",
  },
  {
    step: 2,
    text: "Đang gói hàng",
  },
  {
    step: 3,
    text: "Đang giao hàng",
  },
  {
    step: 4,
    text: "Đã giao hàng",
  },
  {
    step: 5,
    text: "Đã hủy",
  },
  {
    step: 6,
    text: "Trả hàng",
  },
]

const OrderDetail = () => {
  const [initialStep, setInitialStep] = useState(3)
  const [selected, setSelected] = useState(status[initialStep - 1])

  const [paymentType, setPaymentType] = useState(1)

  const handleSort = () => {}

  return (
    <div className='flex flex-col gap-3 overflow-auto'>
      <div className='flex items-center justify-between rounded bg-white p-3 shadow'>
        <div className='flex items-center gap-3'>
          <div className='text-blue-600'>
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
          <div className='text-lg font-medium text-blue-700'>
            Thông tin đơn hàng
          </div>
        </div>

        <Dropdown
          selected={selected}
          setSelected={(value) => setSelected(value)}
          placeholder={"Trạng thái"}
          items={status.filter((item) => item.step >= initialStep)}
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

      <div className='flex flex-col gap-3 rounded bg-white p-5 shadow'>
        <div className='flex items-center gap-2'>
          <div className='text-red-500'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6'
            >
              <path
                fillRule='evenodd'
                d='m11.54 22.351.07.04.028.016a.76.76 0 0 0 .723 0l.028-.015.071-.041a16.975 16.975 0 0 0 1.144-.742 19.58 19.58 0 0 0 2.683-2.282c1.944-1.99 3.963-4.98 3.963-8.827a8.25 8.25 0 0 0-16.5 0c0 3.846 2.02 6.837 3.963 8.827a19.58 19.58 0 0 0 2.682 2.282 16.975 16.975 0 0 0 1.145.742ZM12 13.5a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z'
                clipRule='evenodd'
              />
            </svg>
          </div>
          <div className='text-lg font-medium text-ct-black-300'>
            Địa chỉ nhận hàng
          </div>
        </div>

        <div className='flex items-center gap-5'>
          <div className='font-medium'>Phạm Tuấn Kiệt</div>
          <div className='font-medium'>0835489396</div>
          <div>
            KTX Khu B ĐHQG, Phường Linh Trung, Thành Phố Thủ Đức, TP. Hồ Chí
            Minh
          </div>
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-5 shadow'>
        <div className='flex items-center justify-between'>
          <div className='text-xl'>Sản phẩm</div>
          <div className='flex'>
            <div className='min-w-44 text-center'></div>
            <div className='min-w-44 text-center'>Đơn giá</div>
            <div className='min-w-44 text-center'>Số lượng</div>
            <div className='min-w-44 text-center'>Thành tiền</div>
          </div>
        </div>

        <div className='flex items-center justify-between text-sm'>
          <div className='flex items-center gap-2'>
            <img
              className='h-20 w-20'
              alt='img'
              src='https://down-vn.img.susercontent.com/file/vn-11134207-7r98o-ltkh7zcofuoa65_tn'
            />
            <div className='line-clamp-3 text-sm'>
              Áo Sơ Mi Denim Basic Godmother Xuân Hè 2024
            </div>
          </div>

          <div className='flex items-center'>
            <div
              className='flex min-w-44 max-w-44 flex-col items-start justify-start 
              rounded p-3'
            >
              <div className='flex items-center gap-1 text-sm'>
                <span className='font-medium'>Phân loại hàng</span>
              </div>
              <div className='text-start text-sm'>123</div>
            </div>
            <div className='min-w-44 text-center'>
              <VND number={249000} />
            </div>
            <div className='min-w-44 text-center'>1</div>
            <div className='min-w-44 text-center'>
              <VND number={249000} />
            </div>
          </div>
        </div>
      </div>

      <div className='flex flex-col justify-between rounded bg-white p-5 shadow'>
        <div className='flex items-center gap-2'>
          <div className='text-blue-700'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6'
            >
              <path d='M4.5 3.75a3 3 0 0 0-3 3v.75h21v-.75a3 3 0 0 0-3-3h-15Z' />
              <path
                fillRule='evenodd'
                d='M22.5 9.75h-21v7.5a3 3 0 0 0 3 3h15a3 3 0 0 0 3-3v-7.5Zm-18 3.75a.75.75 0 0 1 .75-.75h6a.75.75 0 0 1 0 1.5h-6a.75.75 0 0 1-.75-.75Zm.75 2.25a.75.75 0 0 0 0 1.5h3a.75.75 0 0 0 0-1.5h-3Z'
                clipRule='evenodd'
              />
            </svg>
          </div>
          <div className='text-xl font-medium text-gray-600'>
            Phương thức thanh toán
          </div>
        </div>

        <div className='mt-5 flex items-center justify-start gap-5'>
          {paymentType === 1 && (
            <div
              className={`${paymentType === 1 && "border-[2px] border-ct-green-400"} 
                relative flex items-center gap-2 rounded border-[1px]
                px-3 py-2 text-gray-600 hover:cursor-pointer hover:border-ct-green-400`}
            >
              <div className=''>Thanh toán ghi nhận hàng (COD)</div>
              {paymentType === 1 && (
                <div
                  className='absolute bottom-0 right-0 h-0 w-0 
                border-b-[12px] border-l-[12px] border-b-ct-green-400 border-l-transparent'
                ></div>
              )}
            </div>
          )}

          {paymentType === 2 && (
            <div
              className={`${paymentType === 2 && "border-[2px] border-ct-green-400"} 
                relative flex items-center gap-2 rounded border-[1px]
                px-3 py-2 text-gray-600 hover:cursor-pointer hover:border-ct-green-400`}
            >
              <div className=''>VNPay</div>
              {paymentType === 2 && (
                <div
                  className='absolute bottom-0 right-0 h-0 w-0 
                border-b-[12px] border-l-[12px] border-b-ct-green-400 border-l-transparent'
                ></div>
              )}
            </div>
          )}
        </div>

        <div className='w-full py-5'>
          <Divider />
        </div>

        <div className='flex flex-col items-end gap-5'>
          <div className='flex items-center'>
            <div className='min-w-52'>Tổng tiền hàng</div>
            <VND className={`min-w-52 text-end`} number={518000} />
          </div>

          <div className='flex items-center'>
            <div className='min-w-52'>Phí vận chuyển</div>
            <VND className={`min-w-52 text-end`} number={35000} />
          </div>

          <div className='flex items-center'>
            <div className='min-w-52'>Giảm giá phí vận chuyển</div>

            <div className='flex min-w-52 items-center justify-end'>
              <span>-</span>
              <VND className={`text-end`} number={35000} />
            </div>
          </div>

          <div className='flex items-center'>
            <div className='min-w-52'>Voucher</div>

            <div className='flex min-w-52 items-center justify-end'>
              <span>-</span>
              <VND className={`text-end`} number={5000} />
            </div>
          </div>

          <div className='flex items-center'>
            <div className='min-w-52'>KKBooks Xu</div>

            <div className='flex min-w-52 items-center justify-end'>
              <span>-</span>
              <VND className={`text-end`} number={500} />
            </div>
          </div>

          <div className='flex items-center'>
            <div className='min-w-52'>Tổng thanh toán</div>
            <VND
              className={`min-w-52 text-end text-3xl font-medium text-ct-green-400`}
              number={35000}
            />
          </div>
        </div>

        <div className='w-full py-5'>
          <Divider />
        </div>

        <div className='flex items-center justify-end gap-5'>
          <Button className={`min-w-64`}>Cập nhật đơn</Button>
        </div>
      </div>
    </div>
  )
}

export default OrderDetail
