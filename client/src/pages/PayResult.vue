<template>
  <div>
    <div v-if="error">
      <v-alert text color="error">
        <h3 class="headline">خطا. پرداخت انجام نشد</h3>
        <div>{{ error }}</div>
        <v-divider class="my-4 info" style="opacity: 0.22"></v-divider>
        <v-row align="center" no-gutters>
          <v-col class="grow">کد رهگیری: {{ code }}</v-col>
          <div class="flex-grow-1"></div>
          <v-col class="shrink">
            <v-btn color="info" :to="redirect" outlined>بازگشت</v-btn>
          </v-col>
        </v-row>
      </v-alert>
    </div>
    <div v-else>
      <v-alert text color="success">
        <h3 class="headline">پرداخت با موفقیت انجام شد.</h3>
        <v-divider class="my-4 info" style="opacity: 0.22"></v-divider>
        <v-row align="center" no-gutters>
          <v-col class="grow">کد رهگیری: {{ code }}</v-col>
          <div class="flex-grow-1"></div>
          <v-col class="shrink">
            <v-btn color="info" :to="redirect" outlined>بازگشت</v-btn>
          </v-col>
        </v-row>
      </v-alert>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import queryString from "query-string";

export default Vue.extend({
  data() {
    return {
      error: "",
      redirect: "",
      code: ""
    };
  },
  mounted() {
    const query = queryString.parse(window.location.search);
    this.error = (query.err as string) || "";
    this.redirect = (query.redirect as string) || "";
    this.code = (query.code as string) || "";
  }
});
</script>
