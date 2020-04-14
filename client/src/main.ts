import Vue from "vue";
Vue.config.productionTip = false;

import "@/plugins/vue-fragment";
import "@/plugins/sweetAlert";
import "@/plugins/vue-moment-jalaali";
import "@/plugins/prototype";
import "@/plugins/filters";
import "@/plugins/finalForm";
import "@/plugins/axios";
import { createRouter } from "./router/index";
import store from "./store/index";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import "@/plugins/mdi";
import "@/plugins/vue-cropperjs";
import "@/plugins/vue-loading-overlay";
import "@/plugins/vue-infinite-loading";

import "@mdi/font/css/materialdesignicons.css";
import "vuetify/dist/vuetify.css";
import "cropperjs/dist/cropper.css";
import "vue-loading-overlay/dist/vue-loading.css";
import "@/assets/style/iransans.css";
import "@/assets/style/app.css";

const router = createRouter();
const app = new Vue({
  store,
  router,
  vuetify,
  render: h => h(App)
});
router.onReady(() => {
  app.$mount("#Charity-app");
});
