/* eslint-disable react/prop-types */
import { Divider, FormControlLabel, Radio, RadioGroup } from "@mui/material"
import { green } from "@mui/material/colors"

const AddressRadio = ({ options, onChange, value, onUpdate }) => {
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
              <div className='flex flex-col justify-center'>
                <div className='flex items-center gap-2'>
                  <div className='font-bold text-ct-black-300'>
                    {option.name}
                  </div>
                  <Divider orientation='vertical' flexItem />

                  <div>{option.phone}</div>

                  <Divider orientation='vertical' flexItem />

                  <div
                    onClick={onUpdate}
                    className='text-blue-500 hover:cursor-pointer'
                  >
                    Cập nhật
                  </div>
                </div>

                <div className='line-clamp-2'>{option.addressDetail}</div>

                <div className='mb-3'>
                  {option.commune +
                    ", " +
                    option.district +
                    ", " +
                    option.province}
                </div>

                <Divider />
              </div>
            }
          />
        ))}
      </RadioGroup>
    </div>
  )
}

export default AddressRadio
