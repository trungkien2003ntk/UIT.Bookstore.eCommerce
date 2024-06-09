/* eslint-disable no-loss-of-precision */
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import dayjs from "dayjs"

import MyDatePicker from "../../components/Admin/MyDatePicker"
import DataTable from "../../components/Admin/DataTable"
import CellItem from "../../components/Admin/CellItem"
import Button from "../../components/Button"
import { convertToGMT7 } from "../../components/helpers/convertDate"

const columns = [
  { field: "id", headerName: "Mã khuyến mãi", width: 100 },
  {
    field: "name",
    sortable: false,
    headerName: "Tên khuyến mãi",
    width: 200,
  },
  {
    field: "type",
    headerName: "Loại khuyến mãi",
    width: 100,
    renderCell: (value) => {
      return (
        <CellItem
          type={"ProductType"}
          text={value.row.type === "order" ? "Đơn hàng" : "Vận chuyển"}
        />
      )
    },
  },
  {
    field: "usageLimitOverall",
    headerName: "Số phiếu còn lại",
    width: 150,
    headerAlign: "center",
    align: "center",
    renderCell: (value) => {
      return value.row.usageLimitOverall - value.row.usageCount
    },
  },
  {
    field: "startDate",
    headerName: "Ngày bắt đầu",
    width: 200,
    valueGetter: (value) => {
      return convertToGMT7(value)
    },
  },
  {
    field: "endDate",
    headerName: "Ngày kết thúc",
    width: 200,
    valueGetter: (value) => {
      return convertToGMT7(value)
    },
  },
  {
    field: "usageCount",
    headerName: "Trạng thái",
    headerAlign: "center",
    align: "center",
    width: 200,
    renderCell: (value) => {
      const isExpired =
        dayjs(value.row.endDate).isBefore(dayjs()) ||
        value.row.usageLimitOverall === value.row.usageCount

      return (
        <CellItem
          text={isExpired ? "Hết hiệu lực" : "Còn hiệu lực"}
          iconCheck={!isExpired}
          iconX={isExpired}
        />
      )
    },
  },
]

const object = {
  items: [
    {
      type: "shipping",
      name: "Giảm giá 80%, tối đa ₫30k",
      code: "SHIP10P100K1",
      valueType: 1,
      value: 0.8,
      maximumDiscountValue: 30000,
      minimumSpend: 59000,
      startDate: "2024-06-01T07:00:00+07:00",
      endDate: "2024-06-30T23:59:59+07:00",
      usageLimitOverall: 150,
      usageLimitPerUser: 1,
      usageCount: 150,
      usedPercentage: 0.0066666666666666666666666667,
      isRedeemable: true,
      status: "running",
      id: 3,
    },
    {
      type: "shipping",
      name: "Giảm giá ₫15k phí vận chuyển",
      code: "SHIP50K1",
      valueType: 0,
      value: 15000,
      maximumDiscountValue: null,
      minimumSpend: 50000,
      startDate: "2024-06-01T07:00:00+07:00",
      endDate: "2024-06-30T23:59:59+07:00",
      usageLimitOverall: 300,
      usageLimitPerUser: 2,
      usageCount: 1,
      usedPercentage: 0.0033333333333333333333333333,
      isRedeemable: true,
      status: "canceled",
      id: 4,
    },
    {
      type: "order",
      name: "Giảm giá 15%, tối đa ₫500k",
      code: "VOUCHER15P500K1",
      valueType: 1,
      value: 0.15,
      maximumDiscountValue: 500000,
      minimumSpend: 100000,
      startDate: "2024-06-01T07:00:00+07:00",
      endDate: "2024-06-30T23:59:59+07:00",
      usageLimitOverall: 100,
      usageLimitPerUser: 1,
      usageCount: 100,
      usedPercentage: 0.01,
      isRedeemable: true,
      status: "stopped",
      id: 1,
    },
    {
      type: "order",
      name: "Giảm giá ₫50k",
      code: "VOUCHER50K1",
      valueType: 0,
      value: 50000,
      maximumDiscountValue: null,
      minimumSpend: 500000,
      startDate: "2024-06-01T07:00:00+07:00",
      endDate: "2024-06-30T23:59:59+07:00",
      usageLimitOverall: 200,
      usageLimitPerUser: 1,
      usageCount: 1,
      usedPercentage: 0.005,
      isRedeemable: false,
      status: "running",
      id: 2,
    },
  ],
}

const rows = object.items

const Promotions = () => {
  const navigate = useNavigate()

  return (
    <div className='flex flex-col gap-3 overflow-hidden'>
      <div className='flex items-center justify-between rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Quản lý khuyến mãi
        </div>

        <div className='flex gap-5'>
          <Button
            leftIcon={
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6'
              >
                <path
                  fillRule='evenodd'
                  d='M12 3.75a.75.75 0 0 1 .75.75v6.75h6.75a.75.75 0 0 1 0 1.5h-6.75v6.75a.75.75 0 0 1-1.5 0v-6.75H4.5a.75.75 0 0 1 0-1.5h6.75V4.5a.75.75 0 0 1 .75-.75Z'
                  clipRule='evenodd'
                />
              </svg>
            }
          >
            Thêm khuyến mãi
          </Button>
        </div>
      </div>

      <DataTable
        height={500}
        columns={columns}
        rows={rows}
        checkboxSelection
        onRowClick={(value) => {}}
      />
    </div>
  )
}

export default Promotions
