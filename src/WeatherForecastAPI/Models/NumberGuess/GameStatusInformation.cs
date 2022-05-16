using System;
using System.Collections.Generic;

namespace WeatherForecastAPI.Models.NumberGuess
{
    public class GameStatusInformation
    {
        public string GameIdentifier { get; set; }
        public int GameRetry { get; set; }
        public int[] GameAnswer { get; set; } = null!;
        public string GameStatus { get; set; } = null!;
        public List<GameHistory> GameHistories { get; set; } = null!;
    }
}