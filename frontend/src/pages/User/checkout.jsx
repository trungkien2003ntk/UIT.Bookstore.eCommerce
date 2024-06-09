import { useState } from "react"

import DeliveryDiningIcon from "@mui/icons-material/DeliveryDining"
import { Autocomplete, Checkbox, Divider, TextField } from "@mui/material"
import { green } from "@mui/material/colors"

import Modal from "../../components/Modal"
import Button from "../../components/Button"
import Input from "../../components/Input"
import VND from "../../components/vnd"
import AddressRadio from "../../components/Checkout/AddressRadio"
import VoucherRadio from "../../components/Checkout/VoucherRadio"

const options = [
  {
    id: 1,
    name: "Phạm Tuấn Kiệt",
    phone: "0835489396",
    province: "An Giang",
    district: "An Phú",
    commune: "Khánh Bình",
    addressDetail: "Khu dân cư xã Khánh Bình",
  },
  {
    id: 2,
    name: "Nguuyễn Thị Loan",
    phone: "0918885747",
    province: "An Giang",
    district: "An Phú",
    commune: "Quốc Thái",
    addressDetail: "Cửa hàng Điện thoại Di Động Chí Khang",
  },
]

const provinces = [
  {
    id: 269,
    label: "Lào Cai",
  },
  {
    id: 268,
    label: "Hưng Yên",
  },
  {
    id: 267,
    label: "Hòa Bình",
  },
  {
    id: 266,
    label: "Sơn La",
  },
  {
    id: 265,
    label: "Điện Biên",
  },
  {
    id: 264,
    label: "Lai Châu",
  },
  {
    id: 263,
    label: "Yên Bái",
  },
  {
    id: 262,
    label: "Bình Định",
  },
  {
    id: 261,
    label: "Ninh Thuận",
  },
  {
    id: 260,
    label: "Phú Yên",
  },
  {
    id: 259,
    label: "Kon Tum",
  },
  {
    id: 258,
    label: "Bình Thuận",
  },
  {
    id: 253,
    label: "Bạc Liêu",
  },
  {
    id: 252,
    label: "Cà Mau",
  },
  {
    id: 250,
    label: "Hậu Giang",
  },
  {
    id: 249,
    label: "Bắc Ninh",
  },
  {
    id: 248,
    label: "Bắc Giang",
  },
  {
    id: 247,
    label: "Lạng Sơn",
  },
  {
    id: 246,
    label: "Cao Bằng",
  },
  {
    id: 245,
    label: "Bắc Kạn",
  },
  {
    id: 244,
    label: "Thái Nguyên",
  },
  {
    id: 243,
    label: "Quảng Nam",
  },
  {
    id: 242,
    label: "Quảng Ngãi",
  },
  {
    id: 241,
    label: "Đắk Nông",
  },
  {
    id: 240,
    label: "Tây Ninh",
  },
  {
    id: 239,
    label: "Bình Phước",
  },
  {
    id: 238,
    label: "Quảng Trị",
  },
  {
    id: 237,
    label: "Quảng Bình",
  },
  {
    id: 236,
    label: "Hà Tĩnh",
  },
  {
    id: 235,
    label: "Nghệ An",
  },
  {
    id: 234,
    label: "Thanh Hóa",
  },
  {
    id: 233,
    label: "Ninh Bình",
  },
  {
    id: 232,
    label: "Hà Nam",
  },
  {
    id: 231,
    label: "Nam Định",
  },
  {
    id: 230,
    label: "Quảng Ninh",
  },
  {
    id: 229,
    label: "Phú Thọ",
  },
  {
    id: 228,
    label: "Tuyên Quang",
  },
  {
    id: 227,
    label: "Hà Giang",
  },
  {
    id: 226,
    label: "Thái Bình",
  },
  {
    id: 225,
    label: "Hải Dương",
  },
  {
    id: 224,
    label: "Hải Phòng",
  },
  {
    id: 223,
    label: "Thừa Thiên - Huế",
  },
  {
    id: 221,
    label: "Vĩnh Phúc",
  },
  {
    id: 220,
    label: "Cần Thơ",
  },
  {
    id: 219,
    label: "Kiên Giang",
  },
  {
    id: 218,
    label: "Sóc Trăng",
  },
  {
    id: 217,
    label: "An Giang",
  },
  {
    id: 216,
    label: "Đồng Tháp",
  },
  {
    id: 215,
    label: "Vĩnh Long",
  },
  {
    id: 214,
    label: "Trà Vinh",
  },
  {
    id: 213,
    label: "Bến Tre",
  },
  {
    id: 212,
    label: "Tiền Giang",
  },
  {
    id: 211,
    label: "Long An",
  },
  {
    id: 210,
    label: "Đắk Lắk",
  },
  {
    id: 209,
    label: "Lâm Đồng",
  },
  {
    id: 208,
    label: "Khánh Hòa",
  },
  {
    id: 207,
    label: "Gia Lai",
  },
  {
    id: 206,
    label: "Bà Rịa - Vũng Tàu",
  },
  {
    id: 205,
    label: "Bình Dương",
  },
  {
    id: 204,
    label: "Đồng Nai",
  },
  {
    id: 203,
    label: "Đà Nẵng",
  },
  {
    id: 202,
    label: "Hồ Chí Minh",
  },
  {
    id: 201,
    label: "Hà Nội",
  },
]

