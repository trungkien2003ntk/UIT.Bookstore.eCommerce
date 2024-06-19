import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { Checkbox } from "@mui/material"
import { green } from "@mui/material/colors"

import ProductItemCart from "../../components/Cart/ProductItemCart"
import VND from "../../components/vnd"
import Button from "../../components/Button"
import Modal from "../../components/Modal"
import VoucherRadio from "../../components/Checkout/VoucherRadio"

import * as cartServices from "../../apiServices/cartServices"
import * as voucherServices from "../../apiServices/voucherServices"
import * as checkoutServices from "../../apiServices/checkoutServices"
import useToast from "../../hooks/useToast"

const Cart = () => {
  const navigate = useNavigate()
  const toast = useToast()
  const [items, setItems] = useState(null)
  const [vouchers, setVouchers] = useState(null)

  const [date, setDate] = useState(new Date())

  const [money, setMoney] = useState({
    subtotal: 0,
    total: 0,
    totalSaved: 0,
  })

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

  const getShoppingCart = async () => {
    const response = await cartServices.getShoppingCart().catch((error) => {
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        // http.ClientRequest in node.js
        console.log(error.request)
      } else {
        // Something happened in setting up the request that triggered an Error
        console.log("Error", error.message)
      }
      console.log(error.config)
    })

    if (response) {
      console.log(response)
      setItems(response.items)
    }
  }

  const getVouchers = async () => {
    const response = await voucherServices
      .getVouchers({
        SelectedItemIds: selectedItems,
      })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data)
          console.log(error.response.status)
          console.log(error.response.headers)
        } else if (error.request) {
          console.log(error.request)
        } else {
          // Something happened in setting up the request that triggered an Error
          console.log("Error", error.message)
        }
        console.log(error.config)
      })

    if (response) {
      setVouchers(response.orderVouchers)
    }
  }

  useEffect(() => {
    getShoppingCart()
    getVouchers()
  }, [])

  const getConfirmCheckout = async () => {
    const response = await checkoutServices
      .confirmCheckout({ ItemIds: selectedItems })
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

    return response
  }

  const [selectedItems, setSelectedItems] = useState([])
  const [checkedAll, setCheckedAll] = useState(false)

  const updateCart = async (obj) => {
    const response = await cartServices.updateCart(obj).catch((error) => {
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
      getVouchers()

      console.log(response)

      setMoney(response.discountDetail)
      // const newItems = items

      // response.updatedItems.map((item) => {
      //   const index = newItems.findIndex((i) => i.skuId === item.skuId)
      //   newItems[index] = item
      // })

      // setItems(newItems)
    }
  }

  useEffect(() => {
    updateCart({
      ActionType: 0,
      SelectedItemIds: selectedItems,
      UpdateItems: [],
    })
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedItems])

  const handleCheckAll = (isChecked) => {
    setCheckedAll(isChecked)
    if (isChecked) {
      setSelectedItems(items.map((item) => item.id))
    } else {
      setSelectedItems([])
    }
  }

  const onChange = (isChecked, item) => {
    console.log({ isChecked, item })
    if (isChecked) {
      setSelectedItems([...selectedItems, item.id])
    } else {
      setSelectedItems(selectedItems.filter((i) => i !== item.id))
    }
  }

  const onChangeQuantity = (quantity, item) => {
    updateCart({
      ActionType: 1,
      SelectedItemIds: selectedItems,
      UpdateItems: [
        {
          Id: item.id,
          Quantity: quantity,
          OldQuantity: item.quantity,
        },
      ],
    })
  }

  const onSubmit = (selectedOptions) => {
    console.log(selectedOptions)

    const newItems = items

    newItems.map((item, index) => {
      if (item.id === selectedOptions[0].selected.id) {
        newItems[index] = {
          ...item,
          ...selectedOptions[0].selected.skuItem,
          imageUrl:
            item.productOptions[0].images[
              selectedOptions[0].selected.skuItem.optionIndex[0]
            ],
        }
      }
    })

    console.log("newItems", newItems)
    setItems(newItems)
    setDate(new Date())

    updateCart({
      ActionType: 2,
      SelectedItemIds: selectedItems,
      UpdateItems: [
        {
          Id: selectedOptions[0].selected.id,
          SkuId: selectedOptions[0].selected.skuItem.id,
        },
      ],
    })
  }

  const onDelete = (item) => {
    updateCart({
      ActionType: 3,
      SelectedItemIds: selectedItems,
      UpdateItems: [
        {
          Id: item.id,
        },
      ],
    })

    const newArray = items.filter((i) => i.id !== item.id)

    setItems(newArray)
  }

  const handleDelete = () => {
    updateCart({
      ActionType: 3,
      SelectedItemIds: selectedItems,
      UpdateItems: items.map((item) => ({
        Id: item.id,
      })),
    })

    const newArray = items.filter((i) => {
      if (!selectedItems.includes(i.id)) {
        return i
      }
    })

    setItems(newArray)
  }

  return (
    <div className='relative w-full'>
      <div
        className='mx-auto flex w-full max-w-screen-xl flex-col justify-between 
        gap-y-5 p-4 lg:px-8'
      >
        <div className='flex items-center gap-3 rounded bg-white p-5 shadow'>
          <div className='text-orange-500'>
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-14 '
            >
              <path
                fillRule='evenodd'
                d='M7.5 6v.75H5.513c-.96 0-1.764.724-1.865 1.679l-1.263 12A1.875 1.875 0 0 0 4.25 22.5h15.5a1.875 1.875 0 0 0 1.865-2.071l-1.263-12a1.875 1.875 0 0 0-1.865-1.679H16.5V6a4.5 4.5 0 1 0-9 0ZM12 3a3 3 0 0 0-3 3v.75h6V6a3 3 0 0 0-3-3Zm-3 8.25a3 3 0 1 0 6 0v-.75a.75.75 0 0 1 1.5 0v.75a4.5 4.5 0 1 1-9 0v-.75a.75.75 0 0 1 1.5 0v.75Z'
                clipRule='evenodd'
              />
            </svg>
          </div>

          <span className='text-2xl font-medium text-orange-500'>Giỏ hàng</span>
        </div>

        <div className='flex items-center rounded bg-white p-2 font-medium shadow'>
          <Checkbox
            sx={{
              color: green[500],
              "&.Mui-checked": {
                color: green[500],
              },
            }}
            checked={checkedAll}
            onChange={(e) => handleCheckAll(e.target.checked)}
          />

          <span className='flex-1'>Sản phẩm</span>

          <span className='w-44 text-center'></span>
          <span className='w-44 text-center'>Đơn giá</span>
          <span className='w-44 text-center'>Số lượng</span>
          <span className='w-44 text-center'>Số tiền</span>
          <span className='w-44 text-center'>Thao tác</span>
        </div>

        {items && (
          <div className='flex w-full flex-col gap-y-5 bg-transparent'>
            {items.map((item, index) => (
              <ProductItemCart
                key={index}
                item={item}
                checked={selectedItems.includes(item.id)}
                onChange={onChange}
                onChangeQuantity={onChangeQuantity}
                onSubmit={onSubmit}
                onDelete={onDelete}
              />
            ))}
          </div>
        )}

        <div
          className={`flex items-center justify-between rounded bg-white px-3 py-3 shadow`}
        >
          <div className='flex items-center gap-5'>
            <Checkbox
              sx={{
                color: green[500],
                "&.Mui-checked": {
                  color: green[500],
                },
              }}
              checked={checkedAll}
              onChange={(e) => handleCheckAll(e.target.checked)}
            />

            <div
              className='hover:cursor-pointer hover:text-ct-green-400'
              onClick={() => handleCheckAll(true)}
            >
              Chọn tất cả ({items ? items.length : 0})
            </div>

            <div
              className='hover:cursor-pointer hover:text-red-500'
              onClick={handleDelete}
            >
              Xóa
            </div>

            <div
              className='hover:cursor-pointer hover:text-blue-500'
              onClick={handleVoucher}
            >
              Chọn voucher
            </div>

            <Modal
              open={openVoucher}
              setOpen={handleVoucher}
              title={"VOUCHER"}
              contentComp={
                <div>
                  {vouchers && (
                    <VoucherRadio
                      options={vouchers}
                      onChange={handleChangeVoucher}
                      value={selectedVoucher}
                    />
                  )}
                </div>
              }
              actionComp={<Button onClick={handleVoucher}>Xác nhận</Button>}
              isCancelButton
            ></Modal>
          </div>

          <div className='flex items-center gap-5'>
            <div>Tổng thanh toán ({selectedItems.length} sản phẩm):</div>
            <div>
              {money.subtotal > 0 && (
                <VND
                  className={`font-medium text-gray-400 line-through`}
                  number={money?.subtotal && money?.subtotal}
                />
              )}
              <VND
                className={`text-xl font-medium text-ct-green-400`}
                number={money?.total && money?.total}
              />
            </div>
            <Button
              className={`min-w-52`}
              onClick={() => {
                if (selectedItems.length > 0) {
                  getConfirmCheckout().then((res) => {
                    navigate("/user/checkout", { state: { res } })
                  })
                } else {
                  toast("error", "Vui lòng chọn sản phẩm trước khi mua hàng")
                }
              }}
            >
              Mua hàng
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Cart
