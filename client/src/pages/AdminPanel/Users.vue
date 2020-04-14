<template>
  <div>
    <v-card>
      <v-card-title>کاربران</v-card-title>
      <v-card-text>
        <v-data-table
          :loading="loading"
          :headers="headers"
          :items="items"
          :options.sync="options"
          :server-items-length="totalItems"
          class="elevation-1"
          loading-text="درحال بارگذاری..."
          item-key="id"
          sort-by="loginDateTime"
          sort-desc
          must-sort
          :multi-sort="false"
          @update:options="loadData"
        >
          <template v-slot:top>
            <v-text-field @input="loadDataWithDelay" class="mx-4" v-model="fullNameSearch" label="نام و نام خانوادگی را جستجو کنید"></v-text-field>
            <v-select
              class="mx-4"
              @change="loadData"
              v-model="selectedRole"
              :items="roles"
              item-text="text"
              item-value="name"
              label="نقش کاربر را فیلتر کنید"
              solo
            ></v-select>
          </template>
          <template v-slot:item.loginDateTime="s">{{ s.item.loginDateTime | longDateTime }}</template>
          <template v-slot:item.actions="{ item }">
            <v-btn color="primary" @click="login(item)">
              ورود به پنل کاربر
            </v-btn>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import UserService from "@/services/UserService";
import { UserInfo } from "@/store";

export default Vue.extend({
  data() {
    return {
      loading: false,
      saving: false,
      items: [] as any[],
      totalItems: 0,
      options: {
        page: 1,
        itemsPerPage: 10,
        sortBy: ["loginDateTime"],
        sortDesc: [true],
        groupBy: [],
        groupDesc: [],
        mustSort: true,
        multiSort: false
      },
      headers: [
        { text: "شناسه", value: "id" },
        { text: "نام و نام خانوادگی", value: "fullName" },
        { text: "نام کاربری", value: "userName" },
        { text: "ایمیل", value: "email" },
        { text: "موبایل", value: "phoneNumber" },
        { text: "زمان آخرین ورود", value: "loginDateTime" },
        { text: "ورود", value: "actions" }
      ],
      selectedRole: "All",
      roles: [
        { name: "All", text: "همه نقش ها" },
        { name: "Admin", text: "مدیران سایت" },
        { name: "Charity", text: "حسابدار ها" },
        { name: "User", text: "کاربران" }
      ],
      fullNameSearch: "",
      timeOutId: 0
    };
  },
  methods: {
    async loadData() {
      try {
        this.loading = true;
        const result = await UserService.getUsers(this.options, this.selectedRole, this.fullNameSearch);
        this.totalItems = result.totalCount;
        this.items = result.data;
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.loading = false;
      }
    },
    loadDataWithDelay() {
      if (this.timeOutId) {
        window.clearTimeout(this.timeOutId);
      }
      this.timeOutId = window.setTimeout(() => {
        this.options.page = 1;
        this.loadData();
      }, 1000);
    },
    setUserInfo(userInfo: UserInfo) {
      return this.$store.dispatch("setUserInfo", { userInfo, vm: this });
    },
    async login(user) {
      try {
        this.loading = true;
        const userInfo = await UserService.loginUser(user.userName);
        await this.setUserInfo(userInfo);
        this.$router.push("/profile");
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.loading = false;
      }
    }
  }
});
</script>
