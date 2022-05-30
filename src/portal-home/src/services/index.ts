import axios, { AxiosRequestConfig } from "axios";
import { jwtManager } from "@/models/JwtManager";

// dev env base url
if (process.env.NODE_ENV === "development") {
  axios.defaults.baseURL = "http://localhost:15000";
}

const service = axios.create();

// intercept request
service.interceptors.request.use(
  async (config) => {
    if (await jwtManager.isAuthenticated()) {
      const token = jwtManager.getAccessToken();
      if (token) {
        config.headers = {
          Authorization: "Bearer " + token,
        };
      }
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

interface ApiConfig<TRequest> {
  method: "get" | "post";
  url: string;
  params: TRequest;
  config: AxiosRequestConfig | undefined;
  onError: (e: unknown) => void;
}

const callApi = async <TRequest, TResponse>(
  api: ApiConfig<TRequest>
): Promise<{ data: TResponse; status: number } | undefined> => {
  try {
    let data: TResponse;
    let status: number;
    switch (api.method) {
      case "get": {
        const getResponse = await service.get<TResponse>(api.url, api.config);
        data = getResponse.data;
        status = getResponse.status;
        break;
      }
      case "post": {
        const postResponse = await service.post<TResponse>(api.url, api.params, api.config);
        data = postResponse.data;
        status = postResponse.status;
        break;
      }
      default:
        throw new Error();
    }
    return { data, status };
  } catch (e) {
    api.onError(e);
    return undefined;
  }
};

const axiosCallApi = async <TRequest, TResponse>(api: ApiConfig<TRequest>): Promise<TResponse | undefined> => {
  try {
    let data: TResponse;
    switch (api.method) {
      case "get": {
        const getResponse = await axios.get<TResponse>(api.url, api.config);
        data = getResponse.data;
        break;
      }
      case "post": {
        const postResponse = await axios.post<TResponse>(api.url, api.params, api.config);
        data = postResponse.data;
        break;
      }
      default:
        throw new Error();
    }
    return data;
  } catch (e) {
    api.onError(e);
    return undefined;
  }
};

export { ApiConfig, callApi, axiosCallApi };
