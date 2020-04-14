import Vue from "vue";
import VueRouter from "vue-router";
import { routes } from "./routes";
import store, { UserInfo } from "../store";
import UserService from "@/services/UserService";
import { scrollTo, easing } from "../plugins/scrollTo";

Vue.use(VueRouter);

function getRedirectLocation(to) {
  let newLocation;
  if (to.meta && to.meta.authorize && !store.state.userInfo.isAuthenticated) {
    newLocation = `/login-register?returnUrl=${to.path}&r=1`;
  } else if (to.meta && to.meta.authorize && to.meta.roles) {
    const found = store.state.userInfo.roles.some(r => to.meta.roles.indexOf(r.name) >= 0);
    if (!found) {
      newLocation = `/login-register?returnUrl=${to.path}&r=2`;
    }
  }
  return newLocation;
}

function changeDocumentTitle(to) {
  if (typeof document !== "undefined") {
    document.title = to.meta && to.meta.title ? `${to.meta.title}` : `ترنسر`;
  }
}

export function createRouter() {
  const router = new VueRouter({
    scrollBehavior(to, from, savedPosition) {
      return new Promise(resolve => {
        scrollTo(0, 800, easing.easeOutQuart, () => {
          resolve({ x: 0, y: 0 });
        });
      });
    },
    mode: "history",
    routes
  });

  router.beforeEach(async (to, from, next) => {
    changeDocumentTitle(to);
    let newLocation;
    if (store.state.userInfo.id === -1) {
      const userInfo: UserInfo = await UserService.getUserInfo();
      await store.dispatch("setUserInfo", { userInfo, vm: null });
      if (userInfo.id !== -1 && userInfo.roles) {
        store.dispatch("loadUserDashboard");
      }
      newLocation = getRedirectLocation(to);
      newLocation ? next(newLocation) : next();
    } else {
      store.dispatch("loadUserDashboard");
      newLocation = getRedirectLocation(to);
      newLocation ? next(newLocation) : next();
    }
  });
  return router;
}
