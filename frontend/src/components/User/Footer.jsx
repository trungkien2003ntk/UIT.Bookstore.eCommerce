import CombinationLogo from "../../assets/images/combination-logo.svg"

const Footer = () => {
  return (
    <footer className='relative w-full border-t-8 border-green-400 bg-white shadow'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-wrap items-center 
        justify-between gap-y-10 p-4 py-10 lg:px-8'
      >
        <img className='mx-10 w-[200px]' src={CombinationLogo} alt='svg' />

        <div className='mx-5 grid min-w-40 flex-1 grid-cols-1 gap-x-5 gap-y-10 sm:grid-cols-2 md:grid-cols-3'>
          <div className=''>
            <div className='font-semibold uppercase'>Dịch vụ</div>
            <div className='mt-3 flex flex-col gap-2'>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Điều khoản sử dụng
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Chính sách bảo mật thông tin cá nhân
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Chính sách bảo mật thanh toán
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Hệ thống trung tâm nhà sách
              </div>
            </div>
          </div>
          <div className=''>
            <div className='font-semibold uppercase'>Hỗ trợ</div>
            <div className='mt-3 flex flex-col gap-2'>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Chính sách đổi trả
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Chính sách vận chuyển
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Chính sách bảo hành
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Phương thức thanh toán
              </div>
            </div>
          </div>
          <div className=''>
            <div className='font-semibold uppercase'>Tài khoản</div>
            <div className='mt-3 flex flex-col gap-2'>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Đăng nhập / Tạo mới tài khoản
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Sổ địa chỉ
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Chi tiết tài khoản
              </div>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                Lịch sử mua hàng
              </div>
            </div>
          </div>
          <div className=''>
            <div className='font-semibold uppercase'>Liên hệ</div>
            <div className='mt-3 flex flex-col gap-2'>
              <div className='hover:cursor-pointer hover:text-orange-400'>
                kkbooks@gmail.com
              </div>
            </div>
          </div>
        </div>
      </div>
    </footer>
  )
}

export default Footer
