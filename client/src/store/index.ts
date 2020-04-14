import Vue from "vue";
import Vuex from "vuex";
import { state } from "./state";
import { mutations } from "./mutations";
import { actions } from "./actions";
import { getters } from "./getters";

Vue.use(Vuex);

export interface AppState {
  quickLoginDialog: boolean;
  userInfo: UserInfo;
  drawerIsOpen: boolean;
  userDashboard: any;
}

export interface UserInfo {
  id: number;
  isAuthenticated: boolean;
  userName: string | null;
  fullName: string;
  avatar: any;
  title: string | null;
  name: string;
  surname: string;
  loginDateTime: Date | null;
  lastLoginDateTime: Date | null;
  chatId: string | null;
  roles: Role[];
  email: string | null;
  phoneNumber: string | null;
  token: string;
}

export interface Role {
  name: string;
}

export default new Vuex.Store({
  state,
  mutations,
  actions,
  getters
});
