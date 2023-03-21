using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.AlphaBetaTree;

namespace WeatherForecastAPI.Models.GoBang
{
    public class ValuedGoBangBoard : IValueableTreeNodeElement<ValuedGoBangBoard>
    {
        public GoBangBoard Board { get; }
        public IGoBangBoardFactory Factory { get; }
        public IGoBangAnalyzer Analyzer { get; }

        public long Value { get; private set; }

        public ValuedGoBangBoard(GoBangBoard board, IGoBangBoardFactory factory, IGoBangAnalyzer analyzer)
        {
            Board = board;
            Factory = factory;
            Analyzer = analyzer;
        }

        public async Task<List<ValuedGoBangBoard>> GetChildElements()
        {
            List<GoBangChessPosition> nextStepPositions = await GetNextStepPosition();

            ConcurrentBag<ValuedGoBangBoard> list = new ConcurrentBag<ValuedGoBangBoard>();
            await Parallel.ForEachAsync(nextStepPositions, async (p, t) =>
            {
                var newBoard = Factory.PutChess(Board.Board, new GoBangChess(Board.LastChess.ChessType.ReverseChess(), p.Row, p.Column));
                var newValuedBoard = new ValuedGoBangBoard(newBoard, Factory, new GoBangAnalyzer(newBoard));
                await newValuedBoard.CalculateValue();
                list.Add(newValuedBoard);
            });

            return list.ToList();
        }

        public GoBangChessPosition GetPosition()
        {
            return Board.LastChess.Position;
        }

        private async Task CalculateValue()
        {
            var valueFromCache = GlobalCache.GetValue(Board.BoardHash);

            if (valueFromCache == null)
            {
                var blackScore = await Analyzer.GetBlackChessScore();
                var whiteScore = await Analyzer.GetWhiteChessScore();

                Value = blackScore - whiteScore;
                GlobalCache.SetValue(Board.BoardHash, Value);
            }
            else
            {
                Value = valueFromCache.Value;
            }
        }

        private async Task<List<GoBangChessPosition>> GetNextStepPosition()
        {
            var winnerChess = await Analyzer.GetWinnerChessType();
            if (winnerChess != null)
            {
                return new List<GoBangChessPosition>();
            }
            var positions = await Analyzer.GetMustFollowChessPositions();

            if (positions.Any())
            {
                return positions;
            }

            return GetPossiblePositions();
        }

        private List<GoBangChessPosition> GetPossiblePositions()
        {
            List<GoBangChessPosition> positions = new List<GoBangChessPosition>();

            for (int i = 0; i < GoBangBoard.BOARD_SIZE; i++)
            {
                for (int j = 0; j < GoBangBoard.BOARD_SIZE; j++)
                {
                    if ((i > 0 && Board[i - 1, j].ChessType != GoBangChessType.Blank) ||  // 上
                        (i < GoBangBoard.BOARD_SIZE - 1 && Board[i + 1, j].ChessType != GoBangChessType.Blank) || // 下
                        (j > 0 && Board[i, j - 1].ChessType != GoBangChessType.Blank) || // 左
                        (j < GoBangBoard.BOARD_SIZE - 1 && Board[i, j + 1].ChessType != GoBangChessType.Blank) || // 右
                        (i > 0 && j > 0 && Board[i - 1, j - 1].ChessType != GoBangChessType.Blank) || // 左上
                        (i > 0 && j < GoBangBoard.BOARD_SIZE - 1 && Board[i - 1, j + 1].ChessType != GoBangChessType.Blank) || // 右上
                        (i < GoBangBoard.BOARD_SIZE - 1 && j > 0 && Board[i + 1, j - 1].ChessType != GoBangChessType.Blank) || // 左下
                        (i < GoBangBoard.BOARD_SIZE - 1 && j < GoBangBoard.BOARD_SIZE - 1 && Board[i + 1, j + 1].ChessType != GoBangChessType.Blank)) // 右下
                    {
                        if (Board[i, j].ChessType == GoBangChessType.Blank)
                        {
                            positions.Add(new GoBangChessPosition { Row = i, Column = j });
                        }
                    }
                }
            }

            return positions;
        }
    }
}
