<template>
  <div class="contenBody">
    <!-- header của content  -->
    <div class="contentHeader">
      <!-- nút thêm  -->
      <button
        title="Thêm mới cửa hàng"
        class="contentHeaderButton"
        @click="btnAddOnClick"
      >
        <div class="iconHeader">
          <div class="iconAdd"></div>
        </div>
        <div class="iconText">Thêm mới</div>
      </button>
      <!-- <ms-button class="contentHeaderButton" :bgcolor="bg-color" :icon="iconAdd">
        Thêm mới
      </ms-button> -->
      <!-- nhân bản  -->
      <button
        title="Nhân bản thêm một cửa hàng nữa"
        class="contentHeaderButton"
      >
        <div class="iconHeader">
          <div class="iconDupplicate"></div>
        </div>
        <div class="iconText">Nhân bản</div>
      </button>
      <!-- sửa  -->
      <button title="Chỉnh sửa thông tin cửa hàng" class="contentHeaderButton">
        <div class="iconHeader">
          <div class="iconEdit"></div>
        </div>
        <div class="iconText">Sửa</div>
      </button>
      <!-- xóa  -->
      <button title="Xóa cửa hàng" class="contentHeaderButton" @click="btnDeleteOnClick">
        <div class="iconHeader">
          <div class="iconDelete"></div>
        </div>
        <div class="iconText">Xóa</div>
      </button>
      <Popup
        :isHidePopup="isHidePopupParent" 
        @outIsHidePopup="outIsHidePopup"
        :product="product"
      />
      <!-- nạp  -->
      <button class="contentHeaderButton">
        <div class="iconHeader">
          <div class="iconImport"></div>
        </div>
        <div class="iconText">Nạp</div>
      </button>
    </div>

    <div class="gridTable">
      <table class="tableContent">
        <thead>
          <tr class="filter">
            <th width="4%">
              <input type="checkbox" >
            </th>
            <th width="10%">
              SKU
              <div class="filterField">
                <div class="iconSearch">*</div>
                <input
                  class="searchField"
                  @keyup.enter="getPaginate"
                  v-model.trim="enterSKU"
                />
              </div>
            </th>
            <th width="15%">
              Tên hàng hóa
              <div class="filterField">
                <div class="iconSearch">*</div>
                <input
                  class="searchField"
                  @keyup.enter="getPaginate"
                  v-model.trim="enterProductName"
                />
              </div>
            </th>
            <th width="25%">
              Nhóm hàng hóa
              <div class="filterField">
                <div class="iconSearch">*</div>
                <input class="searchField" 
                @keyup.enter="getPaginate"
                  v-model.trim="enterCategory"
                />
              </div>
            </th>
            <th width="8%">
              Đơn vị tính
              <div class="filterField">
                <div class="iconSearch">*</div>
                <input class="searchField"
                @keyup.enter="getPaginate"
                  v-model.trim="enterUnitName"
                />
              </div>
            </th>
            <th width="10%" >
              Giá bán TB
              <div class="filterField">
                <div class="iconSearch">≥</div>
                <input class="searchField"
                @keyup.enter="getPaginate"
                  v-model.number="enterSellPrice"
                  style="text-align:right"
                />
              </div>
            </th>
            <th width="15%">
              Hiển thị trên màn hình bán hàng
              <div class="filterField">
                <select style="width: 100%;height: 38px">
                  <option value="2" selected>Tất cả</option>
                  <option value="1">Có</option>
                  <option value="0">Không</option>
                </select>
              </div>
            </th>
            <th width="12%">
              Trạng thái
              <div class="filterField">
                <select style="width: 100%;height: 38px">
                  <option value="2">Tất cả</option>
                  <option value="1">Đang hoạt động</option>
                  <option value="0">Ngừng hoạt động</option>
                </select>
              </div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            class="tableRow"
            v-for="(product, index) in products"
            :key="index"
            :class="{ hightlight: isActive == index }"
            @click="rowOnClick(product, index)"
          >
            <td>
              <div class="cell">
                <input type="checkbox" v-model="checked" :value="product.productID">
              </div>
            </td>
            <td>
              <div class="cell">
                {{ product.sku }}
              </div>
            </td>
            <td>
              <div class="cell" id="productName">
                {{ product.productName }}
              </div>
            </td>
            <td>
              <div class="cell">
                {{ product.categoryName }}
              </div>
            </td>
            <td>
              <div class="cell">
                {{ product.unitName }}
              </div>
            </td>
            <td>
              <div class="cell" style="text-align: right; padding-right: 5px">
                {{ formatPrice(product.sellPrice) }}
              </div>
            </td>
            <td>
              <div class="cell">{{convertIsShow(product.isShow)}}</div>
            </td>
            <td>
              <div class="cell">{{convertStatus(product.status)}}</div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <!-- Component chi tiết  -->
    <Details :isHide="isHideParent" @outIsHide="outIsHide" />
    <div class="contentFooter">
      <div class="contentFooterLeft">
        <button class="buttonPage">
          <div class="firstPage"></div>
        </button>
        <button class="buttonPage">
          <div class="prevPage"></div>
        </button>
        <div class="contentFooterPaging">
          <a>Trang </a>
          <input
            type="text"
            class="currentPage"
            value="1"
            v-model.number="currentPage"
            @keyup.enter="getPaginate"
          />
          <a> trên </a>
          <div class="totalPage">{{products.length/typePage}}</div>
        </div>
        <button class="buttonPage">
          <div class="nextPage"></div>
        </button>
        <button class="buttonPage">
          <div class="lastPage"></div>
        </button>
        <button class="buttonPage">
          <div class="refresh"></div>
        </button>
        <select class="typePage" v-model="typePage">
          <option value="5">5</option>
          <option value="25" selected>25</option>
          <option value="50">50</option>
          <option value="100">100</option>
        </select>
      </div>

      <div class="contentFooterRight">
        <!-- todo proc cần trả về số bản ghi thỏa mãn và số trang thỏa mãn -->
        <a>Hiển thị 1- {{ products.length }} trên {{ products.length }} kết quả</a>
        <!-- <p>Các input đã check: {{ checked }}</p> -->
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import service from "@/data.js";
import Details from "./ProductListDetails.vue";
import Popup from "../popup/PopupDelete";
export default {
  name: "Product",
  props: {
  },
  components: {
    Details,
    Popup
  },
  methods: {
    /**Chọn dòng  */
     rowOnClick(product ,index) {
      this.isActive = index;
      this.product = product;
    },
    /**Mở form */
    btnAddOnClick() {
      this.isHideParent = !this.isHideParent;
    },
    btnDeleteOnClick(){
      this.isHidePopupParent = !this.isHidePopupParent;
    },

    /**Đóng form */
    outIsHide(e) {
      this.isHideParent = e;
    },
    outIsHidePopup(e) {
      this.isHidePopupParent = e;
    },
    /**format trạng thái */
    convertStatus(status){
      if(status == 0)
      return "Ngừng hoạt động";
      if(status == 1)
      return "Đang hoạt động";
      return "Tất cả";
    },
    /**format hiển thị */
    convertIsShow(isShow){
      if(isShow == 0)
      return "Có";
      if(isShow ==1)
      return "Không";
    },
    /**format tiền */
    formatPrice(value) {
        let val = (value/1).toFixed(2).replace('.', ',')
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")
    },
    /**API phân trang + filter */
    async getPaginate(){
      var offset = (this.currentPage - 1)*this.typePage + 1 ;
      // var sellPriceConverted = this.enterSellPrice.convertSellPrice();
    const response = await axios.get(
      "http://localhost:55810/api/Products/Paginate?offset="
      + offset +"&limit="
      +this.typePage+"&productSKU="
      +this.enterSKU+"&productName="
      +this.enterProductName+"&sellPrice="
      +this.enterSellPrice+"&productUnit="
      +this.enterUnitName+"&productCategory="
      +this.enterCategory+"&isShow=2&status=2"
    );
    this.products = response.data;
    }
  },
  computed:{
    // formatInputPrice(enterSellPrice){
    //   if(enterSellPrice < 0)
    //   enterSellPrice = 0;
    //   return enterSellPrice;
    // }
  },
  data() {
    return {
      product:{},
      checked: [],
      enterSKU:"",
      enterProductName:"",
      enterCategory:"",
      enterUnitName:"",
      enterSellPrice: 0,
      isActive: -1,
      typePage:25,
      currentPage:1,
      isHideParent: true,
      isHidePopupParent:true,
      products: service.getProducts(),
      allMode: "allPages",
      checkBoxesMode: "always",
    };
  },
  /**API ban đầu */
  async created() {
    var offset = (this.currentPage - 1)*this.typePage + 1 ;
    const response = await axios.get(
      "http://localhost:55810/api/Products/Paginate?offset="+ offset +"&limit="+ this.typePage+"&sellPrice=0&isShow=2&status=2"
    );
    this.products = response.data;
  },
};
</script>
<style scoped>
.filter .filterField {
  height: 46px;
  width: 100%;
  padding: 4px;
  display: flex;
}
.iconSearch {
  border: 1px solid #9e9e9e;
  border-radius: 4px;
  margin: 1px;
  height: 100%;
  width: 40px;
  line-height: 40px;
  text-align: center;
  background-color: #ffffff;
}
.searchField {
  margin: 1px;
  height: 100%;
  width: calc(100% - 40px);
  border-radius: 4px;
  border: 1px solid #9e9e9e;
}
.hightlight {
  
  background-color: #dfe3e8;
  border-left: 1px solid #dfe3e8;

}
/* .contenBody{} */

.contentHeader {
  height: 40px;
  background-color: #2b3173;
  position: relative;
  display: flex;
  justify-content: flex-start;
  align-items: center;
}

.contentHeader .contentHeaderButton .iconText {
  color: #ffffff;
  margin-left: 5px;
}

.contentHeader .contentHeaderButton .iconHeader {
  align-items: center;
  justify-content: center;
  padding: 3px;
}

.currentPage {
  width: 45px;
}
.typePage {
  width: 75px;
}
a {
  margin: 10px;
}

</style>