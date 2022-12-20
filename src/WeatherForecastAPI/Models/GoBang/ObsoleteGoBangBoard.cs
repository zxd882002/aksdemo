using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.AlphaBetaTree;

namespace WeatherForecastAPI.Models.GoBang
{
    [Obsolete]
    public class ObsoleteGoBangBoard : IValueableTreeNodeElement<ObsoleteGoBangBoard>
    {
        public const int BOARD_SIZE = 15;

        private IGoBangBoardFactory _factory;
        public GoBangChessType[][] Board { get; set; } = null!;

        public GoBangChess LastChess { get; set; }
        public bool[][] PossibleNextStep { get; set; }
        public ObsoleteGoBangChessGroupDetailCollection GoBangChessGroupDetailCollection { get; set; } = new ObsoleteGoBangChessGroupDetailCollection();

        public ObsoleteGoBangBoard(GoBangChessType[][] board, IGoBangBoardFactory factory)
        {
            Board = new GoBangChessType[BOARD_SIZE][];
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                Board[i] = new GoBangChessType[BOARD_SIZE];
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Board[i][j] = board[i][j];
                }
            }
            _factory = factory;

            PossibleNextStep = new bool[BOARD_SIZE][];
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                PossibleNextStep[i] = new bool[BOARD_SIZE];
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    PossibleNextStep[i][j] = false;
                }
            }
            LastChess = new GoBangChess();
            GoBangChessGroupDetailCollection = new ObsoleteGoBangChessGroupDetailCollection();
        }

        public ObsoleteGoBangBoard Copy()
        {
            GoBangChessType[][] chessCopy = new GoBangChessType[BOARD_SIZE][];
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                chessCopy[i] = new GoBangChessType[BOARD_SIZE];
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    chessCopy[i][j] = Board[i][j];
                }
            }

            ObsoleteGoBangBoard board = new ObsoleteGoBangBoard(chessCopy, _factory);
            board.PossibleNextStep = new bool[BOARD_SIZE][];
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                board.PossibleNextStep[i] = new bool[BOARD_SIZE];
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    board.PossibleNextStep[i][j] = PossibleNextStep[i][j];
                }
            }
            board.LastChess = LastChess;
            board.GoBangChessGroupDetailCollection = GoBangChessGroupDetailCollection.Copy();
            return board;
        }

        public long BlackChessScore => GoBangChessGroupDetailCollection.SumSore(GoBangChessType.BlackChess);
        public long WhiteChessScore => GoBangChessGroupDetailCollection.SumSore(GoBangChessType.WhiteChess);

        public ObsoleteGoBangBoard GetBoardInfoAfterPuttingChess(GoBangChess currentChess)
        {
            ObsoleteGoBangBoard board = Copy();

            board.Board[currentChess.Position.Row][currentChess.Position.Column] = currentChess.ChessType;
            board.LastChess = currentChess;
            board.UpdatePossibleNextStep(board.LastChess.Position);

            board.GoBangChessGroupDetailCollection.Remove(currentChess);
            List<ObsoleteGoBangChessGroupDetail> matchedPositions = new List<ObsoleteGoBangChessGroupDetail>();
            matchedPositions.AddRange(board.GetMatchLine(currentChess, @"-"));
            matchedPositions.AddRange(board.GetMatchLine(currentChess, @"|"));
            matchedPositions.AddRange(board.GetMatchLine(currentChess, @"\"));
            matchedPositions.AddRange(board.GetMatchLine(currentChess, @"/"));
            matchedPositions.ForEach(matchedPosition => board.GoBangChessGroupDetailCollection.Add(matchedPosition));

            return board;
        }

        private void UpdatePossibleNextStep(GoBangChessPosition goBangChessPosition)
        {
            int row = goBangChessPosition.Row;
            int column = goBangChessPosition.Column;

            int fromRow = row <= 1 ? 0 : row - 1;
            int toRow = BOARD_SIZE - 2 <= row ? BOARD_SIZE - 1 : row + 1;
            int fromColumn = column <= 1 ? 0 : column - 1;
            int toColumn = BOARD_SIZE - 2 <= column ? BOARD_SIZE - 1 : column + 1;
            for (int i = fromRow; i <= toRow; i++)
            {
                for (int j = fromColumn; j <= toColumn; j++)
                {
                    PossibleNextStep[i][j] = true;
                }
            }
        }

        private void UpdatePossibleNextStep2(GoBangChessPosition goBangChessPosition)
        {
            int row = goBangChessPosition.Row;
            int column = goBangChessPosition.Column;

            int fromRow = row <= 2 ? 0 : row - 2;
            int toRow = BOARD_SIZE - 3 <= row ? BOARD_SIZE - 1 : row + 2;
            int fromColumn = column <= 2 ? 0 : column - 2;
            int toColumn = BOARD_SIZE - 3 <= column ? BOARD_SIZE - 1 : column + 2;
            for (int i = fromRow; i <= toRow; i++)
            {
                for (int j = fromColumn; j <= toColumn; j++)
                {
                    PossibleNextStep[i][j] = true;
                }
            }

            bool hasTop = 3 <= row;
            bool hasBottom = row <= BOARD_SIZE - 4;
            bool hasLeft = 3 <= column;
            bool hasRight = column <= BOARD_SIZE - 4;
            if (hasTop && hasLeft)
            {
                PossibleNextStep[row - 3][column - 3] = true;
            }
            if (hasLeft)
            {
                PossibleNextStep[row][column - 3] = true;
            }
            if (hasBottom && hasLeft)
            {
                PossibleNextStep[row + 3][column - 3] = true;
            }
            if (hasTop)
            {
                PossibleNextStep[row - 3][column] = true;
            }
            if (hasBottom)
            {
                PossibleNextStep[row + 3][column] = true;
            }
            if (hasTop && hasRight)
            {
                PossibleNextStep[row - 3][column + 3] = true;
            }
            if (hasRight)
            {
                PossibleNextStep[row][column + 3] = true;
            }
            if (hasBottom && hasRight)
            {
                PossibleNextStep[row + 3][column + 3] = true;
            }
        }

        /// <summary>
        /// 获取这个方向上的所有的GoBangChessGroupDetail
        /// </summary>
        private List<ObsoleteGoBangChessGroupDetail> GetMatchLine(GoBangChess currentChess, string direction)
        {
            GoBangChess fromChess = currentChess;
            List<ObsoleteGoBangChessGroupDetail> matchedLines = new List<ObsoleteGoBangChessGroupDetail>();
            bool couldContinue = true;
            do
            {
                GoBangChess nextChess = GetNextChess(fromChess, direction);
                if (nextChess.ChessType != GoBangChessType.Wall)
                {
                    couldContinue = false;
                    GoBangChessGroupDefinition[] definitions;

                    if (nextChess.ChessType == GoBangChessType.BlackChess)
                    {
                        definitions = GoBangChessGroupDefinitionCollection.AllBlack;
                    }
                    else
                    {
                        definitions = GoBangChessGroupDefinitionCollection.AllWhite;
                    }

                    foreach (GoBangChessGroupDefinition definition in definitions)
                    {
                        int chessCount = definition.Pattern.Count;
                        List<GoBangChess> goBangChesses = GetChesses(fromChess, direction, chessCount);
                        if (ContainsCurrentChess(goBangChesses, currentChess))
                        {
                            couldContinue = true;

                            if (definition.IsMatch(goBangChesses))
                            {
                                matchedLines.Add(new ObsoleteGoBangChessGroupDetail(definition, goBangChesses, direction));
                            }
                        }
                    }
                }

                if (fromChess.ChessType == GoBangChessType.Wall)
                {
                    break;
                }

                fromChess = fromChess.NegativeMove(this, direction);
            }
            while (couldContinue);

            return matchedLines;
        }

        /// <summary>
        /// 获取当前方向第二个棋子的类型
        /// </summary>
        /// <returns>BlackChess ,WhiteChess,  Wall</returns>
        private GoBangChess GetNextChess(GoBangChess currentChess, string direction)
        {
            while (true)
            {
                currentChess = currentChess.PositiveMove(this, direction);
                if (currentChess.ChessType != GoBangChessType.Blank)
                {
                    return currentChess;
                }
            }
        }

        /// <summary>
        /// 获取当前方向上的所有棋子
        /// </summary>
        private List<GoBangChess> GetChesses(GoBangChess fromChess, string direction, int chessCount)
        {
            List<GoBangChess> chessList = new List<GoBangChess>(new[] { fromChess });
            GoBangChess currentChess = fromChess;
            for (int i = 1; i < chessCount; i++)
            {
                currentChess = currentChess.PositiveMove(this, direction);
                chessList.Add(currentChess);

                if (currentChess.ChessType == GoBangChessType.Wall)
                {
                    break;
                }
            }
            return chessList;
        }

        /// <summary>
        /// 检查chess里有没有current chess
        /// </summary>
        private bool ContainsCurrentChess(List<GoBangChess> chesses, GoBangChess currentChess)
        {
            return chesses.Any(c => c.Position.Row == currentChess.Position.Row && c.Position.Column == currentChess.Position.Column);
        }

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{LastChess.Position.Row:00}{LastChess.Position.Column:00}{LastChess.ChessType.Value}");
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    sb.Append(PossibleNextStep[i][j] ? "1" : "0");
                }
            }
            sb.Append(GoBangChessGroupDetailCollection.ConvertToString());
            return sb.ToString();
        }

        public void Deserialize(string deserializeString)
        {
            int row = 0;
            int column = 0;
            int chessType = 0;
            for (int i = 0; i < 5; i++)
            {
                if (0 <= i && i <= 1)
                {
                    row = row * 10 + (deserializeString[i] - '0');
                }
                if (2 <= i && i <= 3)
                {
                    column = column * 10 + (deserializeString[i] - '0');
                }
                if (i == 4)
                {
                    chessType = deserializeString[i] - '0';
                }
            }
            LastChess = new GoBangChess
            {
                Position = new GoBangChessPosition
                {
                    Row = row,
                    Column = column,
                },
                ChessType = GoBangChessType.Parse(chessType),
            };

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    PossibleNextStep[i][j] = deserializeString[5 + i * BOARD_SIZE + j] == '1' ? true : false;
                }
            }

            string groupDetailCollectionString = deserializeString.Substring(1 + BOARD_SIZE * BOARD_SIZE);
            GoBangChessGroupDetailCollection.ConvertFromString(groupDetailCollectionString);
        }

        public string GetBoardHash()
        {
            StringBuilder sb = new StringBuilder(BOARD_SIZE * BOARD_SIZE);
            foreach (var row in Board)
            {
                foreach (var chess in row)
                {
                    sb.Append(chess.Value);
                }
            }
            return sb.ToString();
        }

        public bool IsEmptyBoard()
        {
            foreach (GoBangChessType[] row in Board)
            {
                foreach (GoBangChessType element in row)
                {
                    if (element == GoBangChessType.WhiteChess || element == GoBangChessType.BlackChess)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsAllFilled()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    if (Board[i][j] == GoBangChessType.Blank)
                        return false;
                }
            }
            return true;
        }

        private long? _value = null;
        public long GetValue()
        {
            if (_value == null)
            {
                //GoBangChessGroupDetailCollection.Remove(LastChess);
                //List<GoBangChessGroupDetail> matchedPositions = new List<GoBangChessGroupDetail>();
                //matchedPositions.AddRange(GetMatchLine(LastChess, @"-"));
                //matchedPositions.AddRange(GetMatchLine(LastChess, @"|"));
                //matchedPositions.AddRange(GetMatchLine(LastChess, @"\"));
                //matchedPositions.AddRange(GetMatchLine(LastChess, @"/"));
                //matchedPositions.ForEach(matchedPosition => GoBangChessGroupDetailCollection.Add(matchedPosition));

                _value = LastChess.ChessType.Value switch
                {
                    1 => BlackChessScore - WhiteChessScore, // black chess
                    2 => WhiteChessScore - BlackChessScore, // white chess
                    _ => throw new NotSupportedException()
                };
            }
            return _value.Value;
        }

        public async Task<List<ObsoleteGoBangBoard>> GetChildElements()
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Debug.WriteLine("GetChildElement start");
            List<GoBangChessPosition> nextStepPositions = GetNextStepPosition();
            //Debug.WriteLine($"Get next step positions ended, ellapse = {sw.ElapsedMilliseconds}");

            ConcurrentBag<ObsoleteGoBangBoard> goBangBoards = new ConcurrentBag<ObsoleteGoBangBoard>();
            Parallel.ForEach(nextStepPositions, (nextStepPosition) =>
            {
                GoBangChessType[][] boardCopy = new GoBangChessType[BOARD_SIZE][];
                for (int i = 0; i < BOARD_SIZE; i++)
                {
                    boardCopy[i] = new GoBangChessType[BOARD_SIZE];
                    for (int j = 0; j < BOARD_SIZE; j++)
                    {
                        if (i == nextStepPosition.Row && j == nextStepPosition.Column)
                        {
                            boardCopy[i][j] = LastChess.ChessType == GoBangChessType.BlackChess ? GoBangChessType.WhiteChess : GoBangChessType.BlackChess;
                        }
                        else
                        {

                            boardCopy[i][j] = Board[i][j];
                        }
                    }
                }
                ObsoleteGoBangBoard nextStepGoBangBoard = _factory.ObseleteGenerateBoard(boardCopy, nextStepPosition.Row, nextStepPosition.Column).Result;
                goBangBoards.Add(nextStepGoBangBoard);
            });

            //Debug.WriteLine($"Add boarded ended, ellapse = {sw.ElapsedMilliseconds}");

            List<ObsoleteGoBangBoard> goBangBoardList = goBangBoards.ToList().OrderByDescending(x => x.GetValue()).ToList();

            //Debug.WriteLine($"GetChildElement, ellapse = {sw.ElapsedMilliseconds}");
            //sw.Stop();
            return goBangBoardList;
        }

        private List<GoBangChessPosition> GetNextStepPosition()
        {
            if (LastChess.ChessType == GoBangChessType.BlackChess)
            {
                // next step is white chess
                if (GoBangChessGroupDetailCollection.BlackWin)
                {
                    return new List<GoBangChessPosition>();
                }

                if (GoBangChessGroupDetailCollection.WhiteMustFollow)
                {
                    return GoBangChessGroupDetailCollection.GetWhiteMustFollowChessPosition();
                }

                return GetPossiblePositions();
            }
            else
            {
                // next step is black chess
                if (GoBangChessGroupDetailCollection.WhiteWin)
                {
                    return new List<GoBangChessPosition>();
                }

                if (GoBangChessGroupDetailCollection.BlackMustFollow)
                {
                    return GoBangChessGroupDetailCollection.GetBlackMustFollowChessPosition();
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
                    if (PossibleNextStep[i][j] == true && Board[i][j] == GoBangChessType.Blank)
                    {
                        positions.Add(new GoBangChessPosition { Row = i, Column = j });
                    }
                }
            }
            return positions;
        }

        Task<long> IValueableTreeNodeElement<ObsoleteGoBangBoard>.GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
