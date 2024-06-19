// CUSTOMER
export const getIsCustomerLogin = () => {
  const isCustomerLogin = window.localStorage.getItem("isCustomerLogin")

  return JSON.parse(isCustomerLogin)
}

export const setIsCustomerLogin = (isCustomerLogin) => {
  window.localStorage.setItem(
    "isCustomerLogin",
    JSON.stringify(isCustomerLogin)
  )
}

export const getCustomerAccessToken = () => {
  const customerAccessToken = window.localStorage.getItem("customerAccessToken")

  return customerAccessToken
}

export const setCustomerAccessToken = (customerAccessToken) => {
  window.localStorage.setItem("customerAccessToken", customerAccessToken)
}

export const getCustomerRefreshToken = () => {
  const customerRefreshToken = window.localStorage.getItem(
    "customerRefreshToken"
  )

  return customerRefreshToken
}

export const setCustomerRefreshToken = (customerRefreshToken) => {
  window.localStorage.setItem("customerRefreshToken", customerRefreshToken)
}

// ADMIN
export const getIsAdminLogin = () => {
  const isAdminLogin = window.localStorage.getItem("isAdminLogin")

  return JSON.parse(isAdminLogin)
}

export const setIsAdminLogin = (isAdminLogin) => {
  window.localStorage.setItem("isAdminLogin", JSON.stringify(isAdminLogin))
}

export const getAdminAccessToken = () => {
  const adminAccessToken = window.localStorage.getItem("adminAccessToken")

  return adminAccessToken
}

export const setAdminAccessToken = (adminAccessToken) => {
  window.localStorage.setItem("adminAccessToken", adminAccessToken)
}

export const getAdminRefreshToken = () => {
  const adminAccessToken = window.localStorage.getItem("adminAccessToken")

  return adminAccessToken
}

export const setAdminRefreshToken = (adminRefreshToken) => {
  window.localStorage.setItem("adminRefreshToken", adminRefreshToken)
}
