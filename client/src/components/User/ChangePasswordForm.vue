<template>
  <final-form
    :initial-values="initialValues"
    :submit="submit"
    :validate="validate"
    @change="updateState"
  >
    <form
      :id="formId"
      slot-scope="formprops"
      autocomplete="off"
      novalidate
      @submit.prevent="formprops.handleSubmit"
    >
      <v-layout wrap>
        <v-flex xs12 sm6>
          <final-field name="currentPassword">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.key"
              label="رمز عبور فعلی"
              type="password"
              :error-messages="(props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched) ? props.meta.error : undefined"
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="newPassword">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.key"
              label="رمز عبور جدید"
              type="password"
              :error-messages="(props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched) ? props.meta.error : undefined"
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="newPasswordConfirm">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.key"
              label="ورود دوباره رمز عبور"
              hint="یکبار دیگر رمز را وارد کنید تا مطمعن شوید آنرا درست وارد کرده اید"
              persistent-hint
              type="password"
              :error-messages="(props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched) ? props.meta.error : undefined"
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
        currentPassword: "",
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
        const userInfo = await UserService.changePassword(values);
        complete();
        formApi.reset();
        this.$emit("success", userInfo);
      } catch (error) {
        formApi.reset();
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
      if (!values.newPassword) {
        errors.newPassword = "لطفا یک رمز عبور برای خود انتخاب کنید";
      } else if (!values.newPasswordConfirm) {
        errors.newPasswordConfirm =
          "یکبار دیگر رمز را وارد کنید تا مطمعن شوید آنرا درست وارد کرده اید";
      } else if (values.newPassword !== values.newPasswordConfirm) {
        errors.newPasswordConfirm = "رمز عبور و تکرار آن برابر نیستند!";
      }
      return errors;
    }
  }
});
</script>
