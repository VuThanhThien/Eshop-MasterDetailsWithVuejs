<template>
  <div class="formBody" :class="{ isHide: isHide }">
    <div class="myButtonDialog" style="border-bottom: solid 1px gray">
      <button class="myButton" id="buttonSave">
        <div class="iconSave"></div>
        Lưu
      </button>
      <button class="myButton" id="buttonCancel" @click="closeForm">
        <div class="iconCancel"></div>
        Hủy bỏ
      </button>
    </div>

    <div class="bodyDialog">
      <h3>THÔNG TIN CƠ BẢN</h3>

      <div class="rowForm">
        <div class="labelForm">Trạng thái kinh doanh</div>
        <input type="radio" name="radiocheck" value="1" v-model="product.status"/>
        <label for="1" style="margin-right: 10px">Đang kinh doanh</label>
        <input type="radio" name="radiocheck" value="0" v-model="product.status"/>
        <label for="2">Ngừng kinh doanh</label>
      </div>

      <div class="rowForm">
        <div class="labelForm">
          Tên hàng hóa<span style="color: red"> *</span>
        </div>
        <input type="text" class="productName" v-model="product.productName"/>
      </div>

      <div class="rowForm">
        <div class="labelForm">Nhóm hàng hóa</div>
        <select class="custom-select" id="selectCategory" style="width: 270px" v-model="product.categoryCode">
          <option selected disabled>Chọn nhóm hàng hóa</option>
          <option value="1">Đồ dùng cá nhân</option>
          <option value="2">Đồ gia dụng</option>
          <option value="3">Đồng hồ</option>
          <option value="4">Giày</option>
          <option value="5">Giày bata</option>
          <option value="6">Giày da</option>
          <option value="7">Giày Sneaker</option>
          <option value="8">Giày vải</option>
          <option value="9">Hoa quả</option>
          <option value="10">Khăn</option>
          <option value="11">Nước ngọt</option>
          <option value="12">Quần</option>
          <option value="13">Váy</option>
          <option value="14">Xe máy</option>
        </select>
        <div class="iconPlusContain">
          <div class="iconPlus"></div>
        </div>
      </div>

      <div class="rowForm">
        <div class="labelForm">Mã SKU</div>
        <input type="text" v-model="product.sku"/>
      </div>

      <div class="rowForm">
        <div class="labelForm">
          Giá mua <i class="fas fa-question-circle"></i>
        </div>
        <!-- <input type="number" class="inputNumber" placeholder="0"/> -->
        <DxNumberBox :value="product.buyPrice" format="#,##0" />
      </div>

      <div class="rowForm">
        <div class="labelForm">Giá bán</div>
        <!-- <input type="number" class="inputNumber" placeholder="0" /> -->
        <DxNumberBox :value="product.sellPrice" format="#,##0" />
      </div>

      <div class="rowForm">
        <div class="labelForm">Đơn vị</div>
        <select class="custom-select" style="width: 270px" v-model="product.unitCode">
          <option selected disabled>Chọn đơn vị</option>
          <option value="1">Đôi</option>
          <option value="2">Chiếc</option>
          <option value="3">Túi</option>
          <option value="3">Kg</option>
          <option value="5">Thùng</option>
          <option value="6">Mét</option>
          <option value="7">Cuộn</option>
          <option value="8">Lít</option>
        </select>
        <div class="iconPlusContain">
          <div class="iconPlus"></div>
        </div>
      </div>

      <div>
        <div class="rowForm" style="align-items: center">
          <input type="checkbox" v-model="product.isShow"/>
          Hiển thị trên màn hình bán hàng <i class="fas fa-question-circle"></i>
        </div>
      </div>

      <h3>THÔNG TIN THUỘC TÍNH</h3>

      <div class="rowForm">
        <div class="labelForm">Thuộc tính</div>
        <div style="display: flex">
          <div class="colorName">Màu sắc</div>
          <input-tag :add-tag-on-blur="true" v-model="tags"></input-tag>
          <!-- <p>{{tags.length}}</p> -->
        </div>
      </div>

      <div class="rowForm" v-if="tags.length > 0">
        <div class="labelForm">Chi tiết thuộc tính</div>
        <div class="gridDetail">
          <table width="100%">
            <thead width="100%">
              <tr class="detailTable">
                <th width="50%">Tên hàng hóa</th>
                <th width="9%">Mã SKU</th>
                <th width="9%">Mã vạch</th>
                <th width="9%">Giá mua</th>
                <th width="9%">Giá bán</th>
                <th width="3%"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(tag, index) in tags" :key="index">
                <td>
                  <div class="cell">{{ tag }}</div>
                </td>
                <td>
                  <div class="cell"></div>
                </td>
                <td>
                  <div class="cell"></div>
                </td>
                <td>
                  <div class="cell">
                    <DxNumberBox :value="13415.24" format="#,##0" />
                  </div>
                </td>
                <td>
                  <div class="cell"></div>
                </td>
                <td>
                  <div class="cell">
                    <div class="iconTrash" @click="deleteEvent(index)"></div>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <h3>THÔNG TIN BỔ SUNG</h3>

      <div class="rowForm">
        <div class="labelForm">Mô tả</div>
        <textarea class="description" v-model="product.description"></textarea>
      </div>

      <div class="rowForm">
        <div class="labelForm">Ảnh hàng hóa</div>
        <div class="boxImage">
          <div class="imgBlock">
            <div class="symbolPen">
              <div class="iconPen"></div>
              Biểu tượng
            </div>
            <div class="imgProduct"></div>
          </div>
          <div class="xboxInner">
            <input type="file" name="file" id="file" class="inputfile" />
            <label for="file">...</label>
          </div>
        </div>
        <div style="padding: 20px">
          <span>- Lựa chọn biểu tượng để thay thế nếu không có ảnh</span><br />
          <span
            >- Định dạng ảnh(jpg, jpeg, png, gif) và dung lượng nhỏ hơn 5MB
          </span>
        </div>
      </div>
    </div>
    <div
      class="myButtonDialog"
      style="border-top: solid 1px gray"
      id="buttonBottom"
    >
      <button class="myButton" id="buttonSave">
        <div class="iconSave"></div>
        Lưu
      </button>
      <button class="myButton" id="buttonCancel" @click="closeForm">
        <div class="iconCancel"></div>
        Hủy bỏ
      </button>
    </div>
  </div>
