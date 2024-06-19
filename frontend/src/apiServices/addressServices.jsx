import * as request from "../utils/request"

export const getAddressList = async () => {
  try {
    const res = await request.getMethod(
      `users/addresses/get-address-list`,
      true,
      false
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getProvince = async () => {
  try {
    const res = await request.getMethod(`location/province`, false, false)
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getDistrict = async ({ provinceId }) => {
  try {
    const res = await request.getMethod(
      `location/district?ProvinceId=${provinceId}`,
      false,
      false
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}

export const getCommune = async ({ districtId }) => {
  try {
    const res = await request.getMethod(
      `location/commune?DistrictId=${districtId}`,
      false,
      false
    )
    return res
  } catch (error) {
    return Promise.reject(error)
  }
}
