using MISA.eShop.Model.Dictionary;
using System.Collections.Generic;

namespace MISA.eShop.DataLayer.Interface
{
    public interface IProductDL: IBaseDL<Product>
    {
        /// <summary>
        /// Lấy danh sách hàng hoá - có phân trang và tìm kiếm
        /// </summary>
        /// <param name="param">Tham số truyền vào Store</param>
        /// <returns>Danh sách hàng hoá</returns>
        IEnumerable<Product> GetPaging(object param);

        /// <summary>
        /// Lấy số lượng bản ghi thỏa mãn điều kiện lọc
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<Product> GetLength(object param);

    }
}
