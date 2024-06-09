import * as request from "~/utils/request"

export const login = async (obj) => {
  try {
    const res = await request.postMethod("Account/login", obj, true)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const forgotPassword = async (obj) => {
  try {
    const res = await request.postMethod("Account/forgotPassword", obj, true)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const updatePassword = async (obj, staffId) => {
  try {
    const res = await request.putMethod("Account/updatePassword", obj, staffId)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
