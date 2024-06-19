import * as request from "../utils/request"

export const getProductRatings = async (id, params) => {
  try {
    const res = await request.getMethod(
      `products/${id}/ratings`,
      false,
      false,
      params
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
