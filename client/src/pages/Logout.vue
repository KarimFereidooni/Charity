<template>
  <v-progress-linear v-if="loggingOut" indeterminate />
</template>

<script lang="ts">
import Vue from "vue";
import UserService from "@/services/UserService";
import store, { UserInfo } from "@/store";
import noProfileImage from "@/assets/images/no-profile.png";

export default Vue.extend({
  data() {
    return {
      loggingOut: false
    };
  },
  created() {
    this.logout();
  },
  methods: {
    setUserInfo(userInfo: UserInfo) {
      return this.$store.dispatch("setUserInfo", { userInfo, vm: this });
    },
    async logout() {
      try {
        this.loggingOut = true;
        await UserService.logout();
        const userInfo: UserInfo = {
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
        };
        this.setUserInfo(userInfo);
        this.loggingOut = false;
        await this.$router.push("/login");
      } catch (error) {
        this.loggingOut = false;
        this.showErrorMessage(error);
      }
    }
  }
});
</script>
