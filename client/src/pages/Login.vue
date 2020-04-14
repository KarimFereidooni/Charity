<template>
  <div>
    <h4 class="headline">ورود به سامانه</h4>
    <p>
      برای ورود به سامانه نام کاربری یا ایمیل یا شماره موبایل خود را بهمراه رمز عبور وارد کنید
    </p>
    <LoginForm form-id="login-form" @submitting="onSubmitting($event)" @success="onLoginSuccess" />
    <v-card-actions>
      <v-btn :disabled="submitting" :loading="submitting" color="primary" type="submit" form="login-form">ورود</v-btn>
      <v-btn text color="default" to="/forgetPassword">رمزعبور را فراموش کرده ام</v-btn>
    </v-card-actions>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import LoginForm from "@/components/User/LoginForm.vue";
import { UserInfo } from "../store";

export default Vue.extend({
  components: {
    LoginForm
  },
  data() {
    return {
      submitting: false
    };
  },
  methods: {
    onSubmitting(value) {
      this.submitting = value;
    },
    onLoginSuccess(userInfo: UserInfo) {
      const returnUrl = this.$route.query.returnUrl as string;
      this.$store.dispatch("loadUserDashboard");
      this.$router.push(returnUrl || "/profile");
    }
  }
});
</script>
