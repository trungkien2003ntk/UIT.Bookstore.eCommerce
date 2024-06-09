import { useState } from "react"
import { useNavigate } from "react-router-dom"
import dayjs from "dayjs"

import MyDatePicker from "../../components/Admin/MyDatePicker"
import DataTable from "../../components/Admin/DataTable"
import CellItem from "../../components/Admin/CellItem"
import Button from "../../components/Button"
import VND from "../../components/vnd"

const columns = [
  { field: "id", headerName: "Mã khách hàng", width: 130 },
  {
    field: "name",
    headerName: "Tên khách hàng",
    width: 200,
  },
  {
    field: "phoneNumber",
    headerName: "Số điện thoại",
    width: 200,
  },
  {
    field: "isActived",
    headerName: "Trạng Thái",
    width: 200,
    renderCell: (row) => {
      return (
        <CellItem
          type={row.value}
          text={row.value ? "Đang giao dịch" : "Ngừng giao dịch"}
          iconCheck={row.value}
          iconX={!row.value}
        />
      )
    },
  },
  {
    field: "totalPayment",
    headerName: "Tổng chi tiêu",
    width: 150,
    renderCell: (row) => {
      return (
        <VND className={`font-medium text-ct-green-400`} number={row.value} />
      )
    },
  },
  {
    field: "totalOrder",
    headerName: "Tổng đơn hàng",
    width: 150,
  },
]

const object = {
  items: [
    {
      id: "cus0001",
      name: "Nguyễn Văn A",
      phoneNumber: "0236987789",
      isActived: true,
      totalPayment: 250000,
      totalOrder: 10,
    },
    {
      id: "cus0002",
      name: "Nguyễn Văn B",
      phoneNumber: "0123456789",
      isActived: true,
      totalPayment: 7000000,
      totalOrder: 79,
    },
    {
      id: "cus0003",
      name: "Nguyễn Văn C",
      phoneNumber: "0558997452",
      isActived: false,
      totalPayment: 0,
      totalOrder: 0,
    },
    {
      id: "cus0004",
      name: "Nguyễn Văn D",
      phoneNumber: "0835489396",
      isActived: true,
      totalPayment: 1200000,
      totalOrder: 5,
    },
  ],
}

const rows = object.items

const Customers = () => {
  const navigate = useNavigate()

  const [startDate, setStartDate] = useState(dayjs())
  const [endDate, setEndDate] = useState()

  return (
    <div className='flex flex-col gap-3 overflow-hidden'>
      <div className='flex items-center justify-between rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Quản lý khách hàng
        </div>
      </div>

      <DataTable
        height={500}
        columns={columns}
        rows={rows}
        checkboxSelection
        onRowClick={(value) => {
          navigate(`/admin/customers/${value.id}`)
        }}
      />
    </div>
  )
}

export default Customers
