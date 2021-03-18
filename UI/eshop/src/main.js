import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Notifications from 'vue-notification'
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import InputTag from 'vue-input-tag'
Vue.component('input-tag', InputTag)
Vue.use(Notifications)
Vue.config.productionTip = false
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
