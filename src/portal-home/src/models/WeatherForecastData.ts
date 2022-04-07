export default class WeatherForecastData {
    constructor(
        public index: number,
        public date: string,
        public tmperatureC: number,
        public temperatureF: number,
        public summary: string
    ) { }
}