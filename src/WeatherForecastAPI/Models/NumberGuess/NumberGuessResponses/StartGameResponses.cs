namespace WeatherForecastAPI.Models.NumberGuess.NumberGuessResponses
{
    public class StartGameResponse
    {
        public string GameIdentifier { get; set; } = null!;
        public int GameRetry { get; set; }
        public string GameStatus { get; set; } = null!;
        public string[] GameHistories { get; set; } = null!;
    }
}