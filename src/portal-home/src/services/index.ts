import axios, { AxiosRequestConfig } from "axios";

// dev env base url
if (process.env.NODE_ENV === "development") {
  axios.defaults.baseURL = "http://localhost:15000";
}

// intercept request
// axios.interceptors.request.use(
//   (config) => {
//     if (localStorage.getItem("BearerToken")) {
//       config.headers!.BearerToken = localStorage.getItem(
//         "BearerToken"
//       ) as string;
//     }

//     return config;
//   },
//   (error) => {
//     return Promise.reject(error);
//   }
// );

interface ApiConfig<TRequest> {
  method: "get" | "post";
  url: string;
  params: TRequest;
  config: AxiosRequestConfig | undefined;
}

const callApi = async <TRequest, TResponse>(
  api: ApiConfig<TRequest>
): Promise<TResponse> => {
  let data: TResponse;
  switch (api.method) {
    case "get": {
      const getResponse = await axios.get<TResponse>(api.url, api.config);
      data = getResponse.data;
      break;
    }
    case "post": {
      const postResponse = await axios.post<TResponse>(
        api.url,
        api.params,
        api.config
      );
      data = postResponse.data;
      break;
    }
    default:
      throw new Error();
  }
  return data;
};

export { ApiConfig, callApi };
