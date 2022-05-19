import axios, { AxiosRequestConfig } from "axios";

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

interface ApiConfig {
  method: "get" | "post";
  url: string;
  params: object;
  config: AxiosRequestConfig | undefined;
}

const CallApi = async <T>(api: ApiConfig) => {
  let data: T;
  switch (api.method) {
    case "get":
      let getResponse = await axios.get<T>(api.url, api.config);
      data = getResponse.data;
      break;
    case "post":
      let postResponse = await axios.post<T>(api.url, api.params, api.config);
      data = postResponse.data;
      break;
  }
  return data;
};
