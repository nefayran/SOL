import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    meta: {
      title: "Home",
      keepAlive: true,
    },
    component: () => import("@/view/pages/index.vue"),
  },
  {
    path: "/registration",
    name: "Registration",
    meta: {
      title: "Registration",
      keepAlive: true,
    },
    component: () => import("@/view/pages/registration/index.vue"),
  },
  {
    path: "/account",
    name: "Account",
    meta: {
      title: "Account",
      keepAlive: true,
    },
    component: () => import("@/view/pages/account/index.vue"),
  },
];
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
