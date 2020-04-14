<template>
  <div>
    <h4 class="headline">بازیابی رمزعبور</h4>
    <p>
      برای بازیابی رمزعبور ایمیل خود را وارد کنید
    </p>
    <ForgetPasswordForm
      form-id="forget-password-form"
      @submitting="onSubmitting($event)"
      @codeGenerated="onCodeGenerated"
      @success="onResetSuccess"
    />
    <v-card-actions>
      <v-btn :disabled="submitting" :loading="submitting" color="primary" type="submit" form="forget-password-form">ارسال</v-btn>
    </v-card-actions>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import ForgetPasswordForm from "@/components/User/ForgetPasswordForm.vue";
import { UserInfo } from "../store";

export default Vue.extend({
  components: {
    ForgetPasswordForm
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
    onCodeGenerated() {
      this.$swal({
        title: "کد اهراز هویت ارسال شد",
        text:
          "کد اهراز هویت به ایمیل شما ارسال شد لطفا کد ارسال شده را به همراه  رمز عبور جدید در همین صفحه وارد کنید. لطفا اگر ایمیل را در Inbox مشاهده نکردید توجه داشته باشید ممکن است ایمیل به قسمت Spam یا Junk رفته باشد",
        icon: "success",
        buttons: { ok: { text: "تایید", value: true } }
      });
    },
    async onResetSuccess() {
      await this.$swal({
        title: "رمز عبور تغییر کرد",
        text: "رمز عبور جدید شما با موفقیت ثبت شد",
        icon: "success",
        buttons: { ok: { text: "تایید", value: true } }
      });
      await this.$router.push("/login");
    }
  }
});
</script>
