import axios from "axios"
import { getLocalStorage } from "../store/localStorage"

const request = axios.create({
  baseURL: "https://kkbookstore-api.azurewebsites.net/api/",
})

export const getMethod = async (path, options = {}, hasAuth = false) => {
  const headers = {
    accessToken: getLocalStorage().user.accessToken,
  }

  if (hasAuth === true) {
    options.headers = headers
  }

  const response = await request.get(path, options)

  return response.data
}

export const postMethod = async (path, options = {}, loginRequest) => {
  const headers = {}

  if (!loginRequest) {
    headers.staffId = getLocalStorage().user.staffId
  }

  // console.log('HEADERS', headers);

  const response = await request.post(path, options, {
    headers: headers,
  })

  return response.data
}

export const putMethod = async (path, options = {}, staffId) => {
  const headers = {}

  if (staffId) {
    headers.staffId = staffId
  } else {
    headers.staffId = getLocalStorage().user.staffId
  }

  const response = await request.put(path, options, {
    headers: headers,
  })
  return response.data
}

export const deleteMethod = async (path) => {
  const response = await request.delete(path, {
    headers: {
      staffId: getLocalStorage().user.staffId,
    },
  })
  return response.data
}

export default request
