/* eslint-disable react/prop-types */
import { useState, useRef } from "react"

const Input = ({
  title,
  value,
  pwd,
  onChange,
  errorMsg,
  required = false,
  ...passProps
}) => {
  const inputRef = useRef(null)

  const props = {
    ...passProps,
  }

  const [isFocused, setIsFocused] = useState(false)

  const [showPwd, setShowPwd] = useState(false)

  const handleVisibilityPwd = () => {
    if (showPwd) {
      inputRef.current.type = "password"
      setShowPwd(false)
    } else {
      inputRef.current.type = "text"
      setShowPwd(true)
    }
  }

  return (
    <div className='relative flex w-full select-none flex-col gap-1'>
      {title && (
        <div className='text-sm font-semibold text-green-800'>
          {title}
          {required && <span className='text-red-500'>*</span>}
        </div>
      )}

      <div
        className={`${isFocused && "border-ct-green-300"} flex items-center 
        rounded border-[1px] px-3 py-2 text-base font-medium`}
      >
        <input
          ref={inputRef}
          className='mr-1 w-full text-sm outline-none placeholder:font-normal'
          value={value}
          onChange={(e) => onChange(e.target.value)}
          onFocus={() => setIsFocused(true)}
          onBlur={() => setIsFocused(false)}
          {...props}
        />
        {pwd && (
          <div
            className='icon text-gray-400 hover:cursor-pointer hover:text-gray-500'
            onClick={handleVisibilityPwd}
          >
            {!showPwd && (
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='h-5 w-5'
              >
                <path d='M3.53 2.47a.75.75 0 0 0-1.06 1.06l18 18a.75.75 0 1 0 1.06-1.06l-18-18ZM22.676 12.553a11.249 11.249 0 0 1-2.631 4.31l-3.099-3.099a5.25 5.25 0 0 0-6.71-6.71L7.759 4.577a11.217 11.217 0 0 1 4.242-.827c4.97 0 9.185 3.223 10.675 7.69.12.362.12.752 0 1.113Z' />
                <path d='M15.75 12c0 .18-.013.357-.037.53l-4.244-4.243A3.75 3.75 0 0 1 15.75 12ZM12.53 15.713l-4.243-4.244a3.75 3.75 0 0 0 4.244 4.243Z' />
                <path d='M6.75 12c0-.619.107-1.213.304-1.764l-3.1-3.1a11.25 11.25 0 0 0-2.63 4.31c-.12.362-.12.752 0 1.114 1.489 4.467 5.704 7.69 10.675 7.69 1.5 0 2.933-.294 4.242-.827l-2.477-2.477A5.25 5.25 0 0 1 6.75 12Z' />
              </svg>
            )}
            {showPwd && (
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='h-5 w-5'
              >
                <path d='M12 15a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z' />
                <path
                  fillRule='evenodd'
                  d='M1.323 11.447C2.811 6.976 7.028 3.75 12.001 3.75c4.97 0 9.185 3.223 10.675 7.69.12.362.12.752 0 1.113-1.487 4.471-5.705 7.697-10.677 7.697-4.97 0-9.186-3.223-10.675-7.69a1.762 1.762 0 0 1 0-1.113ZM17.25 12a5.25 5.25 0 1 1-10.5 0 5.25 5.25 0 0 1 10.5 0Z'
                  clipRule='evenodd'
                />
              </svg>
            )}
          </div>
        )}
      </div>

      {errorMsg && (
        <div className='ml-1 text-[12px] font-medium text-error'>
          {errorMsg}
        </div>
      )}
    </div>
  )
}

export default Input
