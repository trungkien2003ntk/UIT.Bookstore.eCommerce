import { useState } from "react"
import { Divider } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"
import Dropdown from "../../components/Dropdown"
import MyDatePicker from "../../components/Admin/MyDatePicker"
import dayjs from "dayjs"

const status = [
  {
    text: "Voucher",
  },
  {
    text: "Giảm giá vận chuyển",
  },
]

const PromotionDetail = () => {
  const [selected, setSelected] = useState(status[0])

  const [startDate, setStartDate] = useState(dayjs())
  const [endDate, setEndDate] = useState(dayjs())

  return (
    <div className='flex flex-col gap-5'>
      <div className='flex items-center justify-between rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Thông tin khuyến mãi
        </div>

        <Dropdown
          selected={selected}
          setSelected={(value) => setSelected(value)}
          placeholder={"Trạng thái"}
          items={status}
        ></Dropdown>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Thông tin khuyến mãi</div>
        <Divider />
        <div className='flex flex-col gap-5'>
          <Input
            placeholder={"Tên khuyến mãi"}
            title={"Tên khuyến mãi"}
            type='text'
            value={"Khuyến mãi 50%"}
          />
          <Input
            placeholder={"Số lượng mã"}
            title={"Số lượng mã"}
            type='text'
            value={100}
          />
          <Input
            placeholder={"Phần trăm giảm giá"}
            title={"Phần trăm giảm giá"}
            type='text'
            value={"15%"}
          />
          <Input
            placeholder={"Đơn tối thiểu"}
            title={"Đơn tối thiểu"}
            type='text'
            value={Number(50000).toLocaleString() + "₫"}
          />
          <Input
            placeholder={"Giảm tối đa"}
            title={"Giảm tối đa"}
            type='text'
            value={Number(20000).toLocaleString() + "₫"}
          />
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Thời gian</div>
        <Divider />
        <div className='flex flex-col gap-4'>
          <MyDatePicker
            label='Ngày bắt đầu'
            value={startDate}
            maxDate={endDate}
            onChange={(newValue) => setStartDate(newValue)}
          />
          <MyDatePicker
            label='Ngày kết thúc'
            value={endDate}
            onChange={(newValue) => setEndDate(newValue)}
          />
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Lưu lượng</div>
        <Divider />
        <div className='flex flex-col gap-4'>
          <div className='flex items-center gap-1'>
            <div className='font-medium text-ct-green-400'>Đã sử dụng:</div>
            <div>12</div>
            <div className='ml-5 rounded bg-red-100 px-2 py-1 font-medium text-red-600'>
              12%
            </div>
          </div>
        </div>
      </div>

      <div className='mb-10 flex items-center justify-end'>
        <Button className={`min-w-52`}>Lưu</Button>
      </div>
    </div>
  )
}

export default PromotionDetail
