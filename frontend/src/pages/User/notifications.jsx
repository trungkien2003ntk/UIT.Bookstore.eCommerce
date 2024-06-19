import { useState } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"
import { Link } from "react-router-dom"
import VND from "../../components/vnd"

const products = [
  {
    name: "Dép Birken đế trấu quai ngang 2 khoá nam nữ thời trang DETAUNISEX - DETA21",
    image:
      "https://down-vn.img.susercontent.com/file/9698962bff1eae5d88d744bc20b04e7f_tn",
    skuName: "ĐEN, 41",
    quantity: 1,
    price: 32000,
    originalPrice: 35000,
  },
  {
    name: "Dép Birken đế trấu quai ngang 2 khoá nam nữ thời trang DETAUNISEX - DETA21",
    image:
      "https://down-vn.img.susercontent.com/file/9698962bff1eae5d88d744bc20b04e7f_tn",
    skuName: "ĐEN, 41",
    quantity: 1,
    price: 32000,
    originalPrice: 35000,
  },
]

const Notifications = () => {
  const [userInfo, setUserInfo] = useState({
    email: "",
    firstName: "",
    lastName: "",
    phone: "",
    dateOfBirth: "",
  })

  return (
    <div className='flex w-full flex-col gap-3 py-3'>
      <div className='ml-2 text-xl font-medium'>Thông báo</div>

      <Divider />

      <div className='flex items-center '>
        <div className='flex flex-1 flex-col gap-5 pr-10'>
          <Link
            className='rounded border-[1px] bg-white p-3 shadow
            hover:cursor-pointer'
          >
            <div
              className={`${"border-blue-500 text-blue-500"}
              mb-3 rounded border-[1px] p-3
              text-sm font-medium uppercase `}
            >
              {"Đã giao"}
            </div>

            <div className='mb-5 flex gap-2'>
              <div>
                Mã đơn hàng:{" "}
                <span className='font-medium text-ct-green-400'>2252</span>
              </div>
              <div>Đã giao: 18:00 - 10/06/2024</div>
            </div>

            <div className='flex flex-col gap-3'>
              {products.map((item, index) => (
                <div key={index} className='flex items-center gap-2'>
                  <img
                    className='h-24 w-24 border-[1px]'
                    alt='img'
                    src={item.image}
                  />
                  <div className='flex flex-col justify-between gap-2'>
                    <div className='line-clamp-2'>{item.name}</div>
                    <div className='text-sm text-gray-500'>
                      Phân loại hàng: {item.skuName}
                    </div>
                    <div>x{item.quantity}</div>
                  </div>

                  <div className='flex flex-1 items-center justify-end gap-2'>
                    <VND
                      className={`text-gray-400 line-through`}
                      number={item.originalPrice}
                    />
                    <VND
                      className={`font-medium text-ct-green-400`}
                      number={item.price}
                    />
                  </div>
                </div>
              ))}
            </div>

            <div className='py-3'>
              <Divider />
            </div>

            <div className='flex items-center justify-end gap-3 p-3'>
              <div className='font-medium text-gray-700'>Thành tiền:</div>
              <VND
                className={`text-2xl font-medium text-ct-green-400`}
                number={30000}
              />
            </div>
          </Link>
        </div>
      </div>
    </div>
  )
}

export default Notifications
