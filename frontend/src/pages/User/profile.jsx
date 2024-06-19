import { useState, useEffect } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"
import dayjs from "dayjs"

import * as profileService from "../../apiServices/profileServices"
import noImage from "../../assets/images/no-photo.png"

const Profile = () => {
  const [userInfo, setUserInfo] = useState({})

  console.log(userInfo)

  const getMe = async () => {
    const response = await profileService.getMe().catch((error) => {
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        // http.ClientRequest in node.js
        console.log(error.request)
      } else {
        // Something happened in setting up the request that triggered an Error
        console.log("Error", error.message)
      }
    })

    if (response) {
      setUserInfo(response)
    }
  }

  useEffect(() => {
    getMe()
  }, [])

  return (
    <div className='flex w-full flex-col gap-3 py-3'>
      <div className='ml-2 text-xl font-medium'>Hồ sơ</div>

      <Divider />

      <div className='flex items-center '>
        <div className='flex flex-1 flex-col gap-5 pr-10'>
          <div className=''>
            <div className='font-medium text-green-800'>Email</div>
            <div className='flex items-center gap-3'>
              <div>{userInfo.email}</div>
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
            value={userInfo.phoneNumber}
            onChange={(value) =>
              setUserInfo((prev) => ({ ...prev, phoneNumber: value }))
            }
            placeholder={"Số điện thoại"}
            title={"Số điện thoại"}
            type='text'
            // errorMsg={errors.phoneNumber}
          />

          <Input
            value={dayjs(userInfo.dateOfBirth).format("YYYY-MM-DD")}
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
            className='h-28 w-28 rounded-full border-[1px] object-contain shadow'
            alt='img'
            src={userInfo.imageUrl || noImage}
          />
          <Button>Chọn ảnh</Button>
        </div>
      </div>
    </div>
  )
}

export default Profile
