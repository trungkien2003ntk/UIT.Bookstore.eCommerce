import { useState } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"

const Address = () => {
  const [userInfo, setUserInfo] = useState({
    email: "",
    firstName: "",
    lastName: "",
    phone: "",
    dateOfBirth: "",
  })

  return (
    <div className='flex w-full flex-col gap-3 py-3'>
      <div className='ml-2 text-xl font-medium'>Sổ địa chỉ</div>

      <Divider />

      <div className='flex items-center '>
        <div className='flex flex-1 flex-col gap-5 pr-10'>
          <div className='flex flex-col justify-center'>
            <div className='flex items-center gap-2'>
              <div className='font-bold text-ct-black-300'>Phạm Tuấn Kiệt</div>
              <Divider orientation='vertical' flexItem />

              <div>0835422926</div>

              <Divider orientation='vertical' flexItem />

              <div className='text-blue-500 hover:cursor-pointer'>Cập nhật</div>
            </div>

            <div className='line-clamp-2'>KTX Khu B ĐHQG</div>

            <div className='mb-3'>
              {"Phường Linh Trung, Quận Thủ Đức, TP.Hồ Chí Minh"}
            </div>

            <Divider />
          </div>

          <div className='flex flex-col justify-center'>
            <div className='flex items-center gap-2'>
              <div className='font-bold text-ct-black-300'>
                Nguyễn Trung Kiên
              </div>
              <Divider orientation='vertical' flexItem />

              <div>055533652</div>

              <Divider orientation='vertical' flexItem />

              <div className='text-blue-500 hover:cursor-pointer'>Cập nhật</div>
            </div>

            <div className='line-clamp-2'>KTX Khu A ĐHQG</div>

            <div className='mb-3'>
              {"Phường Linh Trung, Quận Thủ Đức, TP.Hồ Chí Minh"}
            </div>

            <Divider />

            <div className='mt-5'>
              <Button>Thêm mới</Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Address
