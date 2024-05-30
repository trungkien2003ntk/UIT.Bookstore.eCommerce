/* eslint-disable react/prop-types */
import StarRating from "./StartRating"

const FiveStar = ({ classNameForSize, rating }) => {
  return (
    <div className='flex items-center justify-center'>
      {Array.from({ length: 5 }).map((_, index) => {
        return (
          <StarRating
            key={index}
            className={classNameForSize}
            percentage={
              Math.trunc(rating) > index
                ? 100
                : Math.trunc(rating) == index
                  ? Math.trunc((rating - Math.trunc(rating)) * 100)
                  : 0
            }
          />
        )
      })}
    </div>
  )
}

export default FiveStar
