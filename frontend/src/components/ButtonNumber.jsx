/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable react/prop-types */

const ButtonNumber = ({ quantity, setQuantity, min, max }) => {
  const handleChange = (event) => {
    const inputValue = event.target.value

    if (/^[0-9]*$/.test(inputValue)) {
      if (Number(inputValue) < min) {
        return setQuantity(min)
      }

      setQuantity(Number(inputValue))
    }
  }

  const handlePlus = () => {
    if (quantity < 99999) {
      setQuantity(quantity + 1)
    }
  }

  const handleMinus = () => {
    if (quantity > min) {
      setQuantity(quantity - 1)
    }
  }

  return (
    <div className='flex w-fit select-none items-center justify-center'>
      <div
        className='rounded-l border-[1px] p-3 hover:cursor-pointer'
        onClick={handleMinus}
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 24 24'
          fill='currentColor'
          className='size-4'
        >
          <path
            fillRule='evenodd'
            d='M4.25 12a.75.75 0 0 1 .75-.75h14a.75.75 0 0 1 0 1.5H5a.75.75 0 0 1-.75-.75Z'
            clipRule='evenodd'
          />
        </svg>
      </div>

      <div className='w-fit border-[1px] px-2 py-2'>
        <input
          className='max-w-12 text-center outline-none'
          value={quantity}
          onChange={handleChange}
          maxLength={5}
        />
      </div>

      <div
        className='rounded-r border-[1px] p-3 hover:cursor-pointer'
        onClick={handlePlus}
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 24 24'
          fill='currentColor'
          className='size-4'
        >
          <path
            fillRule='evenodd'
            d='M12 3.75a.75.75 0 0 1 .75.75v6.75h6.75a.75.75 0 0 1 0 1.5h-6.75v6.75a.75.75 0 0 1-1.5 0v-6.75H4.5a.75.75 0 0 1 0-1.5h6.75V4.5a.75.75 0 0 1 .75-.75Z'
            clipRule='evenodd'
          />
        </svg>
      </div>
    </div>
  )
}

export default ButtonNumber
