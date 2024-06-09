/* eslint-disable react/prop-types */
import { useState } from "react"
import { Checkbox } from "@mui/material"
import { green } from "@mui/material/colors"
import CategoryCart from "./CategoryCart"
import VND from "../vnd"
import ButtonNumber from "../ButtonNumber"

const ProductItemCart = ({ item }) => {
  const [quantity, setQuantity] = useState(1)

  return (
    <div className='flex items-center rounded bg-white px-3 py-5 shadow'>
      <Checkbox
        sx={{
          color: green[500],
          "&.Mui-checked": {
            color: green[500],
          },
        }}
      ></Checkbox>

      <div className='flex flex-1 items-center gap-3'>
        <img className='h-20 w-20' alt='img' src={item.imageUrl} />
        <div className='line-clamp-4 text-sm'>{item.productName}</div>
      </div>

      {item.skuName && (
        <div className='flex min-w-44 items-center justify-center gap-2 text-sm'>
          <CategoryCart item={item} />
        </div>
      )}

      <div className='flex min-w-44 items-center justify-center gap-2 text-sm'>
        <VND
          className={`text-gray-400 line-through`}
          number={item.recommendedRetailPrice}
        />
        <VND number={item.unitPrice} />
      </div>

      <div className='flex min-w-44 items-center justify-center text-sm'>
        <ButtonNumber quantity={quantity} setQuantity={setQuantity} min={1} />
      </div>

      <div className='flex min-w-44 items-center justify-center text-sm'>
        <VND
          className={`font-medium text-ct-green-400`}
          number={item.unitPrice * quantity}
        />
      </div>

      <div className='flex min-w-44 items-center justify-center text-sm'>
        <span className='font-semibold text-red-400 hover:cursor-pointer hover:text-red-500'>
          Xóa
        </span>
      </div>
    </div>
  )
}

export default ProductItemCart
