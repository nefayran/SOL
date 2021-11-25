import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    meta: {
      title: "Home",
      keepAlive: true,
    },
    component: () => import("../pages/index.vue"),
  },
  {
    path: "/registration",
    name: "Registration",
    meta: {
      title: "Registration",
      keepAlive: true,
    },
    component: () => import("../pages/registration/index.vue"),
  },
];
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
