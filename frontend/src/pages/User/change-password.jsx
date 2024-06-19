import { useState } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"

const ChangePassword = () => {
  const [userInfo, setUserInfo] = useState({
    email: "",
    firstName: "",
    lastName: "",
    phone: "",
    dateOfBirth: "",
  })

  return (
    <div className='flex w-full flex-col gap-3 py-3'>
      <div className='ml-2 text-xl font-medium'>Đổi mật khẩu</div>

      <Divider />

      <div className='flex items-center '>
        <div className='flex flex-1 flex-col gap-5 pr-10'>
          <Input
            placeholder={"Mật khẩu cũ"}
            title={"Mật khẩu cũ"}
            type='text'
            // errorMsg={errors.firstName}
          />
          <Input
            placeholder={"Mật khẩu mới"}
            title={"Mật khẩu mới"}
            type='text'
            // errorMsg={errors.firstName}
          />
          <Input
            placeholder={"Xác nhận mật khẩu mới"}
            title={"Xác nhận mật khẩu mới"}
            type='text'
            // errorMsg={errors.firstName}
          />

          <div>
            <Button>Lưu</Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default ChangePassword
