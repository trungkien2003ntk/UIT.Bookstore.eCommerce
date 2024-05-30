/* eslint-disable no-unused-vars */
import { useState } from "react"
import { Link, useNavigate } from "react-router-dom"
import Input from "../../components/Input"
import Button from "../../components/Button"
import Symbol from "../../assets/images/symbol-logo.svg"
import Logo from "../../assets/images/combination-logo.svg"
import {
  isValidEmail,
  isAllPropsNotNull,
  isValidPhoneNumber,
} from "../../components/isValid"

import useToast from "../../hooks/useToast"

const SignUp = () => {
  const navigate = useNavigate()
  const toast = useToast()

  const [step, setStep] = useState(1)
  const [buttonProgressing, setButtonProgressing] = useState(false)

  // STEP 1
  const [email, setEmail] = useState("")
  const [emailErrorMsg, setEmailErrorMsg] = useState("")

  // STEP 2
  const [otp, setOtp] = useState("")
  const [otpErrorMsg, setOtpErrorMsg] = useState("")

  // STEP 3
  const [confirmPwd, setConfirmPwd] = useState("")

  const [errors, setErrors] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    dateOfBirth: "",
    password: "",
    confirmPwd: "",
  })

  const [userInfo, setUserInfo] = useState({
    email: "",
    firstName: "",
    lastName: "",
    phoneNumber: "",
    dateOfBirth: "",
    password: "",
  })

  const handleSignUp = (e) => {
    e.preventDefault()

    clearErrors()

    if (step === 1 && isValidEmail(email)) {
      setButtonProgressing(true)
      setTimeout(() => {
        setButtonProgressing(false)
        setStep(2)
        toast("success", "OTP đã được gửi đến email của bạn")
      }, 2000)
    } else if (email === "") {
      setEmailErrorMsg("Email không được để trống")
    } else {
      setEmailErrorMsg("Email không hợp lệ")
    }

    if (step === 2) {
      if (otp !== "") {
        setButtonProgressing(true)
        setTimeout(() => {
          setButtonProgressing(false)
          setUserInfo((prev) => ({ ...prev, email: email }))
          setStep(3)
        }, 2000)
      } else {
        setOtpErrorMsg("OTP không được để trống")
      }
    }

    if (step === 3) {
      if (isAllPropsNotNull(userInfo) && confirmPwd !== "") {
        if (!isValidPhoneNumber(userInfo.phoneNumber)) {
          setErrors((prev) => ({
            ...prev,
            phoneNumber: "Số điện thoại không hợp lệ",
          }))
        }

        if (userInfo.password !== confirmPwd) {
          setErrors((prev) => ({
            ...prev,
            confirmPwd: "Mật khẩu không trùng khớp",
          }))
        }

        if (
          isValidPhoneNumber(userInfo.phoneNumber) &&
          userInfo.password === confirmPwd
        ) {
          setButtonProgressing(true)
          setTimeout(() => {
            setButtonProgressing(false)
            toast("success", "Đăng ký thành công. Hay đăng nhập để tiếp tục.")
            navigate("/user/login")
          }, 2000)
        }
      } else {
        setErrors((prev) => ({
          ...prev,
          firstName: userInfo.firstName === "" ? "Không được để trống" : "",
          lastName: userInfo.lastName === "" ? "Không được để trống" : "",
          phoneNumber: userInfo.phoneNumber === "" ? "Không được để trống" : "",
          dateOfBirth: userInfo.dateOfBirth === "" ? "Không được để trống" : "",
          password: userInfo.password === "" ? "Không được để trống" : "",
          confirmPwd: confirmPwd === "" ? "Không được để trống" : "",
        }))
      }
    }
  }

  const clearErrors = () => {
    setEmailErrorMsg("")
    setOtpErrorMsg("")
    setErrors(() => ({
      firstName: "",
      lastName: "",
      phoneNumber: "",
      dateOfBirth: "",
      password: "",
      confirmPwd: "",
    }))
  }

  const clearStep2 = () => {
    setOtp("")
  }

  const clearStep3 = () => {
    setUserInfo({
      email: "",
      firstName: "",
      lastName: "",
      phoneNumber: "",
      dateOfBirth: "",
      password: "",
    })
    setConfirmPwd("")
  }

  return (
    <form
      onSubmit={handleSignUp}
      method='post'
      className='relative w-full bg-green-400'
    >
      <div
        className='mx-auto my-5 flex w-full max-w-screen-xl flex-col items-center
        justify-center p-4 lg:px-8'
      >
        <div
          className='2sx:p-10 flex items-center justify-center
            gap-x-20 rounded bg-white p-8 shadow-md'
        >
          <div className='hidden w-56 md:block'>
            <img src={Logo} />
          </div>

          <div className='flex flex-col items-center justify-center gap-y-4 2xs:min-w-80'>
            <div className='logo md:hidden'>
              <img src={Symbol} alt='img' className='h-16 w-16' />
            </div>

            <div className='text-xl font-bold uppercase text-green-800'>
              Đăng ký
            </div>

            <div className='flex w-full flex-col items-center justify-center gap-y-4'>
              {step === 1 && (
                <Input
                  value={email}
                  onChange={(value) => setEmail(value)}
                  placeholder={"Email"}
                  title={"Email"}
                  type='email'
                  errorMsg={emailErrorMsg}
                />
              )}

              {step === 2 && (
                <Input
                  value={otp}
                  onChange={(value) => setOtp(value)}
                  placeholder={"OTP"}
                  title={"Nhập OTP xác nhận"}
                  type='text'
                  errorMsg={otpErrorMsg}
                />
              )}

              {step === 3 && (
                <div className='flex w-full flex-col gap-4'>
                  <Input
                    value={userInfo.firstName}
                    onChange={(value) =>
                      setUserInfo((prev) => ({ ...prev, firstName: value }))
                    }
                    placeholder={"Họ"}
                    title={"Họ"}
                    type='text'
                    errorMsg={errors.firstName}
                  />
                  <Input
                    value={userInfo.lastName}
                    onChange={(value) =>
                      setUserInfo((prev) => ({ ...prev, lastName: value }))
                    }
                    placeholder={"Tên"}
                    title={"Tên"}
                    type='text'
                    errorMsg={errors.lastName}
                  />
                  <Input
                    value={userInfo.phoneNumber}
                    onChange={(value) =>
                      setUserInfo((prev) => ({ ...prev, phoneNumber: value }))
                    }
                    placeholder={"Số điện thoại"}
                    title={"Số điện thoại"}
                    type='text'
                    errorMsg={errors.phoneNumber}
                  />
                  <Input
                    value={userInfo.dateOfBirth}
                    onChange={(value) =>
                      setUserInfo((prev) => ({ ...prev, dateOfBirth: value }))
                    }
                    placeholder={"Ngày sinh"}
                    title={"Ngày sinh"}
                    type='date'
                    errorMsg={errors.dateOfBirth}
                  />
                  <Input
                    value={userInfo.password}
                    onChange={(value) =>
                      setUserInfo((prev) => ({ ...prev, password: value }))
                    }
                    placeholder={"Mật khẩu"}
                    title={"Mật khẩu"}
                    type='password'
                    pwd
                    errorMsg={errors.password}
                  />
                  <Input
                    value={confirmPwd}
                    onChange={(value) => setConfirmPwd(value)}
                    placeholder={"Xác nhận mật khẩu"}
                    title={"Xác nhận mật khẩu"}
                    type='password'
                    pwd
                    errorMsg={errors.confirmPwd}
                  />
                </div>
              )}
            </div>

            <div className='flex w-full flex-col gap-2'>
              <Button
                type='submit'
                className={`w-full`}
                progressing={buttonProgressing}
                disabled={buttonProgressing}
              >
                {step === 1 && "Tiếp tục"}
                {step === 2 && "Xác nhận"}
                {step === 3 && "Đăng ký"}
              </Button>

              {step >= 2 && (
                <Button
                  type='button'
                  className={`w-full border-[1px] border-ct-green-600 !bg-white font-medium 
                  !text-ct-green-600 hover:!bg-gray-100`}
                  onClick={() => {
                    setStep(1)
                    clearErrors()
                    clearStep2()
                    clearStep3()
                  }}
                >
                  Quay lại
                </Button>
              )}
            </div>

            {step === 1 && (
              <div className='flex w-full items-center gap-x-4'>
                <div className='h-[1px] w-full flex-1 bg-gray-200'></div>
                <div className='text-[12px] uppercase text-gray-300'>Hoặc</div>
                <div className='h-[1px] w-full flex-1 bg-gray-200'></div>
              </div>
            )}

            {step === 1 && (
              <div className='w-full'>
                <div
                  className='flex items-center justify-center gap-2 rounded border-[1px] border-gray-300 px-5
                    py-2 font-medium hover:cursor-pointer hover:border-ct-green-400'
                >
                  <div className='logo h-6 w-6'>
                    <img src='https://steelbluemedia.com/wp-content/uploads/2019/06/new-google-favicon-512.png' />
                  </div>
                  <div className='Google'>Google</div>
                </div>
              </div>
            )}

            <div className='flex items-center justify-center gap-2 text-sm'>
              <div className='text-gray-500'>Đã có tài khoản.</div>
              <Link
                to={"/user/login"}
                className='font-medium text-ct-green-400 hover:cursor-pointer
                hover:text-ct-green-500'
              >
                Đăng nhập
              </Link>
            </div>
          </div>
        </div>
      </div>
    </form>
  )
}

export default SignUp
