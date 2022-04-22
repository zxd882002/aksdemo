<template>
  <el-alert :title="errorMsg" type="error" show-icon v-show="errorMsg != ''" />
  <div>
    <div v-show="!gameStarted">
      <div v-if="gameStatus == 'pass'">
        恭喜你，猜对了！正确答案是：{{ gameAnswer }}
      </div>
      <div v-if="gameStatus == 'fail'">
        很遗憾，机会用完，没有猜出正确答案。正确答案是：{{ gameAnswer }}
      </div>
      <div v-if="gameHistories.length != 0">
        <div>历史结果：</div>
        <ul>
          <li v-for="history in gameHistories" :key="history">
            {{ history }}
          </li>
        </ul>
      </div>
      <div v-if="gameStatus == ''">
        <h1>猜数字</h1>
      </div>
      <el-button type="primary" round @click="startGame">点击开始</el-button>
    </div>
    <div v-show="gameStarted">
      <div>
        请输入4个不重复的数字，按回车键提交 - 你还有{{ gameRetry }}次机会
      </div>
      <div class="inputOut">
        <input
          class="input"
          ref="input1"
          v-model="inputNumber1"
          @input="onInputNumber1"
          @keypress="onEnter"
        />
        <input
          class="input"
          ref="input2"
          v-model="inputNumber2"
          @input="onInputNumber2"
          @keypress="onEnter"
        />
        <input
          class="input"
          ref="input3"
          v-model="inputNumber3"
          @input="onInputNumber3"
          @keypress="onEnter"
        />
        <input
          class="input"
          ref="input4"
          v-model="inputNumber4"
          @input="onInputNumber4"
          @keypress="onEnter"
        />
      </div>
      <div v-if="gameHistories.length != 0">
        <h3>历史结果：</h3>
        <ul>
          <li v-for="history in gameHistories" :key="history">
            {{ history }}
          </li>
        </ul>
      </div>
    </div>
    <div>
      <h1>游戏规则</h1>
      <ol>
        <li>点击开始以后，系统会生成一个随机的不重复的4位数。（比如：1234）</li>
        <li>你可以输入4个不重复的数字并提交。（比如:1357)</li>
        <li>系统根据你输入的数字，给出相应的提示：xAyB。（比如：1A1B)</li>
        <li>
          其中A表示的是有哪几个数和系统的数相同，并且位置也相同（比如，数字1在不仅系统中出现，且位置相同）
        </li>
        <li>
          其中B表示的是有哪几个数和系统的数相同，但是位置不相同（比如，数字3在系统中出现，但是位置不同）
        </li>
        <li>当所有的数字都猜出，且位置相同，游戏胜利</li>
        <li>每局有10次机会，如果机会用完都没有猜出，游戏失败</li>
      </ol>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { Guid } from "guid-typescript";
import { computed } from "@vue/reactivity";
import { reactive, toRefs, ref } from "vue";
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
        requestId: Guid.create().toString(),
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
        requestId: Guid.create().toString(),
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

const input1 = ref<HTMLElement | null>(null);
const input2 = ref<HTMLElement | null>(null);
const input3 = ref<HTMLElement | null>(null);
const input4 = ref<HTMLElement | null>(null);

const onInputNumber1 = (event: Event) => {
  const target = event.target as HTMLInputElement;
  const value = target.value;
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber1 = "";
  else if (gameStatusInformation.inputNumber2 == value)
    gameStatusInformation.inputNumber2 = "";
  else if (gameStatusInformation.inputNumber3 == value)
    gameStatusInformation.inputNumber3 = "";
  else if (gameStatusInformation.inputNumber4 == value)
    gameStatusInformation.inputNumber4 = "";
  else input2.value?.focus();
};

const onInputNumber2 = (event: Event) => {
  const target = event.target as HTMLInputElement;
  const value = target.value;
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber2 = "";
  else if (gameStatusInformation.inputNumber1 == value)
    gameStatusInformation.inputNumber1 = "";
  else if (gameStatusInformation.inputNumber3 == value)
    gameStatusInformation.inputNumber3 = "";
  else if (gameStatusInformation.inputNumber4 == value)
    gameStatusInformation.inputNumber4 = "";
  else input3.value?.focus();
};

const onInputNumber3 = (event: Event) => {
  const target = event.target as HTMLInputElement;
  const value = target.value;
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber3 = "";
  else if (gameStatusInformation.inputNumber1 == value)
    gameStatusInformation.inputNumber1 = "";
  else if (gameStatusInformation.inputNumber2 == value)
    gameStatusInformation.inputNumber2 = "";
  else if (gameStatusInformation.inputNumber4 == value)
    gameStatusInformation.inputNumber4 = "";
  else input4.value?.focus();
};

const onInputNumber4 = (event: Event) => {
  const target = event.target as HTMLInputElement;
  const value = target.value;
  if (!value.match("^[0-9]$")) gameStatusInformation.inputNumber4 = "";
  else if (gameStatusInformation.inputNumber1 == value)
    gameStatusInformation.inputNumber1 = "";
  else if (gameStatusInformation.inputNumber2 == value)
    gameStatusInformation.inputNumber2 = "";
  else if (gameStatusInformation.inputNumber3 == value)
    gameStatusInformation.inputNumber3 = "";
};

const onEnter = (event: KeyboardEvent) => {
  if (
    event.key == "Enter" &&
    gameStatusInformation.inputNumber1 != "" &&
    gameStatusInformation.inputNumber2 != "" &&
    gameStatusInformation.inputNumber3 != "" &&
    gameStatusInformation.inputNumber4 != ""
  ) {
    resultCheck();
  }
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

<style lang="scss" scoped>
.inputOut {
  display: flex;
  margin: 10px;
  .input {
    width: 30px;
    height: 30px;
    border-top: none;
    border-left: none;
    border-right: none;
    border-bottom: 1px solid black;
    margin-left: 30px;
    text-align: center;
  }
}
</style>
