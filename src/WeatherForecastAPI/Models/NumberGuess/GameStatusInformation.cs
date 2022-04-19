using System;

namespace WeatherForecastAPI.Models.NumberGuess
{
    public class GameStatusInformation
    {
        public Guid GameIdentifier { get; set; }
        public int GameRetry { get; set; }
        public int[] GameNumber { get; set; }
        public string GameStatus { get; set; }
        public GameHistory[] GameHistories { get; set; }
    }
}