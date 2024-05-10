import { Outlet } from "react-router-dom"

const Layout = () => {
  return (
    <main className='App font-Inter'>
      <Outlet />
    </main>
  )
}

export default Layout
