import { Link, useNavigate } from "react-router-dom"
import useAuth from "../../hooks/useAuth"

const Home = () => {
  const { setAuth } = useAuth()
  const navigate = useNavigate()

  const SignOut = () => {
    setAuth({})
    navigate("/admin/login")
  }

  return (
    <div className='flex flex-col'>
      <Link to='/admin/reports'>REPORTS</Link>
      <Link to='/admin/chat'>CHAT</Link>
      <Link to='/admin/account'>ACCOUNT</Link>

      <button onClick={SignOut}>Sign out</button>
    </div>
  )
}

export default Home
