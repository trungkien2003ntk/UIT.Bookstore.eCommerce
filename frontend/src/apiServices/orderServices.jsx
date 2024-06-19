import * as request from "../utils/request"

export const getAllOrder = async () => {
  try {
    const res = await request.getMethod(`orders`, true, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getOrderDetail = async (id) => {
  try {
    const res = await request.getMethod(`orders/${id}`, true, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
