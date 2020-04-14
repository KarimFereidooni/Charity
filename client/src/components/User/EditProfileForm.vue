<template>
  <final-form
    :initial-values="userInfo"
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
          <final-field name="userName">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="نام کاربری *"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="name">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="نام"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="surname">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="نام خانوادگی"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="email">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="ایمیل"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="phoneNumber">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.cellphone"
              mask="###########"
              label="شماره همراه *"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="nationCode">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="کد ملی"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="idNumber">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="شماره شناسنامه"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="bankAccountNumber">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="شماره حساب بانکی"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="bankName">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="نام بانک"
              hint="حساب شما مربوط به کدام بانک است"
              persistent-hint
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="bankCard">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="شماره کارت"
              hint="شماره کارت بانکی"
              persistent-hint
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="bankIBAN">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="شماره شبا"
              hint="شماره شبای حساب بانکی"
              persistent-hint
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12>
          <final-field name="address">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="آدرس"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="postalCode">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="کد پستی"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="homePhoneNumber">
            <v-text-field
              slot-scope="props"
              :value="props.value"
              :name="props.name"
              prepend-icon="$vuetify.icons.user"
              label="شماره تلفن ثابت"
              type="text"
              :error-messages="
                props.meta.error &&
                (props.meta.dirty || props.meta.submitFailed) &&
                props.meta.touched
                  ? props.meta.error
                  : undefined
              "
              v-on="props.events"
            />
          </final-field>
        </v-flex>
      </v-layout>
    </form>
  </final-form>
</template>

<script lang="ts">
import UserService from "@/services/UserService";
import Vue from "vue";
import { AppState, UserInfo } from "@/store";
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
      formState: null as FormState<any> | null
    };
  },
  computed: {
    userInfo(): UserInfo {
      return this.$store.state.userInfo;
    }
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
        const userInfo = await UserService.updateProfile(values);
        await this.setUserInfo(userInfo);
        complete();
        formApi.reset(userInfo);
        this.$emit("success", userInfo);
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
      if (!values.userName) {
        errors.userName = "نام کاربری انتخاب نشده است";
      }

      if (!values.phoneNumber) {
        errors.phoneNumber = "شماره موبایل خود را وارد کنید";
      } else if (values.phoneNumber.indexOf("09") !== 0) {
        errors.phoneNumber = "شماره موبایل باید با 09 شروع شود";
      } else if (values.phoneNumber.length !== 11) {
        errors.phoneNumber = "شماره موبایل باید 11 رقم باشد";
      }
      return errors;
    }
  }
});
</script>
