<template>
  <div class="text-center">
    <v-btn fab small color="secondary" class="goup-button" @click="scrollTo(0)">
      <v-icon size="20px">$vuetify.icons.chevronUp</v-icon>
    </v-btn>
  </div>
</template>

<style scoped>
div {
  height: 20px;
}

.goup-button {
  z-index: 1;
}
</style>

<script lang="ts">
import Vue from "vue";
import { scrollTo, easing } from "@/plugins/scrollTo";

export default Vue.extend({
  methods: {
    scrollTo(y) {
      scrollTo(y, 800, easing.easeOutQuart);
    }
  }
});
</script>

