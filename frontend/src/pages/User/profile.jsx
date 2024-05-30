import { useState } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"

const Profile = () => {
  const [userInfo, setUserInfo] = useState({})

  return (
    <div className='flex w-full flex-col gap-3 py-3'>
      <div className='ml-2 text-xl font-medium'>Hồ sơ</div>

      <Divider />

      <div className=''>
        {/* <Input
          value={email}
          onChange={(value) => setEmail(value)}
          placeholder={"Email"}
          title={"Email"}
          type='email'
          errorMsg={emailErrorMsg}
        /> */}
      </div>
    </div>
  )
}

export default Profile
