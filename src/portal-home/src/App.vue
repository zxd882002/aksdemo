<template>
  <div class="common-layout">
    <el-container style="border: 1px solid #eee">
      <el-header class="header"> 淘淘の家</el-header>
      <el-container>
        <el-aside width="200px" style="border: 1px solid #eee">
          <el-scrollbar
            ><el-menu
              ><el-sub-menu index="1">
                <template #title>
                  <coordinate
                    style="width: 1em; height: 1em; padding-right: 1em;"
                  /><span>小游戏</span>
                </template>
                <el-menu-item index="1-1">猜数字</el-menu-item>
                <el-menu-item index="1-2">数独</el-menu-item>
                <el-menu-item index="1-3">五子棋</el-menu-item>
              </el-sub-menu>
            </el-menu>
          </el-scrollbar></el-aside
        >
        <el-main style="border: 1px solid #eee"
          ><HelloWorld msg="Welcome to MY Vue.js + TypeScript App"
        /></el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script setup lang="ts">
import HelloWorld from "./components/HelloWorld.vue";
import { Coordinate } from "@element-plus/icons-vue";
</script>

<style scoped>
.header {
  background: #409eff;
  display: flex;
  align-items: center; /*垂直居中*/
  height: 3em;
  color: white;
  font-size: larger;
  font-weight: bold;
}
</style>
