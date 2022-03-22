<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>Weather Forcast ..........</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted } from "vue";
import axios from "axios";

export default defineComponent({
  name: "HelloWorld",
  props: {
    msg: String,
  },
});

onMounted(() => {
  console.log("test: http://weather-forecast-api/api/weatherforecast");
  axios
    .get("http://weather-forecast-api/api/weatherforecast")
    .then((res) => {
      console.log(res);
    })
    .catch((error) => {
      console.log(error);
    });
  console.log("test: http://20.89.82.173/web/WeatherForecast");
  axios
    .get("http://20.89.82.173/web/WeatherForecast")
    .then((res) => {
      console.log(res);
    })
    .catch((error) => {
      console.log(error);
    });
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
