<template>
  <v-list class="pt-0 main-menu-items">
    <template v-if="userInfo.isAuthenticated">
      <v-list-item to="/profile" class="profile-item">
        <v-list-item-avatar>
          <img :src="userInfo.avatar | profileImage" />
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title>{{ userInfo.fullName }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
      <template v-if="userIsAdmin">
        <v-list-item to="/users" exact>
          <v-list-item-action>
            <v-icon>$vuetify.icons.user</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="'کاربران'" />
          </v-list-item-content>
        </v-list-item>
      </template>
      <template v-if="userIsCharity">
        <v-list-item to="/payments" exact>
          <v-list-item-action>
            <v-icon>$vuetify.icons.dollar</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="'پرداختی ها'" />
          </v-list-item-content>
        </v-list-item>
      </template>
      <template v-if="userIsUser"> </template>
    </template>
    <v-divider />
    <template v-for="(item, index) in appMenu">
      <v-list-item :key="index" :to="item.to">
        <v-list-item-action>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title v-text="item.text" />
        </v-list-item-content>
      </v-list-item>
    </template>
    <template v-if="!userInfo.isAuthenticated">
      <v-list-item to="/login">
        <v-list-item-action>
          <v-icon>$vuetify.icons.login</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title v-text="'ورود'" />
        </v-list-item-content>
      </v-list-item>
      <v-list-item to="/register">
        <v-list-item-action>
          <v-icon>$vuetify.icons.register</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title v-text="'ثبت نام'" />
        </v-list-item-content>
      </v-list-item>
    </template>
    <v-list-item v-else to="/logout">
      <v-list-item-action>
        <v-icon>$vuetify.icons.logout</v-icon>
      </v-list-item-action>
      <v-list-item-content>
        <v-list-item-title v-text="'خروج'" />
      </v-list-item-content>
    </v-list-item>
  </v-list>
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
    userDashboard(): any {
      return this.$store.state.userDashboard;
    },
    userIsAdmin(): UserInfo {
      return this.$store.getters.userIsAdmin;
    },
    userIsCharity(): UserInfo {
      return this.$store.getters.userIsCharity;
    },
    userIsUser(): UserInfo {
      return this.$store.getters.userIsUser;
    }
  }
});
</script>

<style scoped>
.main-menu-items {
  background: none;
}
.main-menu-items a {
  color: #41bfc3;
}
.profile-item {
  background-color: #364554;
}
</style>
