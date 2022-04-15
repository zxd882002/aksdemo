<template>
  <el-menu router>
    <el-sub-menu index="1">
      <template #title>
        <coordinate style="width: 1em; height: 1em; padding-right: 1em" />
        <span>小游戏</span>
      </template>
      <el-menu-item index="/games/numberGuess">猜数字</el-menu-item>
      <el-menu-item index="/games/gobang">五子棋</el-menu-item>
      <el-menu-item index="1-3">...</el-menu-item>
    </el-sub-menu>
    <el-sub-menu index="2">
      <template #title>
        <tools style="width: 1em; height: 1em; padding-right: 1em" />
        <span>小工具</span>
      </template>
      <el-menu-item index="/tools/calculator">计算器</el-menu-item>
      <el-menu-item index="2-2">...</el-menu-item>
    </el-sub-menu>
  </el-menu>
</template>

<script setup lang="ts">
import { Coordinate, Tools } from "@element-plus/icons-vue";
</script>

<style scoped>
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
