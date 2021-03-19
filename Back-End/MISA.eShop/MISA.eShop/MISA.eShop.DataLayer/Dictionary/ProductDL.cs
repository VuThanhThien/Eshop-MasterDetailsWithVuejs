
using Dapper;
using MISA.eShop.DataLayer.Base;
using MISA.eShop.DataLayer.Interface;
using MISA.eShop.Model.Dictionary;
using MISA.eShop.Model.Enum;
using System.Collections.Generic;

namespace MISA.eShop.DataLayer.Dictionary
{
    public class ProductDL : BaseDL<Product>, IProductDL
    {
        public ProductDL(IDbContext<Product> dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Lấy danh sách hàng hoá - có phân trang và tìm kiếm
        /// </summary>
        /// <param name="param">Tham số truyền vào Store</param>
        /// <returns>Danh sách hàng hoá</returns>
        public IEnumerable<Product> GetPaging(object param)
        {
            var procName = MISAProc.PROC_GET_PRODUCT_PAGING;

            var parameters = new DynamicParameters(param);
            
            var result = _dbContext.QueryStore(procName, parameters);

            return result;
        }

        /// <summary>
        /// Lấy số lương bản ghi thảo mãn điều kiện lọc
        /// </summary>
        /// <param name="param">Tham số truyền vào store</param>
        /// <returns>Số bản ghi</returns>
        public long GetLength(object param)
        {
            var procName = "Proc_GetLength";

            var parameters = new DynamicParameters(param);

            var result = (long)_dbContext.ExcuteScalarStore(procName, parameters);

            return result;
        }

        /// <summary>
        /// Lấy mã sku sinh tự động
        /// </summary>
        /// <param name="productKey">Chuỗi cắt gọn tên hàng hóa</param>
        /// <returns></returns>
        public string GetSKUGenerate(object param)
        {
            var procName = "Proc_GenProductSKU";

            var parameters = new DynamicParameters(param);

            var result = (string)_dbContext.ExcuteScalarStore(procName, parameters);

            return result;
        }
    }
}
