using System;
using System.Collections.Generic;

namespace MISA.eShop.Model.Dictionary
{
    public class SynchronizeWrapper
    {
        /// <summary>
        /// Danh sách các Product thêm mới hoặc sửa
        /// </summary>
        public List<Product> NewOrEditObject { get; set; }

        /// <summary>
        /// Danh sách các Id product cần xoá
        /// </summary>
        public List<Guid> DeleteObject { get; set; }
    }
}
