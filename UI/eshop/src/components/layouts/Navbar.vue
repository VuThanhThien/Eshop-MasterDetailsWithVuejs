<template>
  <div class="navbar">
    <router-link class="logoSite" to="/Home">
      <div class="logoBox">
        <div class="eShopLogo"></div>
      </div>
    </router-link>
    <div class="navbarContent">
      <router-link title="Hàng hóa" class="navbarItem" to="/product">
        <div class="navbarItemProduct"></div>
        <div class="navbarItemText">Hàng hóa</div>
      </router-link>
      <slot/>
    </div>
  </div>
</template>
<script>
export default {
  name: "MenuBarLinks",
  data() {
    return {
      product: '/product',
    };
  },
};
</script>
