using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.eShop.Business.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.eShop.API.Controllers
{
    /// <summary>
    /// Controller dùng chung
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase where T: class
    {
        /// <summary>
        /// Biến baseBL
        /// </summary>
        protected readonly IBaseBL<T> _baseBL;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="baseBL"></param>
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        // GET: api/<BasesController>
        /// <summary>
        /// Lấy tất cả danh sách đối tượng
        /// </summary>
        /// <returns></returns>
        /// created by vtthien 08.03.2021
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var result = _baseBL.GetAll();
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // GET api/<BasesController>/5
        /// <summary>
        /// Lấy thông tin theo định danh
        /// </summary>
        /// <param name="id">Định danh đối tượng cần lấy</param>
        /// <returns>Đối tượng có id trùng với id truyền lên</returns>
        [HttpGet("{id}")]
        public virtual IActionResult GetByID([FromRoute] Guid id)
        {
            var result = _baseBL.GetByID(id);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        /// <summary>
        /// Lấy thông tin theo code
        /// </summary>
        /// <param name="skuCode">Mã SKu đối tượng cần lấy</param>
        /// <returns>Đối tượng có sku trùng với sku nhập vào</returns>
        [HttpGet("ProductBySKU/{skuCode}")]
        public virtual IActionResult GetBySKU([FromRoute] string skuCode)
        {
            var result = _baseBL.GetBySKU(skuCode);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }


        // POST api/<BasesController>
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            var result = _baseBL.Insert(entity);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // PUT api/<BasesController>/5
        /// <summary>
        /// Cập nhật thông tin
        /// </summary>
        /// <param name="id">Định danh đối tượng</param>
        /// <param name="entity">Đối tượng thay đổi</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual IActionResult Put(Guid id, [FromBody] T entity)
        {
            var result = _baseBL.Update(id, entity);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // DELETE api/<BasesController>/5
        /// <summary>
        /// Xoá thông tin
        /// </summary>
        /// <param name="id">Định danh thông tin cần xoá</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var result = _baseBL.Delete(id);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

 
    }
}
