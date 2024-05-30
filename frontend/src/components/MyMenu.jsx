/* eslint-disable react/prop-types */
import { Fragment } from "react"
import { Menu, Transition, MenuItems, MenuButton } from "@headlessui/react"

const MyMenu = ({ button, children, direction = "right-0" }) => {
  return (
    <Menu as='div' className='relative flex items-center justify-center'>
      <MenuButton className='flex items-center justify-center'>
        {button}
      </MenuButton>

      <Transition
        as={Fragment}
        enter='transition ease-out duration-100'
        enterFrom='transform opacity-0 scale-95'
        enterTo='transform opacity-100 scale-100'
        leave='transition ease-in duration-75'
        leaveFrom='transform opacity-100 scale-100'
        leaveTo='transform opacity-0 scale-95'
      >
        <MenuItems
          className={`absolute top-full z-10 mt-3 w-56 divide-y 
        divide-gray-100 rounded-md bg-white shadow-lg ring-1 ring-black 
          ring-opacity-5 focus:outline-none ${direction}`}
        >
          {children}
        </MenuItems>
      </Transition>
    </Menu>
  )
}

export default MyMenu
