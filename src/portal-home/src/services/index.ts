// import Axios from "axios";
// Axios.defaults.baseURL = "http://localhost:15000";

import axios from "axios";
axios.interceptors.request.use(
  (config) => {
    if (localStorage.getItem("BearerToken")) {
      config.headers!.BearerToken = localStorage.getItem(
        "Authorization"
      ) as string;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);
