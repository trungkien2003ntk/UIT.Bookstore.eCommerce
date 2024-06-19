import * as request from "../utils/request"

export const getVouchers = async (obj) => {
  try {
    console.log(obj)

    const res = await request.postMethod(
      `discount/get-vouchers-cart`,
      true,
      false,
      obj
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
