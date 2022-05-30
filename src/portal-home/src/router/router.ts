import { RouteRecordRaw } from "vue-router";
import "vue-router";

declare module "vue-router" {
  interface RouteMeta {
    title: string;
  }
}

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
        meta: {
          title: "淘淘の家 - 小游戏 - 五子棋",
        },
      },
      {
        path: "numberGuess",
        name: "numberGuess",
        component: () => import("../components/gamePages/numberGuess.vue"),
        meta: {
          title: "淘淘の家 - 小游戏 - 猜数字",
        },
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
        component: () => import("../components/toolPages/scrum.vue"),
        meta: {
          title: "淘淘の家 - 小工具 - 微信scrum",
        },
      },
    ],
  },
  {
    path: "/admin",
    name: "admin",
    component: () => import("../components/login.vue"),
  },
  {
    path: "/apiTest",
    name: "api test",
    component: () => import("../components/adminPages/apiTest.vue"),
  },
  {
    path: "/",
    name: "home",
    component: () => import("../components/home.vue"),
    meta: {
      title: "淘淘の家",
    },
  },
];

export default routes;
