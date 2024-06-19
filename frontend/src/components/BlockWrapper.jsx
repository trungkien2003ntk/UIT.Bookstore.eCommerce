/* eslint-disable react/prop-types */
const BlockWrapper = ({
  children,
  title,
  icon,
  classForTitle,
  classForWrapper,
}) => {
  return (
    <div
      className={`category flex w-full flex-col items-center
      justify-center divide-y-[1px] divide-gray-200 rounded-2xl bg-white px-3 py-2 shadow`}
    >
      <div
        className={`title flex w-full items-center px-1
        py-2 text-lg font-semibold ${classForWrapper}`}
      >
        <div className={`${icon && "mr-2"}`}>{icon}</div>
        <div className={`${classForTitle}`}>{title}</div>
      </div>
      <div className='content flex w-full items-center justify-center px-1 pb-2 pt-4'>
        {children}
      </div>
    </div>
  )
}

export default BlockWrapper
