<template>
  <v-app>
    <v-navigation-drawer
      dark
      right
      fixed
      :temporary="!drawerPermanent"
      :permanent="drawerPermanent"
      :mini-variant="drawerMini"
      v-model="drawerIsOpen"
      app
      clipped
      class="app-drawer"
      width="276"
    >
      <NavigationMenu />
    </v-navigation-drawer>
    <v-app-bar app clipped-right color="#41bfc3">
      <v-app-bar-nav-icon v-if="drawerPermanent" class="app-menu-btn" @click="mini = !mini">
        <i
          :class="{
            'v-icon': true,
            notranslate: true,
            mdi: true,
            'mdi-chevron-left': mini,
            'mdi-chevron-right': !mini
          }"
        ></i>
        <!-- <v-icon>{{ mini ? '$vuetify.icons.chevronLeft' : '$vuetify.icons.chevronRight' }}</v-icon> -->
      </v-app-bar-nav-icon>
      <v-app-bar-nav-icon v-if="!drawerPermanent" class="app-menu-btn" @click="toggleDrawerOpen" />
      <v-toolbar-items class="hidden-sm-and-down">
        <MainMenu />
      </v-toolbar-items>
      <v-spacer />
      <router-link to="/" class="logo-link">
        <img src="./assets/images/logo.png" height="100%" />
      </router-link>
    </v-app-bar>
    <v-content>
      <v-container fluid class="pages-container">
        <transition name="slide-y-transition" mode="out-in">
          <router-view />
        </transition>
      </v-container>
      <go-up />
      <v-footer class="d-block body-1 pa-0" color="#606271" :height="this.$vuetify.breakpoint.name === 'xs' ? 300 : 200">
        <v-parallax
          :src="require('./assets/images/footer.png?lazy')"
          :height="this.$vuetify.breakpoint.name === 'xs' ? 300 : 200"
          class="text-center"
        >
          <p>با ما در شبکه های اجتماعی در تماس باشید</p>
          <div class="social-links">
            <v-btn v-for="social in socialLinks" :key="social.icon" target="_blank" :href="social.link" class="mx-4" dark icon>
              <v-icon size="24px">{{ social.icon }}</v-icon>
            </v-btn>
          </div>
          <div class="external-links">
            <v-btn v-for="link in links" :key="link.href" :href="link.href" target="_blank" class="mx-4 link" depressed text color="#fff">{{
              link.text
            }}</v-btn>
          </div>
        </v-parallax>
      </v-footer>
      <v-footer class="d-block text-center pt-2 copyright" height="auto">
        کلیه حقوق سایت متعلق به شرکت ... می باشد.
      </v-footer>
    </v-content>
    <v-dialog v-model="quickLoginDialog" max-width="500" persistent scrollable>
      <v-card>
        <v-card-title class="headline">ورود به سیستم</v-card-title>
        <v-card-text>
          <p>بدلیل اینکه از سیستم خارج شده اید باید دوباره وارد سیستم شوید</p>
          <LoginForm form-id="main-login-form" @success="onLoginSuccess" @submitting="onSubmitting($event)" />
        </v-card-text>
        <v-card-actions>
          <v-btn :disabled="submitting" :loading="submitting" color="green darken-1" text form="main-login-form" type="submit">ورود</v-btn>
          <v-btn :disabled="submitting" color="green darken-1" text @click="closeQuickLoginDialog">انصراف</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import GoUp from "@/components/GoUp.vue";
import NavigationMenu from "@/components/NavigationMenu.vue";
import MainMenu from "@/components/MainMenu.vue";
import appData from "@/appData.ts";
import LoginForm from "@/components/User/LoginForm.vue";
import store, { UserInfo } from "@/store";

export default Vue.extend({
  components: {
    GoUp,
    NavigationMenu,
    MainMenu,
    LoginForm
  },
  data() {
    return {
      socialLinks: appData.socialLinks,
      links: appData.links,
      mini: false,
      submitting: false
    };
  },
  computed: {
    userInfo(): UserInfo {
      return this.$store.state.userInfo;
    },
    drawerIsOpen: {
      get(): boolean {
        return this.$store.state.drawerIsOpen;
      },
      set(newValue: boolean) {
        this.handleDrawerOpen(newValue);
      }
    },
    quickLoginDialog(): UserInfo {
      return this.$store.state.quickLoginDialog;
    },
    drawerPermanent(): boolean {
      return (
        this.userInfo.isAuthenticated &&
        (this.$vuetify.breakpoint.name === "lg" || this.$vuetify.breakpoint.name === "xl" || this.$vuetify.breakpoint.name === "md")
      );
    },
    drawerMini(): boolean {
      return this.drawerPermanent ? this.mini : false;
    },
  },
  mounted() {
    for (const element of document.getElementsByClassName("app-loader")) {
      element.remove();
    }
    const e = document.getElementById("app-loader-style");
    if (e) {
      e.remove();
    }
  },
  methods: {
    closeQuickLoginDialog(): any {
      return this.$store.dispatch("closeQuickLoginDialog");
    },
    toggleDrawerOpen() {
      return this.$store.dispatch("toggleDrawerOpen");
    },
    handleDrawerOpen(value: boolean) {
      return this.$store.dispatch("handleDrawerOpen", value);
    },
    async loadDashboard() {
      await this.$store.dispatch("loadUserDashboard");
    },
    async onLoginSuccess() {
      await this.$swal({
        text: "ورود به سیستم موفقیت آمیز بود. میتوانید ادامه دهید\nممکن است نیاز باشد صفحه را رفرش کنید",
        icon: "success",
        buttons: { ok: { text: "تایید", value: true } }
      });
      this.closeQuickLoginDialog();
    },
    onSubmitting(value) {
      this.submitting = value;
    }
  }
});
</script>

<style scoped>
.app-drawer {
  background-image: url("./assets/images/menu-back.png");
  background-color: #4d5b69 !important;
  background-repeat: no-repeat;
  background-size: auto;
}

.pages-container {
  min-height: 340px;
}

.app-menu-btn {
  margin: 6px 0 6px 6px !important;
}

.copyright {
  font-size: 13px;
}

.logo-link {
  height: 100%;
}

.link {
  color: inherit;
  text-decoration: inherit;
}

.seo-text {
  display: none;
}
</style>
