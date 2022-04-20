<template>
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
</template>

<script setup lang="ts">
import SideMenu from "./components/sideMenu.vue";
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
