<template>
  <v-tooltip bottom>
    <template v-slot:activator="{ on }">
      <div class="ma-2">
        <a @click="loadCaptcha(true)">
          <img :src="src" v-on="on" />
        </a>
      </div>
    </template>
    <span>تصویر نامشخص است؟ کلیک کنید تا مجددا بارگذاری شود</span>
  </v-tooltip>
</template>

<script lang="ts">
import Vue from "vue";
import UserService from "../services/UserService";

export default Vue.extend({
  data() {
    return {
      src: null as string | null,
      loading: false
    };
  },
  created() {
    this.loadCaptcha(false);
  },
  methods: {
    async loadCaptcha(loading) {
      this.loading = loading;
      const img = await UserService.getCaptchaImage();
      this.loading = false;
      this.src = img;
    }
  }
});
</script>

<style scoped>
img {
  border: 1px solid #ddd;
  border-radius: 6;
  width: 232px;
  height: 62px;
}
</style>
