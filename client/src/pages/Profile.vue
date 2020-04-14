<template>
  <v-card>
    <v-card-text>
      <v-tooltip left>
        <template v-slot:activator="{ on }">
          <v-img
            v-ripple
            class="profile-image"
            :src="userInfo.avatar|profileImage"
            aspect-ratio="1"
            @click="imageDialog = true"
            v-on="on"
          />
        </template>
        <span>برای تغییر تصویر کلیک کنید</span>
      </v-tooltip>
      <v-list class="pa-0">
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>{{ userInfo.fullName }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>نام کاربری:</v-list-item-title>
            <v-list-item-subtitle>{{ userInfo.userName }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-list-item v-if="userInfo.lastLoginDateTime">
          <v-list-item-content>
            <v-list-item-title>آخرین بازدید:</v-list-item-title>
            <v-list-item-subtitle>{{ userInfo.lastLoginDateTime | moment("calendar") }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>ایمیل:</v-list-item-title>
            <v-list-item-subtitle>{{ userInfo.email }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>شماره همراه:</v-list-item-title>
            <v-list-item-subtitle>{{ userInfo.phoneNumber }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
      <v-btn tile color="primary" @click="profileFormDialog=true">ویرایش مشخصات</v-btn>&nbsp;
      <v-btn tile color="primary" @click="changePasswordDialog=true">تغییر رمز عبور</v-btn>
    </v-card-text>
    <v-dialog v-model="profileFormDialog" scrollable max-width="80%">
      <v-card>
        <v-card-title class="headline">ویرایش مشخصات</v-card-title>
        <v-card-text>
          <EditProfileForm
            form-id="profile-form"
            @submitting="profileSubmitting=$event"
            @success="profileFormDialog=false"
          />
        </v-card-text>
        <v-card-actions>
          <v-btn
            :disabled="profileSubmitting"
            :loading="profileSubmitting"
            color="green darken-1"
            text
            type="submit"
            form="profile-form"
          >ثبت</v-btn>
          <v-btn
            :disabled="profileSubmitting"
            color="green darken-1"
            text
            @click="profileFormDialog=false"
          >انصراف</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="changePasswordDialog" scrollable max-width="80%">
      <v-card>
        <v-card-title class="headline">تغییر رمز عبور</v-card-title>
        <v-card-text>
          <ChangePasswordForm
            form-id="change-password-form"
            @submitting="changePasswordSubmitting=$event"
            @success="changePasswordDialog=false"
          />
        </v-card-text>
        <v-card-actions>
          <v-btn
            :disabled="changePasswordSubmitting"
            :loading="changePasswordSubmitting"
            color="green darken-1"
            text
            type="submit"
            form="change-password-form"
          >ثبت</v-btn>
          <v-btn
            :disabled="changePasswordSubmitting"
            color="green darken-1"
            text
            @click="changePasswordDialog=false"
          >انصراف</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="imageDialog" scrollable max-width="80%">
      <v-card>
        <v-card-title class="headline">تغییر تصویر پروفایل</v-card-title>
        <v-card-text>
          <v-container grid-list-xs>
            <v-layout wrap align-center justify-center>
              <v-flex xs12>
                <input
                  id="file-upload"
                  ref="fileUpload"
                  accept="image/*"
                  type="file"
                  style="display:none"
                  @change="loadImage()"
                />
                <v-btn :disabled="submitting" color="secondary" @click="chooseFiles">انتخاب فایل</v-btn>
              </v-flex>
              <v-flex>
                <vue-cropper
                  id="vue-cropper"
                  ref="cropper"
                  :src="userInfo.avatar|profileImage"
                  :crop="showPrevImage"
                  :modal="false"
                />
              </v-flex>
              <v-flex>
                <img id="profile-image-crop-prev" :src="prevImage" />
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-btn
            :disabled="submitting"
            :loading="submitting"
            color="green darken-1"
            text
            @click="saveProfileImage()"
          >ثبت</v-btn>
          <v-btn
            :disabled="submitting"
            color="green darken-1"
            text
            @click="imageDialog=false"
          >انصراف</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";
import { debounce } from "@/tlence";
import UserService from "@/services/UserService";
import EditProfileForm from "@/components/User/EditProfileForm.vue";
import ChangePasswordForm from "@/components/User/ChangePasswordForm.vue";
import { AppState, UserInfo } from "../store";

export default Vue.extend({
  components: {
    EditProfileForm,
    ChangePasswordForm,
  },
  data() {
    return {
      imageDialog: false,
      submitting: false,
      prevImage: null,
      profileFormDialog: false,
      changePasswordDialog: false,
      profileSubmitting: false,
      changePasswordSubmitting: false
    };
  },
  computed: {
    userInfo(): UserInfo {
      return this.$store.state.userInfo;
    },
    showPrevImage(): any {
      return debounce(this.showPrevImageFunc, 100);
    }
  },
  methods: {
    setUserInfo(userInfo: UserInfo) {
      return this.$store.dispatch("setUserInfo", { userInfo, vm: this });
    },
    async saveProfileImage() {
      if (this.submitting) {
        return;
      }
      if (!this.$refs.cropper) {
        return;
      }
      try {
        this.submitting = true;
        const userInfo = await UserService.updateProfilePhoto(
          (this.$refs.cropper as any).getCroppedCanvas().toDataURL()
        );
        this.setUserInfo(userInfo);
        (this.$refs.cropper as any).replace(userInfo.avatar);
        this.imageDialog = false;
      } catch (error) {
       this.showErrorMessage(error);
      } finally {
        this.submitting = false;
      }
    },
    loadImage() {
      if (this.$refs.fileUpload) {
        if ((this.$refs.fileUpload as any).files[0]) {
          const reader = new FileReader();
          reader.onload = ev => {
            if (ev && ev.target) {
              if (this.$refs.cropper) {
                (this.$refs.cropper as any).replace(reader.result);
              }
            }
          };
          reader.readAsDataURL((this.$refs.fileUpload as any).files[0]);
        }
      }
    },
    chooseFiles() {
      (document.getElementById("file-upload") as HTMLElement).click();
    },
    showPrevImageFunc() {
      if (this.$refs.cropper) {
        const c = (this.$refs.cropper as any).getCroppedCanvas();
        if (c) {
          this.prevImage = c.toDataURL();
        }
      }
    }
  }
});
</script>

<style scoped>
#vue-cropper {
  max-height: 300px;
  width: 100%;
}
#profile-image-crop-prev {
  width: 200px;
  height: 200px;
  max-height: 200px;
  max-width: 200px;
  border: 1px solid gray;
  overflow: hidden;
}
.profile-image {
  cursor: pointer;
  width: 120px;
  height: 120px;
  border: 3px solid gray;
  border-radius: 6px;
}
</style>

