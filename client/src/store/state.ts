import { AppState } from ".";
import noProfileImage from "@/assets/images/no-profile.png";

export const state: AppState = {
  quickLoginDialog: false,
  drawerIsOpen: false,
  userDashboard: {},
  userInfo: {
    id: -1,
    isAuthenticated: false,
    userName: null,
    fullName: "کاربر مهمان",
    avatar: noProfileImage,
    title: null,
    name: "کاربر",
    surname: "مهمان",
    loginDateTime: null,
    lastLoginDateTime: null,
    chatId: null,
    roles: [],
    email: null,
    phoneNumber: null,
    token: ""
  }
};
