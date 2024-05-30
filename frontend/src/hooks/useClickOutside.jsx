import { useEffect } from "react"

const useClickOutside = (ref, callback) => {
  useEffect(() => {
    function handleClickOutside(event) {
      if (ref.current && !ref.current.contains(event.target)) {
        callback()
      }
    }

    // Bind the event listener
    document.addEventListener("mousedown", handleClickOutside)

    return () => {
      // Unbind the event listener on clean up
      document.removeEventListener("mousedown", handleClickOutside)
    }
  }, [ref, callback])
}

export default useClickOutside
