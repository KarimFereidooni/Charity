<template>
  <v-container grid-list-sm fluid>
    <v-layout wrap>
      <v-flex v-if="loginError || roleError" xs12>
        <v-alert :value="loginError" type="warning">ابتدا وارد شوید یا ثبت نام کنید</v-alert>
        <v-alert
          :value="roleError"
          type="error"
        >با نقش مناسب وارد نشده اید. برای دسترسی به صفحه مورد نظر باید با نقش مناسب وارد شوید</v-alert>
      </v-flex>
      <v-flex xs12 md6>
        <v-card>
          <v-card-title>
            <h4 class="headline">ورود به سامانه</h4>
            <p>برای ورود به سامانه نام کاربری یا ایمیل یا شماره موبایل خود را بهمراه رمز عبور وارد کنید</p>
            <LoginForm
              form-id="login-form"
              @submitting="onLoginSubmitting($event)"
              @success="onLoginSuccess"
            />
          </v-card-title>
          <v-card-actions>
            <v-btn
              :disabled="loggingIn"
              :loading="loggingIn"
              color="primary"
              type="submit"
              form="login-form"
            >ورود</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
      <v-flex xs12 md6>
        <v-card>
          <v-card-title>
            <h4 class="headline">ثبت نام در سامانه</h4>
            <p>برای ثبت نام در سامانه مشخصات خود را وارد کنید (وارد کردن موارد ستاره دار اجباری است)</p>
            <RegisterForm
              form-id="register-form"
              @submitting="onRegisterSubmitting($event)"
              @success="onRegisterSuccess"
            />
          </v-card-title>
          <v-card-actions>
            <v-btn
              :disabled="registering"
              :loading="registering"
              color="primary"
              type="submit"
              form="register-form"
            >من را ثبت نام کن</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import LoginForm from "@/components/User/LoginForm.vue";
import RegisterForm from "@/components/User/RegisterForm.vue";

export default Vue.extend({
  components: {
    LoginForm,
    RegisterForm
  },
  data() {
    return {
      loggingIn: false,
      registering: false
    };
  },
  computed: {
    loginError(): boolean {
      return this.$route.query.r === "1";
    },
    roleError(): boolean {
      return this.$route.query.r === "2";
    }
  },
  methods: {
    onLoginSubmitting(value) {
      this.loggingIn = value;
    },
    onRegisterSubmitting(value) {
      this.registering = value;
    },
    onLoginSuccess() {
      const returnUrl = this.$route.query.returnUrl as string;
      this.$router.push(returnUrl || "/");
    },
    onRegisterSuccess() {
      const returnUrl = this.$route.query.returnUrl as string;
      this.$router.push(returnUrl || "/profile");
    }
  }
});
</script>
