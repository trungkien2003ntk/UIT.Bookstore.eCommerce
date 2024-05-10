import { Routes, Route } from "react-router-dom"

import Login from "./pages/Admin/login"
import Home from "./pages/Admin/home"
import Chat from "./pages/Admin/chat"
import Reports from "./pages/Admin/reports"
import Account from "./pages/Admin/account"
import Unauthorized from "./pages/Admin/unauthorized"

import { default as UserHome } from "./pages/User/home"

import Layout from "./components/Layout"
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
        <Route element={<RequireAuth allowedRoles={["admin"]} />}>
          <Route path='admin/reports' element={<Reports />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={["admin", "staff"]} />}>
          <Route path='admin/' element={<Home />} />
          <Route path='admin/chat' element={<Chat />} />
          <Route path='admin/account' element={<Account />} />
        </Route>

        {/* USER */}
        {/* public routes */}
        <Route path='/' element={<UserHome />} />

        {/* catch all */}
        <Route path='*' element={<Missing />} />
      </Route>
    </Routes>
  )
}

export default App
