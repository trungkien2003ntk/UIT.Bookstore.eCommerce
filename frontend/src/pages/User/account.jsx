import { Outlet } from "react-router-dom"
import ButtonCollapse from "../../components/ButtonCollapse"
import { Divider } from "@mui/material"

const Account = () => {
  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col items-center justify-between 
        gap-y-5 p-4 lg:px-8'
      >
        <div
          className='flex min-h-[500px] w-full justify-center rounded 
            border-[1px] bg-white p-3 shadow'
        >
          <div className='flex h-full flex-col justify-end gap-3 pr-3'>
            <ButtonCollapse
              icon={
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='size-5 text-red-500'
                >
                  <path
                    fillRule='evenodd'
                    d='M18.685 19.097A9.723 9.723 0 0 0 21.75 12c0-5.385-4.365-9.75-9.75-9.75S2.25 6.615 2.25 12a9.723 9.723 0 0 0 3.065 7.097A9.716 9.716 0 0 0 12 21.75a9.716 9.716 0 0 0 6.685-2.653Zm-12.54-1.285A7.486 7.486 0 0 1 12 15a7.486 7.486 0 0 1 5.855 2.812A8.224 8.224 0 0 1 12 20.25a8.224 8.224 0 0 1-5.855-2.438ZM15.75 9a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0Z'
                    clipRule='evenodd'
                  />
                </svg>
              }
              title={"Tài khoản của tôi"}
              items={[
                { name: "Hồ sơ", path: "user/account/profile" },
                { name: "Sổ địa chỉ", path: "user/account/address" },
                { name: "Đổi mật khẩu", path: "user/account/change-password" },
                {
                  name: "Cài đặt thông báo",
                  path: "user/account/noti-settings",
                },
                {
                  name: "Thiết lập tài khoản",
                  path: "user/account/delete-account",
                },
              ]}
            ></ButtonCollapse>

            <ButtonCollapse
              icon={
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='size-5 text-blue-500'
                >
                  <path d='M3.375 3C2.339 3 1.5 3.84 1.5 4.875v.75c0 1.036.84 1.875 1.875 1.875h17.25c1.035 0 1.875-.84 1.875-1.875v-.75C22.5 3.839 21.66 3 20.625 3H3.375Z' />
                  <path
                    fillRule='evenodd'
                    d='m3.087 9 .54 9.176A3 3 0 0 0 6.62 21h10.757a3 3 0 0 0 2.995-2.824L20.913 9H3.087Zm6.163 3.75A.75.75 0 0 1 10 12h4a.75.75 0 0 1 0 1.5h-4a.75.75 0 0 1-.75-.75Z'
                    clipRule='evenodd'
                  />
                </svg>
              }
              title={"Đơn hàng của tôi"}
              items={[{ name: "Tất cả đơn hàng", path: "user/orders" }]}
            ></ButtonCollapse>

            <ButtonCollapse
              icon={
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='size-5 text-orange-500'
                >
                  <path
                    fillRule='evenodd'
                    d='M5.25 9a6.75 6.75 0 0 1 13.5 0v.75c0 2.123.8 4.057 2.118 5.52a.75.75 0 0 1-.297 1.206c-1.544.57-3.16.99-4.831 1.243a3.75 3.75 0 1 1-7.48 0 24.585 24.585 0 0 1-4.831-1.244.75.75 0 0 1-.298-1.205A8.217 8.217 0 0 0 5.25 9.75V9Zm4.502 8.9a2.25 2.25 0 1 0 4.496 0 25.057 25.057 0 0 1-4.496 0Z'
                    clipRule='evenodd'
                  />
                </svg>
              }
              title={"Thông báo"}
              items={[
                {
                  name: "Tất cả thông báo",
                  path: "user/notifications",
                },
              ]}
            ></ButtonCollapse>

            <ButtonCollapse
              icon={
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='size-5 text-purple-600'
                >
                  <path d='M9.375 3a1.875 1.875 0 0 0 0 3.75h1.875v4.5H3.375A1.875 1.875 0 0 1 1.5 9.375v-.75c0-1.036.84-1.875 1.875-1.875h3.193A3.375 3.375 0 0 1 12 2.753a3.375 3.375 0 0 1 5.432 3.997h3.943c1.035 0 1.875.84 1.875 1.875v.75c0 1.036-.84 1.875-1.875 1.875H12.75v-4.5h1.875a1.875 1.875 0 1 0-1.875-1.875V6.75h-1.5V4.875C11.25 3.839 10.41 3 9.375 3ZM11.25 12.75H3v6.75a2.25 2.25 0 0 0 2.25 2.25h6v-9ZM12.75 12.75v9h6.75a2.25 2.25 0 0 0 2.25-2.25v-6.75h-9Z' />
                </svg>
              }
              title={"Vouchers"}
              items={[
                {
                  name: "Tất cả vouchers",
                  path: "user/vouchers",
                },
              ]}
            ></ButtonCollapse>

            <ButtonCollapse
              icon={
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='size-5 text-yellow-400'
                >
                  <path d='M21 6.375c0 2.692-4.03 4.875-9 4.875S3 9.067 3 6.375 7.03 1.5 12 1.5s9 2.183 9 4.875Z' />
                  <path d='M12 12.75c2.685 0 5.19-.586 7.078-1.609a8.283 8.283 0 0 0 1.897-1.384c.016.121.025.244.025.368C21 12.817 16.97 15 12 15s-9-2.183-9-4.875c0-.124.009-.247.025-.368a8.285 8.285 0 0 0 1.897 1.384C6.809 12.164 9.315 12.75 12 12.75Z' />
                  <path d='M12 16.5c2.685 0 5.19-.586 7.078-1.609a8.282 8.282 0 0 0 1.897-1.384c.016.121.025.244.025.368 0 2.692-4.03 4.875-9 4.875s-9-2.183-9-4.875c0-.124.009-.247.025-.368a8.284 8.284 0 0 0 1.897 1.384C6.809 15.914 9.315 16.5 12 16.5Z' />
                  <path d='M12 20.25c2.685 0 5.19-.586 7.078-1.609a8.282 8.282 0 0 0 1.897-1.384c.016.121.025.244.025.368 0 2.692-4.03 4.875-9 4.875s-9-2.183-9-4.875c0-.124.009-.247.025-.368a8.284 8.284 0 0 0 1.897 1.384C6.809 19.664 9.315 20.25 12 20.25Z' />
                </svg>
              }
              title={"Tích điểm"}
              items={[
                {
                  name: "Coin",
                  path: "user/coin",
                },
              ]}
            ></ButtonCollapse>
          </div>

          <Divider orientation='vertical' flexItem />

          <div className='flex-1 pl-3'>
            <Outlet />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Account
