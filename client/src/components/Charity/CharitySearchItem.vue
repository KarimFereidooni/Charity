<template>
  <v-card>
    <v-card-text class="d-block">
      <div>
        <v-checkbox v-model="selected" @change="handleCheck"></v-checkbox>
        <h4 class="ml-2 title">{{ charity.name }}</h4>
      </div>
      <v-chip
        title="برای فیلتر بر اساس این شهر کلیک کنید"
        class="ml-2 chip-filter"
        color="light-green"
        text-color="white"
        @click="addFilter('location', charity.location)"
        >{{ charity.location }}</v-chip
      >
      <v-chip
        :key="tag"
        v-for="tag in charity.tags"
        title="برای فیلتر بر اساس این تگ کلیک کنید"
        class="ml-2 chip-filter"
        color="light-blue"
        text-color="white"
        @click="addFilter('tag', tag)"
        >{{ tag }}</v-chip
      >
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    charity: {
      type: Object,
      required: true
    },
    addFilter: {
      type: Function,
      required: true
    },
    value: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      selected: this.value
    };
  },
  methods: {
    handleCheck() {
      this.$emit("input", this.selected);
    }
  },
  watch: {
    value(newValue) {
      this.selected = newValue;
    }
  }
});
</script>
