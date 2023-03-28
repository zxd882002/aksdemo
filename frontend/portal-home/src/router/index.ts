import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/games",
      name: "games",
      component: () => import("../views/gamePages/Games.vue"),
      children: [
        {
          path: "gobang",
          name: "gobang",
          component: () => import("../views/gamePages/Gobang.vue"),
          meta: {
            title: "淘淘の家 - 小游戏 - 五子棋",
          },
        },
        {
          path: "numberGuess",
          name: "numberGuess",
          component: () => import("../views/gamePages/NumberGuess.vue"),
          meta: {
            title: "淘淘の家 - 小游戏 - 猜数字",
          },
        },
      ],
    },
    {
      path: "/tools",
      name: "tools",
      component: () => import("../views/toolPages/tools.vue"),
      children: [
        {
          path: "calculator",
          name: "calculator",
          component: () => import("../views/toolPages/scrum.vue"),
          meta: {
            title: "淘淘の家 - 小工具 - 微信scrum",
          },
        },
      ],
    },
    {
      path: "/admin",
      name: "admin",
      component: () => import("../views/Login.vue"),
    },
    {
      path: "/apiTest",
      name: "api test",
      component: () => import("../views/adminPages/ApiTest.vue"),
    },
    {
      path: "/",
      name: "home",
      component: () => import("../views/Home.vue"),
      meta: {
        title: "淘淘の家",
      },
    },
  ]
})

router.beforeEach((to, from, next) => {
  console.log(to)
  if (to.meta.title) {
    document.title = to.meta.title as string
  }
  next()
})

export default router
