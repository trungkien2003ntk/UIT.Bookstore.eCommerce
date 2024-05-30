import { useContext } from "react"
import { ToastContext } from "../contexts/ToastContext"

const useToast = () => {
  return useContext(ToastContext)
}

export default useToast
