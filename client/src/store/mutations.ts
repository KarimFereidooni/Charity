import { AppState, UserInfo } from ".";
import {
  SET_USER_INFO,
  OPEN_QUICK_LOGIN_DIALOG,
  CLOSE_QUICK_LOGIN_DIALOG,
  TOGGLE_DRAWER_OPEN,
  HANDLE_DRAWER_OPEN,
  SET_USER_DASHBOARD
} from "./types";

export const mutations = {
  [SET_USER_INFO](state: AppState, payload: { userInfo: UserInfo }) {
    state.userInfo = payload.userInfo;
  },
  [OPEN_QUICK_LOGIN_DIALOG](state: AppState) {
    state.quickLoginDialog = true;
  },
  [CLOSE_QUICK_LOGIN_DIALOG](state: AppState) {
    state.quickLoginDialog = false;
  },
  [TOGGLE_DRAWER_OPEN](state: AppState) {
    state.drawerIsOpen = !state.drawerIsOpen;
  },
  [HANDLE_DRAWER_OPEN](state: AppState, value: boolean) {
    state.drawerIsOpen = value;
  },
  [SET_USER_DASHBOARD](state: AppState, value) {
    state.userDashboard = value;
  }
};
