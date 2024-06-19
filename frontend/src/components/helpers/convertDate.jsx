export const convertToGMT7 = (dateString) => {
  // Parse the input date string to a Date object
  const date = new Date(dateString)

  // Get the time in GMT+7 timezone
  const options = {
    timeZone: "Asia/Bangkok", // GMT+7 timezone
    hour: "2-digit",
    minute: "2-digit",
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  }

  // Use Intl.DateTimeFormat to format the date
  const formatter = new Intl.DateTimeFormat("en-GB", options)
  const parts = formatter.formatToParts(date)

  // Extract the formatted parts
  const formattedDateParts = {}
  parts.forEach((part) => {
    formattedDateParts[part.type] = part.value
  })

  // Construct the output string
  const formattedDateString = `${formattedDateParts.hour}:${formattedDateParts.minute} - ${formattedDateParts.day}/${formattedDateParts.month}/${formattedDateParts.year}`

  return formattedDateString
}
