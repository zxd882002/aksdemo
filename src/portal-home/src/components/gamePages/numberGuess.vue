<template>
  <el-alert :title="errorMsg" type="error" show-icon v-show="errorMsg != ''" />
  <div>
    <div v-show="!gameStarted">
      <div v-if="gameStatus == 'pass'">
        恭喜通关了，正确答案是：{{ gameAnswer }}
      </div>
      <div v-if="gameStatus == 'fail'">
        很遗憾，没有过关，正确答案是：{{ gameAnswer }}
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
      <div style="display: flex">
        <el-input
          v-model="inputNumber1"
          size="large"
          placeholder="Please Input"
          @input="onInputNumber1"
        />
        <el-input
          v-model="inputNumber2"
          size="large"
          placeholder="Please Input"
          @input="onInputNumber2"
        />
        <el-input
          v-model="inputNumber3"
          size="large"
          placeholder="Please Input"
          @input="onInputNumber3"
        />
        <el-input
          v-model="inputNumber4"
          size="large"
          placeholder="Please Input"
          @input="onInputNumber4"
        />
      </div>
      <button @click="resultCheck">提交</button>
      <div>历史结果：</div>
      <ul>
        <li v-for="history in gameHistories" :key="history">
          {{ history }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { Guid } from "guid-typescript";
import { computed } from "@vue/reactivity";
import { reactive, toRefs } from "vue";
import NumberGuessStartGameRequest from "@/models/games/numberGuessStartGameRequest";
import NumberGuessStartGameResponse from "@/models/games/numberGuessStartGameResponse";
import NumberGuessCheckResultRequest from "@/models/games/numberGuessCheckResultRequest";
import NumberGuessCheckResultResponse from "@/models/games/numberGuessCheckResultResponse";
const gameStatusInformation = reactive({
  gameIdentifier: "",
  gameRetry: 10,
  gameAnswer: "", // "", "1234"
  gameStatus: "", // "started", "pass", "fail"
  gameHistories: [] as string[], // ["1234 - 1A1B", "5678 - 0A2B"],
  inputNumber1: "",
  inputNumber2: "",
  inputNumber3: "",
  inputNumber4: "",
  errorMsg: "",
});

const startGame = async () => {
  try {
    const request: NumberGuessStartGameRequest = {
      header: {
        requestId: Guid.create(),
      },
    };
    const { data } = await axios.post<NumberGuessStartGameResponse>(
      "/api/NumberGuess/StartGame",
      request
    );
    gameStatusInformation.gameIdentifier = data.gameIdentifier;
    gameStatusInformation.gameRetry = data.gameRetry;
    gameStatusInformation.gameStatus = data.gameStatus;
    gameStatusInformation.gameHistories = data.gameHistories;
  } catch (error) {
    gameStatusInformation.errorMsg = error as string;
  }
};

const resultCheck = async () => {
  try {
    const request: NumberGuessCheckResultRequest = {
      header: {
        requestId: Guid.create(),
      },
      gameIdentifier: gameStatusInformation.gameIdentifier,
      input: [
        +gameStatusInformation.inputNumber1,
        +gameStatusInformation.inputNumber2,
        +gameStatusInformation.inputNumber3,
        +gameStatusInformation.inputNumber4,
      ],
    };
    const { data } = await axios.post<NumberGuessCheckResultResponse>(
      "/api/NumberGuess/CheckResult",
      request
    );
    gameStatusInformation.gameIdentifier = data.gameIdentifier;
    gameStatusInformation.gameRetry = data.gameRetry;
    gameStatusInformation.gameStatus = data.gameStatus;
    gameStatusInformation.gameHistories = data.gameHistories;
    gameStatusInformation.gameAnswer = data.gameAnswer ?? "";
  } catch (error) {
    gameStatusInformation.errorMsg = error as string;
  }
};

const onInputNumber1 = (value: string) => {
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber1 = "";
  if (gameStatusInformation.inputNumber2 == value)
    gameStatusInformation.inputNumber2 = "";
  if (gameStatusInformation.inputNumber3 == value)
    gameStatusInformation.inputNumber3 = "";
  if (gameStatusInformation.inputNumber4 == value)
    gameStatusInformation.inputNumber4 = "";
};

const onInputNumber2 = (value: string) => {
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber2 = "";
  if (gameStatusInformation.inputNumber1 == value)
    gameStatusInformation.inputNumber1 = "";
  if (gameStatusInformation.inputNumber3 == value)
    gameStatusInformation.inputNumber3 = "";
  if (gameStatusInformation.inputNumber4 == value)
    gameStatusInformation.inputNumber4 = "";
};

const onInputNumber3 = (value: string) => {
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber3 = "";
  if (gameStatusInformation.inputNumber1 == value)
    gameStatusInformation.inputNumber1 = "";
  if (gameStatusInformation.inputNumber2 == value)
    gameStatusInformation.inputNumber2 = "";
  if (gameStatusInformation.inputNumber4 == value)
    gameStatusInformation.inputNumber4 = "";
};

const onInputNumber4 = (value: string) => {
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber4 = "";
  if (gameStatusInformation.inputNumber1 == value)
    gameStatusInformation.inputNumber1 = "";
  if (gameStatusInformation.inputNumber2 == value)
    gameStatusInformation.inputNumber2 = "";
  if (gameStatusInformation.inputNumber3 == value)
    gameStatusInformation.inputNumber3 = "";
};

const gameStarted = computed(() => {
  gameStatusInformation.gameStatus === "started";
});

const {
  gameRetry,
  gameAnswer,
  gameStatus,
  gameHistories,
  inputNumber1,
  inputNumber2,
  inputNumber3,
  inputNumber4,
  errorMsg,
} = toRefs(gameStatusInformation);
</script>

<style scoped></style>
