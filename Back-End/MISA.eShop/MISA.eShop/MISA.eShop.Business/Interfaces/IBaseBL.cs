using MISA.eShop.Model;
using System;

namespace MISA.eShop.Business.Interfaces
{
    public interface IBaseBL<T> where T: class
    {
        BaseResponse GetAll();

        BaseResponse GetByID(Guid id);

        BaseResponse GetBySKU(string skuCode);

        BaseResponse Insert(T entity);

        BaseResponse Update(Guid id, T entity);

        BaseResponse Delete(Guid id);
    }
}
