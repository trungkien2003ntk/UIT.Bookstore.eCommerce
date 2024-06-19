import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Divider } from "@mui/material"

import VND from "../../components/vnd"

import * as orderServices from "../../apiServices/orderServices"
import noImage from "../../assets/images/no-photo.png"

const filters = [
  { name: "Tất cả", value: "all" },
  { name: "Chờ xác nhận", value: "waiting" },
  { name: "Chờ lấy hàng", value: "pending" },
  { name: "Chờ giao hàng", value: "shipping" },
  { name: "Đã giao", value: "delivered" },
  { name: "Đã hủy", value: "cancelled" },
  { name: "Trả hàng", value: "returned" },
]

// const orders = [
//   {
//     products: [
//       {
//         name: "Dép Birken đế trấu quai ngang 2 khoá nam nữ thời trang DETAUNISEX - DETA21",
//         image:
//           "https://down-vn.img.susercontent.com/file/9698962bff1eae5d88d744bc20b04e7f_tn",
//         skuName: "ĐEN, 41",
//         quantity: 1,
//         price: 32000,
//         originalPrice: 35000,
//       },
//       {
//         name: "Dép Birken đế trấu quai ngang 2 khoá nam nữ thời trang DETAUNISEX - DETA21",
//         image:
//           "https://down-vn.img.susercontent.com/file/9698962bff1eae5d88d744bc20b04e7f_tn",
//         skuName: "ĐEN, 41",
//         quantity: 1,
//         price: 32000,
//         originalPrice: 35000,
//       },
//     ],
//     status: "delivered",
//     totalPrice: 32000,
//   },
//   {
//     products: [
//       {
//         name: "Dép Birken đế trấu quai ngang 2 khoá nam nữ thời trang DETAUNISEX - DETA21",
//         image:
//           "https://down-vn.img.susercontent.com/file/9698962bff1eae5d88d744bc20b04e7f_tn",
//         skuName: "ĐEN, 41",
//         quantity: 1,
//         price: 32000,
//         originalPrice: 35000,
//       },
//       {
//         name: "Dép Birken đế trấu quai ngang 2 khoá nam nữ thời trang DETAUNISEX - DETA21",
//         image:
//           "https://down-vn.img.susercontent.com/file/9698962bff1eae5d88d744bc20b04e7f_tn",
//         skuName: "ĐEN, 41",
//         quantity: 1,
//         price: 32000,
//         originalPrice: 35000,
//       },
//     ],
//     status: "pending",
//     totalPrice: 32000,
//   },
// ]

const Orders = () => {
  const [selectedFilter, setSelectedFilter] = useState(filters[0])

  const [obj, setObj] = useState({})
  const [orders, setOrders] = useState([])

  const getAllOrder = async () => {
    const response = await orderServices.getAllOrder().catch((error) => {
      console.log("error", error)
    })

    if (response) {
      console.log("response", response)
      setObj(response)
      setOrders(response.items)
    }
  }

  useEffect(() => {
    getAllOrder()
  }, [])

  return (
    <div className='flex w-full flex-col'>
      <div className='flex items-center justify-between'>
        {filters.map((item, index) => (
          <div
            key={index}
            className={`${selectedFilter.name === item.name && "bg-green-50 text-ct-green-400"}
            relative border-[1px] px-7 py-3 hover:cursor-pointer 
          hover:text-ct-green-400`}
            onClick={() => setSelectedFilter(item)}
          >
            {item.name}
            <div
              className={`${selectedFilter.name === item.name ? "block" : "hidden"} 
              absolute left-0 top-full h-1 w-full bg-ct-green-400`}
            ></div>
          </div>
        ))}
      </div>

      <div className='mt-5 flex flex-col gap-5'>
        {orders.map((item, index) => (
          <Link
            to={`/user/orders/${item.id}`}
            key={index}
            className='rounded border-[1px] bg-white p-3 shadow
            hover:cursor-pointer'
          >
            <div
              className={`${
                item.status === "Delivered"
                  ? "border-blue-500 text-blue-500"
                  : "text-gray-700"
              }
              mb-3 rounded border-[1px] p-3
              text-sm font-medium uppercase `}
            >
              {item.status}
              {/* {item.status === "Delivered" && "Đã giao"}
              {item.status === "pending" && "Chờ lấy hàng"} */}
            </div>

            <div className='flex flex-col gap-3'>
              {item.orderLines.map((product, index) => (
                <div key={index} className='flex items-center gap-2'>
                  <img
                    className='h-24 w-24 border-[1px]'
                    alt='img'
                    src={product.thumbnailUrl || noImage}
                  />
                  <div className='flex flex-col justify-between gap-2'>
                    <div className='line-clamp-2'>{product.productName}</div>
                    <div className='text-sm text-gray-500'>
                      Phân loại hàng: {product.skuOptionName}
                    </div>
                    <div>x{product.quantity}</div>
                  </div>

                  <div className='flex flex-1 items-center justify-end gap-2'>
                    {/* <VND
                      className={`text-gray-400 line-through`}
                      number={product.total}
                    /> */}
                    <VND
                      className={`font-medium text-ct-green-400`}
                      number={product.total}
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
                number={item.total}
              />
            </div>
          </Link>
        ))}
      </div>
    </div>
  )
}

export default Orders
