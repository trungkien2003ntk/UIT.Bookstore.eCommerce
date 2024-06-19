/* eslint-disable react/prop-types */
import PropTypes from "prop-types"

const VND = ({ number, className }) => {
  return <div className={className}>{number.toLocaleString() + "₫"}</div>
}

VND.propTypes = {
  number: PropTypes.number.isRequired,
  className: PropTypes.string,
}

export default VND
