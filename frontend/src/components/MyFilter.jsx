/* eslint-disable react/prop-types */
import { useState, useEffect } from "react"
import {
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
} from "@headlessui/react"
import { MinusIcon, PlusIcon } from "@heroicons/react/20/solid"

const MyFilter = ({ section }) => {
  const [mySection, setMySection] = useState(section)
  const [selectedColors, setSelectedColors] = useState([])

  const handleCheckboxChange = (index) => {
    const newOptions = [...mySection.options]
    newOptions[index].checked = !newOptions[index].checked
    setMySection({
      ...mySection,
      options: newOptions,
    })
  }

  useEffect(() => {
    const selected = mySection.options
      .filter((option) => option.checked)
      .map((option) => option.value)
    setSelectedColors(selected)
  }, [mySection])

  console.log(selectedColors)

  return (
    <Disclosure as='div' className='border-b border-gray-200 py-6'>
      {({ open }) => (
        <>
          <h3 className='-my-3 flow-root'>
            <DisclosureButton
              className='flex w-full items-center justify-between 
                bg-white py-3 text-sm text-gray-400 hover:text-gray-500'
            >
              <span className='font-medium text-gray-900'>
                {mySection.name}
              </span>
              <span className='ml-6 flex items-center'>
                {open ? (
                  <MinusIcon className='h-5 w-5' aria-hidden='true' />
                ) : (
                  <PlusIcon className='h-5 w-5' aria-hidden='true' />
                )}
              </span>
            </DisclosureButton>
          </h3>
          <DisclosurePanel className='pt-6'>
            <div className='space-y-4'>
              {mySection.options.map((option, optionIdx) => (
                <div key={option.value} className='flex items-center'>
                  <input
                    defaultValue={option.value}
                    type='checkbox'
                    defaultChecked={option.checked}
                    className='h-4 w-4 rounded border-gray-300 text-green-500
                    checked:bg-green-600 focus:bg-green-500'
                    onChange={() => handleCheckboxChange(optionIdx)}
                  />
                  <label
                    htmlFor={`filter-${mySection.id}-${optionIdx}`}
                    className='ml-3 text-sm text-gray-600'
                  >
                    {option.label}
                  </label>
                </div>
              ))}
            </div>
          </DisclosurePanel>
        </>
      )}
    </Disclosure>
  )
}

export default MyFilter
