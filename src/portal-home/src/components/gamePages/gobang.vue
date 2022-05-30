<template>
  <h1>AI五子棋</h1>
  <div>
    <canvas ref="goBangBoard" class="goBangBoard" id="board" @click="putGoBang" width="450" height="450"></canvas>
  </div>
  <button @click="startGame">游戏开始</button>
  <div>x = {{ x }}</div>
  <div>y = {{ y }}</div>
  <div>x = {{ i }}</div>
  <div>y = {{ j }}</div>
  <div>当前下棋 {{ currentGo }}</div>
  <div>board {{ board }}</div>
</template>

<script setup lang="ts">
import { onMounted, ref, reactive, toRefs } from "vue";

const goBangBoard = ref<HTMLCanvasElement | null>(null);
const goBangStatus = reactive({
  currentGo: "black" as "black" | "white",
  x: -1,
  y: -1,
  i: -1,
  j: -1,
  board: [] as number[][],
});
const { currentGo, x, y, i, j, board } = toRefs(goBangStatus);

let canvasContext: CanvasRenderingContext2D | null | undefined;
const drawBoard = () => {
  if (canvasContext) {
    for (var i = 0; i < 15; i++) {
      canvasContext.strokeStyle = "#D6D1D1";
      canvasContext.moveTo(15 + i * 30, 15); //垂直方向画15根线，相距30px;
      canvasContext.lineTo(15 + i * 30, 435);
      canvasContext.stroke();
      canvasContext.moveTo(15, 15 + i * 30); //水平方向画15根线，相距30px;棋盘为14*14；
      canvasContext.lineTo(435, 15 + i * 30);
      canvasContext.stroke();
    }
  }
};
const drawGoBang = () => {
  if (canvasContext) {
    canvasContext.beginPath();
    canvasContext.arc(15 + goBangStatus.i * 30, 15 + goBangStatus.j * 30, 13, 0, 2 * Math.PI); //绘制棋子
    var g = canvasContext.createRadialGradient(
      15 + goBangStatus.i * 30,
      15 + goBangStatus.j * 30,
      13,
      15 + goBangStatus.i * 30,
      15 + goBangStatus.j * 30,
      0
    ); //设置渐变
    if (goBangStatus.currentGo == "black") {
      g.addColorStop(0, "#0A0A0A"); //黑棋
      g.addColorStop(1, "#636766");
    } else {
      g.addColorStop(0, "#D1D1D1"); //白棋
      g.addColorStop(1, "#F9F9F9");
    }
    canvasContext.fillStyle = g;
    canvasContext.fill();
    canvasContext.closePath();
  }
};

const putGoBang = (e: MouseEvent) => {
  goBangStatus.x = e.offsetX;
  goBangStatus.y = e.offsetY;
  goBangStatus.i = Math.floor(goBangStatus.x / 30);
  goBangStatus.j = Math.floor(goBangStatus.y / 30);
  if (goBangStatus.board[goBangStatus.j][goBangStatus.i] == 0) {
    goBangStatus.board[goBangStatus.j][goBangStatus.i] = goBangStatus.currentGo == "black" ? 1 : 2;
    drawGoBang();
    goBangStatus.currentGo = goBangStatus.currentGo == "black" ? "white" : "black";
  }
};

const startGame = () => {
  for (let i = 0; i < 15; i++) {
    goBangStatus.board[i] = [] as number[];
    for (let j = 0; j < 15; j++) {
      goBangStatus.board[i][j] = 0;
    }
  }
};

onMounted(() => {
  canvasContext = goBangBoard.value?.getContext("2d");
  drawBoard();
});
</script>

<style scoped lang="scss">
.goBangBoard {
  width: 450px;
  height: 450px;
  margin: 20px auto;
  box-shadow: -2px -2px 2px #f3f2f2, 3px 3px 3px #6f6767;
  background-color: #daca39;
}
</style>
