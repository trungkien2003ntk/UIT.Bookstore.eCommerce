import { useState, useEffect } from "react"
import { useLocation, useNavigate } from "react-router-dom"

import DeliveryDiningIcon from "@mui/icons-material/DeliveryDining"
import {
  Autocomplete,
  CircularProgress,
  Divider,
  TextField,
} from "@mui/material"
import { green } from "@mui/material/colors"

import Modal from "../../components/Modal"
import Button from "../../components/Button"
import Input from "../../components/Input"
import VND from "../../components/vnd"
import AddressRadio from "../../components/Checkout/AddressRadio"
import VoucherRadio from "../../components/Checkout/VoucherRadio"
import noImage from "../../assets/images/no-photo.png"

import * as addressServices from "../../apiServices/addressServices"
import * as voucherServices from "../../apiServices/voucherServices"
import * as checkoutServices from "../../apiServices/checkoutServices"

const CheckOut = () => {
  const nav = useNavigate()
  const { state } = useLocation()
  const [obj, setObj] = useState(state.res)
  const [address, setAddress] = useState(obj.shippingAddresses[0])
  const [addressList, setAddressList] = useState(null)

  console.log(obj)

  const [date, setDate] = useState(new Date())

  const [provinces, setProvinces] = useState([])
  const [districts, setDistricts] = useState([])
  const [communes, setCommunes] = useState([])

  const [products, setProducts] = useState(obj.items)
  const [subTotal, setSubTotal] = useState(0)
  const [shippingFee, setShippingFee] = useState(0)
  const [discountShip, setDiscountShip] = useState(0)
  const [voucher, setVoucher] = useState(0)
  const [total, setTotal] = useState(0)

  const [listVouchers, setListVouchers] = useState()
  const [listDiscountShip, setListDiscountShip] = useState()

  const handleCalculate = () => {
    setTotal(subTotal + shippingFee - discountShip - voucher)
  }

  const getAddressList = async () => {
    const response = await addressServices.getAddressList().catch((error) => {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log("Error", error.message)
      }
      console.log(error.config)
    })

    if (response) {
      setAddressList(response)
    }
  }

  const getVouchers = async () => {
    const response = await voucherServices
      .getVouchers({ selectedItemIds: products.map((i) => i.id) })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          console.log(error.request)
        } else {
          console.log("Error", error.message)
        }
        console.log(error.config)
      })
    if (response) {
      // console.log(response)
      setListVouchers(response.orderVouchers)
      setListDiscountShip(response.shippingVouchers)
    }
  }

  const getProvince = async () => {
    const response = await addressServices.getProvince().catch((error) => {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log("Error", error.message)
      }
      console.log(error.config)
    })
    if (response) {
      setProvinces(response)
    }
  }

  const getDistrict = async ({ provinceId }) => {
    const response = await addressServices
      .getDistrict({ provinceId })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          console.log(error.request)
        } else {
          console.log("Error", error.message)
        }
        console.log(error.config)
      })
    if (response) {
      console.log(response)
      setDistricts(response)
    }
  }

  const getCommune = async ({ districtId }) => {
    const response = await addressServices
      .getCommune({ districtId })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          console.log(error.request)
        } else {
          console.log("Error", error.message)
        }
        console.log(error.config)
      })
    if (response) {
      console.log(response)
      setCommunes(response)
    }
  }

  const getConfirmCheckout = async ({
    OrderDiscountVoucherId,
    ShippingDiscountVoucherId,
  }) => {
    const response = await checkoutServices
      .confirmCheckout({
        ItemIds: obj.items.map((i) => i.id),
        OrderDiscountVoucherId,
        ShippingDiscountVoucherId,
      })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          console.log(error.request)
        } else {
          console.log("Error", error.message)
        }
        console.log(error.config)
      })

    if (response) {
      setObj(response)
    }
  }

  useEffect(() => {
    // handleCalculate()
    getAddressList()
    getVouchers()
  }, [])

  // MODAL ADDRESS
  const [openAddress, setOpenAddress] = useState(false)
  const handleAddress = () => {
    setOpenAddress((prev) => !prev)
  }

  // MODAL EDIT ADDRESS
  const [editAddress, setEditAddress] = useState({
    receiverName: "",
    phoneNumber: "",
    province: "",
    district: "",
    commune: "",
    detailAddress: "",
  })

  const [openEditAddress, setOpenEditAddress] = useState(false)

  const handleOpenEditAddress = (objAddress) => {
    setEditAddress(objAddress)
    // setDate(new Date())
    // getProvince()

    // const province = provinces.find((i) => i.label === objAddress.province)
    // setValueProvince(province)

    // getDistrict({ provinceId: province.id })
    // const district = districts.find((i) => i.label === objAddress.district)
    // setValueDistrict(district)

    // getCommune({ districtId: district.id })
    // const commune = communes.find((i) => i.label === objAddress.commune)
    // setValueCommune(commune)

    handleAddress()
    setOpenEditAddress(true)
  }

  const handleCloseEditAddress = () => {
    setOpenEditAddress(false)
  }

  // AUTOCOMPLETE
  const [valueProvince, setValueProvince] = useState()
  const [valueDistrict, setValueDistrict] = useState()
  const [valueCommune, setValueCommune] = useState()

  const [inputValueAutoComplete, setInputValueAutoComplete] = useState({
    province: "",
    district: "",
    commune: "",
  })

  //   RADIO BUTTON
  const [selectedValue, setSelectedValue] = useState(JSON.stringify(address))
  const handleChange = (event) => {
    setSelectedValue(event.target.value)

    console.log(event.target.value)
  }

  // VOUCHER
  const [openVoucher, setOpenVoucher] = useState(false)
  const handleVoucher = () => {
    setOpenVoucher((prev) => !prev)
  }
  // VOUCHER RADIO
  const [selectedVoucher, setSelectedVoucher] = useState(null)
  const handleChangeVoucher = (event) => {
    setSelectedVoucher(event.target.value)
  }

  const handleAddVoucher = () => {
    getConfirmCheckout({
      OrderDiscountVoucherId: selectedVoucher && JSON.parse(selectedVoucher).id,
      ShippingDiscountVoucherId:
        selectedFreeShipping && JSON.parse(selectedFreeShipping).id,
    })

    handleVoucher()
  }

  const handleAddDiscountShipping = () => {
    getConfirmCheckout({
      OrderDiscountVoucherId: selectedVoucher && JSON.parse(selectedVoucher).id,
      ShippingDiscountVoucherId:
        selectedFreeShipping && JSON.parse(selectedFreeShipping).id,
    })

    handleFreeShipping()
  }

  // FREE SHIPPING
  const [openFreeShipping, setOpenFreeShipping] = useState(false)
  const handleFreeShipping = () => {
    setOpenFreeShipping((prev) => !prev)
  }
  // FREE SHIPPING RADIO
  const [selectedFreeShipping, setSelectedFreeShipping] = useState(null)
  const handleChangeFreeShipping = (event) => {
    setSelectedFreeShipping(event.target.value)
  }

  const [paymentType, setPaymentType] = useState(1)

  const handleOrder = async () => {
    const response = await checkoutServices
      .placeOrder({
        ItemIds: obj.items.map((i) => i.id),
        ShippingAddressId: obj.shippingAddresses[0].id,
        PaymentMethodId: paymentType,
        DeliveryMethodId: 1,
        OrderDiscountVoucherId:
          selectedVoucher && JSON.parse(selectedVoucher).id,
        ShippingVoucherId:
          selectedFreeShipping && JSON.parse(selectedFreeShipping).id,
        ShippingFee: obj.priceSummary.shippingFee,
        ExpectedDeliveryWhen: obj.deliveryMethods[0].expectedDeliveryWhen,
        Note: "",
        PaymentReturnUrl: "http://localhost:5173/user/result",
      })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          console.log(error.request)
        } else {
          console.log("Error", error.message)
        }
        console.log(error.config)
      })

    if (response) {
      window.location.replace(response.paymentUrl)
      console.log(response)
    }
  }

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

          {address === null ? (
            <CircularProgress />
          ) : (
            <div className='flex items-center gap-5'>
              <div className='font-medium'>{address.receiverName}</div>
              <div className='font-medium'>{address.phoneNumber}</div>
              <div>
                {address.detailAddress}, {address.commune}, {address.district},{" "}
                {address.province}
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
                      options={addressList}
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
                      onClick={(objAddress) => {
                        handleOpenEditAddress(objAddress)
                      }}
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
                      value={editAddress?.receiverName}
                      onChange={(value) =>
                        setEditAddress((prev) => ({
                          ...prev,
                          receiverName: value,
                        }))
                      }
                      placeholder={"Tên"}
                      title={"Tên"}
                      type='text'
                      // errorMsg={errors.lastName}
                    />

                    <Input
                      value={editAddress?.phoneNumber}
                      onChange={(value) =>
                        setEditAddress((prev) => ({
                          ...prev,
                          phoneNumber: value,
                        }))
                      }
                      placeholder={"Số điện thoại"}
                      title={"Số điện thoại"}
                      type='text'
                      // errorMsg={errors.phoneNumber}
                    />

                    <Input
                      value={editAddress?.detailAddress}
                      onChange={(value) =>
                        setEditAddress((prev) => ({
                          ...prev,
                          detailAddress: value,
                        }))
                      }
                      placeholder={"Địa chỉ cụ thể"}
                      title={"Địa chỉ cụ thể"}
                      type='text'
                      // errorMsg={errors.lastName}
                    />

                    {valueProvince && (
                      <Autocomplete
                        value={valueProvince}
                        onChange={(event, newValue) => {
                          setValueProvince(newValue)
                        }}
                        inputValue={editAddress?.province}
                        onInputChange={(event, newInputValue) => {
                          setEditAddress((prev) => ({
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
                    )}

                    {valueDistrict && (
                      <Autocomplete
                        value={valueDistrict}
                        onChange={(event, newValue) => {
                          setValueDistrict(newValue)
                        }}
                        inputValue={editAddress.district}
                        onInputChange={(event, newInputValue) => {
                          setEditAddress((prev) => ({
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
                    )}

                    {valueCommune && (
                      <Autocomplete
                        value={valueCommune}
                        onChange={(event, newValue) => {
                          setValueCommune(newValue)
                        }}
                        inputValue={editAddress.commune}
                        onInputChange={(event, newInputValue) => {
                          setEditAddress((prev) => ({
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
                    )}
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
          )}
        </div>

        <div className='flex flex-col gap-3 rounded bg-white p-5 shadow'>
          <div className='flex items-center justify-between'>
            <div className='text-xl'>Sản phẩm</div>
            <div className='flex'>
              <div className='min-w-44 text-center'></div>
              <div className='min-w-44 text-center'>Đơn giá</div>
              <div className='min-w-44 text-center'>Số lượng</div>
              <div className='min-w-44 text-center'>Thành tiền</div>
            </div>
          </div>

          {products &&
            products.map((item, index) => (
              <div
                key={index}
                className='flex items-center justify-between text-sm'
              >
                <div className='flex items-center gap-2'>
                  <img
                    className='h-20 w-20'
                    alt='img'
                    src={item.imageUrl || noImage}
                  />
                  <div className='line-clamp-3 text-sm'>{item.productName}</div>
                </div>

                <div className='flex'>
                  {item.skuName && (
                    <div
                      className='flex min-w-44 max-w-44 flex-col items-start justify-start 
                  rounded bg-slate-100 p-3 hover:cursor-pointer'
                    >
                      <div className='flex items-center gap-1 text-sm'>
                        <span className='font-medium'>Phân loại hàng</span>
                      </div>
                      <div className='text-start text-sm'>{item.skuName}</div>
                    </div>
                  )}
                  <div className='min-w-44 text-center'>
                    <VND number={item.unitPrice} />
                  </div>
                  <div className='min-w-44 text-center'>{item.quantity}</div>
                  <div className='min-w-44 text-center'>
                    <VND number={item.totalPrice} />
                  </div>
                </div>
              </div>
            ))}
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
                  options={listVouchers}
                  onChange={handleChangeVoucher}
                  value={selectedVoucher}
                />
              </div>
            }
            actionComp={<Button onClick={handleAddVoucher}>Xác nhận</Button>}
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
                  options={listDiscountShip}
                  onChange={handleChangeFreeShipping}
                  value={selectedFreeShipping}
                />
              </div>
            }
            actionComp={
              <Button onClick={handleAddDiscountShipping}>Xác nhận</Button>
            }
            isCancelButton
          ></Modal>
        </div>

        {/* <div className='flex justify-between rounded bg-white p-5 shadow'>
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
        </div> */}

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
              <VND
                className={`min-w-52 text-end`}
                number={obj.priceSummary.subTotal || 0}
              />
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Phí vận chuyển</div>
              <VND
                className={`min-w-52 text-end`}
                number={obj.priceSummary.shippingFee || 0}
              />
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Giảm giá phí vận chuyển</div>

              <div className='flex min-w-52 items-center justify-end'>
                <span>-</span>
                <VND
                  className={`text-end`}
                  number={parseInt(obj.priceSummary.shippingDiscount) || 0}
                />
              </div>
            </div>

            <div className='flex items-center'>
              <div className='min-w-52'>Voucher</div>

              <div className='flex min-w-52 items-center justify-end'>
                <span>-</span>
                <VND
                  className={`text-end`}
                  number={obj.priceSummary.orderVoucherDiscount || 0}
                />
              </div>
            </div>

            {/* <div className='flex items-center'>
              <div className='min-w-52'>KKBooks Xu</div>

              <div className='flex min-w-52 items-center justify-end'>
                <span>-</span>
                <VND className={`text-end`} number={500} />
              </div>
            </div> */}

            <div className='flex items-center'>
              <div className='min-w-52'>Tổng thanh toán</div>
              <VND
                className={`min-w-52 text-end text-3xl font-medium text-ct-green-400`}
                number={parseInt(obj.priceSummary.total) || 0}
              />
            </div>
          </div>

          <div className='w-full py-5'>
            <Divider />
          </div>

          <div className='flex items-center justify-end'>
            <Button className={`min-w-64`} onClick={handleOrder}>
              Đặt hàng
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CheckOut
