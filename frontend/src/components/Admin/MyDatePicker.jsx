import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs"
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider"
import { DatePicker } from "@mui/x-date-pickers/DatePicker"
import "dayjs/locale/vi"

export default function MyDatePicker({ ...props }) {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='vi'>
      <DatePicker {...props} />
    </LocalizationProvider>
  )
}
