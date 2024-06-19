import { useEffect } from "react"
import { Link, useLocation } from "react-router-dom"

import * as checkoutService from "../../apiServices/checkoutServices"

const ResultPayment = () => {
  const location = useLocation()
  useEffect(() => {
    const fetch = async () => {
      const query = new URLSearchParams(location.search)
      const res = await checkoutService.handleIPN(query).catch((err) => {
        console.log(err)
      })

      if (res) {
        console.log(res)
      }
    }

    fetch()
  }, [location.search])
  return (
    <main className='grid min-h-full place-items-center bg-white px-6 py-24 sm:py-32 lg:px-8'>
      <div className='text-center'>
        <p className='flex items-center justify-center text-center text-base font-semibold text-ct-green-300'>
          <svg
            xmlns='http://www.w3.org/2000/svg'
            viewBox='0 0 24 24'
            fill='currentColor'
            className='size-20'
          >
            <path
              fillRule='evenodd'
              d='M2.25 12c0-5.385 4.365-9.75 9.75-9.75s9.75 4.365 9.75 9.75-4.365 9.75-9.75 9.75S2.25 17.385 2.25 12Zm13.36-1.814a.75.75 0 1 0-1.22-.872l-3.236 4.53L9.53 12.22a.75.75 0 0 0-1.06 1.06l2.25 2.25a.75.75 0 0 0 1.14-.094l3.75-5.25Z'
              clipRule='evenodd'
            />
          </svg>
        </p>
        <h1 className='mt-4 text-3xl font-bold tracking-tight text-gray-900 sm:text-5xl'>
          Thanh toán thành công
        </h1>
        <div className='mt-10 flex items-center justify-center gap-x-6'>
          <Link
            to='/'
            className='rounded-md bg-ct-green-300 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm 
        hover:bg-ct-green-300 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 
        focus-visible:outline-ct-green-300'
          >
            Về trang chủ
          </Link>
        </div>
      </div>
    </main>
  )
}

export default ResultPayment
