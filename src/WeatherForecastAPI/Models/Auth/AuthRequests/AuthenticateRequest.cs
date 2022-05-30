namespace WeatherForecastAPI.Models.Auth.AuthRequests
{
    public class AuthenticateRequest
    {
        public string TraceId { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
