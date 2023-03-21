using System;

namespace WeatherForecastAPI.Models.GoBang
{
    public struct GoBangChess
    {
        public GoBangChessType ChessType { get; set; }
        public GoBangChessPosition Position { get; set; }

        public GoBangChess(GoBangChessType chessType, int row, int column)
        {
            Position = new GoBangChessPosition
            {
                Row = row,
                Column = column
            };
            ChessType = chessType;
        }

        public GoBangChess(int chessType, int row, int column)
        {
            Position = new GoBangChessPosition
            {
                Row = row,
                Column = column
            };
            ChessType = GoBangChessType.Parse(chessType);
        }

        public GoBangChess PositiveMove(GoBangBoard board, string direction, int step = 1)
        {
            GoBangChessPosition newPosition = Position.PositiveMove(direction, step);
            GoBangChessType chessType = GetChessType(newPosition, board);
            return new GoBangChess
            {
                ChessType = chessType,
                Position = newPosition
            };
        }         

        public GoBangChess NegativeMove(GoBangBoard board, string direction, int step = 1)
        {
            GoBangChessPosition newPosition = Position.NegativeMove(direction, step);
            GoBangChessType chessType = GetChessType(newPosition, board);
            return new GoBangChess
            {
                ChessType = chessType,
                Position = newPosition
            };
        }
    
        private GoBangChessType GetChessType(GoBangChessPosition position, GoBangBoard board)
        {
            if (position.IsInsideBoundage())
            {
                return board.Board[position.Row][position.Column].ChessType;
            }
            else
            {
                return GoBangChessType.Wall;
            }
        }
    }
}
