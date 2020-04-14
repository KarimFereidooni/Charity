import {
  SET_USER_INFO,
  OPEN_QUICK_LOGIN_DIALOG,
  CLOSE_QUICK_LOGIN_DIALOG,
  TOGGLE_DRAWER_OPEN,
  HANDLE_DRAWER_OPEN,
  SET_USER_DASHBOARD
} from "./types";
import UserService from "@/services/UserService";

export const actions = {
  setUserInfo({ commit }, payload) {
    commit(SET_USER_INFO, payload);
  },
  openQuickLoginDialog({ commit }) {
    commit(OPEN_QUICK_LOGIN_DIALOG);
  },
  closeQuickLoginDialog({ commit }) {
    commit(CLOSE_QUICK_LOGIN_DIALOG);
  },
  toggleDrawerOpen({ commit }) {
    commit(TOGGLE_DRAWER_OPEN);
  },
  handleDrawerOpen({ commit }, value: boolean) {
    commit(HANDLE_DRAWER_OPEN, value);
  },
  setUserDashboard({ commit }, value) {
    commit(SET_USER_DASHBOARD, value);
  },
  async loadUserDashboard({ commit }) {
    const result = await UserService.getUserDashboard();
    commit(SET_USER_DASHBOARD, result);
  }
};
