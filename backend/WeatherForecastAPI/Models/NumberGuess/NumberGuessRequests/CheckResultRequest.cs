namespace WeatherForecastAPI.Models.NumberGuess.NumberGuessRequests
{
    public class CheckResultRequest
    {
        public string GameIdentifier { get; set; } = null!;
        public int[] Input { get; set; } = null!;
    }
}