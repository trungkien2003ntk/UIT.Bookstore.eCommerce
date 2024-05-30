/* eslint-disable react/prop-types */
import { Link } from "react-router-dom"
import CircularProgress from "@mui/material/CircularProgress"

const Button = ({
  to,
  href,
  children,
  className,
  leftIcon,
  rightIcon,
  onClick,
  disabled = false,
  progressing = false,
  ...passProps
}) => {
  let Comp = "button"

  const props = {
    onClick,
    disabled,
    ...passProps,
  }

  if (disabled) {
    delete props.onClick
  }

  if (to) {
    props.to = to
    Comp = Link
  } else if (href) {
    props.href = href
    Comp = "a"
  }

  return (
    <Comp
      className={`flex items-center justify-center gap-x-2 rounded bg-ct-green-400 px-4 py-2 text-white 
      hover:bg-green-600 ${className}`}
      {...props}
    >
      {progressing || (
        <div>
          {leftIcon && <span className=''>{leftIcon}</span>}
          <span className=''>{children}</span>
          {rightIcon && <span className=''>{rightIcon}</span>}
        </div>
      )}

      {progressing && (
        <div className='h-6 w-6'>
          <CircularProgress sx={{ color: "white" }} size={24} thickness={5} />
        </div>
      )}
    </Comp>
  )
}

export default Button
