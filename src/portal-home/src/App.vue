<template>
  <div v-show="isLoginPage" style="height: 100%">
    <router-view></router-view>
  </div>
  <div v-show="!isLoginPage" style="height: 100%">
    <el-container style="height: 100%">
      <el-header>
        <router-link to="/" class="title">淘淘の家</router-link>
      </el-header>
      <el-container>
        <SideMenu />
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script setup lang="ts">
import SideMenu from "./components/sideMenu.vue";
import { useRouter } from "vue-router";
import { computed } from "@vue/reactivity";
const router = useRouter();
const isLoginPage = computed(() => {
  return router.currentRoute.value.name == "admin";
});
</script>

<style lang="scss" scoped>
.el-header {
  background: var(--el-color-primary-dark-2);
  display: flex;
  align-items: center; /*垂直居中*/
  height: 3em;
  .title {
    color: white;
    font-size: larger;
    font-weight: bold;
    text-decoration: none;
  }
}
</style>
