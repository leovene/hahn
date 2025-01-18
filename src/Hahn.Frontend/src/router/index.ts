import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import ArtObjectsView from "@/views/ArtObjectsView.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "ArtObjects",
    component: ArtObjectsView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
