<template>
  <div id="app">
    <Navbar />
    <Header />
    <Content />
    <notifications position="bottom right" clean: true style="margin-bottom: 20px"/>
  </div>
</template>
<script>
import Navbar from "@/components/layouts/Navbar";
import Header from "@/components/layouts/Header";
import Content from "@/components/layouts/Content";

export default {
  name: "App",

  components: {
    Navbar,
    Header,
    Content,
  },

  data: () => ({
    //
  }),
};
</script>
<style lang="scss">
@import "./styles/root.css";

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  text-decoration:none;
}
body {
    margin: 0;
    font-family: GoogleSans-Regular !important;
    font-size: 13px !important;
}
.vue-notification {
  padding: 10px;
  margin: 0 5px 5px;
 
  font-size: 13px !important;
 
  color: #ffffff;
  background: #44A4FC;
  border-left: 5px solid #187FE7;
 
  &.warn {
    background: #ffb648;
    border-left-color: #f48a06;
  }
 
  &.error {
    background: #E54D42;
    border-left-color: #B82E24;
  }
 
  &.success {
    background: #68CD86;
    border-left-color: #42A85F;
  }
}
</style>
