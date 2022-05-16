namespace WeatherForecastAPI.Models.Requests.AuthRequests
{
    public class AuthenticateRequest
    {
        public RequestHeader Header { get; set; } = null!;
        public string TraceId { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
