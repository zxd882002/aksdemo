namespace WeatherForecastAPI.Models.GoBang.GoBangRequest
{
    public class GetNextStepPointRequest
    {
        public int[,] GameBoard { get; set; } // 0 - blank, 1 - black, 2 - white
        public int Row { get; set; } // 0-14
        public int Column { get; set; } // 0-14
    }
}
