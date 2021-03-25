<template>
  <div class="contenBody">
    <!-- header của content  -->
    <div class="contentHeader">
      <!-- nút thêm  -->
      <button
        title="Ctrl + O"
        class="contentHeaderButton"
        @click="btnAddOnClick"
        v-shortkey="['ctrl', 'o']"
        @shortkey="btnAddOnClick()"
      >
        <div class="iconHeader">
          <div class="iconAdd"></div>
        </div>
        <div class="iconText">Thêm mới</div>
      </button>
      <button
        title="Ctrl + D"
        class="contentHeaderButton"
        @click="btnDuplicateOnClick"
        v-shortkey="['ctrl', 'd']"
        @shortkey="btnDuplicateOnClick()"
      >
        <div class="iconHeader">
          <div class="iconDupplicate"></div>
        </div>
        <div class="iconText">Nhân bản</div>
      </button>
      <!-- sửa  -->
      <button
        title="Ctrl + E"
        class="contentHeaderButton"
        @click="btnEditOnClick"
        v-shortkey="['ctrl', 'e']"
        @shortkey="btnEditOnClick()"
      >
        <div class="iconHeader">
          <div class="iconEdit"></div>
        </div>
        <div class="iconText">Sửa</div>
      </button>
      <!-- xóa  -->
      <button
        title="Ctrl X"
        class="contentHeaderButton"
        @click="btnDeleteOnClick"
        v-shortkey="['ctrl', 'x']"
        @shortkey="btnDeleteOnClick()"
      >
        <div class="iconHeader">
          <div class="iconDelete"></div>
        </div>
        <div class="iconText">Xóa</div>
      </button>
      <Popup
        :isHidePopup="isHidePopupParent"
        @outIsHidePopup="outIsHidePopup"
        :checked="checked"
      />
      <!-- nạp  -->
      <button class="contentHeaderButton" @click="reload">
        <div class="iconHeader">
          <div class="iconImport"></div>
        </div>
        <div class="iconText">Nạp</div>
      </button>
    </div>
