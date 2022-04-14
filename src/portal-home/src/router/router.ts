import { RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/games",
    name: "games",
    component: () => import("../components/gamePages/games.vue"),
    children: [
      {
        path: "gobang",
        name: "gobang",
        component: () => import("../components/gamePages/gobang.vue"),
      },
      {
        path: "numberGuess",
        name: "numberGuess",
        component: () => import("../components/gamePages/numberGuess.vue"),
      },
    ],
  },
  {
    path: "/tools",
    name: "tools",
    component: () => import("../components/toolPages/tools.vue"),
    children: [
      {
        path: "calculator",
        name: "calculator",
        component: () => import("../components/toolPages/calculator.vue"),
      },
    ],
  },
  {
    path: "/",
    name: "home",
    component: () => import("../components/home.vue"),
  },
];

export default routes;
