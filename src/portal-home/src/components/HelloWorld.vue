<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <h3>Weather Forcast</h3>
    <el-button type="primary" v-if="!show" @click="get"
      >Get /api/weatherforecast</el-button
    >
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
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import WeatherForecastData from "@/models/WeatherForecastData";
import { reactive, toRefs, defineProps } from "vue";

defineProps({ msg: String });

const state = reactive({
  weatherDatas: [] as WeatherForecastData[],
  show: false,
  errorMsg: "",
});

const get = async () => {
  try {
    const { data } = await axios.get("/api/weatherforecast");
    state.weatherDatas = data;
    state.show = true;
  } catch (error) {
    state.errorMsg = error as string;
  }
};

const { weatherDatas, show, errorMsg } = toRefs(state);
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
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
