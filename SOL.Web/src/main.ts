/**
 * beforeCreate -> setup()
 * created -> setup()
 * beforeMount -> onBeforeMount
 * mounted -> onMounted
 * beforeUpdate -> onBeforeUpdate
 * updated -> onUpdated
 * beforeDestroy -> onBeforeUnmount
 * destroyed -> onUnmounted
 * errorCaptured -> onErrorCaptured
 */
import { createApp } from "vue";
import App from "./App.vue";

import router from "./router"; // 路由

const app = createApp(App);

app.use(router);

router.isReady().then(() => {
  app.mount("#app");
});
