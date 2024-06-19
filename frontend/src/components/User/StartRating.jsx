/* eslint-disable react/prop-types */
const StarRating = ({ percentage, className }) => {
  return (
    <div className={`relative ${className}`}>
      {/* Full Star (Unfilled) */}
      <svg
        viewBox='0 0 15 15'
        className='absolute h-full w-full text-[#d5d5d5]'
      >
        <polygon
          points='7.5 .8 9.7 5.4 14.5 5.9 10.7 9.1 11.8 14.2 7.5 11.6 3.2 14.2 4.3 9.1 .5 5.9 5.3 5.4'
          fill='currentColor'
          strokeLinecap='round'
          strokeLinejoin='round'
          strokeMiterlimit='10'
        />
      </svg>
      {/* Clipped Star (Filled) */}
      <svg
        viewBox='0 0 15 15'
        className='absolute left-0 top-0 h-full w-full overflow-hidden'
        style={{ clipPath: `inset(0 ${100 - percentage}% 0 0)` }}
      >
        <polygon
          points='7.5 .8 9.7 5.4 14.5 5.9 10.7 9.1 11.8 14.2 7.5 11.6 3.2 14.2 4.3 9.1 .5 5.9 5.3 5.4'
          fill='currentColor'
          className='text-[#ffce3d]'
          strokeLinecap='round'
          strokeLinejoin='round'
          strokeMiterlimit='10'
        />
      </svg>
    </div>
  )
}

export default StarRating
