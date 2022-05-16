namespace WeatherForecastAPI.Models.ConfigOptions
{
    public class WeatherForecastApiOptions
    {
        public const string SectionName= "WeatherForecastAPI";
        public string RedisEndPoint { get; set; } = string.Empty;
        public string RedisPassword { get; set; } = string.Empty;
    }
}