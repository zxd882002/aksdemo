export default class WeatherForecastData {
    constructor(
        public date: string,
        public tmperatureC: number,
        public temperatureF: number,
        public summary: string
    ) { }
}