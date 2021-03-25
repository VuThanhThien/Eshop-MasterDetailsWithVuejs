<template>
  <div>
    <div class="modal" :class="{ isHidePopup: isHidePopup }">
      <div class="modalMask"></div>
      <div class="popup">
        <div class="dialogHeader">
          <button class="closeDialog" @click="closePopup">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
              class="feather feather-x"
            >
              <line x1="18" y1="6" x2="6" y2="18"></line>
              <line x1="6" y1="6" x2="18" y2="18"></line>
            </svg>
          </button>
          <h2>Xóa dữ liệu</h2>
        </div>
        <div class="popupContent">
          <div class="question">
            <i
              id="iconquestion"
              class="fa fa-question-circle fa-4x"
              aria-hidden="true"
            ></i>
          </div>
          <div class="message">
            Bạn có chắc muốn xóa tất cả sản phẩm đã chọn        
            khỏi danh sách cửa hàng hay không ?
          </div>
        </div>
        <hr />
        <div class="dialogFooter" style="justify-content: flex-end">
          <div class="dialogFooterButtonBox">
            <button
            title="Enter"
              class="dialogFooterButton"
              id="buttonDelete"
              @click="deleteOnClick"
              v-shortkey="['enter']" @shortkey="deleteOnClick()"
            >
              <div class="iconDelete"></div>
              Xóa
            </button>
            <button
              class="dialogFooterButton"
              id="buttonCancel"
              @click="closePopup"
              v-shortkey="['esc']" @shortkey="closePopup()"
            >
              <div class="iconCancel"></div>
              Hủy bỏ
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
export default {
  name: "PopupDelete",

  props: {
    checked: {
      type: Array,
      default() {
        return [];
      },
    },
    isHidePopup: {
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
  methods: {
    /**
     * đóng popup
     */
    closePopup() {
      this.$emit("outIsHidePopup", !this.isHidePopup);
    },

    async deleteOnClick() {
      var i = 0;
      var count = 0;
      for (i = 0; i < this.checked.length; i++) {
        const response = await axios
          .delete("http://localhost:55810/api/Products/" + this.checked[i])
          .catch((e) => console.log(e));
        if (response) {
          count = count + 1;
        }
      }
      this.closePopup();
      if (count == i) {
        this.$notify({
          type: "success",
          title: "Important message",
          text: "Xóa thành công " + count + " cửa hàng !",
        });
        // load lai trang sau 2s
        setTimeout(() => location.reload(), 2000);
      }
    },
  },
  data() {
    return {};
  },
};
</script>

<style scoped>
#bold {
  font-weight: bold;
}
.question {
  height: 60px;
  width: 60px;
  align-items: center;
}
#iconquestion {
  color: #026b97;
  padding: 14px;
}
.isHidePopup {
  display: none;
}
.modalMask {
  position: fixed;
  z-index: 3;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}
h2 {
  padding: 15px;
}
</style>