import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { container } from "tsyringe";
import LoginUserCommand from "@/app/domain/commands/User/LoginUserCommand";
import CheckTokenCommand from "@/app/domain/commands/Token/CheckTokenCommand";
import ICommandHandlerBase from "@/app/core/commands/ICommandHandlerBase";

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
    path: "/login",
    name: "Login",
    meta: {
      title: "Account",
      keepAlive: true,
    },
    component: () => import("@/view/pages/login/index.vue"),
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

router.beforeEach(async (to, from, next) => {
  const publicPages = ["/login", "/register", "/home"];
  const authRequired = !publicPages.includes(to.path);
  const token = localStorage.getItem("token");
  let commandResult = false;
  if (token) {
    const checkTokenCommand: CheckTokenCommand = new CheckTokenCommand({ Token: token });
    const checkTokenCommandHandler: ICommandHandlerBase = container.resolve("CheckTokenCommandHandler");
    commandResult = await checkTokenCommandHandler.handle(checkTokenCommand);
  }
  // trying to access a restricted page + not logged in
  // redirect to login page
  if (authRequired && !commandResult.Success) {
    next("/login");
  } else {
    next();
  }
});

export default router;
