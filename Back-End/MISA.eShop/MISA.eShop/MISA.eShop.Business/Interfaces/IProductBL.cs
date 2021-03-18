using MISA.eShop.Model;
using MISA.eShop.Model.Dictionary;
using System.Collections.Generic;

namespace MISA.eShop.Business.Interfaces
{
    public interface IProductBL: IBaseBL<Product>
    {
        BaseResponse GetPaging(int offset = 1, int limit = 25,
            string productSKU = "",
            string productName = "",
            int categoryCode = 0,
            int unitCode = 0,
            double sellPrice = 0,
            int isShow = 2,
            int status = 2);

    }
}
