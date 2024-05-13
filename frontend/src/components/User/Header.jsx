import { Fragment, useState } from "react"
import { Link } from "react-router-dom"
import CombinationLogo from "../../assets/images/combination-logo.svg"

import {
  PopoverButton,
  Popover,
  PopoverPanel,
  Transition,
} from "@headlessui/react"
import { ChevronDownIcon } from "@heroicons/react/20/solid"

const category = [
  {
    name: "Sách trong nước",
    icon: (
      <svg
        xmlns='http://www.w3.org/2000/svg'
        viewBox='0 0 24 24'
        fill='currentColor'
        className='h-6 w-6 text-orange-500'
      >
        <path d='M11.25 4.533A9.707 9.707 0 0 0 6 3a9.735 9.735 0 0 0-3.25.555.75.75 0 0 0-.5.707v14.25a.75.75 0 0 0 1 .707A8.237 8.237 0 0 1 6 18.75c1.995 0 3.823.707 5.25 1.886V4.533ZM12.75 20.636A8.214 8.214 0 0 1 18 18.75c.966 0 1.89.166 2.75.47a.75.75 0 0 0 1-.708V4.262a.75.75 0 0 0-.5-.707A9.735 9.735 0 0 0 18 3a9.707 9.707 0 0 0-5.25 1.533v16.103Z' />
      </svg>
    ),
    children: [
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh chungf dsdsd dsdsdkn dsdskd sdskdsd sdskdsds sds ddfd",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
      {
        name: "Nuôi dạy con",
        children: [
          {
            name: "Cẩm nang làm cha mẹ",
          },
          {
            name: "Phương pháp giáo dục trẻ các nước",
          },
          {
            name: "Phát triển trí tuệ cho trẻ",
          },
          {
            name: "Phát triển kỹ năng cho trẻ",
          },
        ],
      },
    ],
  },
  {
    name: "Sách nước ngoài",
    icon: (
      <svg
        xmlns='http://www.w3.org/2000/svg'
        viewBox='0 0 24 24'
        fill='currentColor'
        className='h-6 w-6 text-[#6A52F3]'
      >
        <path d='M21.721 12.752a9.711 9.711 0 0 0-.945-5.003 12.754 12.754 0 0 1-4.339 2.708 18.991 18.991 0 0 1-.214 4.772 17.165 17.165 0 0 0 5.498-2.477ZM14.634 15.55a17.324 17.324 0 0 0 .332-4.647c-.952.227-1.945.347-2.966.347-1.021 0-2.014-.12-2.966-.347a17.515 17.515 0 0 0 .332 4.647 17.385 17.385 0 0 0 5.268 0ZM9.772 17.119a18.963 18.963 0 0 0 4.456 0A17.182 17.182 0 0 1 12 21.724a17.18 17.18 0 0 1-2.228-4.605ZM7.777 15.23a18.87 18.87 0 0 1-.214-4.774 12.753 12.753 0 0 1-4.34-2.708 9.711 9.711 0 0 0-.944 5.004 17.165 17.165 0 0 0 5.498 2.477ZM21.356 14.752a9.765 9.765 0 0 1-7.478 6.817 18.64 18.64 0 0 0 1.988-4.718 18.627 18.627 0 0 0 5.49-2.098ZM2.644 14.752c1.682.971 3.53 1.688 5.49 2.099a18.64 18.64 0 0 0 1.988 4.718 9.765 9.765 0 0 1-7.478-6.816ZM13.878 2.43a9.755 9.755 0 0 1 6.116 3.986 11.267 11.267 0 0 1-3.746 2.504 18.63 18.63 0 0 0-2.37-6.49ZM12 2.276a17.152 17.152 0 0 1 2.805 7.121c-.897.23-1.837.353-2.805.353-.968 0-1.908-.122-2.805-.353A17.151 17.151 0 0 1 12 2.276ZM10.122 2.43a18.629 18.629 0 0 0-2.37 6.49 11.266 11.266 0 0 1-3.746-2.504 9.754 9.754 0 0 1 6.116-3.985Z' />
      </svg>
    ),
    children: [
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
    ],
  },
  {
    name: "Sách giáo khoa",
    icon: (
      <svg
        xmlns='http://www.w3.org/2000/svg'
        viewBox='0 0 24 24'
        fill='currentColor'
        className='h-6 w-6 text-red-500'
      >
        <path d='M12 7.5a2.25 2.25 0 1 0 0 4.5 2.25 2.25 0 0 0 0-4.5Z' />
        <path
          fillRule='evenodd'
          d='M1.5 4.875C1.5 3.839 2.34 3 3.375 3h17.25c1.035 0 1.875.84 1.875 1.875v9.75c0 1.036-.84 1.875-1.875 1.875H3.375A1.875 1.875 0 0 1 1.5 14.625v-9.75ZM8.25 9.75a3.75 3.75 0 1 1 7.5 0 3.75 3.75 0 0 1-7.5 0ZM18.75 9a.75.75 0 0 0-.75.75v.008c0 .414.336.75.75.75h.008a.75.75 0 0 0 .75-.75V9.75a.75.75 0 0 0-.75-.75h-.008ZM4.5 9.75A.75.75 0 0 1 5.25 9h.008a.75.75 0 0 1 .75.75v.008a.75.75 0 0 1-.75.75H5.25a.75.75 0 0 1-.75-.75V9.75Z'
          clipRule='evenodd'
        />
        <path d='M2.25 18a.75.75 0 0 0 0 1.5c5.4 0 10.63.722 15.6 2.075 1.19.324 2.4-.558 2.4-1.82V18.75a.75.75 0 0 0-.75-.75H2.25Z' />
      </svg>
    ),
    children: [
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
    ],
  },
  {
    name: "Văn phòng phẩm",
    icon: (
      <svg
        xmlns='http://www.w3.org/2000/svg'
        viewBox='0 0 24 24'
        fill='currentColor'
        className='h-6 w-6 text-[#44A1F7]'
      >
        <path d='M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157 3.712 3.712 1.157-1.157a2.625 2.625 0 0 0 0-3.712ZM19.513 8.199l-3.712-3.712-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32L19.513 8.2Z' />
      </svg>
    ),
    children: [
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
    ],
  },
  {
    name: "Đồ chơi trẻ em",
    icon: (
      <svg
        xmlns='http://www.w3.org/2000/svg'
        viewBox='0 0 24 24'
        fill='currentColor'
        className='h-6 w-6 text-[#F7AD11]'
      >
        <path d='M11.25 5.337c0-.355-.186-.676-.401-.959a1.647 1.647 0 0 1-.349-1.003c0-1.036 1.007-1.875 2.25-1.875S15 2.34 15 3.375c0 .369-.128.713-.349 1.003-.215.283-.401.604-.401.959 0 .332.278.598.61.578 1.91-.114 3.79-.342 5.632-.676a.75.75 0 0 1 .878.645 49.17 49.17 0 0 1 .376 5.452.657.657 0 0 1-.66.664c-.354 0-.675-.186-.958-.401a1.647 1.647 0 0 0-1.003-.349c-1.035 0-1.875 1.007-1.875 2.25s.84 2.25 1.875 2.25c.369 0 .713-.128 1.003-.349.283-.215.604-.401.959-.401.31 0 .557.262.534.571a48.774 48.774 0 0 1-.595 4.845.75.75 0 0 1-.61.61c-1.82.317-3.673.533-5.555.642a.58.58 0 0 1-.611-.581c0-.355.186-.676.401-.959.221-.29.349-.634.349-1.003 0-1.035-1.007-1.875-2.25-1.875s-2.25.84-2.25 1.875c0 .369.128.713.349 1.003.215.283.401.604.401.959a.641.641 0 0 1-.658.643 49.118 49.118 0 0 1-4.708-.36.75.75 0 0 1-.645-.878c.293-1.614.504-3.257.629-4.924A.53.53 0 0 0 5.337 15c-.355 0-.676.186-.959.401-.29.221-.634.349-1.003.349-1.036 0-1.875-1.007-1.875-2.25s.84-2.25 1.875-2.25c.369 0 .713.128 1.003.349.283.215.604.401.959.401a.656.656 0 0 0 .659-.663 47.703 47.703 0 0 0-.31-4.82.75.75 0 0 1 .83-.832c1.343.155 2.703.254 4.077.294a.64.64 0 0 0 .657-.642Z' />
      </svg>
    ),
    children: [
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
      {
        name: "Nuôi dạy con",
        children: [
          {
            name: "Cẩm nang làm cha mẹ",
          },
          {
            name: "Phương pháp giáo dục trẻ các nước",
          },
          {
            name: "Phát triển trí tuệ cho trẻ",
          },
          {
            name: "Phát triển kỹ năng cho trẻ",
          },
        ],
      },
    ],
  },
  {
    name: "Đồ lưu niệm",
    icon: (
      <svg
        xmlns='http://www.w3.org/2000/svg'
        viewBox='0 0 24 24'
        fill='currentColor'
        className='h-6 w-6 text-ct-green-400'
      >
        <path d='M9.375 3a1.875 1.875 0 0 0 0 3.75h1.875v4.5H3.375A1.875 1.875 0 0 1 1.5 9.375v-.75c0-1.036.84-1.875 1.875-1.875h3.193A3.375 3.375 0 0 1 12 2.753a3.375 3.375 0 0 1 5.432 3.997h3.943c1.035 0 1.875.84 1.875 1.875v.75c0 1.036-.84 1.875-1.875 1.875H12.75v-4.5h1.875a1.875 1.875 0 1 0-1.875-1.875V6.75h-1.5V4.875C11.25 3.839 10.41 3 9.375 3ZM11.25 12.75H3v6.75a2.25 2.25 0 0 0 2.25 2.25h6v-9ZM12.75 12.75v9h6.75a2.25 2.25 0 0 0 2.25-2.25v-6.75h-9Z' />
      </svg>
    ),
    children: [
      {
        name: "Văn học",
        children: [
          {
            name: "Tiểu thuyết",
          },
          {
            name: "Truyện ngắn",
          },
          {
            name: "Light Novel",
          },
          {
            name: "Ngôn tình",
          },
        ],
      },
      {
        name: "Kinh tế",
        children: [
          {
            name: "Nhân vật - Bài học kinh doanh",
          },
          {
            name: "Quản trị - Lãnh đạo",
          },
          {
            name: "Marketing - Bán hàng",
          },
          {
            name: "Phân tích kinh tế",
          },
        ],
      },
      {
        name: "Tâm lý - Kỹ năng sống",
        children: [
          {
            name: "Kỹ năng sống",
          },
          {
            name: "Rèn luyện nhân cách",
          },
          {
            name: "Tâm lý",
          },
          {
            name: "Sách cho tuổi mới lớn",
          },
        ],
      },
      {
        name: "Nuôi dạy con",
        children: [
          {
            name: "Cẩm nang làm cha mẹ",
          },
          {
            name: "Phương pháp giáo dục trẻ các nước",
          },
          {
            name: "Phát triển trí tuệ cho trẻ",
          },
          {
            name: "Phát triển kỹ năng cho trẻ",
          },
        ],
      },
    ],
  },
]

