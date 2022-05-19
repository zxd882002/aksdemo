<template>
  <div class="hello">
    <h1>{{ msg ?? "这是我们的主页" }}</h1>
    <h3>Weather Forcast</h3>
    <div>you are authenticated? {{ isAuthenticated }}</div>
    <el-button type="primary" @click="get">
      Get /api/weatherforecast if you are authenticated
    </el-button>
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
import { reactive, toRefs, defineProps, computed } from "vue";
import { useStore } from "@/stores";
defineProps({ msg: String });

const store = useStore();
const isAuthenticated = computed(() => {
  return store.isAuthenticated;
});

const state = reactive({
  weatherDatas: [] as WeatherForecastData[],
  show: false,
  errorMsg: "",
});
const get = async () => {
  try {
    const { data } = await axios.get(
      "/api/weatherforecast",
      isAuthenticated.value
        ? {
            headers: {
              Authorization: "Bearer:" + localStorage.getItem("BearerToken"),
            },
          }
        : undefined
    );
    state.weatherDatas = data;
    state.show = true;
  } catch (error) {
    state.errorMsg = error as string;
  }
};
const { weatherDatas, show, errorMsg } = toRefs(state);
</script>

<style lang="scss" scoped>
.hello {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
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
