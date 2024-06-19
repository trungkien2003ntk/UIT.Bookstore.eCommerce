import { useState } from "react"
import { useNavigate } from "react-router-dom"
import dayjs from "dayjs"

import MyDatePicker from "../../components/Admin/MyDatePicker"
import DataTable from "../../components/Admin/DataTable"
import CellItem from "../../components/Admin/CellItem"
import { convertToGMT7 } from "../../components/helpers/convertDate"
import VND from "../../components/vnd"

const columns = [
  { field: "orderNumber", headerName: "Mã đơn hàng", width: 130 },
  {
    field: "orderWhen",
    headerName: "Ngày tạo đơn",
    width: 200,
    valueGetter: (value) => {
      return convertToGMT7(value)
    },
  },
  {
    field: "status",
    headerName: "Trạng Thái",
    width: 200,
    renderCell: (row) => {
      return (
        <CellItem
          type={row.value}
          text={
            row.value === "Pending"
              ? "Chờ xác nhận"
              : row.value === "Processing"
                ? "Đang gói hàng"
                : row.value === "Shipped"
                  ? "Đang giao hàng"
                  : row.value === "Delivered"
                    ? "Đã giao hàng"
                    : "Đã hủy"
          }
          iconCheck={row.value === "Delivered"}
          iconX={row.value === "Cancelled"}
          iconLoad={row.value === "Shipped"}
        />
      )
    },
  },
  {
    field: "total",
    headerName: "Thành tiền",
    width: 200,
    renderCell: (row) => {
      return (
        <VND className={`font-medium text-ct-green-400`} number={row.value} />
      )
    },
  },
]

