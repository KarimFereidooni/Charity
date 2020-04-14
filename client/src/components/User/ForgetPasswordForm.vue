<template>
  <final-form :initial-values="initialValues" :submit="submit" :validate="validate" @change="updateState">
    <form :id="formId" slot-scope="formprops" autocomplete="off" novalidate @submit.prevent="formprops.handleSubmit">
      <v-layout wrap>
        <v-flex xs12>
          <final-field name="email">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.email"
              label="ایمیل"
              type="text"
              class="field"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            />
            <template v-slot:label>
              ایمیل
              <span class="required-mark">*</span>
            </template>
          </final-field>
        </v-flex>
        <template v-if="level == 2">
          <v-flex xs12>
            <final-field name="code">
              <v-text-field
                slot-scope="props"
                :value="props.value"
                :name="props.name"
                prepend-icon="$vuetify.icons.key"
                hint="کدی که به ایمیل شما ارسال شده است را اینجا وارد کنید"
                persistent-hint
                label="کد ارسال شده به ایمیل"
                type="text"
                class="field"
                :error-messages="
                  props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined
                "
                v-on="props.events"
              >
                <template v-slot:label>
                  کد ارسال شده به ایمیل
                  <span class="required-mark">*</span>
                </template>
              </v-text-field>
            </final-field>
          </v-flex>
          <v-flex xs12>
            <final-field name="newPassword">
              <v-text-field
                slot-scope="props"
                :name="props.name"
                :value="props.value"
                prepend-icon="$vuetify.icons.key"
                label="رمز عبور جدید"
                type="password"
                class="field"
                hint="یک رمز عبور جدید برای خود انتخاب کنید"
                persistent-hint
                :error-messages="
                  props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined
                "
                v-on="props.events"
              >
                <template v-slot:label>
                  رمز عبور جدید
                  <span class="required-mark">*</span>
                </template>
              </v-text-field>
            </final-field>
          </v-flex>
          <v-flex xs12>
            <final-field name="newPasswordConfirm">
              <v-text-field
                slot-scope="props"
                :name="props.name"
                :value="props.value"
                prepend-icon="$vuetify.icons.key"
                label="ورود دوباره رمز عبور جدید"
                hint="یکبار دیگر رمز جدید را وارد کنید تا مطمعن شوید آنرا درست وارد کرده اید"
                persistent-hint
                class="field"
                type="password"
                :error-messages="
                  props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined
                "
                v-on="props.events"
              >
                <template v-slot:label>
                  ورود دوباره رمز عبور جدید
                  <span class="required-mark">*</span>
                </template>
              </v-text-field>
            </final-field>
          </v-flex>
        </template>
      </v-layout>
    </form>
  </final-form>
</template>

<script lang="ts">
import Vue from "vue";
import UserService from "@/services/UserService";
import { FormState } from "final-form";
import { UserInfo } from "@/store";

export default Vue.extend({
  props: {
    formId: {
      required: true,
      type: String
    }
  },
  data() {
    return {
      formState: null as FormState<any> | null,
      level: 1,
      initialValues: {
        email: "",
        code: "",
        newPassword: "",
        newPasswordConfirm: ""
      }
    };
  },
  methods: {
    async submit(values, formApi, complete) {
      if (this.formState!.submitting) {
        return;
      }
      this.$emit("submitting", true);
      try {
        if (this.level === 1) {
          await UserService.generateResetPasswordCode(values);
          await this.$emit("codeGenerated");
          this.level = 2;
        } else if (this.level === 2) {
          await UserService.resetPassword(values);
          this.$emit("success");
          complete();
        }
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.$emit("submitting", false);
      }
    },
    updateState({ state }) {
      this.formState = state;
    },
    validate(values) {
      const errors: any = {};
      if (!values.email) {
        errors.email = "ایمیل خود را وارد کنید";
      } else {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!re.test(String(values.email).toLowerCase())) {
          errors.email = "ایمیل وارد شده نامعتبر است";
        }
      }
      if (this.level === 2) {
        if (!values.code) {
          errors.code = "کد ارسال شده به ایمیل خود را وارد کنید";
        }

        if (!values.newPassword) {
          errors.newPassword = "لطفا یک رمز عبور برای خود انتخاب کنید";
        } else if (!values.newPasswordConfirm) {
          errors.newPasswordConfirm = "یکبار دیگر رمز را وارد کنید تا مطمعن شوید آنرا درست وارد کرده اید";
        } else if (values.newPassword !== values.newPasswordConfirm) {
          errors.newPasswordConfirm = "رمز عبور و تکرار آن برابر نیستند!";
        }
      }
      return errors;
    }
  }
});
</script>

<style scoped>
@media screen and (min-width: 800px) {
  .field {
    max-width: 600px;
  }
}
</style>
