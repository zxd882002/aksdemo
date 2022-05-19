<template>
  <el-alert :title="errorMsg" type="error" show-icon v-show="errorMsg != ''" />
  <div>
    <div v-show="!gameStarted">
      <div v-if="gameStatus == 'Pass'" class="pass">
        <h3>恭喜你，猜对了！</h3>
        <div>正确答案:</div>
        <div>{{ gameAnswer }}</div>
      </div>
      <div v-if="gameStatus == 'Fail'">
        <h3>很遗憾，机会用完，没有猜出正确答案</h3>
        <div>正确答案:</div>
        <div>{{ gameAnswer }}</div>
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
      <el-button type="primary" round @click="startGame">
        点击<span v-show="gameStatus == 'Pass' || gameStatus == 'Fail'"
          >重新</span
        >开始
      </el-button>
    </div>
    <div v-show="gameStarted">
      <div>
        请输入4个不重复的数字，按回车键提交 - 你还有<span
          v-bind:style="{
            color: gameRetry <= 3 ? 'white' : 'black',
            fontWeight: gameRetry <= 3 ? 'bold' : 'normal',
            fontSize: 20 + 'px',
            background: gameRetry <= 3 ? 'red' : 'white',
            border: '1px solid black',
            padding: '5px',
          }"
          >{{ displayGameRetry }}</span
        >次机会
      </div>
      <div class="inputOut">
        <div class="halfPlaceHolder"></div>
        <input
          type="text"
          class="input"
          contenteditable="true"
          ref="input1"
          v-model="inputNumber[0]"
          @input="onInputNumber1"
          @keyup.enter="onEnter"
        />
        <div class="placeHolder"></div>
        <input
          type="text"
          class="input"
          contenteditable="true"
          ref="input2"
          v-model="inputNumber[1]"
          @input="onInputNumber2"
          @keyup.enter="onEnter"
        />
        <div class="placeHolder"></div>
        <input
          type="text"
          class="input"
          contenteditable="true"
          ref="input3"
          v-model="inputNumber[2]"
          @input="onInputNumber3"
          @keyup.enter="onEnter"
        />
        <div class="placeHolder"></div>
        <input
          type="text"
          class="input"
          contenteditable="true"
          ref="input4"
          v-model="inputNumber[3]"
          @input="onInputNumber4"
          @keyup.enter="onEnter"
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
import { callApi } from "@/services/index";
import {
  StartGameRequest,
  StartGameResponse,
  startGameApi,
} from "@/services/games/numberGuess/startGameApi";
import NumberGuessCheckResultRequest from "@/models/games/numberGuessCheckResultRequest";
import NumberGuessCheckResultResponse from "@/models/games/numberGuessCheckResultResponse";

const input1 = ref<HTMLElement | null>(null);
const input2 = ref<HTMLElement | null>(null);
const input3 = ref<HTMLElement | null>(null);
const input4 = ref<HTMLElement | null>(null);

const gameStatusInformation = reactive({
  gameIdentifier: "",
  gameRetry: 10,
  gameAnswer: "", // "", "1234"
  gameStatus: "", // "started", "pass", "fail"
  gameHistories: [] as string[], // ["1234 - 1A1B", "5678 - 0A2B"],
  inputNumber: ["", "", "", ""], // ["1", "2", "3", "4"]
  errorMsg: "",
});

const startGame = async () => {
  try {
    const data = await callApi<StartGameRequest, StartGameResponse>(
      startGameApi(Guid.create().toString())
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
        +gameStatusInformation.inputNumber[0],
        +gameStatusInformation.inputNumber[1],
        +gameStatusInformation.inputNumber[2],
        +gameStatusInformation.inputNumber[3],
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
    gameStatusInformation.inputNumber[0] = "";
    gameStatusInformation.inputNumber[1] = "";
    gameStatusInformation.inputNumber[2] = "";
    gameStatusInformation.inputNumber[3] = "";
    input1.value?.focus();
  } catch (error) {
    gameStatusInformation.errorMsg = error as string;
  }
};

const onInputNumber1 = (event: Event) => {
  const inputEvent = event as InputEvent;
  const input = inputEvent.data ?? "";
  if (input.match("^[0-9]$")) handleInput(0, input);
  else clearInput(0);
};

const onInputNumber2 = (event: Event) => {
  const inputEvent = event as InputEvent;
  const input = inputEvent.data ?? "";
  if (input.match("^[0-9]$")) handleInput(1, input);
  else clearInput(1);
};

const onInputNumber3 = (event: Event) => {
  const inputEvent = event as InputEvent;
  const input = inputEvent.data ?? "";
  if (input.match("^[0-9]$")) handleInput(2, input);
  else clearInput(2);
};

const onInputNumber4 = (event: Event) => {
  const inputEvent = event as InputEvent;
  const input = inputEvent.data ?? "";
  if (input.match("^[0-9]$")) handleInput(3, input);
  else clearInput(3);
};

const onEnter = (event: KeyboardEvent) => {
  if (
    event.key == "Enter" &&
    gameStatusInformation.inputNumber[0] != "" &&
    gameStatusInformation.inputNumber[1] != "" &&
    gameStatusInformation.inputNumber[2] != "" &&
    gameStatusInformation.inputNumber[3] != ""
  ) {
    resultCheck();
  }
};

const handleInput = (inputIndex: number, inputNumber: string) => {
  for (
    let index = 0;
    index < gameStatusInformation.inputNumber.length;
    index++
  ) {
    if (index == inputIndex) {
      gameStatusInformation.inputNumber[index] = inputNumber;
    } else if (gameStatusInformation.inputNumber[index] == inputNumber) {
      gameStatusInformation.inputNumber[index] = "";
    }
  }

  if (inputIndex == 0 && gameStatusInformation.inputNumber[1] == "")
    input2.value?.focus();
  if (inputIndex == 1 && gameStatusInformation.inputNumber[2] == "")
    input3.value?.focus();
  if (inputIndex == 2 && gameStatusInformation.inputNumber[3] == "")
    input4.value?.focus();
};

const clearInput = (inputIndex: number) => {
  gameStatusInformation.inputNumber[inputIndex] = "";
};

const gameStarted = computed(() => {
  return gameStatusInformation.gameStatus === "Started";
});

const displayGameRetry = computed(() => {
  return gameStatusInformation.gameRetry < 10
    ? "0" + gameStatusInformation.gameRetry
    : gameStatusInformation.gameRetry;
});

const {
  gameRetry,
  gameAnswer,
  gameStatus,
  gameHistories,
  inputNumber,
  errorMsg,
} = toRefs(gameStatusInformation);
</script>

<style lang="scss" scoped>
.inputOut {
  display: flex;
  height: 30vw;
  max-height: 90px;
  align-items: center;
  .input {
    width: 10vw;
    height: 10vw;
    border-top: none;
    border-left: none;
    border-right: none;
    border-bottom: px solid black;
    text-align: center;
    max-width: 30px;
    max-height: 30px;
  }
  .halfPlaceHolder {
    width: 5vw;
    height: 10vw;
    max-width: 15px;
    max-height: 30px;
  }
  .placeHolder {
    width: 10vw;
    height: 10vw;
    max-width: 30px;
    max-height: 30px;
  }
}
</style>
