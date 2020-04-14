<template>
  <final-form :initial-values="initialValues" :submit="submit" @change="updateState">
    <form :id="formId" slot-scope="formprops" autocomplete="off" novalidate @submit.prevent="formprops.handleSubmit">
      <v-layout wrap>
        <v-flex xs12>
          <final-field name="userName" :validate="v => (v ? null : 'نام کاربری یا ایمیل یا شماره تلفن را وارد کنید')">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="نام کاربری، ایمیل، شماره تلفن"
              type="text"
              class="login-field"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12>
          <final-field name="password" :validate="v => (v ? null : 'رمز عبور را وارد کنید')">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.key"
              label="رمز عبور"
              type="password"
              class="login-field"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            />
          </final-field>
        </v-flex>
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
      initialValues: {
        userName: "",
        password: "",
        rememberMe: true
      }
    };
  },
  methods: {
    setUserInfo(userInfo: UserInfo) {
      return this.$store.dispatch("setUserInfo", { userInfo, vm: this });
    },
    async submit(values, formApi, complete) {
      if (this.formState!.submitting) {
        return;
      }
      this.$emit("submitting", true);
      try {
        const userInfo = await UserService.login(values);
        await this.setUserInfo(userInfo);
        complete();
        formApi.reset();
        this.$emit("success", userInfo);
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.$emit("submitting", false);
      }
    },
    updateState({ state }) {
      this.formState = state;
    }
  }
});
</script>

<style scoped>
@media screen and (min-width: 800px) {
  .login-field {
    max-width: 400px;
  }
}
</style>
