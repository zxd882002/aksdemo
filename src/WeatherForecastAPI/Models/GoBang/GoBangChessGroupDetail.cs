using System.Collections.Generic;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDetail
    {
        public GoBangChessGroupDefinition GoBangChessGroupDefinition { get; }
        public GoBangChess StartChess { get; }
        public string Direction { get; }
        private List<GoBangChess> chesses;

        public GoBangChessGroupDetail(GoBangChessGroupDefinition goBangChessGroupDefinition, GoBangChess startChesses, string direction)
        {
            GoBangChessGroupDefinition = goBangChessGroupDefinition;
            StartChess = startChesses;
            Direction = direction;
            chesses = null;
        }

        public List<GoBangChess> GetDefinitionChesses(GoBangBoard board)
        {
            if (chesses == null)
            {
                int chessCount = GoBangChessGroupDefinition.Pattern.Count;
                chesses = new List<GoBangChess>(chessCount);
                GoBangChess currentChess = StartChess;
                for (int i = 0; i < chessCount; i++)
                {
                    chesses.Add(currentChess);
                    currentChess = currentChess.PositiveMove(board, Direction);
                    if (currentChess.ChessType == GoBangChessType.Wall)
                        break;
                }
            }

            return chesses;
        }
    }
}
