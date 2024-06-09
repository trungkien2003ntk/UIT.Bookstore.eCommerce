import { Routes, Route } from "react-router-dom"

import Login from "./pages/Admin/login"
import Home from "./pages/Admin/home"
import Chat from "./pages/Admin/chat"
import Reports from "./pages/Admin/reports"
import Account from "./pages/Admin/account"
import Unauthorized from "./pages/Admin/unauthorized"
import Orders from "./pages/Admin/orders"
import Products from "./pages/Admin/products"
import OrderDetail from "./pages/Admin/order-detail"
import ProductDetail from "./pages/Admin/product-detail"
import Customers from "./pages/Admin/customers"
import CustomerDetail from "./pages/Admin/customer-detail"
import Promotions from "./pages/Admin/promotions"
import PromotionDetail from "./pages/Admin/promotion-detail"

import { default as UserHome } from "./pages/User/home"
import { default as UserLogin } from "./pages/User/login"
import { default as UserSignUp } from "./pages/User/sign-up"
import { default as UserForgotPassword } from "./pages/User/forgot-password"
import { default as UserAccount } from "./pages/User/account"
import { default as UserAddress } from "./pages/User/address"
import { default as UserChangePassword } from "./pages/User/change-password"
import { default as UserNotiSettings } from "./pages/User/noti-settings"
import { default as UserDeleteAccount } from "./pages/User/delete-account"
import { default as UserProfile } from "./pages/User/profile"
import { default as UserOrders } from "./pages/User/orders"
import { default as UserNotifications } from "./pages/User/notifications"
import { default as UserVouchers } from "./pages/User/vouchers"
import { default as UserCoin } from "./pages/User/coin"
import { default as UserCatalog } from "./pages/User/catalog"
import { default as UserProductDetail } from "./pages/User/product-detail"
import { default as UserCart } from "./pages/User/cart"
import { default as UserCheckOut } from "./pages/User/checkout"
import { default as UserOrderDetail } from "./pages/User/order-detail"

import Layout from "./pages/Layout"
import UserLayout from "./components/User/UserLayout"
import RequireAuth from "./components/RequireAuth"

import Missing from "./pages/missing"

function App() {
  return (
    <Routes>
      <Route path='/' element={<Layout />}>
        {/* ADMIN */}
        {/* public routes */}
        <Route path='admin/login' element={<Login />} />
        <Route path='admin/unauthorized' element={<Unauthorized />} />

        {/* protected routes */}
        <Route element={<RequireAuth allowedRoles={["admin"]} />}></Route>

        <Route element={<Home />}>
          <Route path='admin/reports' element={<Reports />} />
          <Route path='admin/orders' element={<Orders />} />
          <Route path='admin/orders/:id' element={<OrderDetail />} />
          <Route path='admin/products' element={<Products />} />
          <Route path='admin/products/:id' element={<ProductDetail />} />
          <Route path='admin/customers' element={<Customers />} />
          <Route path='admin/customers/:id' element={<CustomerDetail />} />
          <Route path='admin/promotions' element={<Promotions />} />
          <Route path='admin/promotions/:id' element={<PromotionDetail />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={["admin", "staff"]} />}>
          <Route path='admin/chat' element={<Chat />} />
          <Route path='admin/account' element={<Account />} />
        </Route>

        {/* USER */}
        {/* public routes */}
        <Route element={<UserLayout />}>
          <Route path='/' element={<UserHome />} />

          <Route path='user/catalog' element={<UserCatalog />} />

          <Route
            path='user/product-detail/:id'
            element={<UserProductDetail />}
          />

          <Route path='user/orders/:id' element={<UserOrderDetail />} />

          <Route path='user/cart' element={<UserCart />} />
          <Route path='user/checkout' element={<UserCheckOut />} />

          <Route path='user/login' element={<UserLogin />} />
          <Route path='user/sign-up' element={<UserSignUp />} />
          <Route path='user/forgot-password' element={<UserForgotPassword />} />

          <Route element={<UserAccount />}>
            {/* user account */}
            <Route path='user/account/profile' element={<UserProfile />} />
            <Route path='user/account/address' element={<UserAddress />} />
            <Route
              path='user/account/change-password'
              element={<UserChangePassword />}
            />
            <Route
              path='user/account/noti-settings'
              element={<UserNotiSettings />}
            />
            <Route
              path='user/account/delete-account'
              element={<UserDeleteAccount />}
            />

            {/* user orders */}
            <Route path='user/orders' element={<UserOrders />} />

            {/* user notifications */}
            <Route path='user/notifications' element={<UserNotifications />} />

            {/* vouchers */}
            <Route path='user/vouchers' element={<UserVouchers />} />

            {/* coin */}
            <Route path='user/coin' element={<UserCoin />} />
          </Route>
        </Route>

        {/* catch all */}
        <Route path='*' element={<Missing />} />
      </Route>
    </Routes>
  )
}

export default App
