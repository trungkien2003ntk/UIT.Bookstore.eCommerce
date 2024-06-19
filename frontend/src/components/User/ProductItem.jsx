/* eslint-disable react/prop-types */
import { Link } from "react-router-dom"
import FiveStar from "./FiveStar"
import VND from "../vnd"
import toK from "../toK"

import noImage from "../../assets/images/no-photo.png"

const ProductItem = ({ item }) => {
  return (
    <Link
      to={`/user/products/${item.id}`}
      className='m-1 min-w-44 max-w-44 border-[1px] bg-white
      transition-all ease-in-out hover:-translate-y-[1px] hover:border-ct-green-400'
    >
      <img
        className='h-[200px] object-cover'
        src={item.thumbnailImageUrl || noImage}
        alt='img'
      />
      <div className='content flex flex-col gap-1 p-2'>
        <div className='name line-clamp-2 h-10 text-sm font-medium'>
          {item.name}
        </div>
        <div className='flex items-center gap-2'>
          <VND
            className={`font-semibold text-ct-green-500`}
            number={item.minUnitPrice}
          />
          {item.minDiscountRate > 0 && (
            <div className='rounded bg-red-100 px-2 py-1 text-xs font-medium text-red-600'>
              {item.minDiscountRate + "%"}
            </div>
          )}
        </div>
        <div className='h-5'>
          {item.minRecommendedRetailPrice > 0 && (
            <VND
              className={`text-sm text-gray-400 line-through`}
              number={item.minRecommendedRetailPrice}
            />
          )}
        </div>
        <div className='flex items-center justify-between'>
          {item.averageRating > 0 && (
            <FiveStar
              rating={item.averageRating}
              classNameForSize={`h-3 w-3`}
            />
          )}
          {item.soldCount && (
            <div className='text-xs'>{"Đã bán " + toK(item.soldCount)}</div>
          )}
        </div>
      </div>
    </Link>
  )
}

export default ProductItem