<!-- table  -->
    <div class="gridTable">
      <table class="tableContent">
        <thead>
          <tr class="filter">
            <!-- checkAll -->
            <th width="3%">
              <input
                type="checkbox"
                id="checkAll"
                @click="checkAll"
                style="margin: 0"
              />
            </th>
            <!-- SKU -->
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
            <!-- Tên hàng hóa  -->
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
            <!-- Nhóm hàng hóa fix ở data.js -->
            <th width="20%">
              Nhóm hàng hóa
              <div class="filterField">
                <div class="iconSearch">*</div>
                <select
                  class="searchField"
                  v-model="enterCategory"
                  @change="getPaginate"
                >
                  <option selected value="0">Tất cả</option>
                  <option
                    v-for="category in categories"
                    :key="category"
                    :value="category.id"
                  >
                    {{ category.name }}
                  </option>
                </select>
              </div>
            </th>
            <!-- đơn vị fix ở data.js -->
            <th width="10%">
              Đơn vị tính
              <div class="filterField">
                <div class="iconSearch">*</div>
                <select
                  class="searchField"
                  v-model="enterUnitCode"
                  @change="getPaginate"
                >
                  <option selected value="0">Tất cả</option>
                  <option v-for="unit in units" :key="unit" :value="unit.id">
                    {{ unit.name }}
                  </option>
                </select>
              </div>
            </th>
            <th width="10%">
              Giá bán TB
              <div class="filterField">
                <div class="iconSearch">≥</div>
                <input
                  class="searchField"
                  @keyup.enter="getPaginate"
                  v-model.number="enterSellPrice"
                  style="text-align: right"
                />
              </div>
            </th>
            <th width="15%">
              Hiển thị trên màn hình bán hàng
              <div class="filterField">
                <select
                  style="width: 100%; height: 38px"
                  v-model="enterIsShow"
                  @change="getPaginate"
                >
                  <option value="2" selected>Tất cả</option>
                  <option value="1">Có</option>
                  <option value="0">Không</option>
                </select>
              </div>
            </th>
            <th width="12%">
              Trạng thái
              <div class="filterField">
                <select
                  style="width: 100%; height: 38px"
                  v-model="enterStatus"
                  @change="getPaginate"
                >
                  <option value="2">Tất cả</option>
                  <option value="1">Đang kinh doanh</option>
                  <option value="0">Ngừng kinh doanh</option>
                </select>
              </div>
            </th>
          </tr>
        </thead>
        <!-- body -->
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
                <input
                  type="checkbox"
                  class="checkbox"
                  v-model="checked"
                  :value="product.productID"
                />
              </div>
            </td>
            <td>
              <div class="cell">
                {{ product.sku }}
              </div>
            </td>
            <td>
              <div class="cell" id="productName" @dblclick="btnEditOnClick">
                {{ product.productName }}
              </div>
            </td>
            <td>
              <div class="cell">
                {{ convertCategory(product.categoryCode) }}
              </div>
            </td>
            <td>
              <div class="cell">
                {{ convertUnitName(product.unitCode) }}
              </div>
            </td>
            <td>
              <div class="cell" style="text-align: right; padding-right: 20px">
                {{ formatPrice(product.sellPrice) }}
              </div>
            </td>
            <td>
              <div class="cell">{{ convertIsShow(product.isShow) }}</div>
            </td>
            <td>
              <div class="cell">{{ convertStatus(product.status) }}</div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <!-- Component chi tiết  -->
    <Details
      :isHide="isHideParent"
      :product="product"
      :productChildren="productChildren"
      @outIsHide="outIsHide"
    />
    <!-- Footer -->
    <div class="contentFooter">
      <div class="contentFooterLeft">
        <!-- Trang đầu -->
        <button
          title="Home"
          class="buttonPage"
          @click="firstPage"
          v-shortkey="['home']"
          @shortkey="firstPage()"
        >
          <div class="firstPage"></div>
        </button>
        <!-- Trang trước -->
        <button
          title="Ctrl <"
          class="buttonPage"
          @click="prevPage"
          v-shortkey="['ctrl', 'arrowleft']"
          @shortkey="prevPage()"
        >
          <div class="prevPage"></div>
        </button>
        <div class="contentFooterPaging">
          <a>Trang </a>
          <!-- trang hiện tại -->
          <input
            type="text"
            class="currentPage"
            value="1"
            v-model.number="currentPage"
            @keyup.enter="getPaginate"
          />
          <a> trên </a>
          <!-- Tổng trang -->
          <div class="totalPage">
            {{ Math.ceil(lengthOfFilter / typePage) }}
          </div>
        </div>
        <!-- Trang tiếp theo -->
        <button
          title="Ctrl >"
          class="buttonPage"
          @click="nextPage"
          v-shortkey="['ctrl', 'arrowright']"
          @shortkey="nextPage()"
        >
          <div class="nextPage"></div>
        </button>
        <!-- Trang cuối -->
        <button
          title="End"
          class="buttonPage"
          @click="lastPage"
          v-shortkey="['end']"
          @shortkey="lastPage()"
        >
          <div class="lastPage"></div>
        </button>
        <button class="buttonPage" @click="reload">
          <div class="refresh"></div>
        </button>
        <!-- Bao nhiêu bản ghi trên một trang -->
        <select class="typePage" v-model="typePage" @change="getPaginate">
          <option value="5">5</option>
          <option value="25" selected>25</option>
          <option value="50">50</option>
          <option value="100">100</option>
        </select>
      </div>

      <div class="contentFooterRight">
        <a
          >Hiển thị {{ typePage * (currentPage - 1) + 1 }}-
          {{ currentPage * typePage }} trên {{ lengthOfFilter }} kết quả</a
        >
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
  props: {},
  components: {
    Details,
    Popup,
  },
  methods: {
    /**Chọn dòng  */
    rowOnClick(product, index) {
      this.isActive = index;
      this.product = product;
      this.getProductDetail();
    },
    /**Mở form */
    /**Mở form để thêm mới */
    btnAddOnClick() {
      // gán cho product bind lên là prodcut rỗng
      this.product = this.productEmpty;
      // Mở form
      this.isHideParent = !this.isHideParent;
      //clear hàng hóa temp lưu những thằng vừa click
      this.productEmpty = {};
      //clear hàng hóa chi tiết của thằng vừa được click
      this.productChildren = [];
      //tắt active đi
      this.isActive = -1;
    },

    /**Nút sửa */
    //Createdby VTT 19/03/21
    btnEditOnClick() {
      if (this.isActive < 0) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn hàng cần sửa",
        });
      } else this.isHideParent = !this.isHideParent;
      //tắt active đi
      this.isActive = -1;
    },

    /**Sự kiện nhân bản */
    btnDuplicateOnClick() {
      // gán id null và sku null
      this.product.productID = null;
      this.product.sku = "";
      this.product.barCode = null;
      //Createdby VTT 22/03/21
      if (this.isActive < 0) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn hàng cần nhân bản",
        });
      } else this.isHideParent = !this.isHideParent;
      //tắt active đi
      this.isActive = -1;
    },
    /**Xóa hàng hóa */
    btnDeleteOnClick() {
      //Createdby VTT 19/03/21
      if (this.checked.length == 0) {
        this.$notify({
          type: "warn",
          title: "Important message",
          text: "Vui lòng click vào ô checkbox của hàng hóa bạn muốn xóa!",
        });
      } else this.isHidePopupParent = !this.isHidePopupParent;
    },

    /**Đóng form */
    //Createdby VTT 19/03/21
    outIsHide(e) {
      this.isHideParent = e;
      this.product = this.productEmpty;
    },
    /**Đóng popup */
    outIsHidePopup(e) {
      this.isHidePopupParent = e;
    },
    /**format trạng thái */
    //Createdby VTT 19/03/21
    convertStatus(status) {
      if (status == 0) return "Ngừng kinh doanh";
      if (status == 1) return "Đang kinh doanh";
      return "Tất cả";
    },

    /**format nhóm hàng hóa */
    convertCategory(categoryCode) {
      if (categoryCode == 1) return "Đồ dùng cá nhân";
      if (categoryCode == 2) return "Đồ gia dụng";
      if (categoryCode == 3) return "Đồng hồ";
      if (categoryCode == 4) return "Giày";
      if (categoryCode == 5) return "Giày bata";
      if (categoryCode == 6) return "Giày da";
      if (categoryCode == 7) return "Giày Sneaker";
      if (categoryCode == 8) return "Giày vải";
      if (categoryCode == 9) return "Hoa quả";
      if (categoryCode == 10) return "Khăn";
      if (categoryCode == 11) return "Nước ngọt";
      if (categoryCode == 12) return "Quần";
      if (categoryCode == 13) return "Váy";
      if (categoryCode == 14) return "Xe máy";
      if (categoryCode == 15) return "Áo";
    },

    convertUnitName(unitCode) {
      if (unitCode == 1) return "Đôi";
      if (unitCode == 2) return "Chiếc";
      if (unitCode == 3) return "Túi";
      if (unitCode == 4) return "Kg";
      if (unitCode == 5) return "Thùng";
      if (unitCode == 6) return "Mét";
      if (unitCode == 7) return "Cuộn";
      if (unitCode == 8) return "Lít";
    },
    /**format hiển thị */
    //Createdby VTT 19/03/21
    convertIsShow(isShow) {
      if (isShow == 1) return "Có";
      if (isShow == 0) return "Không";
    },
    /**format tiền */
    formatPrice(value) {
      if (value == null) value = "";
      return value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
    },
    /**API phân trang + filter */
    async getPaginate() {
      //Giá nhập vào null thì bằng 0
      if (this.enterSellPrice == "") {
        this.enterSellPrice = 0;
      }
      //trang hiện tại < 1 thì trả vè trang 1
      if (this.currentPage < 1) {
        this.currentPage = 1;
      }
      //Trang hiện tại > trang lớn nhất trả về trang lớn nhất
      if (this.currentPage > Math.ceil(this.lengthOfFilter / this.typePage)) {
        this.currentPage = Math.ceil(this.lengthOfFilter / this.typePage);
      }

      var offset = (this.currentPage - 1) * this.typePage;
      const response = await axios
        .get(
          "http://localhost:55810/api/Products/Paginate?offset=" +
            offset +
            "&limit=" +
            this.typePage +
            "&categoryCode=" +
            this.enterCategory +
            "&productSKU=" +
            this.enterSKU +
            "&productName=" +
            this.enterProductName +
            "&sellPrice=" +
            this.enterSellPrice +
            "&unitCode=" +
            this.enterUnitCode +
            "&isShow=" +
            this.enterIsShow +
            "&status=" +
            this.enterStatus
        )
        .catch((e) => console.log(e));
      this.products = response.data;
      this.getLength();
    },

    /**Sang trang tiếp theo */
    nextPage() {
      this.currentPage = this.currentPage + 1;

      /**Nếu this.currentPage > tối đa thì gán bằng tối đa */
      if (this.currentPage > Math.ceil(this.lengthOfFilter / this.typePage))
        this.currentPage = Math.ceil(this.lengthOfFilter / this.typePage);
      this.getPaginate();
    },

    /**về trang trước */
    prevPage() {
      this.currentPage = this.currentPage - 1;

      if (this.currentPage < 1) this.currentPage = 1;
      this.getPaginate();
    },

    /**Về trang đầu */
    firstPage() {
      this.currentPage = 1;
      this.getPaginate();
    },

    /**Trang cuối */
    lastPage() {
      this.currentPage = Math.ceil(this.lengthOfFilter / this.typePage);
      this.getPaginate();
    },
    /**reload */
    reload() {
      location.reload();
    },
    /**API laays lengthOfFilter */
    async getLength() {
      let apiUrl =
        "http://localhost:55810/api/Products/Length?productSKU=" +
        this.enterSKU +
        "&productName=" +
        this.enterProductName +
        "&categoryCode=" +
        this.enterCategory +
        "&unitCode=" +
        this.enterUnitCode +
        "&sellPrice=" +
        this.enterSellPrice +
        "&isShow=" +
        this.enterIsShow +
        "&status=" +
        this.enterStatus;
      const response = await axios.get(apiUrl).catch((e) => console.log(e));
      this.lengthOfFilter = response.data;
    },
    /**Sự kiện select all checkbox */
    checkAll() {
      var checkBoxList = document.querySelectorAll(".checkbox");
      if (document.getElementById("checkAll").checked == true) {
        for (const checkbox of checkBoxList) {
          checkbox.checked = true;
        }
        this.checked = [];
        for (const product of this.products) {
          this.checked.push(product.productID);
        }
      } else {
        for (const checkbox of checkBoxList) {
          checkbox.checked = false;
        }
        this.checked = [];
      }
    },

    /**Lấy danh sách hàng hóa chi tiết theo id hàng hóa cha DONE
     * CreatedBy Vtthien 22/03/21
     */
    async getProductDetail() {
      const response = await axios.get(
        "http://localhost:55810/api/Products/ByParent/" + this.product.productID
      );
      this.productChildren = response.data;
      // console.log(this.productChildren);
    },
  },
  computed: {},
  data() {
    return {
      productChildren: [],
      lengthOfFilter: 0,
      product: {},
      productEmpty: {},
      checked: [],
      enterSKU: "",
      enterProductName: "",
      enterCategory: 0,
      enterUnitCode: 0,
      enterStatus: 2,
      enterIsShow: 2,
      enterSellPrice: 0,
      isActive: -1,
      typePage: 25,
      currentPage: 1,
      isHideParent: true,
      isHidePopupParent: true,
      categories: service.getCategories(),
      units: service.getUnits(),
      products: service.getProducts(),
      allMode: "allPages",
      checkBoxesMode: "always",
    };
  },
  /**API ban đầu */
  async created() {
    this.productEmpty = {};
    var offset = (this.currentPage - 1) * this.typePage + 1;
    const response = await axios.get(
      "http://localhost:55810/api/Products/Paginate?offset=" +
        offset +
        "&limit=" +
        this.typePage +
        "&sellPrice=0&isShow=2&status=2"
    );
    this.products = response.data;

    let apiUrl =
      "http://localhost:55810/api/Products/Length?productSKU=" +
      this.enterSKU +
      "&productName=" +
      this.enterProductName +
      "&categoryCode=" +
      this.enterCategory +
      "&unitCode=" +
      this.enterUnitCode +
      "&sellPrice=" +
      this.enterSellPrice +
      "&isShow=" +
      this.enterIsShow +
      "&status=" +
      this.enterStatus;
    const length = await axios.get(apiUrl);
    this.lengthOfFilter = length.data;
  },
};
</script>
<style scoped>
.filter .filterField {
  height: 46px;
  width: 100%;
  padding: 4px;
  display: flex;
  border-radius: 4px;
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
  width: 50px;
}
.typePage {
  width: 75px;
}
a {
  margin: 10px;
}
.totalPage {
  width: 15px;
}
</style>