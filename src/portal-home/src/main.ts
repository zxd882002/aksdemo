import { createApp } from "vue";
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";
import App from "./App.vue";
import router from "./router/index";

import Axios from "axios";
Axios.defaults.baseURL = "http://localhost:15000";

const app = createApp(App);
app.use(ElementPlus);
app.use(router);
app.mount("#app");
