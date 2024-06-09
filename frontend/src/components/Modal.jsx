/* eslint-disable react/display-name */
/* eslint-disable react/prop-types */
import { memo } from "react"
import {
  Dialog,
  DialogPanel,
  DialogTitle,
  Transition,
  TransitionChild,
} from "@headlessui/react"
import Button from "../components/Button"

const Modal = memo(
  ({
    open,
    setOpen,
    actionComp,
    clickOutside,
    title,
    contentComp,
    isCancelButton,
    freeShipping,
    discount,
  }) => {
    const handleClose = () => {
      if (clickOutside) {
        setOpen()
      }
    }

    return (
      <Transition show={open}>
        <Dialog className='relative z-10' onClose={handleClose}>
          <TransitionChild
            enter='ease-out duration-300'
            enterFrom='opacity-0'
            enterTo='opacity-100'
            leave='ease-in duration-200'
            leaveFrom='opacity-100'
            leaveTo='opacity-0'
          >
            <div className='fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity' />
          </TransitionChild>

          <div className='fixed inset-0 z-10 w-screen overflow-y-auto'>
            <div className='flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0'>
              <TransitionChild
                enter='ease-out duration-300'
                enterFrom='opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95'
                enterTo='opacity-100 translate-y-0 sm:scale-100'
                leave='ease-in duration-200'
                leaveFrom='opacity-100 translate-y-0 sm:scale-100'
                leaveTo='opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95'
              >
                <DialogPanel className='relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg'>
                  <div className='bg-white px-4 pb-4 pt-5 sm:p-4 sm:pb-4'>
                    <div className='sm:flex sm:items-start'>
                      {freeShipping && (
                        <div
                          className='mx-auto flex flex-shrink-0 items-center justify-center rounded-full 
                        bg-blue-400 sm:mx-0 sm:h-10 sm:w-10'
                        >
                          <img
                            className='h-10 w-10'
                            alt='img'
                            src='https://down-vn.img.susercontent.com/file/sg-11134004-22120-4cskiffs0olvc3'
                          />
                        </div>
                      )}
                      {discount && (
                        <div
                          className='mx-auto flex flex-shrink-0 items-center justify-center rounded-full 
                        bg-[#FFB323] sm:mx-0 sm:h-10 sm:w-10'
                        >
                          <img
                            className='h-6 w-6'
                            alt='img'
                            src='https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/event_cart_2/ico_promotion.svg?q=105685'
                          />
                        </div>
                      )}
                      <div
                        className={`${discount || freeShipping ? "sm:ml-4" : ""} 
                        mt-3 flex-1 text-center sm:mt-0 sm:text-left`}
                      >
                        <DialogTitle
                          as='h3'
                          className='text-base font-semibold uppercase leading-6 text-gray-900'
                        >
                          {title}
                        </DialogTitle>
                        <div className='mt-2 max-h-[400px] w-full overflow-auto'>
                          <div className='text-sm text-gray-500'>
                            {contentComp}
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className='justify-end gap-2 bg-gray-50 px-3 py-3 sm:flex'>
                    {isCancelButton && (
                      <Button
                        className={`border-[1px] border-ct-green-400 bg-green-50 
                        !text-ct-green-400  hover:!bg-green-100`}
                        onClick={setOpen}
                      >
                        {actionComp ? "Hủy" : "Đóng"}
                      </Button>
                    )}
                    {actionComp}
                  </div>
                </DialogPanel>
              </TransitionChild>
            </div>
          </div>
        </Dialog>
      </Transition>
    )
  }
)

export default Modal
