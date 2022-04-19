namespace WeatherForecastAPI.Models.Requests.NumberGuessRequests
{
    public class CheckResultRequest
    {
        public RequestHeader Header { get; set; } = null!;
        public string GameIdentifier { get; set; } = null!;
        public int[] Input { get; set; } = null!;
    }
}