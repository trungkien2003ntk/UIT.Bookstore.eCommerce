import * as request from "../utils/request"

export const getTrendyProducts = async () => {
  try {
    const res = await request.getMethod("products/trendy", false, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getProductList = async (str) => {
  try {
    const res = await request.getMethod("products" + str, false, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getProductList2 = async (str) => {
  try {
    const res = await request.getMethod("products/search" + str, false, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getProduct = async (id) => {
  try {
    const res = await request.getMethod(`products/${id}`, false, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
