using System.Text;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangBoard // : IValueableTreeNodeElement<GoBangBoard>
    {
        public const int BOARD_SIZE = 15;

        public GoBangChess[][] Board { get; set; } = null!;
        public GoBangChess LastChess { get; set; }

        public string BoardHash { get; set; }
        public bool IsEmptyBoard { get; set; }
        public bool IsAllFilledBoard { get; set; }
        
        public bool[][] PossibleNextStep { get; set; }

        public GoBangBoard(GoBangChess[][] board, GoBangChess lastChess)
        {
            bool isEmptyBoard = true;
            bool isAllFilledBoard = true;
            StringBuilder boardHashStringBuilder = new StringBuilder(BOARD_SIZE * BOARD_SIZE);
            PossibleNextStep = new bool[BOARD_SIZE][];

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                PossibleNextStep[i] = new bool[BOARD_SIZE];
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (board[i][j].ChessType == GoBangChessType.Blank) isAllFilledBoard = false;
                    else isEmptyBoard = false;
                    boardHashStringBuilder.Append(board[i][j].ChessType.Value);

                    if ((i > 0 && board[i - 1][j].ChessType != GoBangChessType.Blank) ||  // 上
                        (i < BOARD_SIZE - 1 && board[i + 1][j].ChessType != GoBangChessType.Blank) || // 下
                        (j > 0 && board[i][j - 1].ChessType != GoBangChessType.Blank) || // 左
                        (j < BOARD_SIZE - 1 && board[i][j + 1].ChessType != GoBangChessType.Blank) || // 右
                        (i > 0 && j > 0 && board[i - 1][j - 1].ChessType != GoBangChessType.Blank) || // 左上
                        (i > 0 && j < BOARD_SIZE - 1 && board[i - 1][j + 1].ChessType != GoBangChessType.Blank) || // 右上
                        (i < BOARD_SIZE - 1 && j > 0 && board[i + 1][j - 1].ChessType != GoBangChessType.Blank) || // 左下
                        (i < BOARD_SIZE - 1 && j < BOARD_SIZE - 1 && board[i + 1][j + 1].ChessType != GoBangChessType.Blank)) // 右下
                    {
                        PossibleNextStep[i][j] = true;
                    }
                }
            }

            Board = board;
            LastChess = lastChess;
            IsEmptyBoard = isEmptyBoard;
            IsAllFilledBoard = isAllFilledBoard;
            BoardHash = boardHashStringBuilder.ToString();
        }
        public GoBangChess this[GoBangChessPosition position] => Board[position.Row][position.Column];


        /*

        public async Task<long> GetValue()
        {
            await AnalyzeAllDefinitions(this);

            if (LastChess.ChessType == GoBangChessType.BlackChess)
            {
                return BlackChessScore - WhiteChessScore;
            }
            else if (LastChess.ChessType == GoBangChessType.WhiteChess)
            {
                return WhiteChessScore - BlackChessScore;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<List<GoBangBoard>> GetChildElements()
        {
            List<GoBangChessPosition> nextStepPositions = GetNextStepPosition();

            throw new NotImplementedException();
        }

        private List<GoBangChessPosition> GetNextStepPosition()
        {
            if (LastChess.ChessType == GoBangChessType.BlackChess)
            {
                // next step is white chess
                if (BlackChessWin)
                {
                    return new List<GoBangChessPosition>();
                }

                if (GoBangChessGroupDetailCollection.ContainsEnemyMustFollow(GoBangChessType.BlackChess))
                {
                    return GoBangChessGroupDetailCollection.GetMustFollowChessPosition(GoBangChessType.BlackChess, this);
                }

                return GetPossiblePositions();
            }
            else
            {
                // next step is black chess
                if (WhiteChessWin)
                {
                    return new List<GoBangChessPosition>();
                }

                if (GoBangChessGroupDetailCollection.ContainsEnemyMustFollow(GoBangChessType.WhiteChess))
                {
                    return GoBangChessGroupDetailCollection.GetMustFollowChessPosition(GoBangChessType.WhiteChess, this);
                }

                return GetPossiblePositions();
            }
        }

        private List<GoBangChessPosition> GetPossiblePositions()
        {
            List<GoBangChessPosition> positions = new List<GoBangChessPosition>();
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (PossibleNextStep[i][j] == true && Board[i][j].ChessType == GoBangChessType.Blank)
                    {
                        positions.Add(new GoBangChessPosition { Row = i, Column = j });
                    }
                }
            }
            return positions;
        }

        */
    }
}
