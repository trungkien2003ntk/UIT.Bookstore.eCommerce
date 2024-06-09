import { useState } from "react"
import { Divider } from "@mui/material"
import ChartComp from "../../components/Admin/ChartComp"
import Dropdown from "../../components/Dropdown"
import Button from "../../components/Button"
import MyDatePicker from "../../components/Admin/MyDatePicker"
import dayjs from "dayjs"

const thousandBreakOptions = {
  scales: {
    y: {
      ticks: {
        callback(value) {
          return Number(value).toLocaleString("en")
        },
      },
    },
  },
}

const topProductsOptions = {
  plugins: {
    legend: {
      position: "right",
    },
  },
  indexAxis: "y",
  scales: {
    x: {
      ticks: {
        callback(value) {
          return Number(value).toLocaleString("en")
        },
      },
    },
  },
}

const types = [
  {
    text: "Biểu đồ cột",
    value: "bar",
  },
  {
    text: "Biểu đồ đường",
    value: "line",
  },
]

const typesTime = [
  {
    text: "Ngày",
    value: "day",
  },
  {
    text: "Tháng",
    value: "month",
  },
  {
    text: "Năm",
    value: "year",
  },
]

const Reports = () => {
  const [chartType, setChartType] = useState(types[0])
  const [typeTime, setTypeTime] = useState(typesTime[0])

  const [moneyLabels, setMoneyLabels] = useState(["28/10/2002"])
  const [invests, setInvests] = useState([320000])
  const [profits, setProfits] = useState([500000])
  const [revenues, setRevenues] = useState([100000])
  const moneyDatasets = [
    {
      label: "Doanh thu",
      data: revenues,
    },
    {
      label: "Lợi nhuận",
      data: profits,
    },
    {
      label: "Tiền vốn",
      data: invests,
    },
  ]

  const [startDate, setStartDate] = useState(dayjs())
  const [endDate, setEndDate] = useState(dayjs())

  const [orderCountsLabels, setOrderCountsLabels] = useState(["28/10/2006"])

  const [orderCounts, setOrderCounts] = useState([32])

  const orderCountsDatasets = [
    {
      label: "Số lượng",
      data: orderCounts,
      backgroundColor: "#34c9a2",
      borderColor: "#34c9a2",
    },
  ]

  const [topProductsLabels, setTopProductsLabels] = useState([
    "Dế mèn phiêu lưu ký",
  ])
  const [topProductsQuantity, setTopProductsQuantity] = useState([12])

  const topProductsDatasets = [
    {
      label: "Số lượng",
      data: topProductsQuantity,
      backgroundColor: "#3a57e8",
      borderColor: "#3a57e8",
    },
  ]

  return (
    <div className='flex flex-col gap-5'>
      <div className='rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>Báo cáo</div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>HÔM NAY</div>
        <Divider />
        <div className='flex justify-between gap-4'>
          <div
            className='flex flex-1 flex-col gap-2 rounded-md bg-gradient-to-r
          from-indigo-500 to-indigo-600 p-5 font-bold text-white'
          >
            <div className='text-lg'>Số lượng đơn hàng</div>
            <div className='text-3xl'>325</div>
          </div>

          <div className='flex gap-5'>
            <div
              className='flex flex-col gap-2 rounded-md bg-gradient-to-r
            from-blue-500 to-blue-600 p-5 font-bold text-white'
            >
              <div className='text-lg'>Doanh thu</div>
              <div className='text-3xl'>
                {Number(320000).toLocaleString() + "₫"}
              </div>
            </div>
            <div
              className='flex flex-col gap-2 rounded-md bg-gradient-to-r
            from-orange-500 to-orange-600 p-5 font-bold text-white'
            >
              <div className='text-lg'>Lợi nhuận</div>
              <div className='text-3xl'>
                {Number(320000).toLocaleString() + "₫"}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>
          BÁO CÁO DOANH THU, LỢI NHUẬN, TIỀN VỐN
        </div>
        <Divider />
        <div className='flex flex-col'>
          <div className='mb-5 flex items-center gap-3'>
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
            <Dropdown
              selected={typeTime}
              setSelected={(value) => setTypeTime(value)}
              placeholder={"Nhóm theo"}
              items={typesTime}
            ></Dropdown>

            <Dropdown
              selected={chartType}
              setSelected={(value) => setChartType(value)}
              placeholder={"Loại biểu đồ"}
              items={types}
            ></Dropdown>

            <Button>Xem</Button>
          </div>

          <div className='min-h-[500px]'>
            <ChartComp
              type={chartType.value}
              labels={moneyLabels}
              datasets={moneyDatasets}
              options={thousandBreakOptions}
            />
          </div>
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>SỐ LƯỢNG ĐƠN HÀNG</div>
        <Divider />
        <div className='flex flex-col'>
          <div className='mb-5 flex items-center gap-3'>
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
            <Dropdown
              selected={typeTime}
              setSelected={(value) => setTypeTime(value)}
              placeholder={"Nhóm theo"}
              items={typesTime}
            ></Dropdown>

            <Button>Xem</Button>
          </div>

          <div className='min-h-[500px]'>
            <ChartComp
              type={"bar"}
              labels={orderCountsLabels}
              datasets={orderCountsDatasets}
              options={thousandBreakOptions}
            />
          </div>
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>
          TOP 10 SẢN PHẨM BÁN CHẠY THÁNG NÀY
        </div>
        <Divider />
        <div className='flex flex-col'>
          <div className='min-h-[500px]'>
            <ChartComp
              type={"bar"}
              labels={topProductsLabels}
              datasets={topProductsDatasets}
              options={topProductsOptions}
            />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Reports
