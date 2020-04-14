<template>
  <div>
    <h4 class="headline">ثبت نام در سامانه</h4>
    <p>برای ثبت نام در سامانه مشخصات خود را وارد کنید (وارد کردن موارد ستاره دار اجباری است)</p>
    <RegisterForm :resetAfterSubmit="!userIsAdmin" form-id="register-form" @submitting="onSubmitting($event)" @success="onRegisterSuccess" />
    <div>
      <v-btn :disabled="submitting" :loading="submitting" color="primary" type="submit" form="register-form">من را ثبت نام کن</v-btn>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import RegisterForm from "@/components/User/RegisterForm.vue";

export default Vue.extend({
  components: {
    RegisterForm
  },
  data() {
    return {
      submitting: false,
      userIsAdmin: this.$store.getters.userIsAdmin
    };
  },
  methods: {
    onSubmitting(value) {
      this.submitting = value;
    },
    onRegisterSuccess() {
      if (this.userIsAdmin) {
        this.$swal({
          title: "ثبت نام موفق",
          text: "ثبت نام با موفقیت انجام شد",
          icon: "success",
          buttons: { ok: { text: "تایید", value: true } }
        });
      } else {
        const returnUrl = this.$route.query.returnUrl;
        this.$router.push((returnUrl || "/profile") as string);
      }
    }
  }
});
</script>
