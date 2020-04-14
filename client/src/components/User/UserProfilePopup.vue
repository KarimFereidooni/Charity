<template>
  <v-card>
    <v-card-text>
      <v-avatar size="64px" class="float-left ml-4">
        <img :src="avatar|profileImage" alt="تصویر کاربر" />
      </v-avatar>
      <v-rating :value="rate" :length="5" readonly size="18" half-increments />
      <p>امتیاز: {{ rate||0 }} از {{ rateCount }} رای</p>
      <p>{{ fullName }}</p>
      <p>{{ userName }}</p>
    </v-card-text>
    <v-card-actions>
      <v-btn outlined color="primary" :to="`/view-profile/${id}`">مشاهده پروفایل</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    avatar: {
      type: String,
      required: false,
      default: null
    },
    fullName: {
      type: String,
      required: false,
      default: ""
    },
    userName: {
      type: String,
      required: false,
      default: ""
    },
    id: {
      type: Number,
      required: true
    },
    rate: {
      type: Number,
      required: false,
      default: 0
    },
    rateCount: {
      type: Number,
      required: true,
      default: 0
    }
  }
});
</script>

