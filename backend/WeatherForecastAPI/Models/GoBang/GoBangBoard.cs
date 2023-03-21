using System.Text;
using WeatherForecastAPI.Models.GoBang.GoBangRequest;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangBoard
    {
        public const int BOARD_SIZE = 15;

        public GoBangChess[][] Board { get; set; } = null!;
        public GoBangChess LastChess { get; set; }
        public string BoardHash { get; set; }
        public bool IsAllFilledBoard { get; set; }
        public bool IsEmptyBoard
        {
            get; set;
        }

        public GoBangBoard(GoBangChess[][] board, GoBangChess lastChess)
        {
            Board = board;
            LastChess = lastChess;

            StringBuilder boardHashStringBuilder = new StringBuilder(BOARD_SIZE * BOARD_SIZE);
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (board[i][j].ChessType == GoBangChessType.Blank) IsAllFilledBoard = false;
                    else IsEmptyBoard = false;
                    boardHashStringBuilder.Append(board[i][j].ChessType.Value);
                }
            }
            BoardHash = boardHashStringBuilder.ToString();
        }

        public GoBangChess this[int row, int column] => Board[row][column];
        public GoBangChess this[GoBangChessPosition position] => Board[position.Row][position.Column];
    }
}
