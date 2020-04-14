import Vue from "vue";
import VueSwal from "vue-swal";
import { SweetAlert } from "sweetalert/typings/core";

Vue.use(VueSwal);

Vue.prototype.showErrorMessage = error => {
  const span = document.createElement("span");
  span.innerHTML = error.message || error.toString();
  Vue.prototype.$swal({
    title: "",
    content: span,
    icon: "error",
    buttons: { ok: { text: "تایید", value: true } }
  });
};

declare module "vue/types/vue" {
  interface Vue {
    $swal: SweetAlert;
    showErrorMessage: (error) => void;
  }
}
