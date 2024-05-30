import { Outlet } from "react-router-dom"
import Header from "./Header"
import Footer from "./Footer"

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

const UserLayout = () => {
  return (
    <div className='wrapper w-full'>
      <div className='content flex w-full flex-col bg-[#F3F4F6]'>
        <Header category={category} />
        <Outlet />
        <Footer />
      </div>
    </div>
  )
}

export default UserLayout
