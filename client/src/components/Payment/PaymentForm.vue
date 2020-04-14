<template>
  <div>
    <v-container grid-list-sm>
      <v-card class="mb-2">
        <v-card-title>
          <form id="charity-search-form" style="width:100%" autocomplete="off" novalidate @submit.prevent="submitSearch">
            <v-layout wrap>
              <v-flex xs12>
                <v-text-field
                  v-model="searchText"
                  prepend-icon="$vuetify.icons.search"
                  label="نام خیریه را جستجو کنید"
                  type="text"
                  name="name"
                  hint="برای جستجو کلید Enter را فشار دهید"
                />
              </v-flex>
              <v-flex xs12>
                <v-chip v-if="$route.query.location" close class="ml-2" color="purple" text-color="white" @click:close="removeFilter('location')"
                  >شهر: {{ $route.query.location | location }}</v-chip
                >
                <v-chip v-if="$route.query.tag" close class="ml-2" color="purple" text-color="white" @click:close="removeFilter('tag')"
                  >تگ: {{ $route.query.tag | tag }}</v-chip
                >
              </v-flex>
            </v-layout>
          </form>
        </v-card-title>
        <v-progress-linear class="progress" v-if="loading" height="5px" color="primary" indeterminate />
      </v-card>
      <v-data-iterator
        v-show="loadComplete"
        :items="items"
        :footer-props="{ 'items-per-page-options': rowsPerPageItems }"
        :options.sync="options"
        :server-items-length="totalItems"
        row
        wrap
        @update:options="loadData"
      >
        <template v-slot:no-data>{{ loading ? "در حال بارگذاری..." : "اطلاعاتی برای نمایش وجود ندارد" }}</template>
        <template v-slot:no-result>موردی پیدا نشد</template>
        <template v-slot:default="props">
          <v-layout wrap>
            <v-flex xs12 md6 v-for="item in props.items" :key="item.id">
              <CharitySearchItem v-model="item.selected" :charity="item" :addFilter="addFilter" />
            </v-flex>
          </v-layout>
        </template>
      </v-data-iterator>
      <h4 class="headline">پرداخت به خیریه</h4>
      <p>
        برای پرداخت مبلغ مورد نظر را وارد کنید
      </p>
      <final-form :submit="submitForm" :initial-values="paymentInfo" @change="updateFormState">
        <form id="add-payment-form" slot-scope="formprops" autocomplete="off" novalidate @submit.prevent="formprops.handleSubmit">
          <v-container grid-list-md fluid class="pa-0">
            <v-layout wrap>
              <v-flex xs12 sm6>
                <final-field name="amount" :validate="v => (v ? null : 'مبلغ را وارد کنید')">
                  <v-text-field
                    slot-scope="props"
                    :value="props.value"
                    :name="props.name"
                    prepend-icon="$vuetify.icons.dollar"
                    label="مبلغ مورد نظر شما"
                    :error-messages="
                      props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined
                    "
                    min="0"
                    suffix="تومان"
                    type="number"
                    persistent-hint
                    :hint="amountHint"
                    v-on="props.events"
                  >
                    <template v-slot:label>
                      مبلغ مورد نظر شما
                      <span class="required-mark">*</span>
                    </template>
                  </v-text-field>
                </final-field>
              </v-flex>
              <v-flex xs12>
                <final-field name="description">
                  <v-textarea
                    slot-scope="props"
                    :value="props.value"
                    :name="props.name"
                    prepend-icon="$vuetify.icons.newspaper"
                    label="توضیحات"
                    hint="هر توضیحی که فکر می کنید لازم است اینجا بنویسید"
                    rows="1"
                    persistent-hint
                    auto-grow
                    :error-messages="
                      props.meta.error && (props.meta.dirty || props.meta.submitFailed) && props.meta.touched ? props.meta.error : undefined
                    "
                    v-on="props.events"
                  />
                </final-field>
              </v-flex>
              <v-flex xs12>
                <v-btn :disabled="formState.submitting" :loading="formState.submitting" color="primary" type="submit" form="add-payment-form"
                  >پرداخت</v-btn
                >
              </v-flex>
            </v-layout>
          </v-container>
        </form>
      </final-form>
    </v-container>
    <v-dialog v-model="quickRegisterDialog" scrollable>
      <v-card>
        <v-toolbar dark color="primary">
          <v-btn icon dark @click="quickRegisterDialog = false">
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <v-toolbar-title>{{ showLogin ? "ورود به سیستم" : "ثبت نام در سیستم" }}</v-toolbar-title>
        </v-toolbar>
        <v-card-text class="pt-3">
          <template v-if="showLogin">
            <p>
              برای ورود به سامانه نام کاربری یا ایمیل یا شماره موبایل خود را بهمراه رمز عبور وارد کنید
            </p>
            <LoginForm form-id="quick-login-form" @submitting="onSubmitting($event)" @success="onLoginSuccess" />
          </template>
          <template v-else>
            <p>لطفا مشخصات خود را وارد کنید تا در سامانه ثبت نام شوید</p>
            <RegisterForm form-id="quick-register-form" @success="onRegisterSuccess" @submitting="onSubmitting($event)" />
          </template>
        </v-card-text>
        <v-card-actions>
          <template v-if="showLogin">
            <v-btn :disabled="submitting" :loading="submitting" color="primary" form="quick-login-form" type="submit">ورود</v-btn>
            <v-btn :disabled="submitting" :loading="submitting" @click="showLogin = false" color="green darken-1" text>میخواهم ثبت نام کنم</v-btn>
          </template>
          <template v-else>
            <v-btn :disabled="submitting" :loading="submitting" color="primary" form="quick-register-form" type="submit">ثبت نام</v-btn>
            <v-btn :disabled="submitting" :loading="submitting" @click="showLogin = true" color="green darken-1" text
              >اگر قبلا ثبت نام کرده اید اینجا کلیک کنید</v-btn
            >
          </template>
          <v-btn :disabled="submitting" color="default" text @click="closeQuickRegisterDialog">انصراف</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import vue from "vue";
