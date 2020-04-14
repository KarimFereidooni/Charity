<template>
  <final-form :initial-values="initialValues" :submit="submit" :validate="validate" @change="updateState">
    <form :id="formId" slot-scope="formprops" autocomplete="off" novalidate @submit.prevent="formprops.handleSubmit">
      <v-layout wrap>
        <v-flex xs12 sm6>
          <final-field name="userName">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.user"
              label="نام کاربری"
              type="text"
              hint="یک نام به دلخواه انتخاب کنید. دیگران شما را با نام کاربری می شناسند"
              persistent-hint
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <template v-slot:label>
                نام کاربری
                <span class="required-mark">*</span>
              </template>
            </v-text-field>
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="name">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.user"
              label="نام"
              type="text"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="surname">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.user"
              label="نام خانوادگی"
              type="text"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            />
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="phoneNumber">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.cellphone"
              label="شماره موبایل"
              type="text"
              hint="مثال: 09121234567"
              persistent-hint
              mask="###########"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <template v-slot:label>
                شماره موبایل
                <span class="required-mark">*</span>
              </template>
            </v-text-field>
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="email">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.email"
              label="ایمیل"
              type="email"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <template v-slot:label>
                ایمیل
                <span class="required-mark">*</span>
              </template>
            </v-text-field>
          </final-field>
        </v-flex>
        <v-flex v-if="userIsAdmin" xs12 sm6>
          <final-field name="selectedRole">
            <v-select
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              :items="roles"
              attach
              prepend-icon="$vuetify.icons.user"
              label="در چه نقشی ثبت نام می کنید"
              hint="نقش خود را انتخاب کنید. مترجم هستید یا مشتری؟"
              persistent-hint
              item-text="text"
              item-value="name"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <template v-slot:label>
                در چه نقشی ثبت نام می کنید
                <span class="required-mark">*</span>
              </template>
            </v-select>
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="password">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.key"
              label="رمز عبور"
              type="password"
              hint="یک رمز عبور برای خود انتخاب کنید"
              persistent-hint
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <!-- <template v-slot:label>
                رمز عبور
                <span class="required-mark">*</span>
              </template> -->
            </v-text-field>
          </final-field>
        </v-flex>
        <v-flex xs12 sm6>
          <final-field name="passwordConfirm">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.key"
              label="ورود دوباره رمز عبور"
              hint="یکبار دیگر رمز را وارد کنید تا مطمعن شوید آنرا درست وارد کرده اید"
              persistent-hint
              type="password"
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <!-- <template v-slot:label>
                ورود دوباره رمز عبور
                <span class="required-mark">*</span>
              </template> -->
            </v-text-field>
          </final-field>
        </v-flex>
        <v-flex v-if="!userIsAdmin" xs12 sm6>
          <final-field name="captcha">
            <v-text-field
              slot-scope="props"
              :name="props.name"
              :value="props.value"
              prepend-icon="$vuetify.icons.squareRoot"
              label="جواب معادله"
              type="text"
              hint="شما ربات نیستید؟ بنابراین جواب معادله درون تصویر را وارد کنید"
              persistent-hint
              :error-messages="props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined"
              v-on="props.events"
            >
              <template v-slot:label>
                جواب معادله
                <span class="required-mark">*</span>
              </template>
            </v-text-field>
          </final-field>
        </v-flex>
        <v-flex v-if="!userIsAdmin" xs12 sm6>
          <CaptchaTextImage />
        </v-flex>
      </v-layout>
    </form>
  </final-form>
</template>

<script lang="ts">
import Vue from "vue";
import CaptchaTextImage from "@/components/CaptchaTextImage.vue";
import UserService from "@/services/UserService";
import { FormState } from "final-form";
import { UserInfo } from "@/store";

export default Vue.extend({
  components: {
    CaptchaTextImage
  },
  props: {
    formId: {
      required: true,
      type: String
    },
    resetAfterSubmit: {
      required: false,
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      formState: null as FormState<any> | null,
      userIsAdmin: this.$store.getters.userIsAdmin,
      initialValues: {
        name: "",
        surname: "",
        userName: "",
        phoneNumber: "",
        email: "",
        password: "",
        passwordConfirm: "",
        captcha: "",
        selectedRole: this.$store.getters.userIsAdmin ? "Charity" : "User"
      },
      roles: [
        { name: "Charity", text: "حسابدار" },
        { name: "User", text: "کاربر" },
        { name: "Admin", text: "مدیرسیستم" }
      ]
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
        const userInfo = await UserService.register(values);
        this.setUserInfo(userInfo);
        complete();
        if (this.resetAfterSubmit) {
          formApi.reset();
        }
        this.$emit("success");
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

      if (!values.email) {
        errors.email = "ایمیل خود را وارد کنید";
      } else {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!re.test(String(values.email).toLowerCase())) {
          errors.email = "ایمیل وارد شده نامعتبر است";
        }
      }

      if (!values.selectedRole) {
        errors.selectedRole = "لطفا مشخص کنید بعنوان مترجم ثبت نام می کنید یا مشتری";
      }

      // if (!values.password) {
      //   errors.password = "لطفا یک رمز عبور برای خود انتخاب کنید";
      // } else if (!values.passwordConfirm) {
      //   errors.passwordConfirm = "یکبار دیگر رمز را وارد کنید تا مطمعن شوید آنرا درست وارد کرده اید";
      // } else if (values.password !== values.passwordConfirm) {
      //   errors.passwordConfirm = "رمز عبور و تکرار آن برابر نیستند!";
      // }

      if (!this.userIsAdmin && !values.captcha) {
        errors.captcha = "لطفا جواب معادله درون تصویر را وارد کنید تا مشخص شود شما ربات نیستید";
      }
      return errors;
    }
  }
});
</script>
