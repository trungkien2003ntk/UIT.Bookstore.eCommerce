import * as request from "../utils/request"

export const loginCustomer = async (obj) => {
  try {
    const res = await request.postMethod("auth/sign-in", false, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const loginAdmin = async (obj) => {
  try {
    const res = await request.postMethod("auth/sign-in", false, true, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const requestOTP = async (obj) => {
  try {
    const res = await request.postMethod("auth/request-otp", false, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const verifyOTP = async (obj) => {
  try {
    const res = await request.postMethod("auth/verify-otp", false, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const register = async (obj) => {
  try {
    const res = await request.postMethod("auth/register", false, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const customerRefreshToken = async (obj) => {
  try {
    const res = await request.postMethod("auth/refresh", false, false, obj)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const adminRefreshToken = async (obj) => {
  try {
    const res = await request.postMethod("auth/refresh", false, true, obj)
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
