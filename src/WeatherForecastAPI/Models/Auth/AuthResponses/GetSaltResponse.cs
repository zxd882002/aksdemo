namespace WeatherForecastAPI.Models.Auth.AuthResponses
{
    public class GetSaltResponse
    {
        public string Salt { get; set; } = string.Empty;
        public string TraceId { get; set; }= string.Empty;
    }
}
