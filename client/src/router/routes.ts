import LoadingComponent from "@/components/LoadingComponent.vue";
import LoadingErrorComponent from "@/components/LoadingComponent.vue";
import NotFound from "@/components/NotFound.vue";
import Home from "@/pages/Home.vue";
import About from "@/pages/About.vue";
import Login from "@/pages/Login.vue";
import ForgetPassword from "@/pages/ForgetPassword.vue";
import Register from "@/pages/Register.vue";
import Profile from "@/pages/Profile.vue";
import Logout from "@/pages/Logout.vue";
import LoginRegister from "@/pages/LoginRegister.vue";
import ViewProfile from "@/pages/ViewProfile.vue";
import PayResult from "@/pages/PayResult.vue";
import Users from "@/pages/AdminPanel/Users.vue";

function lazyLoadView(AsyncView) {
  const AsyncHandler = () => ({
    component: AsyncView,
    loading: LoadingComponent,
    delay: 200,
    error: LoadingErrorComponent,
    timeout: 10000
  });

  return Promise.resolve({
    functional: true,
    render(h, { data, children }) {
      return h(AsyncHandler, data, children);
    }
  });
}

export const routes = [
  {
    path: "/",
    name: "home",
    meta: {
      title: "خیریه",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: Home
  },
  {
    path: "/about",
    name: "about",
    meta: {
      title: "درباره ما",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: About
  },
  {
    path: "/login",
    name: "login",
    meta: {
      title: "ورود به سامانه",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: Login
  },
  {
    path: "/forgetPassword",
    name: "forgetPassword",
    meta: {
      title: "بازیابی رمزعبور",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: ForgetPassword
  },
  {
    path: "/register",
    name: "register",
    meta: {
      title: "ثبت نام در سامانه",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: Register
  },
  {
    path: "/profile",
    name: "profile",
    meta: {
      title: "پروفایل",
      authorize: true,
      roles: ["Admin", "Charity", "User"]
    },
    component: Profile
  },
  {
    path: "/logout",
    name: "logout",
    meta: {
      title: "خروج از سامانه",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: Logout
  },
  {
    path: "/login-register",
    name: "loginRegister",
    meta: {
      title: "ورود یا ثبت نام",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: LoginRegister
  },
  {
    path: "/view-profile/:id",
    name: "viewProfile",
    meta: {
      title: "مشاهده پروفایل",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: ViewProfile,
    props: true
  },
  {
    path: "/payResult",
    name: "payResult",
    meta: {
      title: "نتیجه پرداخت",
      authorize: false
    },
    component: PayResult
  },
  {
    path: "/users",
    name: "users",
    meta: {
      title: "کاربران",
      authorize: true,
      roles: ["Admin"]
    },
    component: Users
  },
  {
    path: "*",
    meta: {
      title: "خطای 404",
      authorize: false,
      roles: ["Admin", "Charity", "User"]
    },
    component: NotFound
  }
];
