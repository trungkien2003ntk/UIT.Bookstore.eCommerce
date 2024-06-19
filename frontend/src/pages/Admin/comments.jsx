import { useState } from "react"
import { useNavigate } from "react-router-dom"

import FiveStar from "../../components/User/FiveStar"
import { Divider, Pagination } from "@mui/material"
import Button from "../../components/Button"

// const object = {
//   items: [
//     {
//       id: 1,
//       name: "Phạm Tuấn Kiệt",
//       avt: "https://se.uit.edu.vn/images/Logo/background.jpg",
//       comment: "Sản phẩm rất tốt",
//       rating: 5,
//       time: "2021-10-10",
//     },
//   ],
// }

const ratings = {
  averageRating: 4.0,
  totalApprovedRating: 5,
  totalRating: 5,
  total5StarRating: 3,
  total4StarRating: 1,
  total3StarRating: 0,
  total2StarRating: 0,
  total1StarRating: 1,
  totalRatingWithComment: 5,
  totalRatingWithImage: 0,
  ratings: {
    items: [
      {
        comment: "Good Product!",
        ratingValue: 5,
        userName: "Nguyễn Trung Kiên",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và bố",
        likesCount: 1,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 1,
      },
      {
        comment: "Amazing thing, but need some upgrades",
        ratingValue: 4,
        userName: "Trần Tiêm Kích",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và bố",
        likesCount: 1,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 2,
      },
      {
        comment: "Good Product!",
        ratingValue: 5,
        userName: "Nguyễn Trung Kiên",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và mẹ",
        likesCount: 0,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 3,
      },
      {
        comment: "Hơi tệ một chút",
        ratingValue: 1,
        userName: "Trần Tiêm Kích",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và ông",
        likesCount: 0,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 4,
      },
      {
        comment: "Good Product!",
        ratingValue: 5,
        userName: "Nguyễn Trung Kiên",
        userAvatarUrl: null,
        skuName: "Một ngày của tớ và bà",
        likesCount: 0,
        reportedCount: 0,
        response: "Thank you for your compliment!",
        status: "Approved",
        id: 5,
      },
    ],
    totalCount: 5,
    pageSize: 12,
    pageNumber: 1,
    totalPages: 1,
  },
}

const Comments = () => {
  const navigate = useNavigate()

  const handleChange = (e, page) => {
    console.log(page)
  }

  return (
    <div className='flex flex-col gap-3 overflow-hidden'>
      <div className='rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Quản lý bình luận
        </div>
      </div>
      <div className='rounded bg-white p-3 shadow'>
        <div className=' flex flex-col gap-5 p-5'>
          {ratings.ratings.items.map((item, index) => (
            <div key={index} className='flex flex-col gap-5'>
              <div className='flex gap-x-3'>
                <img
                  className='h-10 w-10 rounded-full'
                  alt='img'
                  src='https://down-vn.img.susercontent.com/file/sg-11134004-7qvef-lfjhk3o78hho40_tn'
                />

                <div className='flex min-w-96 flex-col gap-1'>
                  <div className='text-sm font-medium'>{item.userName}</div>
                  <div>
                    <FiveStar
                      classNameForSize={`w-5 h-5`}
                      rating={item.ratingValue}
                    />
                  </div>
                  <div className='text-sm'>
                    Sản phẩm bình luận:{" "}
                    <span className='font-medium text-blue-600 hover:cursor-pointer'>
                      {item.skuName}
                    </span>
                  </div>
                  <div className='mt-3'>{item.comment}</div>
                </div>

                <div className='flex min-w-72 items-center justify-center gap-3'>
                  <Button
                    className={`border-[1px] border-error bg-white !text-red-500 hover:!bg-red-50`}
                  >
                    Xóa
                  </Button>
                  <Button>Duyệt</Button>
                </div>
              </div>

              <Divider className='mt-5' />
            </div>
          ))}

          <div className='flex items-center justify-center'>
            <Pagination
              count={10}
              variant='outlined'
              color='primary'
              size='large'
              onChange={handleChange}
            />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Comments
