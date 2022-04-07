<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>Weather Forcast</p>
    <button @click="get">Get /api/weatherforecast</button>
    <div v-if="show">
      {{ weatherDatas[0].date }}
      <table>
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
            <td>{{ item.tmperatureC }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import axios from "axios";
import WeatherForecastData from "../models/WeatherForecastData";
import { defineComponent, reactive, ref, toRefs } from "vue";

export default defineComponent({
  name: "HelloWorld",
  props: {
    msg: String,
  },
  setup: () => {
    let state = reactive({
      weatherDatas: [] as WeatherForecastData[],
      show: false,
    });

    const get = async () => {
      try {
        const { data } = await axios.get("/api/weatherforecast");
        state.weatherDatas = data;
        console.log("data:");
        console.log(state.weatherDatas);
        state.show = true;
      } catch (error) {
        console.log("error:");
        console.log(error);
      }
    };

    return { get, ...toRefs(state) };
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
