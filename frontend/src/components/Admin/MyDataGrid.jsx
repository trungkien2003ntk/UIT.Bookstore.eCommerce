/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import { useState } from "react"
import { DataGrid } from "@mui/x-data-grid"
import { localeText } from "./localeText"

const MyDataGrid = ({ rows, columns, height, ...rest }) => {
  const [pageSize, setPageSize] = useState(12)

  return (
    <div style={{ height: height, width: "100%" }}>
      <DataGrid
        {...rest}
        rows={rows}
        columns={columns}
        // pagination
        pagination
        pageSize={pageSize}
        onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
        // filter
        disableColumnFilter
        filterMode='server'
        //
        localeText={localeText}
      />
    </div>
  )
}

export default MyDataGrid
