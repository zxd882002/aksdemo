namespace WeatherForecastAPI.Models.NumberGuessRequests
{
    public class CheckResultRequest
    {
        public string GameIdentifier { get; set; } = null!;
        public int[] Input { get; set; } = null!;
    }
}