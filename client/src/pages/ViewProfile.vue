<template>
  <div>
    <v-progress-linear v-if="loading" indeterminate />
    <v-card v-if="userInfo">
      <v-card-text>
        <v-img class="profile-image" :src="userInfo.avatar | profileImage" aspect-ratio="1" />
        <v-rating :value="userInfo.rate" :length="5" readonly size="32" half-increments />
        <v-list class="pa-0">
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>{{ userInfo.fullName }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>نام کاربری:</v-list-item-title>
              <v-list-item-subtitle>{{ userInfo.userName }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import UserService from "@/services/UserService";

export default Vue.extend({
  props: {
    id: {
      required: true,
      type: String
    }
  },
  data() {
    return {
      loading: false,
      userInfo: null as any
    };
  },
  created() {
    this.loadProfile();
  },
  methods: {
    async loadProfile() {
      if (this.id) {
        this.loading = true;
        try {
          this.userInfo = await UserService.getProfile(this.id);
        } catch (error) {
          this.showErrorMessage(error);
        } finally {
          this.loading = false;
        }
      }
    }
  }
});
</script>

<style scoped>
.profile-image {
  width: 120px;
  height: 120px;
  border: 3px solid gray;
  border-radius: 6px;
}
.danger {
  color: red;
}
.good {
  color: green;
}
</style>