const districts = [
  {
    id: 3715,
    label: "33",
  },
  {
    id: 3713,
    label: "Quận mới",
  },
  {
    id: 3695,
    label: "Thành Phố Thủ Đức 1",
  },
  {
    id: 2090,
    label: "Huyện Cần Giờ",
  },
  {
    id: 1534,
    label: "Huyện Nhà Bè",
  },
  {
    id: 1533,
    label: "Huyện Bình Chánh",
  },
  {
    id: 1463,
    label: "Quận Thủ Đức",
  },
  {
    id: 1462,
    label: "Quận Bình Thạnh",
  },
  {
    id: 1461,
    label: "Quận Gò Vấp",
  },
  {
    id: 1460,
    label: "Huyện Củ Chi",
  },
  {
    id: 1459,
    label: "Huyện Hóc Môn",
  },
  {
    id: 1458,
    label: "Quận Bình Tân",
  },
  {
    id: 1457,
    label: "Quận Phú Nhuận",
  },
  {
    id: 1456,
    label: "Quận Tân Phú",
  },
  {
    id: 1455,
    label: "Quận Tân Bình",
  },
  {
    id: 1454,
    label: "Quận 12",
  },
  {
    id: 1453,
    label: "Quận 11",
  },
  {
    id: 1452,
    label: "Quận 10",
  },
  {
    id: 1451,
    label: "Quận 9",
  },
  {
    id: 1450,
    label: "Quận 8",
  },
  {
    id: 1449,
    label: "Quận 7",
  },
  {
    id: 1448,
    label: "Quận 6",
  },
  {
    id: 1447,
    label: "Quận 5",
  },
  {
    id: 1446,
    label: "Quận 4",
  },
  {
    id: 1444,
    label: "Quận 3",
  },
  {
    id: 1443,
    label: "Quận 2",
  },
  {
    id: 1442,
    label: "Quận 1",
  },
]

const communes = [
  {
    id: "90768",
    label: "Phường An Khánh",
  },
  {
    id: "90767",
    label: "Phường Bình Trưng Tây",
  },
  {
    id: "90766",
    label: "Phường Bình Trưng Đông",
  },
  {
    id: "90765",
    label: "Phường An Phú",
  },
  {
    id: "90764",
    label: "Phường Thảo Điền",
  },
  {
    id: "90763",
    label: "Phường Phú Hữu",
  },
  {
    id: "90762",
    label: "Phường Phước Bình",
  },
  {
    id: "90761",
    label: "Phường Long Trường",
  },
  {
    id: "90760",
    label: "Phường Long Phước",
  },
  {
    id: "90759",
    label: "Phường Trường Thạnh",
  },
  {
    id: "90758",
    label: "Phường Phước Long B",
  },
  {
    id: "90757",
    label: "Phường Phước Long A",
  },
  {
    id: "90756",
    label: "Phường Tăng Nhơn Phú B",
  },
  {
    id: "90755",
    label: "Phường Tăng Nhơn Phú A",
  },
  {
    id: "90754",
    label: "Phường Hiệp Phú",
  },
  {
    id: "90753",
    label: "Phường Tân Phú",
  },
  {
    id: "90752",
    label: "Phường Long Thạnh Mỹ",
  },
  {
    id: "90751",
    label: "Phường Long Bình",
  },
  {
    id: "90750",
    label: "Phường Thủ Thiêm",
  },
  {
    id: "90749",
    label: "Phường An Lợi Đông",
  },
  {
    id: "90748",
    label: "Phường Thạnh Mỹ Lợi",
  },
  {
    id: "90747",
    label: "Phường Cát Lái",
  },
  {
    id: "90746",
    label: "Phường Trường Thọ",
  },
  {
    id: "90745",
    label: "Phường Bình Thọ",
  },
  {
    id: "90744",
    label: "Phường Linh Đông",
  },
  {
    id: "90743",
    label: "Phường Linh Tây",
  },
  {
    id: "90742",
    label: "Phường Linh Chiểu",
  },
  {
    id: "90741",
    label: "Phường Hiệp Bình Chánh",
  },
  {
    id: "90740",
    label: "Phường Hiệp Bình Phước",
  },
  {
    id: "90739",
    label: "Phường Tam Phú",
  },
  {
    id: "90738",
    label: "Phường Tam Bình",
  },
  {
    id: "90737",
    label: "Phường Linh Trung",
  },
  {
    id: "90736",
    label: "Phường Bình Chiểu",
  },
  {
    id: "90735",
    label: "Phường Linh Xuân",
  },
]

