import * as request from "../utils/request"

export const getAllProductTypes = async () => {
  try {
    const res = await request.getMethod(
      "product-types?Flatten=true",
      false,
      false
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
