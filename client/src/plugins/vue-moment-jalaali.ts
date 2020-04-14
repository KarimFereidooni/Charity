import Vue from "vue";
// import "moment";
import jMoment from "moment-jalaali";
jMoment.loadPersian({ usePersianDigits: false, dialect: "persian-modern" });
import vueMoment from "vue-moment-jalaali";

Vue.use(vueMoment);