const vouchers = [
  {
    id: 1,
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    id: 2,
    type: "discount",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
]

const freeShipping = [
  {
    id: 1,
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    id: 2,
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
  {
    id: 3,
    type: "freeShipping",
    title: "Mã giảm giá 50K cho đơn hàng từ 500K",
    description:
      "Mã giảm giá cho đơn hàng từ 500K chỉ áp dụng cho sản phẩm này",
    expired: "20/10/2022",
  },
]

const CheckOut = () => {
  // MODAL ADDRESS
  const [openAddress, setOpenAddress] = useState(false)
  const handleAddress = () => {
    setOpenAddress((prev) => !prev)
  }

  // MODAL EDIT ADDRESS
  const [editAddress, setEditAddress] = useState({
    name: "",
    phone: "",
    province: "",
    district: "",
    commune: "",
    addressDetail: "",
  })

  const [openEditAddress, setOpenEditAddress] = useState(false)

  const handleOpenEditAddress = (objAddress) => {
    if (objAddress) {
      setEditAddress(objAddress)
    } else {
      setEditAddress({
        name: "",
        phone: "",
        province: "",
        district: "",
        commune: "",
        addressDetail: "",
      })
    }

    handleAddress()
    setOpenEditAddress(true)
  }

  const handleCloseEditAddress = () => {
    setOpenEditAddress(false)
  }

  // AUTOCOMPLETE
  const [valueProvince, setValueProvince] = useState(provinces[0])
  const [valueDistrict, setValueDistrict] = useState(districts[0])
  const [valueCommune, setValueCommune] = useState(communes[0])

  const [inputValueAutoComplete, setInputValueAutoComplete] = useState({
    province: "",
    district: "",
    commune: "",
  })

  //   RADIO BUTTON
  const [selectedValue, setSelectedValue] = useState(JSON.stringify(options[0]))
  const handleChange = (event) => {
    setSelectedValue(event.target.value)
  }

  // VOUCHER
  const [openVoucher, setOpenVoucher] = useState(false)
  const handleVoucher = () => {
    setOpenVoucher((prev) => !prev)
  }
  // VOUCHER RADIO
  const [selectedVoucher, setSelectedVoucher] = useState({})
  const handleChangeVoucher = (event) => {
    setSelectedVoucher(event.target.value)
  }

  // FREE SHIPPING
  const [openFreeShipping, setOpenFreeShipping] = useState(false)
  const handleFreeShipping = () => {
    setOpenFreeShipping((prev) => !prev)
  }
  // FREE SHIPPING RADIO
  const [selectedFreeShipping, setSelectedFreeShipping] = useState({})
  const handleChangeFreeShipping = (event) => {
    setSelectedFreeShipping(event.target.value)
  }

  const [paymentType, setPaymentType] = useState(1)

  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col justify-between 
        gap-y-5 p-4 lg:px-8'
      >
        <div className='flex items-center gap-3 rounded bg-white p-5 shadow'>
          <div className='text-yellow-400'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-16'
            >
              <path d='M10.464 8.746c.227-.18.497-.311.786-.394v2.795a2.252 2.252 0 0 1-.786-.393c-.394-.313-.546-.681-.546-1.004 0-.323.152-.691.546-1.004ZM12.75 15.662v-2.824c.347.085.664.228.921.421.427.32.579.686.579.991 0 .305-.152.671-.579.991a2.534 2.534 0 0 1-.921.42Z' />
              <path
                fillRule='evenodd'
                d='M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25ZM12.75 6a.75.75 0 0 0-1.5 0v.816a3.836 3.836 0 0 0-1.72.756c-.712.566-1.112 1.35-1.112 2.178 0 .829.4 1.612 1.113 2.178.502.4 1.102.647 1.719.756v2.978a2.536 2.536 0 0 1-.921-.421l-.879-.66a.75.75 0 0 0-.9 1.2l.879.66c.533.4 1.169.645 1.821.75V18a.75.75 0 0 0 1.5 0v-.81a4.124 4.124 0 0 0 1.821-.749c.745-.559 1.179-1.344 1.179-2.191 0-.847-.434-1.632-1.179-2.191a4.122 4.122 0 0 0-1.821-.75V8.354c.29.082.559.213.786.393l.415.33a.75.75 0 0 0 .933-1.175l-.415-.33a3.836 3.836 0 0 0-1.719-.755V6Z'
                clipRule='evenodd'
              />
            </svg>
          </div>

          <span className='text-2xl font-medium text-yellow-500'>
            Thanh toán
          </span>
        </div>

        <div className='flex flex-col gap-3 rounded bg-white p-5 shadow'>
          <div className='flex items-center gap-2'>
            <div className='text-ct-green-400'>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6'
              >
                <path
                  fillRule='evenodd'
                  d='m11.54 22.351.07.04.028.016a.76.76 0 0 0 .723 0l.028-.015.071-.041a16.975 16.975 0 0 0 1.144-.742 19.58 19.58 0 0 0 2.683-2.282c1.944-1.99 3.963-4.98 3.963-8.827a8.25 8.25 0 0 0-16.5 0c0 3.846 2.02 6.837 3.963 8.827a19.58 19.58 0 0 0 2.682 2.282 16.975 16.975 0 0 0 1.145.742ZM12 13.5a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z'
                  clipRule='evenodd'
                />
              </svg>
            </div>
            <div className='text-lg font-medium text-ct-green-400'>
              Địa chỉ nhận hàng
            </div>
          </div>

          <div className='flex items-center gap-5'>
            <div className='font-medium'>Phạm Tuấn Kiệt</div>
            <div className='font-medium'>0835489396</div>
            <div>
              KTX Khu B ĐHQG, Phường Linh Trung, Thành Phố Thủ Đức, TP. Hồ Chí
              Minh
            </div>

            <div
              className='text-blue-500 hover:cursor-pointer hover:font-medium'
              onClick={handleAddress}
            >
              Thay đổi
            </div>

            <Modal
              open={openAddress}
              setOpen={handleAddress}
              title={"Địa chỉ nhận hàng"}
              contentComp={
                <div>
                  <AddressRadio
                    options={options}
                    onChange={handleChange}
                    value={selectedValue}
                    onUpdate={() => {
                      handleOpenEditAddress(JSON.parse(selectedValue))
                    }}
                  />

                  <Button
                    className={`mt-5 border-[1px] border-ct-green-400 
                    bg-green-50  !text-ct-green-400 hover:!bg-green-100`}
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
                    onClick={handleOpenEditAddress}
                  >
                    Thêm mới
                  </Button>
                </div>
              }
              actionComp={<Button onClick={handleAddress}>Xác nhận</Button>}
              isCancelButton
            ></Modal>

            <Modal
              open={openEditAddress}
              setOpen={handleCloseEditAddress}
              title={"Cập nhật địa chỉ nhận hàng"}
              contentComp={
                <div className='flex flex-col gap-2'>
                  <Input
                    value={editAddress.name}
                    onChange={(value) =>
                      setEditAddress((prev) => ({ ...prev, name: value }))
                    }
                    placeholder={"Tên"}
                    title={"Tên"}
                    type='text'
                    // errorMsg={errors.lastName}
                  />

                  <Input
                    value={editAddress.phone}
                    onChange={(value) =>
                      setEditAddress((prev) => ({ ...prev, phone: value }))
                    }
                    placeholder={"Số điện thoại"}
                    title={"Số điện thoại"}
                    type='text'
                    // errorMsg={errors.phoneNumber}
                  />

                  <Input
                    value={editAddress.addressDetail}
                    onChange={(value) =>
                      setEditAddress((prev) => ({
                        ...prev,
                        addressDetail: value,
                      }))
                    }
                    placeholder={"Địa chỉ cụ thể"}
                    title={"Địa chỉ cụ thể"}
                    type='text'
                    // errorMsg={errors.lastName}
                  />

                  <Autocomplete
                    value={valueProvince}
                    onChange={(event, newValue) => {
                      setValueProvince(newValue)
                    }}
                    inputValue={inputValueAutoComplete.province}
                    onInputChange={(event, newInputValue) => {
                      setInputValueAutoComplete((prev) => ({
                        ...prev,
                        province: newInputValue,
                      }))
                    }}
                    getOptionLabel={(option) => {
                      return option.label
                    }}
                    noOptionsText='Không có dữ liệu'
                    isOptionEqualToValue={(option, value) =>
                      option.id === value.id
                    }
                    freeSolo={false}
                    id='controllable-states-demo'
                    options={provinces}
                    sx={{ width: "100%", color: green[400] }}
                    className='mt-3'
                    renderInput={(params) => (
                      <TextField
                        sx={{ color: green[400] }}
                        {...params}
                        label='Tỉnh/Thành phố'
                        size='small'
                      />
                    )}
                  ></Autocomplete>

                  <Autocomplete
                    value={valueDistrict}
                    onChange={(event, newValue) => {
                      setValueDistrict(newValue)
                    }}
                    inputValue={inputValueAutoComplete.district}
                    onInputChange={(event, newInputValue) => {
                      setInputValueAutoComplete((prev) => ({
                        ...prev,
                        district: newInputValue,
                      }))
                    }}
                    getOptionLabel={(option) => {
                      return option.label
                    }}
                    isOptionEqualToValue={(option, value) =>
                      option.id === value.id
                    }
                    noOptionsText='Không có dữ liệu'
                    id='controllable-states-demo'
                    options={districts}
                    sx={{ width: "100%", color: green[400] }}
                    className='mt-3'
                    renderInput={(params) => (
                      <TextField
                        sx={{ color: green[400] }}
                        {...params}
                        label='Quận/Huyện'
                        size='small'
                      />
                    )}
                  ></Autocomplete>

                  <Autocomplete
                    value={valueCommune}
                    onChange={(event, newValue) => {
                      setValueCommune(newValue)
                    }}
                    inputValue={inputValueAutoComplete.commune}
                    onInputChange={(event, newInputValue) => {
                      setInputValueAutoComplete((prev) => ({
                        ...prev,
                        commune: newInputValue,
                      }))
                    }}
                    getOptionLabel={(option) => {
                      return option.label
                    }}
                    isOptionEqualToValue={(option, value) =>
                      option.id === value.id
                    }
                    noOptionsText='Không có dữ liệu'
                    id='controllable-states-demo'
                    options={communes}
                    sx={{ width: "100%", color: green[400] }}
                    className='mt-3'
                    renderInput={(params) => (
                      <TextField
                        sx={{ color: green[400] }}
                        {...params}
                        label='Phường/Xã'
                        size='small'
                      />
                    )}
                  ></Autocomplete>
                </div>
              }
              actionComp={
                <div className='flex items-center gap-2'>
                  <Button
                    className={`border-[1px] border-ct-green-400 bg-green-50 
                    !text-ct-green-400  hover:!bg-green-100`}
                    onClick={() => {
                      handleAddress()
                      handleCloseEditAddress()
                    }}
                  >
                    Trở lại
                  </Button>
                  <Button
                    onClick={() => {
                      handleAddress()
                      handleCloseEditAddress()
                    }}
                  >
                    Xác nhận
                  </Button>
                </div>
              }
            ></Modal>
          </div>
        </div>

        <div className='flex flex-col gap-3 rounded bg-white p-5 shadow'>
          <div className='flex items-center justify-between'>
            <div className='text-xl'>Sản phẩm</div>
            <div className='flex'>
              <div className='min-w-44 text-center'>Đơn giá</div>
              <div className='min-w-44 text-center'>Số lượng</div>
              <div className='min-w-44 text-center'>Thành tiền</div>
            </div>
          </div>

          <div className='flex items-center justify-between text-sm'>
            <div className='flex items-center gap-2'>
              <img
                className='h-20 w-20'
                alt='img'
                src='https://down-vn.img.susercontent.com/file/vn-11134207-7r98o-ltkh7zcofuoa65_tn'
              />
              <div className='line-clamp-3 text-sm'>
                Áo Sơ Mi Denim Basic Godmother Xuân Hè 2024
              </div>

              <div
                className='flex min-w-44 max-w-44 flex-col items-start justify-start 
                rounded bg-slate-100 p-3 hover:cursor-pointer'
              >
                <div className='flex items-center gap-1 text-sm'>
                  <span className='font-medium'>Phân loại hàng</span>
                </div>
                <div className='text-start text-sm'>123</div>
              </div>
            </div>

            <div className='flex'>
              <div className='min-w-44 text-center'>
                <VND number={249000} />
              </div>
              <div className='min-w-44 text-center'>1</div>
              <div className='min-w-44 text-center'>
                <VND number={249000} />
              </div>
            </div>
          </div>
        </div>

        <div className='flex justify-between rounded bg-white p-5 shadow'>
          <div className='flex items-center gap-2'>
            <div className='text-orange-500'>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6'
              >
                <path d='M9.375 3a1.875 1.875 0 0 0 0 3.75h1.875v4.5H3.375A1.875 1.875 0 0 1 1.5 9.375v-.75c0-1.036.84-1.875 1.875-1.875h3.193A3.375 3.375 0 0 1 12 2.753a3.375 3.375 0 0 1 5.432 3.997h3.943c1.035 0 1.875.84 1.875 1.875v.75c0 1.036-.84 1.875-1.875 1.875H12.75v-4.5h1.875a1.875 1.875 0 1 0-1.875-1.875V6.75h-1.5V4.875C11.25 3.839 10.41 3 9.375 3ZM11.25 12.75H3v6.75a2.25 2.25 0 0 0 2.25 2.25h6v-9ZM12.75 12.75v9h6.75a2.25 2.25 0 0 0 2.25-2.25v-6.75h-9Z' />
              </svg>
            </div>
            <div className='text-xl font-medium text-gray-600'>
              KKBooks Voucher
            </div>
          </div>

          <div
            className='text-blue-500 hover:cursor-pointer hover:font-medium'
            onClick={handleVoucher}
          >
            Chọn Voucher
          </div>

          <Modal
            open={openVoucher}
            setOpen={handleVoucher}
            title={"VOUCHER"}
            contentComp={
              <div>
                <VoucherRadio
                  options={vouchers}
                  onChange={handleChangeVoucher}
                  value={selectedVoucher}
                />
              </div>
            }
            actionComp={<Button onClick={handleVoucher}>Xác nhận</Button>}
            isCancelButton
          ></Modal>
        </div>

        <div className='flex justify-between rounded bg-white p-5 shadow'>
          <div className='flex items-center gap-2'>
            <div className='text-purple-500'>
              <DeliveryDiningIcon />
            </div>
            <div className='text-xl font-medium text-gray-600'>
              Mã giảm giá vận chuyển
            </div>
          </div>

          <div
            className='text-blue-500 hover:cursor-pointer hover:font-medium'
            onClick={handleFreeShipping}
          >
            Chọn mã giảm giá
          </div>

          <Modal
            open={openFreeShipping}
            setOpen={handleFreeShipping}
            title={"Mã giảm giá vận chuyển"}
            contentComp={
              <div>
                <VoucherRadio
                  options={freeShipping}
                  onChange={handleChangeFreeShipping}
                  value={selectedFreeShipping}
                />
              </div>
            }
            actionComp={<Button onClick={handleFreeShipping}>Xác nhận</Button>}
            isCancelButton
          ></Modal>
        </div>

        <div className='flex justify-between rounded bg-white p-5 shadow'>
          <div className='flex items-center gap-2'>
            <div className='text-yellow-400'>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6'
              >
                <path
                  fillRule='evenodd'
                  d='M8.603 3.799A4.49 4.49 0 0 1 12 2.25c1.357 0 2.573.6 3.397 1.549a4.49 4.49 0 0 1 3.498 1.307 4.491 4.491 0 0 1 1.307 3.497A4.49 4.49 0 0 1 21.75 12a4.49 4.49 0 0 1-1.549 3.397 4.491 4.491 0 0 1-1.307 3.497 4.491 4.491 0 0 1-3.497 1.307A4.49 4.49 0 0 1 12 21.75a4.49 4.49 0 0 1-3.397-1.549 4.49 4.49 0 0 1-3.498-1.306 4.491 4.491 0 0 1-1.307-3.498A4.49 4.49 0 0 1 2.25 12c0-1.357.6-2.573 1.549-3.397a4.49 4.49 0 0 1 1.307-3.497 4.49 4.49 0 0 1 3.497-1.307Zm7.007 6.387a.75.75 0 1 0-1.22-.872l-3.236 4.53L9.53 12.22a.75.75 0 0 0-1.06 1.06l2.25 2.25a.75.75 0 0 0 1.14-.094l3.75-5.25Z'
                  clipRule='evenodd'
                />
              </svg>
            </div>
            <div className='text-xl font-medium text-gray-600'>KKBooks Xu</div>
          </div>

          <div className='flex items-center'>
            <VND className={`text-gray-300`} number={500} />
            <Checkbox />
          </div>
        </div>

        <div className='flex flex-col justify-between rounded bg-white p-5 shadow'>
          <div className='flex items-center gap-2'>
            <div className='text-blue-700'>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                viewBox='0 0 24 24'
                fill='currentColor'
                className='size-6'
              >
                <path d='M4.5 3.75a3 3 0 0 0-3 3v.75h21v-.75a3 3 0 0 0-3-3h-15Z' />
                <path
                  fillRule='evenodd'
                  d='M22.5 9.75h-21v7.5a3 3 0 0 0 3 3h15a3 3 0 0 0 3-3v-7.5Zm-18 3.75a.75.75 0 0 1 .75-.75h6a.75.75 0 0 1 0 1.5h-6a.75.75 0 0 1-.75-.75Zm.75 2.25a.75.75 0 0 0 0 1.5h3a.75.75 0 0 0 0-1.5h-3Z'
                  clipRule='evenodd'
                />
              </svg>
            </div>
            <div className='text-xl font-medium text-gray-600'>
              Phương thức thanh toán
            </div>
          </div>

          <div className='mt-5 flex items-center justify-start gap-5'>
            <div
              className={`${paymentType === 1 && "border-[2px] border-ct-green-400"} 
                relative flex items-center gap-2 rounded border-[1px]
                px-3 py-2 text-gray-600 hover:cursor-pointer hover:border-ct-green-400`}
              onClick={() => setPaymentType(1)}
            >
              <div className=''>Thanh toán ghi nhận hàng (COD)</div>
              {paymentType === 1 && (
                <div
                  className='absolute bottom-0 right-0 h-0 w-0 
                border-b-[12px] border-l-[12px] border-b-ct-green-400 border-l-transparent'
                ></div>
              )}
            </div>

            <div
              className={`${paymentType === 2 && "border-[2px] border-ct-green-400"} 
                relative flex items-center gap-2 rounded border-[1px]
                px-3 py-2 text-gray-600 hover:cursor-pointer hover:border-ct-green-400`}
              onClick={() => setPaymentType(2)}
            >
              <div className=''>VNPay</div>
              {paymentType === 2 && (
                <div
                  className='absolute bottom-0 right-0 h-0 w-0 
                border-b-[12px] border-l-[12px] border-b-ct-green-400 border-l-transparent'
                ></div>
              )}
            </div>
          </div>

          <div className='w-full py-5'>
            <Divider />
          </div>

          <div className='flex flex-col items-end gap-5'>
            <div className='flex items-center'>
              <div className='min-w-52'>Tổng tiền hàng</div>
              <VND className={`min-w-52 text-end`} number={518000} />
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Phí vận chuyển</div>
              <VND className={`min-w-52 text-end`} number={35000} />
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Giảm giá phí vận chuyển</div>

              <div className='flex min-w-52 items-center justify-end'>
                <span>-</span>
                <VND className={`text-end`} number={35000} />
              </div>
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Voucher</div>

              <div className='flex min-w-52 items-center justify-end'>
                <span>-</span>
                <VND className={`text-end`} number={5000} />
              </div>
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>KKBooks Xu</div>

              <div className='flex min-w-52 items-center justify-end'>
                <span>-</span>
                <VND className={`text-end`} number={500} />
              </div>
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Tổng thanh toán</div>
              <VND
                className={`min-w-52 text-end text-3xl font-medium text-ct-green-400`}
                number={35000}
              />
            </div>
          </div>

          <div className='w-full py-5'>
            <Divider />
          </div>

          <div className='flex items-center justify-end'>
            <Button className={`min-w-64`}>Đặt hàng</Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CheckOut