const Header = () => {
  const [catePop, setCatePop] = useState(category[0])

  const handleCategory = (index) => {
    setCatePop(category[index])
  }

  return (
    <header className='relative w-full bg-white shadow'>
      <div className='mx-auto flex w-full max-w-screen-xl items-center justify-between p-4 lg:px-8'>
        <div className='logo w-full basis-1/5'>
          <img className='w-[150px]' src={CombinationLogo} alt='svg' />
        </div>

        <div className='flex basis-3/5 items-center justify-center'>
          <div className='category flex'>
            <div className='p-1 hover:cursor-pointer hover:rounded hover:bg-gray-200'>
              <Popover className=''>
                <PopoverButton className='flex select-none items-center outline-none'>
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={2}
                    stroke='currentColor'
                    className='h-8 w-8 cursor-pointer text-ct-green-400'
                  >
                    <path
                      strokeLinecap='round'
                      strokeLinejoin='round'
                      d='M3.75 6A2.25 2.25 0 0 1 6 3.75h2.25A2.25 2.25 0 0 1 10.5 6v2.25a2.25 2.25 0 0 1-2.25 2.25H6a2.25 2.25 0 0 1-2.25-2.25V6ZM3.75 15.75A2.25 2.25 0 0 1 6 13.5h2.25a2.25 2.25 0 0 1 2.25 2.25V18a2.25 2.25 0 0 1-2.25 2.25H6A2.25 2.25 0 0 1 3.75 18v-2.25ZM13.5 6a2.25 2.25 0 0 1 2.25-2.25H18A2.25 2.25 0 0 1 20.25 6v2.25A2.25 2.25 0 0 1 18 10.5h-2.25a2.25 2.25 0 0 1-2.25-2.25V6ZM13.5 15.75a2.25 2.25 0 0 1 2.25-2.25H18a2.25 2.25 0 0 1 2.25 2.25V18A2.25 2.25 0 0 1 18 20.25h-2.25A2.25 2.25 0 0 1 13.5 18v-2.25Z'
                    />
                  </svg>
                  <ChevronDownIcon
                    className='h-5 w-5 text-ct-green-400'
                    aria-hidden='true'
                  />
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
                    className='absolute left-1/2 top-full z-10 mt-2 flex max-h-fit w-full max-w-screen-xl 
                  -translate-x-1/2 hover:cursor-default'
                  >
                    <div
                      className='max-w-screen-xl flex-auto overflow-hidden rounded-3xl 
                    bg-white text-sm leading-6 shadow-lg ring-1 ring-gray-900/5'
                    >
                      <div className='grid grid-cols-4 divide-x-2 p-4'>
                        <div className='pr-4'>
                          <div className='title mb-4 inline-block pl-2 text-xl font-semibold text-ct-green-400'>
                            Danh mục sản phẩm
                          </div>

                          <div
                            className='list-cate col-span-1 flex h-full max-h-[450px] flex-col gap-2 
                            overflow-auto scrollbar-thin'
                          >
                            {category.map((item, index) => (
                              <div
                                key={index}
                                className='group flex justify-between rounded p-2 font-medium 
                                hover:cursor-pointer hover:bg-[#EDFFEB] hover:font-semibold'
                                onClick={() => handleCategory(index)}
                              >
                                <div className='flex items-center justify-center'>
                                  <div className='icon mr-2'>{item.icon}</div>
                                  <div className='name'>{item.name}</div>
                                </div>

                                <div className='arrow'>
                                  <svg
                                    xmlns='http://www.w3.org/2000/svg'
                                    fill='none'
                                    viewBox='0 0 24 24'
                                    strokeWidth={1}
                                    stroke='currentColor'
                                    className='h-6 w-6 group-hover:stroke-2'
                                  >
                                    <path
                                      strokeLinecap='round'
                                      strokeLinejoin='round'
                                      d='m8.25 4.5 7.5 7.5-7.5 7.5'
                                    />
                                  </svg>
                                </div>
                              </div>
                            ))}
                          </div>
                        </div>

                        <div className='col-span-3 pl-4'>
                          <div className='header mb-4 flex items-center'>
                            <div className='icon'>{catePop.icon}</div>
                            <div className='title ml-2 text-xl font-semibold text-ct-black-300'>
                              {catePop.name}
                            </div>
                          </div>

                          <div
                            className='grid max-h-[450px] grid-cols-4 gap-x-5 gap-y-5 overflow-auto
                          scrollbar-thin'
                          >
                            {catePop.children.map((child, index) => (
                              <div key={index}>
                                <div className='title font-bold uppercase'>
                                  {child.name}
                                </div>

                                <div className='content flex flex-col'>
                                  {child.children.map(
                                    (smallChild, smallIndex) => (
                                      <Link
                                        key={smallIndex}
                                        className='line-clamp-1 transition-all
                                        duration-200 ease-in-out hover:cursor-pointer 
                                        hover:text-[#F95A51]'
                                      >
                                        {smallChild.name}
                                      </Link>
                                    )
                                  )}

                                  <Link
                                    className='more text-blue-500 hover:cursor-pointer 
                                  hover:font-semibold'
                                  >
                                    Xem tất cả
                                  </Link>
                                </div>
                              </div>
                            ))}
                          </div>
                        </div>
                      </div>
                    </div>
                  </PopoverPanel>
                </Transition>
              </Popover>
            </div>
          </div>

          <div className='search-bar ml-5 flex-1'>
            <div
              className='flex w-full items-center justify-center rounded-lg border-[1px]
              border-slate-300 px-2 py-2 shadow'
            >
              <input
                className='flex-1 px-1 font-medium leading-normal text-ct-black-300 outline-none
                placeholder:text-sm placeholder:font-normal'
                placeholder='Tìm kiếm ...'
              />
              <div className='icon'>
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  viewBox='0 0 24 24'
                  fill='currentColor'
                  className='h-6 w-6 text-ct-green-400 hover:cursor-pointer'
                >
                  <path
                    fillRule='evenodd'
                    d='M10.5 3.75a6.75 6.75 0 1 0 0 13.5 6.75 6.75 0 0 0 0-13.5ZM2.25 10.5a8.25 8.25 0 1 1 14.59 5.28l4.69 4.69a.75.75 0 1 1-1.06 1.06l-4.69-4.69A8.25 8.25 0 0 1 2.25 10.5Z'
                    clipRule='evenodd'
                  />
                </svg>
              </div>
            </div>
          </div>
        </div>

        <ul className='flex w-full basis-1/5 items-center justify-between pl-12'>
          <li className='notifications group rounded p-2 hover:cursor-pointer hover:bg-gray-200'>
            <Link className=''>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                fill='none'
                viewBox='0 0 24 24'
                strokeWidth={2}
                stroke='currentColor'
                className='h-6 w-6 text-ct-green-400'
              >
                <path
                  strokeLinecap='round'
                  strokeLinejoin='round'
                  d='M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0'
                />
              </svg>
            </Link>
          </li>
          <li className='cart rounded p-2 hover:cursor-pointer hover:bg-gray-200'>
            <Link>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                fill='none'
                viewBox='0 0 24 24'
                strokeWidth={2}
                stroke='currentColor'
                className='h-6 w-6 text-ct-green-400'
              >
                <path
                  strokeLinecap='round'
                  strokeLinejoin='round'
                  d='M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z'
                />
              </svg>
            </Link>
          </li>
          <li className='account rounded p-2 hover:cursor-pointer hover:bg-gray-200'>
            <Link>
              <svg
                xmlns='http://www.w3.org/2000/svg'
                fill='none'
                viewBox='0 0 24 24'
                strokeWidth={2}
                stroke='currentColor'
                className='h-6 w-6 text-ct-green-400'
              >
                <path
                  strokeLinecap='round'
                  strokeLinejoin='round'
                  d='M15.75 6a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0ZM4.501 20.118a7.5 7.5 0 0 1 14.998 0A17.933 17.933 0 0 1 12 21.75c-2.676 0-5.216-.584-7.499-1.632Z'
                />
              </svg>
            </Link>
          </li>
        </ul>
      </div>
    </header>
  )
}

export default Header
