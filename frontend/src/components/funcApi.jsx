export const objectToQueryString = (params) => {
  if (!params) return ``

  const queryString = Object.keys(params)
    .map((key) => `${encodeURIComponent(key)}=${params[key].join(",")}`)
    .join("&")

  return `?${queryString}`
}

export const searchParamsToObject = (searchParams) => {
  if (!searchParams) return {}

  const params = {}

  for (const [key, value] of searchParams.entries()) {
    if (value.includes(",")) {
      params[key] = value.split(",").map((value) => value)
    } else {
      params[key] = [value]
    }
  }

  return params
}
