namespace WeatherForecastAPI.Models.Auth.AuthResponses
{
    public class RefreshTokenResponse
    {
        public bool AuthSuccess { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
