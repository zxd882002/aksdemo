import { ApiConfig } from "../index";
interface WeatherForecastData {
  index: number;
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const weatherForecastApi = (isAuthenticated: boolean, onError: (e: unknown) => void): ApiConfig<undefined> => {
  return {
    method: "get",
    url: "/api/weatherforecast",
    params: undefined,
    config: isAuthenticated
      ? {
          headers: {
            Authorization: "Bearer " + localStorage.getItem("BearerToken"),
          },
        }
      : undefined,
    onError: onError,
  } as ApiConfig<undefined>;
};

export { WeatherForecastData, weatherForecastApi };
