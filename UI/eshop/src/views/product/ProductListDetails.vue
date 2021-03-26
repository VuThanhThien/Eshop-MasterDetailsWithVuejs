<template>
  <div class="formBody" :class="{ isHide: isHide }">
    <div class="myButtonDialog" style="border-bottom: solid 1px gray">
      <button
        title="Ctrl Alt S"
        class="myButton"
        id="buttonSave"
        @click.prevent="save"
        v-shortkey="['ctrl', 'alt', 's']"
        @shortkey="save()"
      >
        <div class="iconSave"></div>
        Lưu
      </button>
      <button
        title="Ctrl Alt C"
        class="myButton"
        id="buttonCancel"
        @click="cancel"
        v-shortkey="['ctrl', 'alt', 'c']"
        @shortkey="cancel()"
      >
        <div class="iconCancel"></div>
        Hủy bỏ
      </button>
    </div>

    <div class="bodyDialog">
      <h3>THÔNG TIN CƠ BẢN</h3>

      <div class="rowForm">
        <div class="labelForm">Trạng thái kinh doanh</div>
        <input
          type="radio"
          name="radiocheck"
          value="1"
          v-model="product.status"
        />
        <label for="1" style="margin-right: 10px">Đang kinh doanh</label>
        <input
          type="radio"
          name="radiocheck"
          value="0"
          v-model="product.status"
        />
        <label for="2">Ngừng kinh doanh</label>
      </div>

      <div class="rowForm">
        <div class="labelForm">
          Tên hàng hóa<span style="color: red"> *</span>
        </div>
        <input
          type="text"
          class="productName"
          :class="{ borders: validateProduct.error }"
          v-model="product.productName"
          ref="name"
          @input="autoSku"
        />
        <div class="imgValidate" v-if="validateProduct.error"></div>
      </div>

      <div class="rowForm">
        <div class="labelForm">Nhóm hàng hóa</div>
        <select
          class="custom-select"
          id="selectCategory"
          style="width: 270px"
          v-model="product.categoryCode"
        >
          <option selected disabled>Chọn nhóm hàng hóa</option>
          <option
            v-for="category in categories"
            :key="category.id"
            :value="category.id"
          >
            {{ category.name }}
          </option>
        </select>
        <div class="iconPlusContain">
          <div class="iconPlus"></div>
        </div>
      </div>

      <div class="rowForm">
        <div class="labelForm">Mã SKU</div>
        <input type="text" v-model="product.sku" />
      </div>

      <div class="rowForm">
        <div class="labelForm">
          Giá mua <i class="fas fa-question-circle"></i>
        </div>
        <!-- <input type="number" class="inputNumber" placeholder="0"/> -->
        <DxNumberBox v-model="product.buyPrice" format="#,##0" />
      </div>

      <div class="rowForm">
        <div class="labelForm">Giá bán</div>
        <DxNumberBox v-model="product.sellPrice" format="#,##0" />
      </div>

      <div class="rowForm">
        <div class="labelForm">Đơn vị</div>
        <select
          class="custom-select"
          style="width: 270px"
          v-model="product.unitCode"
        >
          <option selected disabled>Chọn đơn vị</option>
          <option v-for="unit in units" :key="unit.id" :value="unit.id">
            {{ unit.name }}
          </option>
        </select>
        <div class="iconPlusContain">
          <div class="iconPlus"></div>
        </div>
      </div>

      <div>
        <div class="rowForm" style="align-items: center">
          <input type="checkbox" v-model="product.isShow" />
          Hiển thị trên màn hình bán hàng <i class="fas fa-question-circle"></i>
        </div>
      </div>

      <h3>THÔNG TIN THUỘC TÍNH</h3>

      <div class="rowForm">
        <div class="labelForm">Thuộc tính</div>
        <div style="display: flex">
          <div class="colorName">Màu sắc</div>
          <input-tag
            :add-tag-on-blur="true"
            v-model="colors"
            @remove="inputColorOnRemove($event)"
            @add="inputColorOnAdd($event)"
            @input="checkName"
          ></input-tag>
        </div>
      </div>

      <div class="rowForm" v-if="children.length > 0">
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
              </tr>
            </thead>
            <tbody class="detailBodyTable">
              <tr v-for="(tag, index) in children" :key="index">
                <td>
                  <div class="cell" style="width: 150px">
                    <input type="text" v-model="tag.productName" />
                  </div>
                </td>
                <td>
                  <div class="cell">
                    <input style="width: 100px" type="text" v-model="tag.sku" />
                  </div>
                </td>
                <td>
                  <div class="cell">
                    <!-- <DxNumberBox v-model="tag.barCode" format="##0" /> -->
                    <input
                      type="text"
                      v-model="tag.barCode"
                      @keypress="onlyNumber"
                    />
                  </div>
                </td>
                <td>
                  <div class="cell">
                    <DxNumberBox v-model="tag.buyPrice" format="#,##0" />
                  </div>
                </td>
                <td>
                  <div class="cell">
                    <DxNumberBox v-model="tag.sellPrice" format="#,##0" />
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
            <div class="symbolPen" v-if="!url">
              <div class="iconPen"></div>
              Biểu tượng
            </div>
            <div class="imgProduct" v-if="!url"></div>
            <img class="imgProduct" v-if="url" :src="url" />
          </div>
          <div class="xboxInner">
            <input
              type="file"
              name="file"
              id="file"
              class="inputfile"
              accept="image/x-png,image/gif,image/jpeg"
              @change="onFileChange"
            />
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
      <button
        title="Ctrl Alt S"
        class="myButton"
        id="buttonSave"
        @click.prevent="save"
        v-shortkey="['ctrl', 'alt', 's']"
        @shortkey="save()"
      >
        <div class="iconSave"></div>
        Lưu
      </button>
      <button
        title="Ctrl Alt C"
        class="myButton"
        id="buttonCancel"
        @click="cancel"
        v-shortkey="['ctrl', 'alt', 'c']"
        @shortkey="cancel()"
      >
        <div class="iconCancel"></div>
        Hủy bỏ
      </button>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import service from "@/data.js";

