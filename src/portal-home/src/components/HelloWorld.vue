<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>Weather Forcast</p>
    <button @click="get">Get /api/weatherforecast</button>
  </div>
</template>

<script lang="ts">
import axios from "axios";
import WeatherForecastData from "../models/WeatherForecastData";

export default {
  name: "HelloWorld",
  props: {
    msg: String,
  },
  setup() {
    let weatherforecastDatas: WeatherForecastData[] | undefined = undefined;

    var get = async () => {
      try {
        const { data } = await axios.get("/api/weatherforecast");
        weatherforecastDatas = data;
      } catch (error) {
        console.log("error:");
        console.log(error);
      }
    };

    console.log("data:");
    console.log(weatherforecastDatas);

    return { get };
  },
};
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
