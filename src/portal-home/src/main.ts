import { createApp } from "vue";
import ElementPlus from "element-plus";
import zhCn from "element-plus/es/locale/lang/zh-cn";
import "element-plus/dist/index.css";
import App from "./App.vue";
import router from "./router/index";
import { createPinia } from "pinia";

// import Axios from "axios";
// Axios.defaults.baseURL = "http://localhost:15000";

const app = createApp(App);
app.use(ElementPlus, {
  locale: zhCn,
});
app.use(router);
app.use(createPinia());
app.mount("#app");
