namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDetail
    {
        public GoBangChessGroupDefinition GoBangChessGroupDefinition { get; }
        public GoBangChess StartChess { get; }
        public string Direction { get; }

        public GoBangChessGroupDetail(GoBangChessGroupDefinition goBangChessGroupDefinition, GoBangChess startChesses, string direction)
        {
            GoBangChessGroupDefinition = goBangChessGroupDefinition;
            StartChess = startChesses;
            Direction = direction;
        }
    }
}
