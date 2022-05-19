import axios from "axios";

// dev env base url
if (process.env.NODE_ENV === "development") {
  axios.defaults.baseURL = "http://localhost:15000";
}

// intercept request
axios.interceptors.request.use(
  (config) => {
    if (localStorage.getItem("BearerToken")) {
      config.headers!.BearerToken = localStorage.getItem(
        "BearerToken"
      ) as string;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);
