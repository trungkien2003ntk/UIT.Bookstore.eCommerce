/* eslint-disable react/prop-types */
import { useLocation, Navigate, Outlet } from "react-router-dom"
import useAuth from "../hooks/useAuth"

const RequireAuth = ({ allowedRoles }) => {
  const { auth } = useAuth()
  const location = useLocation()

  return allowedRoles.includes(auth?.roles) ? (
    <Outlet />
  ) : auth?.user ? (
    <Navigate to='admin/unauthorized' state={{ from: location }} replace />
  ) : (
    <Navigate to='admin/login' state={{ from: location }} replace />
  )
}

export default RequireAuth
