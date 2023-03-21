namespace WeatherForecastAPI.Models.GoBang.GoBangRequest
{
    public class GetNextStepPointRequest
    {
        public int[][] GameBoard { get; set; } = null!; // 0 - blank, 1 - black, 2 - white
        public int Row { get; set; } // 0-14
        public int Column { get; set; } // 0-14
        public int LastChessType { get; set; }
        public int Deep { get; set; } // 2, 4, 6
    }
}
