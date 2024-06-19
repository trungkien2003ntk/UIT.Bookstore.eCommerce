import * as request from "../utils/request"

export const confirmCheckout = async (obj) => {
  try {
    const res = await request.postMethod("checkout/confirm", true, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const placeOrder = async (obj) => {
  try {
    console.log(obj)

    const res = await request.postMethod(
      "checkout/place-order",
      true,
      false,
      obj
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const handleIPN = async (query) => {
  try {
    const res = await request.getMethod("checkout/IPN?" + query, true, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
