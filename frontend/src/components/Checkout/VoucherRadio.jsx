/* eslint-disable react/prop-types */
import { FormControlLabel, Radio, RadioGroup } from "@mui/material"
import { green } from "@mui/material/colors"

import DiscountItem from "../DiscountItem"

const VoucherRadio = ({ options, onChange, value }) => {
  return (
    <div>
      <RadioGroup className='gap-3' onChange={onChange} value={value}>
        {options.map((option, index) => (
          <FormControlLabel
            key={index}
            value={JSON.stringify(option)}
            control={
              <Radio
                sx={{
                  color: green[400],
                  "&.Mui-checked": {
                    color: green[600],
                  },
                }}
              />
            }
            label={
              <div className='w-full min-w-80'>
                <DiscountItem obj={option} />
              </div>
            }
          />
        ))}
      </RadioGroup>
    </div>
  )
}

export default VoucherRadio
