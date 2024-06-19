import * as request from "../utils/request"

export const getMe = async () => {
  try {
    const res = await request.getMethod("users/me", true, false, {})
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
