<template>
  <el-container style="height: 100%">
    <el-header> 淘淘の家</el-header>
    <el-container>
      <el-aside>
        <el-menu>
          <el-sub-menu index="1">
            <template #title>
              <coordinate style="width: 1em; height: 1em; padding-right: 1em" />
              <span>小游戏</span>
            </template>
            <el-menu-item index="1-1">猜数字</el-menu-item>
            <el-menu-item index="1-2">五子棋</el-menu-item>
            <el-menu-item index="1-3">...</el-menu-item>
          </el-sub-menu>
          <el-sub-menu index="2">
            <template #title>
              <tools style="width: 1em; height: 1em; padding-right: 1em" />
              <span>小工具</span>
            </template>
            <el-menu-item index="2-1">计算器</el-menu-item>
            <el-menu-item index="2-2">...</el-menu-item>
          </el-sub-menu>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { Coordinate, Tools } from "@element-plus/icons-vue";
</script>

<style scoped>
.el-header {
  background: #337ecc;
  display: flex;
  align-items: center; /*垂直居中*/
  height: 3em;
  color: white;
  font-size: larger;
  font-weight: bold;
}
.el-aside {
  width: 200px;
  border-right: 1px solid #eee;
  background-color: #409eff;
}
.el-sub-menu {
  background-color: #409eff;
}
.el-sub-menu > :hover {
  background-color: #409eff;
}
.el-menu-item {
  background-color: #79bbff;
}
</style>
