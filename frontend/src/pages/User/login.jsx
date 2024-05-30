import { useState } from "react"
import { Link } from "react-router-dom"
import Input from "../../components/Input"
import Button from "../../components/Button"
import Symbol from "../../assets/images/symbol-logo.svg"
import Logo from "../../assets/images/combination-logo.svg"

import { isValidEmail, isAllPropsNotNull } from "../../components/isValid"
import useToast from "../../hooks/useToast"

const Login = () => {
  const toast = useToast()
  const [progressingButton, setProgressingButton] = useState(false)

  const [loginInfo, setLoginInfo] = useState({
    email: "",
    password: "",
  })

  const [errors, setErrors] = useState({
    email: "",
    password: "",
  })

  const handleSubmit = (e) => {
    e.preventDefault()

    setErrors({ email: "", password: "" })

    if (isAllPropsNotNull(loginInfo)) {
      if (!isValidEmail(loginInfo.email)) {
        setErrors((prev) => ({ ...prev, email: "Email không hợp lệ" }))
      }

      if (isValidEmail(loginInfo.email) && loginInfo.password === "123") {
        setProgressingButton(true)
        setTimeout(() => {
          setProgressingButton(false)
          toast("success", "Đăng nhập thành công")
        }, 2000)
      } else {
        setProgressingButton(true)
        setTimeout(() => {
          setProgressingButton(false)
          toast("error", "Đăng nhập thất bại")
          setErrors((prev) => ({
            ...prev,
            password: "Mật khẩu không chính xác",
          }))
        }, 2000)
      }
    } else {
      setErrors((prev) => ({
        ...prev,
        email: loginInfo.email === "" ? "Không được để trống" : "",
        password: loginInfo.password === "" ? "Không được để trống" : "",
      }))
    }
  }

  return (
    <div className='relative w-full bg-green-400'>
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

          <form
            onSubmit={handleSubmit}
            className='flex flex-col items-center justify-center gap-y-4 2xs:min-w-80'
          >
            <div className='logo md:hidden'>
              <img src={Symbol} alt='img' className='h-16 w-16' />
            </div>

            <div className='text-xl font-bold uppercase text-green-800'>
              Đăng nhập
            </div>

            <div className='flex w-full flex-col items-center justify-center gap-y-4'>
              <Input
                value={loginInfo.email}
                onChange={(value) =>
                  setLoginInfo((prev) => ({ ...prev, email: value }))
                }
                placeholder={"Email"}
                title={"Email"}
                errorMsg={errors.email}
              />
              <Input
                value={loginInfo.password}
                onChange={(value) =>
                  setLoginInfo((prev) => ({ ...prev, password: value }))
                }
                placeholder={"Mật khẩu"}
                title={"Mật khẩu"}
                type='password'
                pwd
                errorMsg={errors.password}
              />
            </div>

            <Link
              to={"/user/forgot-password"}
              className='w-full text-end text-sm font-semibold text-green-600 
              hover:cursor-pointer hover:text-green-500'
            >
              Quên mật khẩu?
            </Link>

            <Button
              type='submit'
              className={`w-full`}
              progressing={progressingButton}
            >
              Đăng nhập
            </Button>

            <div className='flex w-full items-center gap-x-4'>
              <div className='h-[1px] w-full flex-1 bg-gray-200'></div>
              <div className='text-[12px] uppercase text-gray-300'>Hoặc</div>
              <div className='h-[1px] w-full flex-1 bg-gray-200'></div>
            </div>

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

            <div className='flex items-center justify-center gap-2 text-sm'>
              <div className='text-gray-500'>Chưa có tài khoản?</div>
              <Link
                to={"/user/sign-up"}
                className='font-medium text-ct-green-400 hover:cursor-pointer
              hover:text-ct-green-500'
              >
                Đăng ký
              </Link>
            </div>
          </form>
        </div>
      </div>
    </div>
  )
}

export default Login
