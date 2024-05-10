import { Link } from "react-router-dom"

const Missing = () => {
  return (
    <main className='grid min-h-full place-items-center bg-white px-6 py-24 sm:py-32 lg:px-8'>
      <div className='text-center'>
        <p className='text-base font-semibold text-ct-green-300'>404</p>
        <h1 className='mt-4 text-3xl font-bold tracking-tight text-gray-900 sm:text-5xl'>
          Page not found
        </h1>
        <p className='mt-6 text-base leading-7 text-gray-600'>
          Đường dẫn không tồn tại.
        </p>
        <div className='mt-10 flex items-center justify-center gap-x-6'>
          <Link
            to='/'
            className='rounded-md bg-ct-green-300 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm 
            hover:bg-ct-green-300 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 
            focus-visible:outline-ct-green-300'
          >
            Về trang chủ
          </Link>
          {/* <Link to='/' className='text-sm font-semibold text-gray-900'>
            Contact support <span aria-hidden='true'>&rarr;</span>
          </Link> */}
        </div>
      </div>
    </main>
  )
}

export default Missing
