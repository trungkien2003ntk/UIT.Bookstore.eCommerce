import { Link, Outlet } from "react-router-dom"
// import useAuth from "../../hooks/useAuth"

import Logo from "../../assets/images/combination-logo.svg"
import { MenuItem } from "@headlessui/react"
import MyMenu from "../../components/MyMenu"
import ButtonCollapse from "../../components/ButtonCollapse"

const Home = () => {
  // const { setAuth } = useAuth()
  // const navigate = useNavigate()

  return (
    <div className='flex h-full w-full flex-col '>
      <div className='flex h-20 w-full items-center justify-center  border-[1px] bg-white p-2 shadow'>
        <div className='flex w-full items-center justify-between'>
          <div className='min-w-72'>
            <img className='h-12 ' alt='img' src={Logo} />
          </div>

          <li className='account flex items-center justify-center'>
            <MyMenu
              button={
                <div
                  className='flex select-none items-center justify-center gap-2 rounded
                  px-3 py-2 hover:cursor-pointer hover:bg-gray-100'
                >
                  <img
                    className='h-8 w-8 cursor-pointer rounded-full'
                    alt='img'
                    src='https://down-vn.img.susercontent.com/file/1f1a0bc823c43b9847f65eb13a97f161_tn'
                  />
                  <div className='font-medium'>Phạm Tuấn Kiệt</div>
                </div>
              }
            >
              <div className='py-1'>
                <MenuItem>
                  <Link
                    to={"/user/account/profile"}
                    className='group flex items-center justify-start gap-3 px-4
                          py-2 text-sm font-semibold hover:bg-gray-100'
                  >
                    <div
                      className='icon object-none object-center text-gray-400
                          group-hover:text-gray-600'
                    >
                      <img
                        alt='avt'
                        src='https://th.bing.com/th/id/R.11b315105c63ad7bfd35778ddf984c9a?rik=2LjF8DiLJFIgcA&riu=http%3a%2f%2forig11.deviantart.net%2f0405%2ff%2f2014%2f082%2fb%2f8%2funivers_by_momez-d7bcl1h.jpg&ehk=K9hHXvRywf2iBjx2rp6ywehVhV8366uX%2bi%2bGDI388x8%3d&risl=1&pid=ImgRaw&r=0'
                        className='h-10 w-10 rounded-full'
                      />
                    </div>
                    <div className='title'>Phạm Tuấn Kiệt</div>
                  </Link>
                </MenuItem>
              </div>

              <div className='py-1'>
                <MenuItem>
                  <Link
                    className='group flex items-center justify-start gap-3 px-4
                          py-2 text-sm hover:bg-gray-100'
                  >
                    <div className='icon text-gray-400 group-hover:text-ct-green-300'>
                      <svg
                        xmlns='http://www.w3.org/2000/svg'
                        viewBox='0 0 24 24'
                        fill='currentColor'
                        className='h-6 w-6'
                      >
                        <path
                          fillRule='evenodd'
                          d='M16.5 3.75a1.5 1.5 0 0 1 1.5 1.5v13.5a1.5 1.5 0 0 1-1.5 1.5h-6a1.5 1.5 0 0 1-1.5-1.5V15a.75.75 0 0 0-1.5 0v3.75a3 3 0 0 0 3 3h6a3 3 0 0 0 3-3V5.25a3 3 0 0 0-3-3h-6a3 3 0 0 0-3 3V9A.75.75 0 1 0 9 9V5.25a1.5 1.5 0 0 1 1.5-1.5h6ZM5.78 8.47a.75.75 0 0 0-1.06 0l-3 3a.75.75 0 0 0 0 1.06l3 3a.75.75 0 0 0 1.06-1.06l-1.72-1.72H15a.75.75 0 0 0 0-1.5H4.06l1.72-1.72a.75.75 0 0 0 0-1.06Z'
                          clipRule='evenodd'
                        />
                      </svg>
                    </div>
                    <div className='title'>Đăng xuất</div>
                  </Link>
                </MenuItem>
              </div>
            </MyMenu>
          </li>
        </div>
      </div>

      <div className='flex min-h-[calc(100vh-80px)] flex-1 overflow-hidden'>
        <div className='flex min-w-72 flex-col gap-3 overflow-auto border-[1px] bg-white p-3 shadow'>
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
            title={"Đơn hàng"}
            items={[{ name: "Tất cả đơn hàng", path: "admin/orders" }]}
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
                  d='M6.912 3a3 3 0 0 0-2.868 2.118l-2.411 7.838a3 3 0 0 0-.133.882V18a3 3 0 0 0 3 3h15a3 3 0 0 0 3-3v-4.162c0-.299-.045-.596-.133-.882l-2.412-7.838A3 3 0 0 0 17.088 3H6.912Zm13.823 9.75-2.213-7.191A1.5 1.5 0 0 0 17.088 4.5H6.912a1.5 1.5 0 0 0-1.434 1.059L3.265 12.75H6.11a3 3 0 0 1 2.684 1.658l.256.513a1.5 1.5 0 0 0 1.342.829h3.218a1.5 1.5 0 0 0 1.342-.83l.256-.512a3 3 0 0 1 2.684-1.658h2.844Z'
                  clipRule='evenodd'
                />
              </svg>
            }
            title={"Sản phẩm"}
            items={[{ name: "Tất cả sản phẩm", path: "admin/products" }]}
          ></ButtonCollapse>

          <ButtonCollapse
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6 text-gray-600'
              >
                <path
                  fillRule='evenodd'
                  d='M18.685 19.097A9.723 9.723 0 0 0 21.75 12c0-5.385-4.365-9.75-9.75-9.75S2.25 6.615 2.25 12a9.723 9.723 0 0 0 3.065 7.097A9.716 9.716 0 0 0 12 21.75a9.716 9.716 0 0 0 6.685-2.653Zm-12.54-1.285A7.486 7.486 0 0 1 12 15a7.486 7.486 0 0 1 5.855 2.812A8.224 8.224 0 0 1 12 20.25a8.224 8.224 0 0 1-5.855-2.438ZM15.75 9a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0Z'
                  clipRule='evenodd'
                />
              </svg>
            }
            title={"Khách hàng"}
            items={[{ name: "Tất cả khách hàng", path: "admin/customers" }]}
          ></ButtonCollapse>

          <ButtonCollapse
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6 text-red-500'
              >
                <path d='M9.375 3a1.875 1.875 0 0 0 0 3.75h1.875v4.5H3.375A1.875 1.875 0 0 1 1.5 9.375v-.75c0-1.036.84-1.875 1.875-1.875h3.193A3.375 3.375 0 0 1 12 2.753a3.375 3.375 0 0 1 5.432 3.997h3.943c1.035 0 1.875.84 1.875 1.875v.75c0 1.036-.84 1.875-1.875 1.875H12.75v-4.5h1.875a1.875 1.875 0 1 0-1.875-1.875V6.75h-1.5V4.875C11.25 3.839 10.41 3 9.375 3ZM11.25 12.75H3v6.75a2.25 2.25 0 0 0 2.25 2.25h6v-9ZM12.75 12.75v9h6.75a2.25 2.25 0 0 0 2.25-2.25v-6.75h-9Z' />
              </svg>
            }
            title={"Khuyến mãi"}
            items={[{ name: "Tất cả khuyến mãi", path: "admin/promotions" }]}
          ></ButtonCollapse>

          <ButtonCollapse
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6 text-ct-green-400'
              >
                <path
                  fillRule='evenodd'
                  d='M2.25 2.25a.75.75 0 0 0 0 1.5H3v10.5a3 3 0 0 0 3 3h1.21l-1.172 3.513a.75.75 0 0 0 1.424.474l.329-.987h8.418l.33.987a.75.75 0 0 0 1.422-.474l-1.17-3.513H18a3 3 0 0 0 3-3V3.75h.75a.75.75 0 0 0 0-1.5H2.25Zm6.54 15h6.42l.5 1.5H8.29l.5-1.5Zm8.085-8.995a.75.75 0 1 0-.75-1.299 12.81 12.81 0 0 0-3.558 3.05L11.03 8.47a.75.75 0 0 0-1.06 0l-3 3a.75.75 0 1 0 1.06 1.06l2.47-2.47 1.617 1.618a.75.75 0 0 0 1.146-.102 11.312 11.312 0 0 1 3.612-3.321Z'
                  clipRule='evenodd'
                />
              </svg>
            }
            title={"Báo cáo"}
            items={[{ name: "Doanh thu, lợi nhuận", path: "admin/reports" }]}
          ></ButtonCollapse>

          <ButtonCollapse
            icon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6 text-purple-500'
              >
                <path
                  fillRule='evenodd'
                  d='M4.848 2.771A49.144 49.144 0 0 1 12 2.25c2.43 0 4.817.178 7.152.52 1.978.292 3.348 2.024 3.348 3.97v6.02c0 1.946-1.37 3.678-3.348 3.97a48.901 48.901 0 0 1-3.476.383.39.39 0 0 0-.297.17l-2.755 4.133a.75.75 0 0 1-1.248 0l-2.755-4.133a.39.39 0 0 0-.297-.17 48.9 48.9 0 0 1-3.476-.384c-1.978-.29-3.348-2.024-3.348-3.97V6.741c0-1.946 1.37-3.68 3.348-3.97ZM6.75 8.25a.75.75 0 0 1 .75-.75h9a.75.75 0 0 1 0 1.5h-9a.75.75 0 0 1-.75-.75Zm.75 2.25a.75.75 0 0 0 0 1.5H12a.75.75 0 0 0 0-1.5H7.5Z'
                  clipRule='evenodd'
                />
              </svg>
            }
            title={"Bình luận"}
            items={[{ name: "Kiểm duyệt bình luận", path: "admin/comments" }]}
          ></ButtonCollapse>
        </div>

        <div className='flex-1 overflow-auto bg-gray-100 px-5 py-3'>
          <Outlet />
        </div>
      </div>
    </div>
  )
}

export default Home
