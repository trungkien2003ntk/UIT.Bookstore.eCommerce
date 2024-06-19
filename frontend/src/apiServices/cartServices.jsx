import * as request from "../utils/request"

export const getShoppingCart = async () => {
  try {
    const res = await request.getMethod(`shoppingcart`, true, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const addShoppingCart = async (obj) => {
  try {
    const res = await request.postMethod(`shoppingcart/add`, true, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const updateCart = async (obj) => {
  try {
    const res = await request.postMethod(
      `shoppingcart/update`,
      true,
      false,
      obj
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
