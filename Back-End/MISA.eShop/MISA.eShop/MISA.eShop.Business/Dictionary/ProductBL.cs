using MISA.eShop.Business.Base;
using MISA.eShop.Business.Interfaces;
using MISA.eShop.DataLayer.Interface;
using MISA.eShop.Model;
using MISA.eShop.Model.Dictionary;
using MISA.eShop.Model.Enum;
using System;

namespace MISA.eShop.Business.Dictionary
{
    public class ProductBL: BaseBL<Product>, IProductBL
    {
        private readonly IProductDL _productDL;

        public ProductBL(IBaseDL<Product> _baseDL, IProductDL productDL) : base(_baseDL)
        {
            _productDL = productDL;
        }


        /// <summary>
        /// Lấy danh sách hàng hoá
        /// </summary>
        /// <param name="offset">Vị trí bắt đầu (mặc định là 1)</param>
        /// <param name="limit">Giới hạn bản ghi cần lấy (mặc định là 25)</param>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="productCategory">Loại sản phẩm</param>
        /// <param name="productUnit">Đơn vị tính</param>
        /// <param name="sellPrice">Giá bán</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (1-Đang kinh doanh, 2-Ngừng kinh doanh, 3-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>
        public BaseResponse GetPaging(int offset = 1, int limit = 25,
            string productSKU = "",
            string productName = "",
            int categoryCode = 0,
            int unitCode = 0,
            double sellPrice = 0,
            int isShow = 2,
            int status = 2)
        {

            var param = new
            {
                limit = limit,
                offset = offset,
                productSKU = productSKU,
                productName = productName,
                categoryCode = categoryCode,
                unitCode = unitCode,
                sellPrice = sellPrice,
                isShow = isShow,
                status = status
            };

            var result = _productDL.GetPaging(param);

            // danh sách rỗng => trả vè mã 204
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.No_ConTent,
                    Data = null
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
        }

        /// <summary>
        /// Ghi đè hàm Insert từ base
        /// Với Product bổ sung thêm đoạn check trùng mã SKU trước khi insert
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public override BaseResponse Insert(Product product)
        {
            // kiểm tra trùng mã SKU không
            var productBySku = _baseDL.GetBySKU(product.SKU);

            // nếu tìm thấy mã Sku trong db => trả về lỗi 400
            if(productBySku != null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new MisaError(DevMessage.DuplicateSKU, ErrorCode.DuplicateData)
                };
                return response;
            }

            // tự sinh guidid mới
            product.ProductID = Guid.NewGuid();

            // không mã sku không tồn tại thì tiến hành insert dữ liệu vào db
            return base.Insert(product);
        }

        /// <summary>
        /// Ghi đè lại hàm Update từ base
        /// Với Product bổ sung thêm đoạn check trước khi update
        /// 1. check 2 id truyền lên qua router và object body có trùng khớp không
        /// 2. check người dùng sửa mã sku sang mã sku của sản phẩm khác
        /// </summary>
        /// <param name="productID">ID hàng hoá</param>
        /// <param name="product">Thông tin hàng hoá sửa đổi</param>
        /// <returns></returns>
        public override BaseResponse Update(Guid productID, Product product)
        {
            // Check Id truyền lên và ID trong object => nếu khác nhau thì trả về lỗi 400
            if(productID != product.ProductID)
            {
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new MisaError(DevMessage.Invalite_ProductID, ErrorCode.DataConflic)
                };
                return response;
            }

            // check trùng mã SKU trước khi cho phép cập nhật thông tin
            var productBySku = _baseDL.GetBySKU(product.SKU);

            // nếu tìm thấy mã Sku trong db && mã SKU tìm được đang được gắn cho product khác => trả về lỗi 400
            if (productBySku != null && productBySku.ProductID != productID)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new MisaError(DevMessage.DuplicateSKU, ErrorCode.DuplicateData)
                };
                return response;
            }

            // nếu không tìm thấy ==> sửa sang mã SKU khác
            // hoặc tìm thấy và id (tìm được) trùng với id truyền lên ==> đang sửa chính nó (chỉ sửa các thông tin khác)
            // thì mới cho phép cập nhật
            return base.Update(productID, product);
        }
    }
}
