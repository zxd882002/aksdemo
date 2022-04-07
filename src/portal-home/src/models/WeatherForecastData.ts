export default class WeatherForecastData {
    constructor(
        public index: number,
        public date: string,
        public temperatureC: number,
        public temperatureF: number,
        public summary: string
    ) { }
}