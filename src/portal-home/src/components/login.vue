<template>
  <el-alert :title="errorMsg" type="error" show-icon v-show="errorMsg != ''" />
  <div class="login">
    <div class="loginBox">
      <div class="title">Admin 登录</div>
      <div>问题1：请输入淘淘的出生年月</div>
      <div>
        <div>
          <el-date-picker
            v-model="birthdate"
            type="date"
            placeholder="选择一个日期"
            format="YYYY-MM-DD"
            value-format="YYYY-MM-DD"
          />
        </div>
      </div>
      <div>问题2：请输入淘淘的家的门牌号</div>
      <div>
        <el-input v-model="lane" />
        <span>弄</span>
        <el-input v-model="number" />
        <span>号</span>
        <el-input v-model="room" />
        <span>室</span>
      </div>
      <div>问题3：请输入验证码</div>
      <div>
        <el-input v-model="inputSalt" />
        <el-button @click="getSalt">获取验证码</el-button>
        <span>{{ salt }}</span>
      </div>
      <div>
        <div>
          <el-button type="primary" @click="tryAuth">提交</el-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthenticationRequest from "@/models/auth/authenticateRequest";
import AuthenticationResponse from "@/models/auth/authenticateResponse";
import GetSaltResponse from "@/models/auth/getSaultResponse";
import * as Crypto from "crypto-js";
import axios from "axios";
import { Guid } from "guid-typescript";
import { ref } from "vue";
import { useStore } from "@/stores";
import { useRouter } from "vue-router";

const birthdate = ref("");
const lane = ref("");
const number = ref("");
const room = ref("");
const inputSalt = ref("");
const salt = ref("");
const errorMsg = ref("");
let traceId = "";

const getSalt = async () => {
  try {
    const { data } = await axios.get<GetSaltResponse>("/api/Auth/GetSalt");
    traceId = data.traceId;
    salt.value = data.salt;
  } catch (error) {
    errorMsg.value = error as string;
  }
};

const tryAuth = async () => {
  try {
    const input =
      birthdate.value +
      "-" +
      lane.value +
      "-" +
      number.value +
      "-" +
      room.value +
      "-" +
      inputSalt.value;
    const inputHash = Crypto.SHA256(input).toString();
    const request: AuthenticationRequest = {
      header: {
        requestId: Guid.create().toString(),
      },
      traceId: traceId,
      passwordHash: inputHash,
    };
    const { data } = await axios.post<AuthenticationResponse>(
      "/api/Auth/Authenticate",
      request
    );
    let succeed = data.authSuccess;
    console.log(succeed);
    let token = data.authToken;
    console.log(token);
    const store = useStore();
    store.token = token;
    const router = useRouter();
    router.push("/");
  } catch (error) {
    errorMsg.value = error as string;
  }
};
</script>

<style scoped lang="scss">
.login {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  background-image: linear-gradient(
    to top,
    #1e3c72 0%,
    #1e3c72 1%,
    #2a5298 100%
  );
  .loginBox {
    width: auto;
    height: auto;
    padding: 10px;
    border: 1px solid black;
    div {
      padding: 5px;
    }
    background-color: white;
    .title {
      background-color: var(--el-color-primary);
      color: white;
    }
    span {
      margin-left: 3px;
      margin-right: 3px;
    }
    .el-input {
      width: 100px;
    }
  }
}
</style>