import PaymentService from "@/services/PaymentService";
import { wordifyfa } from "@/plugins/wordifyfa.js";
import { FormState, FormApi } from "final-form";
import { UserInfo } from "@/store";
import RegisterForm from "@/components/User/RegisterForm.vue";
import LoginForm from "@/components/User/LoginForm.vue";
import queryString from "query-string";
import CharitySearchItem from "@/components/Charity/CharitySearchItem.vue";
import CharityService from "@/services/CharityService";

export default vue.extend({
  components: {
    RegisterForm,
    LoginForm,
    CharitySearchItem
  },
  props: {
    id: {
      required: false,
      type: String,
      default: null
    }
  },
  data() {
    return {
      formState: null as FormState<any> | null,
      formApi: null as FormApi | null,
      searchText: (this.$route.query.name || "") as string,
      rowsPerPageItems: [10, 20, 50, 100],
      totalItems: 0,
      options: {
        descending: true,
        page: 1,
        rowsPerPage: 10,
        sortBy: "Id"
      },
      items: [] as any[],
      loading: false,
      loadComplete: false,
      submitting: false,
      showLogin: false,
      quickRegisterDialog: false,
      paymentInfo: {
        id: 0,
        description: "",
        amount: null
      }
    };
  },
  watch: {
    "$route.query"() {
      this.loadData();
    }
  },
  computed: {
    amountHint(): string {
      if (this.formState != null) {
        if (this.formState.values.amount) {
          return this.formState.values.amount.withComma() + ` (${wordifyfa(this.formState.values.amount)} تومان)`;
        }
      }
      return "";
    },
    userInfo(): UserInfo {
      return this.$store.state.userInfo;
    }
  },
  created() {
    if (this.id) {
      this.loadPayment();
    }
  },
  methods: {
    async loadPayment() {
      try {
        this.loading = true;
        const paymentInfo = await PaymentService.getPayment(this.id);
        this.loadPaymentData(paymentInfo);
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.loading = false;
      }
    },
    async submitForm(values, formApi, complete) {
      if (this.formState!.submitting) {
        return;
      }
      if (!this.items.some(x => x.selected)) {
        await this.$swal({
          text: "هیچ خیریه ای انتخاب نشده است",
          icon: "info",
          buttons: { ok: { text: "تایید", value: true } }
        });
        return;
      }
      if (!this.userInfo.isAuthenticated) {
        this.quickRegisterDialog = true;
        return;
      }
      try {
        const charities = [] as number[];
        for (const iterator of this.items) {
          if (iterator.selected) {
            charities.push(iterator.id);
          }
        }
        await PaymentService.addPayment({ charities, ...values });
        complete();
        await this.$swal({
          text: "پرداخت با موفقیت انجام شد",
          icon: "success",
          buttons: { ok: { text: "تایید", value: true } }
        });
        this.formApi!.reset();
      } catch (error) {
        this.showErrorMessage(error);
      }
    },
    loadPaymentData(paymentInfo) {
      (this.formApi as FormApi).reset(paymentInfo);
      this.paymentInfo = paymentInfo;
    },
    updateFormState({ state, formApi }) {
      this.formState = state;
      this.formApi = formApi;
    },
    onSubmitting(value) {
      this.submitting = value;
    },
    async onRegisterSuccess() {
      await this.$swal({
        text: "ثبت نام موفقیت آمیز بود",
        icon: "success",
        buttons: { ok: { text: "تایید", value: true } }
      });
      this.closeQuickRegisterDialog();
      await this.formApi!.submit();
    },
    async onLoginSuccess() {
      this.closeQuickRegisterDialog();
      await this.formApi!.submit();
    },
    closeQuickRegisterDialog() {
      this.quickRegisterDialog = false;
    },
    async loadData() {
      this.loading = true;
      try {
        const result = await CharityService.getCharities({
          ...this.options,
          name: this.$route.query.name || "",
          location: this.$route.query.location || null,
          tag: this.$route.query.tag || null
        });
        this.totalItems = result.totalCount;
        this.items = result.data;
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.loading = false;
        this.loadComplete = true;
      }
    },
    async submitSearch() {
      const query = queryString.parse(window.location.search);
      query.name = this.searchText || undefined;
      const search = queryString.stringify(query);
      if (search) {
        this.$router.replace(`/?${search}`);
      } else {
        this.$router.replace("/");
      }
    },
    removeFilter(f) {
      const query = queryString.parse(window.location.search);
      query[f] = undefined;
      const search = queryString.stringify(query);
      if (search) {
        this.$router.replace(`/?${search}`);
      } else {
        this.$router.replace("/");
      }
    },
    addFilter(f, v) {
      const query = queryString.parse(window.location.search);
      query[f] = v;
      const search = queryString.stringify(query);
      if (search) {
        this.$router.replace(`/?${search}`);
      } else {
        this.$router.replace("/");
      }
    }
  }
});
</script>

<style scoped>
.progress {
  position: absolute;
  bottom: 3px;
  left: 0;
  right: 0;
}
</style>
