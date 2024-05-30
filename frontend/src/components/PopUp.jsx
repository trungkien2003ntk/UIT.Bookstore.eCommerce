/* eslint-disable react/prop-types */
import { Fragment } from "react"
import {
  Popover,
  PopoverButton,
  PopoverPanel,
  Transition,
} from "@headlessui/react"

const PopUp = ({ button, children, direction = "middle" }) => {
  return (
    <Popover className='relative flex items-center justify-center'>
      <PopoverButton className='flex items-center justify-center outline-none'>
        {button}
      </PopoverButton>

      <Transition
        as={Fragment}
        enter='transition ease-out duration-200'
        enterFrom='opacity-0 translate-y-1'
        enterTo='opacity-100 translate-y-0'
        leave='transition ease-in duration-150'
        leaveFrom='opacity-100 translate-y-0'
        leaveTo='opacity-0 translate-y-1'
      >
        <PopoverPanel
          className={`absolute top-full z-10 mt-3 w-screen max-w-max 
          ${direction == "middle" && "left-1/2 -translate-x-1/2 "}
          ${direction == "right" && "right-0"}
          ${direction == "left" && "left-0"}`}
        >
          <div className='w-fit rounded-lg bg-white shadow-lg ring-1 ring-gray-900/5'>
            {children}
          </div>
        </PopoverPanel>
      </Transition>
    </Popover>
  )
}

export default PopUp