import DxNumberBox from "devextreme-vue/number-box";
export default {
  name: "ProductDetails",
  components: { DxNumberBox },

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
    productChildren: {
      type: Array,
      default() {
        return [];
      },
    },
  },

  watch: {
    isHide: {
      handler: function (value) {
        if (!value) {
          this.onShow();
        }
      },
    },
  },

  data() {
    return {
      url: null,
      colors: [],
      children: [],
      productChild: [],
      idToDelete: [],
      categories: service.getCategories(),
      units: service.getUnits(),
      barCode: null,
    };
  },
  methods: {
    /**
     * Input chỉ đc nhập số
     */
    onlyNumber($event) {
      let keyCode = $event.keyCode ? $event.keyCode : $event.which;
      if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) {
        // 46 is dot
        $event.preventDefault();
      }
    },
    checkName() {
      if (this.product.productName == null) {
        this.colors = [];
      }
      if (this.product.productName == "") {
        this.colors = [];
      }
    },
    onShow() {
      // bind các thông tin lên form
      this.bindProductToForm();
    },
    /**bind hàng hóa chi tiết lleen form */
    bindProductToForm() {
      this.children = [...this.productChildren];
      this.bindInputTagColors();
    },
    /**map màu của hàng hóa với màu của thẻ input-tag */
    bindInputTagColors() {
      if (this.children?.length) {
        let colors = [];
        colors = this.children.map((p) => p.color);
        this.colors = colors;
      }
    },
    /**xóa tag thì xóa hàng hóa */
    inputColorOnRemove(index) {
      const colorRemove = this.colors[index];
      if (
        this.children[index].productID !==
        "00000000-0000-0000-0000-000000000000"
      )
        this.idToDelete.push(this.children[index].productID);
      this.children = this.children.filter((p) => p.color !== colorRemove);
    },
    /**thêm tag thì thêm hàng hóa chi tiết*/
    inputColorOnAdd(color) {
      let exits = false;
      if (this.children?.length) {
        exits = this.children.findIndex((p) => p.color === color) > -1;
      }

      if (!exits) {
        this.inputBindDefaultChild(color);
      }
    },
    /**gán data mặc định cho hàng hóa chi tiết */
    inputBindDefaultChild(color) {
      let productChild = {};
      if (this.validateProduct.error) {
        this.$notify({
          title: "THÔNG BÁO",
          text: this.validateProduct.msg,
        });
        //focus
        if (this.validateProduct.typeError == "name") {
          this.$refs.name.focus();
        }
      } else {
        productChild.productName =
          this.product.productName.toString() + " ( " + color + " )";
        productChild.color = color;
        productChild.sku =
          this.product.sku +
          "-" +
          this.removeVietnameseTones(color).toUpperCase();
        productChild.productID = "00000000-0000-0000-0000-000000000000";
        productChild.productIDParent = "00000000-0000-0000-0000-000000000000";
        productChild.barCode = this.barCode + this.colors.length;
        productChild.buyPrice = this.product.buyPrice;
        productChild.sellPrice = this.product.sellPrice;
        productChild.status = this.product.status;
        this.children.push(productChild);
      }
    },

    /**Sinh SKU tự động */
    async autoSku() {
      let value = event.target.value;
      if (value != "") {
        value = this.removeVietnameseTones(value);
        const acronym = value
          .toUpperCase()
          .split(/\s/)
          .reduce((response, word) => (response += word.slice(0, 1)), "");
        await axios
          .get("http://localhost:55810/api/Products/SKU?productKey=" + acronym)
          .then((response) => (this.product.sku = response.data));
      }
    },
    /**Sự kiện hủy */
    cancel() {
      this.closeForm();
    },
    /**Đóng form */
    closeForm() {
      this.$emit("outIsHide", !this.isHide);
      this.colors = [];
    },
    /**format tiền */
    formatPrice(value) {
      let val = (value / 1).toFixed(2).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    /**sự kiển up ảnh */
    onFileChange(e) {
      const file = e.target.files[0];
      this.url = URL.createObjectURL(file);
    },
    /**Bỏ dấu  */
    removeVietnameseTones(str) {
      str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
      str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
      str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
      str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
      str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
      str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
      str = str.replace(/đ/g, "d");
      str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
      str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
      str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
      str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
      str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
      str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
      str = str.replace(/Đ/g, "D");
      // Some system encode vietnamese combining accent as individual utf-8 characters
      // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
      str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
      str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
      // Remove extra spaces
      // Bỏ các khoảng trắng liền nhau
      str = str.replace(/ + /g, " ");
      str = str.trim();
      return str;
    },

    /**Thêm sửa cha + thêm, sửa, xóa con ở đây */
    async syncProduct(synchronizeWrapper) {
      await axios
        .post("http://localhost:55810/api/Products/Sync", synchronizeWrapper)
        .then((response) => {
          if (response.data) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text: "Cập nhật thành công ",
            });
            // setTimeout(() => location.reload(), 1500);
            this.closeForm();
          }
        })
        .catch((e) => {
          if (e.response.status == 400) {
            this.$notify({
              // bad request
              type: "error",
              title: "THÔNG BÁO",
              text: e.response.data.devMsg,
            });
          }

          if (e.response.status == 500) {
            this.$notify({
              //Lỗi server
              type: "error",
              title: "THÔNG BÁO",
              text:
                "Vui lòng thay đổi mã vạch hoặc liên hệ MISA để được hỗ trợ!",
            });
          }
        });
    },
    /**ép kiểu lại data  */
    formatData() {
      //Ép kiểu int cho các trường
      if (this.product.categoryCode == null) {
        this.product.categoryCode = 0;
      } else {
        this.product.categoryCode = Number(this.product.categoryCode);
      }

      if (this.product.unitCode == null) {
        this.product.unitCode = 0;
      } else {
        this.product.unitCode = Number(this.product.unitCode);
      }

      if (this.product.status == null) {
        this.product.status = 0;
      } else {
        this.product.status = Number(this.product.status);
      }
      //validate isShow từ true false sang 1 0

      if (this.product.isShow == true) {
        this.product.isShow = 1;
      } else {
        this.product.isShow = 0;
      }

      if (
        this.product.productIDParent == null ||
        this.product.productIDParent == ""
      ) {
        //Gán productID = guid.empty để nó parse được
        this.product.productIDParent = "00000000-0000-0000-0000-000000000000";
      }
      //#endregion
    },
    /**Event nút Lưu */
    async save() {
      this.formatData();
      //Nếu validate thiếu tên thì thông báo và focus
      if (this.validateProduct.error) {
        this.$notify({
          title: "THÔNG BÁO",
          text: this.validateProduct.msg,
        });
        //focus
        if (this.validateProduct.typeError == "name") {
          this.$refs.name.focus();
        }
      } else {
        //Nếu productID null tức là đang thêm mới
        if (this.product.productID == null) {
          //Gán productID = guid.empty để nó parse được
          this.product.productID = "00000000-0000-0000-0000-000000000000";
        }
        //Nối mảng cha...con
        var newOrEditProduct = [this.product, ...this.children];
        var deleteProduct = this.idToDelete;
        var synchronizeWrapper = {
          newOrEditObject: newOrEditProduct,
          deleteObject: deleteProduct,
        };
        this.syncProduct(synchronizeWrapper);
      }
    },
  },
  computed: {
    /**Validate tên hàng hóa*/
    validateProduct() {
      let returnData = {
        error: false,
        msg: "",
        typeError: "",
      };

      if (this.product.sku === "" || this.product.sku == null) {
        returnData = {
          error: true,
          msg: "Vui lòng nhập sku hàng hóa",
          typeError: "sku",
        };
      }
      if (this.product.productName === "" || this.product.productName == null) {
        returnData = {
          error: true,
          msg: "Vui lòng nhập tên hàng hóa",
          typeError: "name",
        };
      }

      this.children.forEach((element) => {
        if (element.barCode == "") {
          returnData = {
            error: true,
            msg: "Vui lòng nhập mã vạch hàng hóa",
            typeError: "barCode",
          };
        }
        if (element.sku == "") {
          returnData = {
            error: true,
            msg: "Vui lòng nhập sku hàng hóa",
            typeError: "sku",
          };
        }
      });
      return returnData;
    },
  },
  async created() {
    var response = await axios.get(
      "http://localhost:55810/api/Products/BarCode"
    );
    this.barCode = response.data;
  },
};
</script>

<style>
.borders {
  border: 1px solid red !important;
}
.imgValidate {
  width: 16px;
  height: 16px;
  margin: 6px;
  background-size: contain;
  background-repeat: no-repeat;
  background-image: url("../../assets/images/exclamation.png");
}
.dx-state-hover {
  box-shadow: none !important;
  border: none !important;
}
.dx-texteditor {
  border: none !important;
}
</style>