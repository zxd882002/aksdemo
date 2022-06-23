using System;

namespace WeatherForecastAPI.Models.GoBang
{
    public struct GoBangChess
    {
        public GoBangChessType Chess { get; set; }
        public GoBangChessPosition Position { get; set; }

        public GoBangChess PositiveMove(GoBangBoard board, string direction, int step = 1)
        {
            GoBangChessPosition newPosition = Position.PositiveMove(direction, step);
            GoBangChessType Chess = GetChessType(newPosition, board);
            return new GoBangChess
            {
                Chess = Chess,
                Position = newPosition
            };
        }

        public GoBangChess NegativeMove(GoBangBoard board, string direction, int step = 1)
        {
            GoBangChessPosition newPosition = Position.NegativeMove(direction, step);
            GoBangChessType Chess = GetChessType(newPosition, board);
            return new GoBangChess
            {
                Chess = Chess,
                Position = newPosition
            };
        }

        private GoBangChessType GetChessType(GoBangChessPosition position, GoBangBoard board)
        {
            if (position.IsInsideBoundage())
            {
                return board.Board[position.Row, position.Column];
            }
            else
            {
                return GoBangChessType.Wall;
            }
        }
    }
}
