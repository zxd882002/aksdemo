﻿namespace WeatherForecastAPI.Models.GoBang.GoBangResponse
{
    public class GetNextStepPointResponse
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string GameStatus { get; set; } = GoBangGameStatus.Unknown;
    }
}
