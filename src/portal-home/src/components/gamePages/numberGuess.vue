<template>
  <div>
    <div v-show="!gameStarted">
      <div v-if="gameStatus == 'pass'">
        恭喜通关了，正确答案是：{{ gameNumber }}
      </div>
      <div v-if="gameStatus == 'fail'">
        很遗憾，没有过关，正确答案是：{{ gameNumber }}
      </div>
      <div v-if="gameHistories.length != 0">
        <div>历史结果：</div>
        <ul>
          <li>1234 - 1A1B</li>
          <li>5678 - 1A1B</li>
        </ul>
      </div>
      <button @click="startGame">点击开始</button>
    </div>
    <div v-show="gameStarted">
      <div>请输入4个不重复的数字 - 你还有{{ gameRetry }}次机会</div>
      <div>1234</div>
      <button>提交</button>
      <div>历史结果：</div>
      <ul>
        <li>1234 - 1A1B</li>
        <li>5678 - 1A1B</li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from "@vue/reactivity";
import { reactive, toRefs } from "vue";
const gameStatusInformation = reactive({
  gameIdentifier: 0,
  gameRetry: 10,
  gameNumber: "", // "", "1234", ...
  gameStatus: "", // "started", "pass", "fail"
  gameHistories: [], // [{"1234","1a1b"}]
});
const startGame = () => {};
const gameStarted = computed(() => {
  gameStatusInformation.gameStatus === "started";
});
const { gameRetry, gameNumber, gameStatus, gameHistories } = toRefs(
  gameStatusInformation
);
</script>

<style scoped></style>
