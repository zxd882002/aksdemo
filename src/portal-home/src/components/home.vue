<template>
  <div class="hello">
    <h1>{{ msg ?? "这是我们的主页" }}</h1>
  </div>
</template>

<script setup lang="ts">
import { defineProps } from "vue";
defineProps({ msg: String });
</script>

<style lang="scss" scoped>
.hello {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
