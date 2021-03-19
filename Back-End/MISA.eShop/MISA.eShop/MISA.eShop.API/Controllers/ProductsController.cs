﻿using Microsoft.AspNetCore.Mvc;
using MISA.eShop.Business.Interfaces;
using MISA.eShop.Model.Dictionary;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.eShop.API.Controllers
{
    /// <summary>
    /// API hàng hoá
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BasesController<Product>
    {
        /// <summary>
        /// Biến productBL gọi lên tầng Bussiness xử lý nghiệp vụ hàng hoá
        /// </summary>
        private readonly IProductBL _productBL;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="productBL"></param>
        public ProductsController(IBaseBL<Product> baseBL, IProductBL productBL): base(baseBL)
        {
            _productBL = productBL;
        }

        // GET api/<ProductsController>
        /// <summary>
        /// Lấy danh sách hàng hoá - kèm theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="offset">Vị trí bắt đầu (mặc định là 1)</param>
        /// <param name="limit">Giới hạn bản ghi cần lấy (mặc định là 25)</param>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="categoryCode">Mã loại sản phẩm</param>
        /// <param name="unitCode"> Mã Đơn vị tính( Mặc định là 0, 1- đôi, 2- chiếc, 3- túi, 4- kg, 5- thùng, 6-met, 7- cuộn, 8-lit, 9- hộp)</param>
        /// <param name="sellPrice">Giá bán tối thiểu (Mặc định là 0)</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (0-Đang kinh doanh, 1-Ngừng kinh doanh, 2-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>
        [HttpGet("Paginate")]
        public IActionResult GetProductPaging(
            [FromQuery] int offset = 0, 
            [FromQuery] int limit = 25, 
            [FromQuery] string productSKU = "", 
            [FromQuery] string productName = "",
            [FromQuery] int categoryCode = 0,
            [FromQuery] int unitCode = 0,
            [FromQuery] double sellPrice = 0,
            [FromQuery] int isShow = 2,
            [FromQuery] int status = 2)
        {
            var result = _productBL.GetPaging(offset, limit, productSKU, productName, categoryCode, unitCode, sellPrice, isShow, status);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // GET api/<ProductsController>
        /// <summary>
        /// Lấy số bản ghi thỏa mãn yêu cầu search
        /// </summary>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="categoryCode">Mã loại sản phẩm</param>
        /// <param name="unitCode"> Mã Đơn vị tính( Mặc định là 0, 1- đôi, 2- chiếc, 3- túi, 4- kg, 5- thùng, 6-met, 7- cuộn, 8-lit, 9- hộp)</param>
        /// <param name="sellPrice">Giá bán tối thiểu (Mặc định là 0)</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (0-Đang kinh doanh, 1-Ngừng kinh doanh, 2-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>
        [HttpGet("Length")]
        public IActionResult GetLength(
            [FromQuery] string productSKU = "",
            [FromQuery] string productName = "",
            [FromQuery] int categoryCode = 0,
            [FromQuery] int unitCode = 0,
            [FromQuery] double sellPrice = 0,
            [FromQuery] int isShow = 2,
            [FromQuery] int status = 2)
        {
            var result = _productBL.GetLength( productSKU, productName, categoryCode, unitCode, sellPrice, isShow, status);
            return StatusCode((int)result.HTTPStatusCode, result);
        }

        // POST api/<ProductsController>
        /// <summary>
        /// Thêm mới một sản phẩm
        /// </summary>
        /// <param name="product">Sản phẩm cần thêm mới</param>
        /// <returns></returns>
        [HttpPost]
        public override IActionResult Post([FromBody] Product product)
        {
            var result = _productBL.Insert(product);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // PUT api/<ProductsController>/5
        /// <summary>
        /// Cập nhật thông tin hàng hoá
        /// </summary>
        /// <param name="productId">ID hàng hoá</param>
        /// <param name="product">Thông tin sửa đổi</param>
        /// <returns></returns>
        [HttpPut]
        public override IActionResult Put([FromQuery] Guid productId, [FromBody] Product product)
        {
            var result = _productBL.Update(productId, product);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

    }
}