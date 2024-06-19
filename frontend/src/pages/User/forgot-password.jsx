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

const ForgotPassword = () => {
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
  const [newPwd, setNewPwd] = useState("")
  const [confirmPwd, setConfirmPwd] = useState("")

  const [errors, setErrors] = useState({
    newPwd: "",
    confirmPwd: "",
  })

  const handleForgotPassword = (e) => {
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
          setStep(3)
        }, 2000)
      } else {
        setOtpErrorMsg("OTP không được để trống")
      }
    }

    if (step === 3) {
      if (newPwd !== "" && confirmPwd !== "") {
        if (newPwd !== confirmPwd) {
          setErrors((prev) => ({
            ...prev,
            confirmPwd: "Mật khẩu không trùng khớp",
          }))
        } else {
          setButtonProgressing(true)
          setTimeout(() => {
            setButtonProgressing(false)
            toast("success", "Đổi mật khẩu thành công. Vui lòng đăng nhập lại!")
            navigate("/user/login")
          }, 2000)
        }
      } else {
        setErrors((prev) => ({
          ...prev,
          newPwd: newPwd === "" ? "Không được để trống" : "",
          confirmPwd: confirmPwd === "" ? "Không được để trống" : "",
        }))
      }
    }
  }

  const clearErrors = () => {
    setEmailErrorMsg("")
    setOtpErrorMsg("")
    setErrors(() => ({
      newPwd: "",
      confirmPwd: "",
    }))
  }

  const clearStep2 = () => {
    setOtp("")
  }

  const clearStep3 = () => {
    setNewPwd("")
    setConfirmPwd("")
  }

  return (
    <form
      onSubmit={handleForgotPassword}
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
              Quên mật khẩu
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
                    value={newPwd}
                    onChange={(value) => setNewPwd(value)}
                    placeholder={"Mật khẩu mới"}
                    title={"Mật khẩu mới"}
                    type='password'
                    pwd
                    errorMsg={errors.newPwd}
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
          </div>
        </div>
      </div>
    </form>
  )
}

export default ForgotPassword
