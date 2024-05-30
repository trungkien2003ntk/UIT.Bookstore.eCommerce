/* eslint-disable react/prop-types */

const VND = ({ number, className }) => {
  return <div className={className}>{number.toLocaleString() + "₫"}</div>
}

export default VND
