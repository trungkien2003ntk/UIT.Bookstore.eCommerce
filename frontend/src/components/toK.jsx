const toK = (number) => {
  if (number >= 1000) {
    if (Math.trunc((number % 1000) / 100)) {
      return (
        Math.trunc(number / 1000) +
        "," +
        Math.trunc((number % 1000) / 100) +
        "k"
      )
    }

    return Math.trunc(number / 1000) + "k"
  }

  return number
}

export default toK
