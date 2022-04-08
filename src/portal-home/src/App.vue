<template>
  <div class="common-layout">
    <el-container style="border: 1px solid #eee">
      <el-header class="header"> 淘淘の家</el-header>
      <el-container>
        <el-aside width="200px" style="border: 1px solid #eee">Aside</el-aside>
        <el-main style="border: 1px solid #eee"
          ><HelloWorld msg="Welcome to MY Vue.js + TypeScript App"
        /></el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script setup lang="ts">
import HelloWorld from "./components/HelloWorld.vue";
</script>

<style scoped>
.header {
  background: #409eff;
  display: flex;
  align-items: center; /*垂直居中*/
  height: 60px;
  color: white;
}
</style>
