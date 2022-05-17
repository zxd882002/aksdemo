namespace WeatherForecastAPI.Models.Responses.AuthResponses
{
    public class AuthenticateResponse
    {
        public ResponseHeader Header { get; set; } = null!;
        public bool AuthSuccess { get; set; }
        public string? AuthToken { get; set; }
    }
}
