import axios from "axios"
import * as localStorage from "../store/localStorage"

const request = axios.create({
  baseURL: "https://kkbookstore-api.azurewebsites.net/api/",
})

export const getMethod = async (
  path,
  hasAuth = false,
  isAdmin = false,
  params = {},
  options = {}
) => {
  const headersCustomer = {
    Authorization: "Bearer " + localStorage.getCustomerAccessToken(),
  }

  const headersAdmin = {
    Authorization: "Bearer " + localStorage.getAdminAccessToken(),
  }

  if (hasAuth) {
    options.headers = isAdmin ? headersAdmin : headersCustomer
  }

  options.params = params || {}

  const response = await request.get(path, options)

  return response.data
}

export const postMethod = async (
  path,
  hasAuth = false,
  isAdmin = false,
  options = {}
) => {
  let headers = {}

  if (hasAuth) {
    headers = {
      Authorization:
        "Bearer " +
        (isAdmin
          ? localStorage.getAdminAccessToken()
          : localStorage.getCustomerAccessToken()),
    }
  }

  const response = await request.post(path, options, {
    headers: headers,
  })

  return response.data
}

export const putMethod = async (path, isAdmin = false, options = {}) => {
  const headersCustomer = {
    accessToken: localStorage.getCustomerAccessToken(),
  }

  const headersAdmin = {
    accessToken: localStorage.getAdminAccessToken(),
  }

  const response = await request.put(path, options, {
    headers: isAdmin ? headersAdmin : headersCustomer,
  })
  return response.data
}

export const deleteMethod = async (path, isAdmin = false) => {
  const headersCustomer = {
    accessToken: localStorage.getCustomerAccessToken(),
  }

  const headersAdmin = {
    accessToken: localStorage.getAdminAccessToken(),
  }

  const response = await request.delete(path, {
    headers: isAdmin ? headersAdmin : headersCustomer,
  })
  return response.data
}

export default request
