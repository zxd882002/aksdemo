import type { ApiConfig } from "..";
interface WeatherForecastData {
  index: number;
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const weatherForecastApi = (onError: (e: unknown) => void): ApiConfig<undefined> => {
  return {
    method: "get",
    url: "/api/weatherforecast",
    params: undefined,
    onError: onError,
  } as ApiConfig<undefined>;
};

export { type WeatherForecastData, weatherForecastApi };
