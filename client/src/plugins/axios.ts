import Vue from "vue";
import axios from "axios";
import { AppSetting } from "@/appSetting";
import store from "@/store";
if (AppSetting.BackendUrl) {
  axios.defaults.baseURL = AppSetting.BackendUrl;
}
axios.defaults.withCredentials = true;
axios.defaults.xsrfHeaderName = "X-XSRF-TOKEN";
axios.defaults.xsrfCookieName = "XSRF-TOKEN";

// Add a request interceptor
axios.interceptors.request.use(config => {
  const token = store.state.userInfo.token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

Vue.prototype.axios = axios;

declare module "vue/types/vue" {
  interface Vue {
    axios: typeof axios;
  }
}

export default axios;
