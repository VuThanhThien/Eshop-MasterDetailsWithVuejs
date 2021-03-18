const products = [
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 10",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 11",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 09",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 01",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 02",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 03",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
    {
      productID: "00000000-0000-0000-0000-000000000000",
      SKU: "Res 04",
      productName: "Res01",
      productCategory: "15 Duy Tân",
      sellPrice: "01662804035",
      unit: "Đôi",
      isShow:"Có hiển thị",
      status:"Tất cả"
    },
  ]

import * as axios from "axios";
  
export default {
    getProducts() {
        return products;
    },
    async created() {
      var offset = (this.currentPage - 1)*this.typePage + 1 ;
      const response = await axios.get(
        "http://localhost:55810/api/Products/Paginate?offset="+ offset +"&limit="+ this.typePage+"&sellPrice=0&isShow=2&status=2"
      );
      this.products = response.data;
    },

};