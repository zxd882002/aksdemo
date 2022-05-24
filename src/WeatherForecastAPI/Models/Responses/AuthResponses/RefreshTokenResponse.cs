namespace WeatherForecastAPI.Models.Responses.AuthResponses
{
    public class RefreshTokenResponse
    {
        public ResponseHeader Header { get; set; } = null!;
        public bool AuthSuccess { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
