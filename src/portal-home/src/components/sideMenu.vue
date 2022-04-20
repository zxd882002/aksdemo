<template>
  <el-aside width="collapse">
    <el-menu router :collapse="collapse">
      <el-sub-menu index="1">
        <template #title>
          <el-icon :size="50">
            <coordinate />
          </el-icon>
          <span style="margin-left: 1rem">小游戏</span>
        </template>
        <el-menu-item index="/games/numberGuess">猜数字</el-menu-item>
        <el-menu-item index="/games/gobang">五子棋</el-menu-item>
        <el-menu-item index="1-3">...</el-menu-item>
      </el-sub-menu>
      <el-sub-menu index="2">
        <template #title>
          <el-icon :size="50">
            <tools />
          </el-icon>
          <span style="margin-left: 1rem">小工具</span>
        </template>
        <el-menu-item index="/tools/calculator">计算器</el-menu-item>
        <el-menu-item index="2-2">...</el-menu-item>
      </el-sub-menu>
    </el-menu>
    <div class="collaps-button" @click="collapse = !collapse">
      <el-icon v-if="!collapse" :size="20"><fold /></el-icon>
      <el-icon v-if="collapse" :size="20"><expand /></el-icon>
      <span v-show="!collapse" style="margin-left: 1rem">点击收起</span>
    </div>
  </el-aside>
</template>

<script setup lang="ts">
import { Coordinate, Tools, Fold, Expand } from "@element-plus/icons-vue";
import { ref } from "vue";
const collapse = ref(false);
window.addEventListener("resize", () => {
  setTimeout(() => {
    const width = window.document.documentElement.getBoundingClientRect().width;
    if (width <= 540) {
      collapse.value = true;
    } else {
      collapse.value = false;
    }
  }, 500);
});
</script>

<style lang="scss" scoped>
.el-aside {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  .el-menu {
    flex-grow: 1;
    &:not(.el-menu--collapse) {
      min-width: 200px;
    }
  }
  .collaps-button {
    padding: 20px;
    background-color: var(--el-color-primary-dark-2);
    color: white;
    display: flex;
    align-items: flex-end;
  }
}
</style>