const object = {
  items: [
    {
      orderNumber: "2024-6-7/aaca3",
      dueWhen: null,
      expectedDeliveryWhen: "2024-06-08T23:59:59+00:00",
      subtotal: 180000,
      total: 158060,
      taxRate: 0,
      comment: "Let's be careful",
      deliveryInstruction: "",
      customerId: 4,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Processing",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-06-07T23:58:29.3661944+07:00",
      orderLines: [
        {
          orderId: 32,
          skuId: 11,
          skuOptionName: "",
          productName:
            "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (Chuẩn) (Không Tin Học)",
          thumbnailUrl: "",
          recommendedRetailPrice: 180000,
          unitPrice: 180000,
          quantity: 1,
          total: 180000,
          id: 50,
        },
      ],
      id: 32,
    },
    {
      orderNumber: "2024-6-6/58c18",
      dueWhen: null,
      expectedDeliveryWhen: "2024-06-07T23:59:59+00:00",
      subtotal: 94500,
      total: 94500,
      taxRate: 0,
      comment: "Please Be Careful",
      deliveryInstruction: null,
      customerId: 4,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Processing",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-06-06T19:14:56.4054007+07:00",
      orderLines: [
        {
          orderId: 24,
          skuId: 2,
          skuOptionName: "Một ngày của tớ và mẹ",
          productName: "Gia Đình Thương Yêu",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg",
          recommendedRetailPrice: 35000,
          unitPrice: 31500,
          quantity: 3,
          total: 94500,
          id: 43,
        },
      ],
      id: 24,
    },
    {
      orderNumber: "2024-6-6/cc4d3",
      dueWhen: null,
      expectedDeliveryWhen: "2024-06-07T23:59:59+00:00",
      subtotal: 94500,
      total: 94500,
      taxRate: 0,
      comment: "Please Be Careful",
      deliveryInstruction: null,
      customerId: 4,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Pending",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-06-06T19:11:18.916681+07:00",
      orderLines: [
        {
          orderId: 23,
          skuId: 2,
          skuOptionName: "Một ngày của tớ và mẹ",
          productName: "Gia Đình Thương Yêu",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367653.jpg",
          recommendedRetailPrice: 35000,
          unitPrice: 31500,
          quantity: 3,
          total: 94500,
          id: 42,
        },
      ],
      id: 23,
    },
    {
      orderNumber: "2024-6-6/aff49",
      dueWhen: null,
      expectedDeliveryWhen: "2024-06-07T23:59:59+00:00",
      subtotal: 63000,
      total: 63000,
      taxRate: 0,
      comment: "Please Be Careful",
      deliveryInstruction: null,
      customerId: 4,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Pending",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-06-06T19:09:17.7442125+07:00",
      orderLines: [
        {
          orderId: 22,
          skuId: 3,
          skuOptionName: "Một ngày của tớ và ông",
          productName: "Gia Đình Thương Yêu",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg",
          recommendedRetailPrice: 35000,
          unitPrice: 31500,
          quantity: 2,
          total: 63000,
          id: 41,
        },
      ],
      id: 22,
    },
    {
      orderNumber: "2024-6-6/b2d74",
      dueWhen: null,
      expectedDeliveryWhen: "2024-06-07T23:59:59+00:00",
      subtotal: 112500,
      total: 112500,
      taxRate: 0,
      comment: "Please Be Careful",
      deliveryInstruction: null,
      customerId: 4,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Pending",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-06-06T19:05:22.05348+07:00",
      orderLines: [
        {
          orderId: 21,
          skuId: 1,
          skuOptionName: "Một ngày của tớ và bố",
          productName: "Gia Đình Thương Yêu",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367646.jpg",
          recommendedRetailPrice: 35000,
          unitPrice: 31500,
          quantity: 3,
          total: 94500,
          id: 39,
        },
        {
          orderId: 21,
          skuId: 5,
          skuOptionName: "Tập 1 - Mùa Xuân",
          productName: "Tô Màu Bốn Mùa",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg",
          recommendedRetailPrice: 20000,
          unitPrice: 18000,
          quantity: 1,
          total: 18000,
          id: 40,
        },
      ],
      id: 21,
    },
    {
      orderNumber: "2022-2-27-7b23f",
      dueWhen: null,
      expectedDeliveryWhen: "2022-03-04T11:53:20.558742+07:00",
      subtotal: 198000,
      total: 228000,
      taxRate: 0,
      comment: "Careful",
      deliveryInstruction: "",
      customerId: 1,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán khi nhận hàng",
      status: "Pending",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2022-02-27T11:53:20.558742+07:00",
      orderLines: [
        {
          orderId: 6,
          skuId: 8,
          skuOptionName: "Tập 4 - Mùa Đông",
          productName: "Tô Màu Bốn Mùa",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294388.jpg",
          recommendedRetailPrice: 20000,
          unitPrice: 18000,
          quantity: 5,
          total: 90000,
          id: 12,
        },
        {
          orderId: 6,
          skuId: 5,
          skuOptionName: "Tập 1 - Mùa Xuân",
          productName: "Tô Màu Bốn Mùa",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg",
          recommendedRetailPrice: 20000,
          unitPrice: 18000,
          quantity: 6,
          total: 108000,
          id: 13,
        },
      ],
      id: 6,
    },
    {
      orderNumber: "2024-2-15-c75f4",
      dueWhen: null,
      expectedDeliveryWhen: "2024-02-20T09:36:58.867039+07:00",
      subtotal: 499500,
      total: 529500,
      taxRate: 0,
      comment: "Careful",
      deliveryInstruction: "",
      customerId: 1,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán khi nhận hàng",
      status: "Processing",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-02-15T09:36:58.867039+07:00",
      orderLines: [
        {
          orderId: 9,
          skuId: 6,
          skuOptionName: "Tập 2 - Mùa Hạ",
          productName: "Tô Màu Bốn Mùa",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294364.jpg",
          recommendedRetailPrice: 20000,
          unitPrice: 18000,
          quantity: 5,
          total: 90000,
          id: 17,
        },
        {
          orderId: 9,
          skuId: 4,
          skuOptionName: "Một ngày của tớ và bà",
          productName: "Gia Đình Thương Yêu",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367677.jpg",
          recommendedRetailPrice: 35000,
          unitPrice: 31500,
          quantity: 6,
          total: 189000,
          id: 18,
        },
        {
          orderId: 9,
          skuId: 3,
          skuOptionName: "Một ngày của tớ và ông",
          productName: "Gia Đình Thương Yêu",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8935212367660.jpg",
          recommendedRetailPrice: 35000,
          unitPrice: 31500,
          quantity: 7,
          total: 220500,
          id: 19,
        },
      ],
      id: 9,
    },
    {
      orderNumber: "2024-2-18-7040f",
      dueWhen: null,
      expectedDeliveryWhen: "2024-02-23T20:41:11.372231+07:00",
      subtotal: 2157900,
      total: 2187900,
      taxRate: 0,
      comment: "Careful",
      deliveryInstruction: "",
      customerId: 5,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng nhanh",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Processing",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2024-02-18T20:41:11.372231+07:00",
      orderLines: [
        {
          orderId: 12,
          skuId: 11,
          skuOptionName: "",
          productName:
            "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Học (Bộ 12 Cuốn) (Chuẩn) (Không Tin Học)",
          thumbnailUrl: "",
          recommendedRetailPrice: 180000,
          unitPrice: 180000,
          quantity: 9,
          total: 1620000,
          id: 25,
        },
        {
          orderId: 12,
          skuId: 10,
          skuOptionName: "",
          productName:
            "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (Chuẩn)",
          thumbnailUrl: "",
          recommendedRetailPrice: 170000,
          unitPrice: 170000,
          quantity: 3,
          total: 510000,
          id: 26,
        },
        {
          orderId: 12,
          skuId: 9,
          skuOptionName: "",
          productName:
            "Atlat Địa lí Việt Nam (Theo Chương Trình Giáo Dục Phổ Thông 2118)",
          thumbnailUrl: "",
          recommendedRetailPrice: 31000,
          unitPrice: 27900,
          quantity: 1,
          total: 27900,
          id: 27,
        },
      ],
      id: 12,
    },
    {
      orderNumber: "2022-2-6-d0d99",
      dueWhen: null,
      expectedDeliveryWhen: "2022-02-11T20:54:18.599744+07:00",
      subtotal: 1190000,
      total: 1220000,
      taxRate: 0,
      comment: "Careful",
      deliveryInstruction: "",
      customerId: 1,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng nhanh",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Pending",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2022-02-06T20:54:18.599744+07:00",
      orderLines: [
        {
          orderId: 14,
          skuId: 10,
          skuOptionName: "",
          productName:
            "Sách Giáo Khoa Bộ Lớp 7 - Chân Trời Sáng Tạo - Sách Bài Tập (Bộ 12 Cuốn) (Chuẩn)",
          thumbnailUrl: "",
          recommendedRetailPrice: 170000,
          unitPrice: 170000,
          quantity: 7,
          total: 1190000,
          id: 29,
        },
      ],
      id: 14,
    },
    {
      orderNumber: "2023-10-13-a2695",
      dueWhen: null,
      expectedDeliveryWhen: "2023-10-18T05:51:13.033982+07:00",
      subtotal: 144000,
      total: 174000,
      taxRate: 0,
      comment: "Careful",
      deliveryInstruction: "",
      customerId: 1,
      shippingAddressId: 1,
      deliveryMethodName: "Giao hàng tiêu chuẩn",
      discountVoucherId: null,
      paymentMethodName: "Thanh toán qua thẻ",
      status: "Processing",
      pickingCompletedWhen: null,
      confirmedDeliveryWhen: null,
      confirmedReceivedWhen: null,
      orderWhen: "2023-10-13T05:51:13.033982+07:00",
      orderLines: [
        {
          orderId: 18,
          skuId: 5,
          skuOptionName: "Tập 1 - Mùa Xuân",
          productName: "Tô Màu Bốn Mùa",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294357.jpg",
          recommendedRetailPrice: 20000,
          unitPrice: 18000,
          quantity: 5,
          total: 90000,
          id: 35,
        },
        {
          orderId: 18,
          skuId: 7,
          skuOptionName: "Tập 3 - Mùa Thu",
          productName: "Tô Màu Bốn Mùa",
          thumbnailUrl:
            "https://cdn0.fahasa.com/media/catalog/product/8/9/8936071294371.jpg",
          recommendedRetailPrice: 20000,
          unitPrice: 18000,
          quantity: 3,
          total: 54000,
          id: 36,
        },
      ],
      id: 18,
    },
  ],
  totalCount: 10,
  pageSize: 12,
  pageNumber: 1,
  totalPages: 1,
}

const rows = object.items

const Orders = () => {
  const navigate = useNavigate()

  const [startDate, setStartDate] = useState(dayjs())
  const [endDate, setEndDate] = useState()

  return (
    <div className='flex flex-col gap-3 overflow-hidden'>
      <div className='rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Quản lý đơn hàng
        </div>
      </div>

      <DataTable
        height={500}
        columns={columns}
        rows={rows}
        disableMultipleRowSelection
        disableRowSelectionOnClick
        isRowSelectable={() => false}
        onRowClick={(value) => {
          navigate(`/admin/orders/${value.row.id}`)
        }}
        filterChildren={
          <div className='flex flex-col gap-5 p-5'>
            <MyDatePicker
              label='Ngày bắt đầu'
              disableFuture
              value={startDate}
              onChange={(newValue) => setStartDate(newValue)}
            />
            <MyDatePicker
              label='Ngày kết thúc'
              disableFuture
              value={endDate}
              onChange={(newValue) => setEndDate(newValue)}
              maxDate={startDate}
              defaultValue={startDate}
            />
          </div>
        }
      />
    </div>
  )
}

export default Orders
