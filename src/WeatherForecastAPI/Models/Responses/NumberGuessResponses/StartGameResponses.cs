namespace WeatherForecastAPI.Models.Responses.NumberGuessResponses
{
    public class StartGameResponse
    {
        public ResponseHeader Header { get; set; }
        public string GameIdentifier { get; set; }
        public int GameRetry { get; set; }
        public string GameStatus { get; set; }
        public string[] GameHistories { get; set; }
    }
}