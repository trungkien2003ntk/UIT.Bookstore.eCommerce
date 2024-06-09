import { useState } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"

const Profile = () => {
  const [userInfo, setUserInfo] = useState({
    email: "",
    firstName: "",
    lastName: "",
    phone: "",
    dateOfBirth: "",
  })

  return (
    <div className='flex w-full flex-col gap-3 py-3'>
      <div className='ml-2 text-xl font-medium'>Hồ sơ</div>

      <Divider />

      <div className='flex items-center '>
        <div className='flex flex-1 flex-col gap-5 pr-10'>
          <div className=''>
            <div className='font-medium text-green-800'>Email</div>
            <div className='flex items-center gap-3'>
              <div>kietphamkb@gmail.com</div>
              <div className='text-sm text-blue-500 hover:cursor-pointer hover:font-medium'>
                Thay đổi
              </div>
            </div>
          </div>

          <Input
            value={userInfo.firstName}
            onChange={(value) =>
              setUserInfo((prev) => ({ ...prev, firstName: value }))
            }
            placeholder={"Họ"}
            title={"Họ"}
            type='text'
            // errorMsg={errors.firstName}
          />

          <Input
            value={userInfo.lastName}
            onChange={(value) =>
              setUserInfo((prev) => ({ ...prev, lastName: value }))
            }
            placeholder={"Tên"}
            title={"Tên"}
            type='text'
            // errorMsg={errors.lastName}
          />

          <Input
            value={userInfo.phone}
            onChange={(value) =>
              setUserInfo((prev) => ({ ...prev, phone: value }))
            }
            placeholder={"Số điện thoại"}
            title={"Số điện thoại"}
            type='text'
            // errorMsg={errors.phoneNumber}
          />

          <Input
            value={userInfo.dateOfBirth}
            onChange={(value) =>
              setUserInfo((prev) => ({ ...prev, dateOfBirth: value }))
            }
            placeholder={"Ngày sinh"}
            title={"Ngày sinh"}
            type='date'
            // errorMsg={errors.dateOfBirth}
          />

          <Button className={`w-fit`}>Lưu</Button>
        </div>

        <Divider flexItem orientation='vertical' />

        <div className='flex flex-col gap-5 px-20'>
          <img
            className='h-28 w-28 rounded-full'
            alt='img'
            src='https://down-vn.img.susercontent.com/file/1f1a0bc823c43b9847f65eb13a97f161_tn'
          />
          <Button>Chọn ảnh</Button>
        </div>
      </div>
    </div>
  )
}

export default Profile
