import { Divider, TextField } from "@mui/material"
import Input from "../../components/Input"
import Button from "../../components/Button"

const ProductDetail = () => {
  return (
    <div className='flex flex-col gap-5'>
      <div className='rounded bg-white p-3 shadow'>
        <div className='text-lg font-medium text-ct-green-400'>
          Thông tin sản phẩm
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Ảnh sản phẩm</div>
        <Divider />
        <div className='flex items-center'>
          <div
            className='flex h-40 w-40 items-center justify-center rounded
                border-[1px] border-dashed border-gray-400 text-gray-400 hover:cursor-pointer'
          >
            <svg
              xmlns='http://www.w3.org/2000/svg'
              viewBox='0 0 24 24'
              fill='currentColor'
              className='size-6'
            >
              <path
                fillRule='evenodd'
                d='M12 3.75a.75.75 0 0 1 .75.75v6.75h6.75a.75.75 0 0 1 0 1.5h-6.75v6.75a.75.75 0 0 1-1.5 0v-6.75H4.5a.75.75 0 0 1 0-1.5h6.75V4.5a.75.75 0 0 1 .75-.75Z'
                clipRule='evenodd'
              />
            </svg>
          </div>
          <img
            className='ml-3 h-40 w-40 rounded border-[1px] border-dashed border-gray-400 object-cover'
            alt='img'
            src='https://cdn0.fahasa.com/media/catalog/product/a/t/atlat_1.jpg'
          />
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Thông tin chung</div>
        <Divider />
        <div className='flex flex-col gap-4'>
          <Input
            placeholder={"Tên sản phẩm"}
            title={"Tên sản phẩm"}
            type='text'
            value={"Alat Địa Lý Việt Nam"}
          />

          <div className='flex flex-col gap-1'>
            <div className='text-sm font-semibold text-green-800'>Mô tả</div>
            <TextField
              value={"Alat Địa Lý Việt Nam xuất bản 2003"}
              color='success'
              multiline
              maxRows={10}
              minRows={5}
            />
          </div>
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Giá sản phẩm</div>
        <Divider />
        <div className='flex flex-col gap-4'>
          <Input title={"Giá nhập"} type='text' value={0} />
          <Input title={"Giá bán"} type='text' value={0} />
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Kho hàng</div>
        <Divider />
        <div className='flex flex-col gap-4'>
          <Input title={"Tồn kho"} type='text' value={0} />
        </div>
      </div>

      <div className='flex flex-col gap-3 rounded bg-white p-3 shadow'>
        <div className='font-semibold text-gray-600'>Thông tin bổ sung</div>
        <Divider />
        <div className='flex flex-col gap-4'>
          <Input
            title={"Loại sản phẩm"}
            type='text'
            placeholder={"Loại sản phẩm"}
          />
        </div>
      </div>

      <div className='mb-10 flex items-center justify-end'>
        <Button className={`min-w-52`}>Lưu</Button>
      </div>
    </div>
  )
}

export default ProductDetail
