<template>
  <el-alert :title="alertMessage" type="warning" show-icon v-show="refreshRemains <= 100" />

  <h3>SMOKE TEST: Weather Forcast</h3>
  <el-button type="primary" @click="get"> Get /api/weatherforecast </el-button>
  <div>{{ errorMsg }}</div>
  <table v-if="show">
    <thead>
      <tr>
        <th>Date</th>
        <th>Summary</th>
        <th>TemperatureC</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in weatherDatas" :key="item.index">
        <td>{{ item.date }}</td>
        <td>{{ item.summary }}</td>
        <td>{{ item.temperatureC }}</td>
      </tr>
    </tbody>
  </table>

  <h3>Redis API</h3>
  <el-button type="primary" @click="clearRedis"> Clear Redis </el-button>

  <h3>请输入你的API测试：</h3>
  <h5>REQUEST</h5>
  <div>Method:</div>
  <div>
    <el-select v-model="inputMethod" placeholder="Select">
      <el-option v-for="item in inputMethods" :key="item" :label="item" :value="item" />
    </el-select>
  </div>
  <div>API URL:</div>
  <div><el-input v-model="inputUrl" placeholder="/api/WeatherForecast" /></div>
  <div>Body (json):</div>
  <div><el-input v-model="inputBody" :autosize="{ minRows: 5 }" type="textarea" /></div>
  <div><el-button type="primary" @click="callTestApi">提交</el-button></div>
  <h5>RESPONSE</h5>
  <div>Status Code:</div>
  <div><el-input v-model="statusCode" /></div>
  <div>Body</div>
  <div><el-input v-model="responseBody" :autosize="{ minRows: 5 }" type="textarea" /></div>
</template>

<script setup lang="ts">
import { callApi } from "@/services";
import { testApi } from "@/services/admin/TestApi";
import { type WeatherForecastData, weatherForecastApi } from "@/services/admin/WeatherForecastApi";
import { clearRedisApi } from "@/services/redis/ClearRedisApi";
import { ref, reactive, toRefs, computed, watchEffect } from "vue";
import { useStore } from "@/stores";
import { useRouter } from "vue-router";
const store = useStore();

const router = useRouter();

const state = reactive({
  weatherDatas: [] as WeatherForecastData[],
  show: false,
  errorMsg: "",
});

let now = ref(new Date());

window.setInterval(() => {
  now.value = new Date();
}, 1000);

const alertMessage = computed(() => "您已经有很长时间没有操作了，再过" + refreshRemains.value + "秒就要重新登录");

const refreshRemains = computed(() => {
  return Math.round((store.refreshTokenExp.getTime() - now.value.getTime()) / 1000);
});

watchEffect(() => {
  if (refreshRemains.value < 0) {
    router.push("/admin");
  }
});

const get = async () => {
  const response = await callApi<undefined, WeatherForecastData[]>(
    weatherForecastApi((e) => (state.errorMsg = e as string))
  );

  if (response) {
    state.weatherDatas = response.data;
    state.show = true;
  }
};

const { weatherDatas, show, errorMsg } = toRefs(state);

const clearRedis = async () => {
  await callApi<undefined, undefined>(clearRedisApi((e) => (state.errorMsg = e as string)));
};

const inputMethods = ["get", "post"];

const inputUrl = ref("");

const inputMethod = ref("");

const inputBody = ref("");

const statusCode = ref("200");

const responseBody = ref('{"Succeed": true}');

const callTestApi = async () => {
  // eslint-disable-next-line
  const response = await callApi<any, any>(
    testApi(
      inputMethod.value,
      inputUrl.value,
      inputBody.value == "" ? undefined : JSON.parse(inputBody.value),
      // eslint-disable-next-line
      (e: any) => {
        if (e.response) {
          statusCode.value = e.response.status.toString();
          responseBody.value = JSON.stringify(e.response.data);
        } else {
          statusCode.value = "";
          responseBody.value = e.message;
        }
      }
    )
  );
  if (response) {
    statusCode.value = response.status.toString();
    responseBody.value = JSON.stringify(response.data);
  }
};
</script>

<style scoped lang="scss">
table {
  margin-top: 40;
  margin-left: auto;
  margin-right: auto;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