</template>

<script>
import DxNumberBox from "devextreme-vue/number-box";
export default {
  name: "ProductDetails",
  props: {
    isHide: {
      type: Boolean,
      default() {
        return true;
      },
    },
    product: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  components: { DxNumberBox },
  data() {
    return {
      tags: [],
    };
  },
  methods: {
    closeForm() {
      this.$emit("outIsHide", !this.isHide);
    },
    deleteEvent(index) {
      this.$delete(this.tags, index);
    },
    /**format tiền */
    formatPrice(value) {
      let val = (value / 1).toFixed(2).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
  },
};
</script>

<style>
.formBody {
  position: fixed;
  z-index: 10;
  top: 60px;
  height: calc(100vh - 70px);
  width: calc(100% - 170px);
  background-color: #ffffff;
}

.rowForm {
  display: flex;
  margin: 10px 10px 10px 10px;
}
.isHide {
  display: none;
}
.myButtonDialog {
  position: relative;
  display: flex;
  padding: 10px;
  height: 52px;
  width: 100%;
  align-items: center;
  justify-content: flex-start !important;
  align-items: center;
  justify-content: space-between;
  background-color: white;
}

.myButton {
  height: 35px;
  width: auto;
  margin-left: 5px;
  margin-right: 5px;
  outline: none;
  border: none;
  cursor: pointer;
  padding: 0 14px 0 14px;
  display: flex;
  border-radius: 3px;
  align-items: center;
}

.colorName {
  height: 100%;
  width: 160px;
  justify-content: center;
  padding-top: 10px;
  padding-left: 10px;
  background-color: #d3d0d0;
}

.bodyDialog {
  max-height: calc(100% - 108px);
  overflow: auto;
}
.labelForm {
  width: 250px;
}
i {
  margin-left: 5px;
}
#buttonBottom {
  position: absolute;
  bottom: 0px;
}
.boxImage {
  height: 180px;
  width: 200px;
  border-color: #636dde !important;
  border-width: 1px !important;
  border-style: dashed;
  border-radius: 5px;
  -moz-border-radius: 3px;
  -webkit-border-radius: 3px;
}
/* .iconPen{
  height: 20px;
  width: 30px;
  background-color: yellow;
  float: right;
} */
.xboxInner {
  overflow: hidden;
  position: relative;
  left: 0;
  top: 0;
  width: 198px;
  height: 32px;
  justify-content: center !important;
}
.inputfile {
  width: 0.1px;
  height: 0.1px;
  opacity: 0;
  overflow: hidden;
  position: absolute;
  z-index: -1;
}
.inputfile + label {
  display: inline-block;
  cursor: pointer;
  background-color: #2b3173;
  color: white !important;
  width: 40px;
  height: 24px;
  right: auto;
  margin-left: 79px;
  margin-top: 4px;
  text-align: center;
  border-radius: 3px;
}
.inputNumber {
  width: 200px;
  background-color: #d3d0d0;
}
h3 {
  font-size: 15px;
  margin: 20px 0 20px 0;
  color: #7e7d7d;
}
span {
  font-style: italic;
  color: #8f8f8f;
}
.vue-input-tag-wrapper {
  max-width: 400px !important;
}
.vue-input-tag-wrapper .input-tag {
  background-color: #2b3173 !important;
  border: none !important;
  color: white !important;
}
.vue-input-tag-wrapper .input-tag .remove {
  color: #ffffff im !important;
}
.vue-input-tag-wrapper .new-tag {
  background: transparent;
  border: none !important;
  color: #777;
  font-size: 13px;
  font-weight: 400;
  margin-bottom: 6px;
  margin-top: 1px;
  padding: 4px;
  padding-left: 0;
  -webkit-box-flex: 1;
  flex-grow: 1;
}
#selectCategory {
  border-radius: 0px;
  border-top-left-radius: 3px;
  border-bottom-left-radius: 3px;
}
.iconPlusContain {
  width: 30px;
  height: 32px;
  background-color: #cccaca;
  padding: 9px;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
}
.gridDetail {
  max-height: 300px;
  min-width: 1450px;
}
.detailTable th {
  text-align: center;
  align-items: center;
  height: 32px;
  padding: 0;
}
.description {
  min-height: 180px;
  max-height: 300px;
  width: 438px;
  overflow: auto;
}
.imgBlock {
  padding: 0px;
  right: auto;
  left: 0px;
  top: 0px;
  margin: 0px;
  width: 198px;
  height: 146px;
}
.imgProduct {
  width: 50%;
  height: 140px;
  margin: 0 auto;
  background-image: url("https://testmisatrinhmisa.mshopkeeper.vn/backendg2/api/Image?id=12EB2AFC-16CA-45EC-8E9A-1A1B829EBFB9&type=4&mode=pad&CompanyCode=testmisatrinhmisa&w=200&h=180");
  background-position: center;
  background-repeat: no-repeat;
  background-size: contain;
}

.symbolPen {
  margin: -1px;
  display: flex;
  width: 100px;
  padding: 6px;
  height: 29px !important;
  margin-left: 50%;
  color: #aaaaaa;
  font-weight: bold;
  border: 1px solid #aaaaaa;
  border-radius: 4px;
}
.symbolPen:hover {
  cursor: pointer;
}
.iconPen {
  background: url("../../assets/common-icon.png") no-repeat -79px -180px;
  width: 12px;
  padding: 8px;
  margin-top: 3px;
}
</style>