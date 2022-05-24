<template>
  <h3>Weather Forcast</h3>
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

  <h3>请输入你的API测试：</h3>
  <div>API URL:</div>
  <div><input /></div>
  <div>Method</div>
  <div><input /></div>
  <div>Parameter (json)</div>
  <div><input /></div>
</template>

<script setup lang="ts">
import { callApi } from "@/services";
import { WeatherForecastData, weatherForecastApi } from "@/services/admin/weatherForecastApi";
import { reactive, toRefs, computed } from "vue";
import { useStore } from "@/stores";
const store = useStore();
const state = reactive({
  weatherDatas: [] as WeatherForecastData[],
  show: false,
  errorMsg: "",
});
const get = async () => {
  const data = await callApi<undefined, WeatherForecastData[]>(
    weatherForecastApi((e) => (state.errorMsg = e as string))
  );

  if (data) {
    state.weatherDatas = data;
    state.show = true;
  }
};
const { weatherDatas, show, errorMsg } = toRefs(state);
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
