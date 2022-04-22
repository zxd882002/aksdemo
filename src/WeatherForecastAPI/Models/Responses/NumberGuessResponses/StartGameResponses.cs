namespace WeatherForecastAPI.Models.Responses.NumberGuessResponses
{
    public class StartGameResponse
    {
        public ResponseHeader Header { get; set; } = null!;
        public string GameIdentifier { get; set; } = null!;
        public int GameRetry { get; set; }
        public string GameStatus { get; set; } = null!;
        public string[] GameHistories { get; set; } = null!;
    }
}