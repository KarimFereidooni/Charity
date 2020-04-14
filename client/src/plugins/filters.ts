import Vue from "vue";
import noProfileImage from "@/assets/images/no-profile.png";
import moment from "moment-jalaali";

Vue.filter("profileImage", value => {
  if (!value) {
    return noProfileImage;
  }
  return value;
});

Vue.filter("longDateTime", value => {
  if (!value) {
    return "";
  }
  return moment(value).format("LLLL");
});

Vue.filter("fromNow", value => {
  if (!value) {
    return "";
  }
  return moment(value).fromNow();
});

Vue.filter("toman", (value: string) => {
  if (!value) {
    return value;
  }
  return value.toCurrency("تومان");
});

Vue.filter("yesNo", value => {
  return !value ? "خیر" : "بله";
});

Vue.filter("haveHas", value => {
  return !value ? "ندارد" : "دارد";
});
