<template>
  <div>
    <v-card>
      <v-card-title>پرداختی ها</v-card-title>
      <v-card-text>
        <v-data-table
          :loading="loading"
          :headers="headers"
          :items="items"
          :options.sync="options"
          :server-items-length="totalItems"
          class="elevation-1"
          loading-text="درحال بارگذاری..."
          item-key="id"
          sort-by="paymentDateTime"
          sort-desc
          must-sort
          :multi-sort="false"
          @update:options="loadData"
        >
          <template v-slot:top>
            <v-text-field @input="loadDataWithDelay" class="mx-4" v-model="nameSearch" label="نام خیریه را جستجو کنید"></v-text-field>
          </template>
          <template v-slot:item.paymentDateTime="s">{{ s.item.paymentDateTime | longDateTime }}</template>
          <template v-slot:item.amount="s">{{ s.item.amount | toman }}</template>
          <template v-slot:footer>
            <v-alert color="info" text> جمع پرداختی: {{ totalAmount | toman }} </v-alert>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import PaymentService from "@/services/PaymentService";

export default Vue.extend({
  data() {
    return {
      loading: false,
      saving: false,
      items: [] as any[],
      totalItems: 0,
      options: {
        page: 1,
        itemsPerPage: 10,
        sortBy: ["paymentDateTime"],
        sortDesc: [true],
        groupBy: [],
        groupDesc: [],
        mustSort: true,
        multiSort: false
      },
      headers: [
        { text: "شناسه", value: "id" },
        { text: "مبلغ", value: "amount" },
        { text: "توضیحات", value: "desceiption" },
        { text: "خیریه", value: "charityName" },
        { text: "پرداخت کننده", value: "userFullName" },
        { text: "زمان پرداخت", value: "paymentDateTime" }
      ],
      nameSearch: "",
      timeOutId: 0,
      totalAmount: 0
    };
  },
  methods: {
    async loadData() {
      try {
        this.loading = true;
        const result = await PaymentService.getPayments(this.options, this.nameSearch);
        this.totalItems = result.totalCount;
        this.items = result.data;
        this.totalAmount = result.totalAmount;
      } catch (error) {
        this.showErrorMessage(error);
      } finally {
        this.loading = false;
      }
    },
    loadDataWithDelay() {
      if (this.timeOutId) {
        window.clearTimeout(this.timeOutId);
      }
      this.timeOutId = window.setTimeout(() => {
        this.options.page = 1;
        this.loadData();
      }, 1000);
    }
  }
});
</script>
