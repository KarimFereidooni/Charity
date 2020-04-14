<template>
  <fragment>
    <template v-for="(item, index) in appMenu">
      <v-btn :key="index" text :to="item.to">
        {{ item.showType === "text" ? item.text : "" }}
        <v-icon v-if="item.showType === 'icon'">{{ item.icon }}</v-icon>
      </v-btn>
    </template>
    <template v-if="!userInfo.isAuthenticated">
      <v-btn text to="/login">ورود</v-btn>
      <v-btn text to="/register">ثبت نام</v-btn>
    </template>
    <template v-if="userInfo.isAuthenticated">
      <v-btn text to="/profile">{{ `پروفایل (${userInfo.fullName})` }}</v-btn>
      <v-btn text to="/logout">خروج</v-btn>
    </template>
  </fragment>
</template>

<script lang="ts">
import Vue from "vue";
import appData from "@/appData.ts";
import { AppState, UserInfo } from "@/store";

export default Vue.extend({
  data() {
    return {
      appMenu: appData.appMenu
    };
  },
  computed: {
    userInfo(): UserInfo {
      return this.$store.state.userInfo;
    },
    isAdmin(): boolean {
      return !!this.userInfo.roles.find(currentValue => {
        return currentValue.name === "Admin";
      });
    },
    isCharity(): boolean {
      return !!this.userInfo.roles.find(currentValue => {
        return currentValue.name === "Charity";
      });
    },
    isUser(): boolean {
      return !!this.userInfo.roles.find(currentValue => {
        return currentValue.name === "User";
      });
    }
  }
});
</script>
