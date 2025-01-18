import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import apiClient from "@/services/axios";
import vuetify from "./plugins/vuetify";
import { loadFonts } from "./plugins/webfontloader";

loadFonts();

const app = createApp(App);

app.config.globalProperties.$http = apiClient;

app.use(store).use(router).use(vuetify).mount("#app");
