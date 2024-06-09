export const getLocalStorage = () => {
  const object = window.localStorage.getItem("object")

  return JSON.parse(object)
}

export const setLocalStorage = (obj) => {
  window.localStorage.setItem("object", JSON.stringify(obj))
}
