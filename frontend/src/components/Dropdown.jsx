/* eslint-disable react/prop-types */
import {
  Listbox,
  ListboxButton,
  ListboxOption,
  ListboxOptions,
  Transition,
} from "@headlessui/react"
import { CheckIcon, ChevronUpDownIcon } from "@heroicons/react/20/solid"

function classNames(...classes) {
  return classes.filter(Boolean).join(" ")
}

const Dropdown = ({
  avt = false,
  placeholder,
  items,
  selected,
  setSelected,
}) => {
  return (
    <Listbox value={selected} onChange={setSelected}>
      {({ open }) => (
        <div className='relative min-w-52'>
          <ListboxButton
            className='relative w-full cursor-default rounded-md bg-white 
            py-1.5 pl-3 pr-10 text-left text-gray-900 shadow-sm ring-1 
            ring-inset ring-gray-300 hover:cursor-pointer focus:outline-none 
            focus:ring-2 focus:ring-ct-green-300 sm:text-sm sm:leading-6'
          >
            <span className='flex items-center'>
              {selected.avt && (
                <img
                  src={selected?.avt}
                  alt=''
                  className='h-5 w-5 flex-shrink-0 rounded-full'
                />
              )}
              <span className={`${avt && "ml-3"} block truncate`}>
                {selected?.text || placeholder}
              </span>
            </span>
            <span className='pointer-events-none absolute inset-y-0 right-0 ml-3 flex items-center pr-2'>
              <ChevronUpDownIcon
                className='h-5 w-5 text-gray-400'
                aria-hidden='true'
              />
            </span>
          </ListboxButton>

          <Transition
            show={open}
            leave='transition ease-in duration-100'
            leaveFrom='opacity-100'
            leaveTo='opacity-0'
          >
            <ListboxOptions
              className='absolute z-10 mt-1 max-h-56 w-full overflow-auto rounded-md 
              bg-white py-1 text-base shadow-lg ring-1 
            ring-black ring-opacity-5 focus:outline-none sm:text-sm'
            >
              {items?.map((item, index) => (
                <ListboxOption
                  key={index}
                  className={({ focus }) =>
                    classNames(
                      focus ? "bg-ct-green-300 text-white" : "",
                      !focus ? "text-gray-900" : "",
                      "relative cursor-default select-none py-2 pl-3 pr-9 hover:cursor-pointer"
                    )
                  }
                  value={item}
                >
                  {({ selected, focus }) => (
                    <>
                      <div className='flex items-center'>
                        {avt && (
                          <img
                            src={item?.avt}
                            alt=''
                            className='h-5 w-5 flex-shrink-0 rounded-full'
                          />
                        )}
                        <span
                          className={classNames(
                            selected ? "font-semibold" : "font-normal",
                            avt && "ml-3",
                            "block truncate"
                          )}
                        >
                          {item.text}
                        </span>
                      </div>

                      {selected ? (
                        <span
                          className={classNames(
                            focus ? "text-white" : "text-ct-green-400",
                            "absolute inset-y-0 right-0 flex items-center pr-4"
                          )}
                        >
                          <CheckIcon className='h-5 w-5' aria-hidden='true' />
                        </span>
                      ) : null}
                    </>
                  )}
                </ListboxOption>
              ))}
            </ListboxOptions>
          </Transition>
        </div>
      )}
    </Listbox>
  )
}

export default Dropdown
