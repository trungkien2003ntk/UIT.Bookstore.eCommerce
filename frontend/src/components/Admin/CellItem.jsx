/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
export const CellItem = ({ type, text, iconCheck, iconX, iconLoad }) => {
  return (
    <div className='flex h-full w-full items-center justify-center'>
      <div
        className={`
        ${iconCheck && "bg-[#ecfdf3] text-[#027948]"}
        ${iconX && "bg-[#fef3f2] text-[#b00000]"}
        ${iconLoad && "bg-orange-100 text-orange-700"}
        ${type === "Processing" && "bg-[#d8ddfa] text-[#101828]"}
        ${type === "ProductType" && "bg-[#d8ddfa] text-[#101828]"}
        flex items-center justify-center gap-1 rounded-3xl
        border-[1px] px-2 py-1`}
      >
        <div>
          {iconCheck && (
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-4'
            >
              <path
                fillRule='evenodd'
                d='M19.916 4.626a.75.75 0 0 1 .208 1.04l-9 13.5a.75.75 0 0 1-1.154.114l-6-6a.75.75 0 0 1 1.06-1.06l5.353 5.353 8.493-12.74a.75.75 0 0 1 1.04-.207Z'
                clipRule='evenodd'
              />
            </svg>
          )}
          {iconLoad && (
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-4'
            >
              <path
                fillRule='evenodd'
                d='M4.755 10.059a7.5 7.5 0 0 1 12.548-3.364l1.903 1.903h-3.183a.75.75 0 1 0 0 1.5h4.992a.75.75 0 0 0 .75-.75V4.356a.75.75 0 0 0-1.5 0v3.18l-1.9-1.9A9 9 0 0 0 3.306 9.67a.75.75 0 1 0 1.45.388Zm15.408 3.352a.75.75 0 0 0-.919.53 7.5 7.5 0 0 1-12.548 3.364l-1.902-1.903h3.183a.75.75 0 0 0 0-1.5H2.984a.75.75 0 0 0-.75.75v4.992a.75.75 0 0 0 1.5 0v-3.18l1.9 1.9a9 9 0 0 0 15.059-4.035.75.75 0 0 0-.53-.918Z'
                clipRule='evenodd'
              />
            </svg>
          )}
          {iconX && (
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-4'
            >
              <path
                fillRule='evenodd'
                d='M5.47 5.47a.75.75 0 0 1 1.06 0L12 10.94l5.47-5.47a.75.75 0 1 1 1.06 1.06L13.06 12l5.47 5.47a.75.75 0 1 1-1.06 1.06L12 13.06l-5.47 5.47a.75.75 0 0 1-1.06-1.06L10.94 12 5.47 6.53a.75.75 0 0 1 0-1.06Z'
                clipRule='evenodd'
              />
            </svg>
          )}
        </div>

        <div className='text-sm font-medium'>{text}</div>
      </div>
    </div>
  )
}

export default CellItem
