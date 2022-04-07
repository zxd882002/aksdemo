<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>Weather Forcast</p>
    <button @click="get">Get /api/weatherforecast</button>
    <table>
      <thead>
        <tr>
          <th>Date</th>
          <th>Summary</th>
          <th>TemperatureC</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in weatherforecastDataReactive" :key="item.index">
          <td>{{ item.date }}</td>
          <td>{{ item.summary }}</td>
          <td>{{ item.tmperatureC }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import axios from "axios";
import WeatherForecastData from "../models/WeatherForecastData";
import { defineComponent, reactive } from "vue";

export default defineComponent({
  name: "HelloWorld",
  props: {
    msg: String,
  },
  setup: () => {
    let weatherforecastDatas: WeatherForecastData[] = [];

    var get = async () => {
      try {
        const { data } = await axios.get("/api/weatherforecast");
        weatherforecastDatas = data;
        console.log("data:");
        console.log(weatherforecastDatas);
      } catch (error) {
        console.log("error:");
        console.log(error);
      }
    };

    let weatherforecastDataReactive = reactive(weatherforecastDatas);

    return { get, weatherforecastDataReactive };
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
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
