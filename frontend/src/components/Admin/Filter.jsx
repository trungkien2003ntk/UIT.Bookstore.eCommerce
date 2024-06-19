/* eslint-disable react-refresh/only-export-components */
/* eslint-disable react/prop-types */
import { useState, memo } from "react"
import { Modal, Box, Slide, Divider } from "@mui/material"
import Button from "../Button"

const style = {
  position: "absolute",
  top: "0",
  right: "0",
  maxWidth: 420,
  minWidth: 250,
  height: "100vh",
  bgcolor: "white",
  border: "none",
  outline: "none",
  display: "flex",
  flexDirection: "column",
}
const Filter = ({ children }) => {
  const [open, setOpen] = useState(false)
  const handleModal = () => setOpen((prev) => !prev)

  return (
    <div>
      <Button
        leftIcon={
          <svg
            xmlns='http://www.w3.org/2000/svg'
            viewBox='0 0 24 24'
            fill='currentColor'
            className='size-4'
          >
            <path
              fillRule='evenodd'
              d='M3.792 2.938A49.069 49.069 0 0 1 12 2.25c2.797 0 5.54.236 8.209.688a1.857 1.857 0 0 1 1.541 1.836v1.044a3 3 0 0 1-.879 2.121l-6.182 6.182a1.5 1.5 0 0 0-.439 1.061v2.927a3 3 0 0 1-1.658 2.684l-1.757.878A.75.75 0 0 1 9.75 21v-5.818a1.5 1.5 0 0 0-.44-1.06L3.13 7.938a3 3 0 0 1-.879-2.121V4.774c0-.897.64-1.683 1.542-1.836Z'
              clipRule='evenodd'
            />
          </svg>
        }
        className={`min-w-28`}
        onClick={handleModal}
      >
        Bộ lọc
      </Button>
      <Modal open={open} onClose={handleModal}>
        <Slide direction='left' in={open}>
          <Box sx={style}>
            <div className='flex h-full w-full flex-col'>
              <div className='flex items-center justify-between p-3'>
                <div className='text-xl font-medium'>Bộ lọc</div>
                <div
                  className='rounded-full p-2 hover:cursor-pointer hover:bg-slate-200'
                  onClick={handleModal}
                >
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    viewBox='0 0 24 24'
                    fill='currentColor'
                    className='size-6'
                  >
                    <path
                      fillRule='evenodd'
                      d='M5.47 5.47a.75.75 0 0 1 1.06 0L12 10.94l5.47-5.47a.75.75 0 1 1 1.06 1.06L13.06 12l5.47 5.47a.75.75 0 1 1-1.06 1.06L12 13.06l-5.47 5.47a.75.75 0 0 1-1.06-1.06L10.94 12 5.47 6.53a.75.75 0 0 1 0-1.06Z'
                      clipRule='evenodd'
                    />
                  </svg>
                </div>
              </div>

              <Divider />

              <div className='flex flex-1 flex-col'>{children}</div>

              <Divider />
              <div className='flex items-center justify-between p-3'>
                <Button
                  className={`border-[1px] bg-white !text-ct-green-400 hover:!bg-green-50`}
                >
                  Xóa bộ lọc
                </Button>
                <Button>Lọc</Button>
              </div>
            </div>
          </Box>
        </Slide>
      </Modal>
    </div>
  )
}

export default memo(Filter)
