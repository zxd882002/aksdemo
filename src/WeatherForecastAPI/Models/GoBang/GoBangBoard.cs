using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.AlphaBetaTree;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.KMP;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangBoard : IValueableTreeNodeElement<GoBangBoard>
    {
        public const int BOARD_SIZE = 15;

        public GoBangChess[][] Board { get; set; } = null!;
        public GoBangChess LastChess { get; set; }
        public string BoardHash { get; set; }
        public bool IsEmptyBoard { get; set; }
        public bool IsAllFilledBoard { get; set; }
        public bool IsCriticalDefinitionAnalyzed { get; set; }
        public bool IsNormalDefinitionAnalyzed { get; set; }
        public bool[][] PossibleNextStep { get; set; }

        // after analyze
        public GoBangChessGroupDetailCollection GoBangChessGroupDetailCollection { get; set; } = new GoBangChessGroupDetailCollection();
        public long BlackChessScore => GoBangChessGroupDetailCollection.SumSore(GoBangChessType.BlackChess);
        public long WhiteChessScore => GoBangChessGroupDetailCollection.SumSore(GoBangChessType.WhiteChess);

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
            IsCriticalDefinitionAnalyzed = false;
            IsNormalDefinitionAnalyzed = false;
        }
        public GoBangChess this[GoBangChessPosition position] => Board[position.Row][position.Column];

        public List<Task> AnalyzeCriticalDefinitions()
        {
            GoBangChessGroupDefinition[] criticalDefinitions = LastChess.ChessType == GoBangChessType.BlackChess
                ? GoBangChessGroupDefinitionCollection.AllCriticalBlack
                : GoBangChessGroupDefinitionCollection.AllCriticalWhite;

            List<Task> tasks = new List<Task>();
            foreach (var criticalDefinition in criticalDefinitions)
            {
                var analyzeDefinitionTask = AnalyzeDefinition(criticalDefinition);
                tasks.Add(analyzeDefinitionTask);
            }

            IsCriticalDefinitionAnalyzed = true;
            return tasks;
        }

        public List<Task> AnalyzeNormalDefinitions()
        {
            GoBangChessGroupDefinition[] normalDefinitions1 = LastChess.ChessType == GoBangChessType.BlackChess
               ? GoBangChessGroupDefinitionCollection.AllNormalBlack
               : GoBangChessGroupDefinitionCollection.AllNormalWhite;

            GoBangChessGroupDefinition[] normalDefinitions2 = LastChess.ChessType == GoBangChessType.BlackChess
                ? GoBangChessGroupDefinitionCollection.AllWhite
                : GoBangChessGroupDefinitionCollection.AllBlack;

            List<GoBangChessGroupDefinition> normalDefinitions = new List<GoBangChessGroupDefinition>();
            normalDefinitions.AddRange(normalDefinitions1);
            normalDefinitions.AddRange(normalDefinitions2);

            List<Task> tasks = new List<Task>();
            foreach (var normalDefinition in normalDefinitions)
            {
                var analyzeDefinitionTask = AnalyzeDefinition(normalDefinition);
                tasks.Add(analyzeDefinitionTask);
            }

            IsNormalDefinitionAnalyzed = true;
            return tasks;
        }

        public async Task AnalyzeAllDefinitions()
        {
            List<Task> tasks1 = AnalyzeCriticalDefinitions();
            List<Task> tasks2 = AnalyzeNormalDefinitions();
            tasks1.AddRange(tasks2);
            await Task.WhenAll(tasks1);
        }

        private async Task AnalyzeDefinition(GoBangChessGroupDefinition definition)
        {
            Task<List<GoBangChessGroupDetail>> line1 = AnalyzeLine(@"-", definition);
            Task<List<GoBangChessGroupDetail>> line2 = AnalyzeLine(@"|", definition);
            Task<List<GoBangChessGroupDetail>> line3 = AnalyzeLine(@"/", definition);
            Task<List<GoBangChessGroupDetail>> line4 = AnalyzeLine(@"\", definition);

            await Task.WhenAll(line1, line2, line3, line4);

            GoBangChessGroupDetailCollection.AddRange(line1.Result);
            GoBangChessGroupDetailCollection.AddRange(line2.Result);
            GoBangChessGroupDetailCollection.AddRange(line3.Result);
            GoBangChessGroupDetailCollection.AddRange(line4.Result);
        }

        private async Task<List<GoBangChessGroupDetail>> AnalyzeLine(string direction, GoBangChessGroupDefinition definition)
        {
            KmpSearcher<GoBangChessType> searcher = new KmpSearcher<GoBangChessType>();
            ConcurrentBag<GoBangChessGroupDetail> details = new ConcurrentBag<GoBangChessGroupDetail>();
            List<GoBangChessPosition> startPositionList = direction switch
            {
                @"-" => ArrayExtensions.CreateInt(0, BOARD_SIZE).Select(x => new GoBangChessPosition { Row = x, Column = 0 }).ToList(),
                @"|" => ArrayExtensions.CreateInt(0, BOARD_SIZE).Select(x => new GoBangChessPosition { Row = 0, Column = x }).ToList(),
                @"/" => ArrayExtensions.CreateInt(0, BOARD_SIZE).Select(x => new GoBangChessPosition { Row = x, Column = 0 }).Union(ArrayExtensions.CreateInt(1, BOARD_SIZE).Select(x => new GoBangChessPosition { Row = BOARD_SIZE - 1, Column = x })).ToList(),
                @"\" => ArrayExtensions.CreateInt(0, BOARD_SIZE).Select(x => new GoBangChessPosition { Row = 0, Column = x }).Union(ArrayExtensions.CreateInt(1, BOARD_SIZE).Select(x => new GoBangChessPosition { Row = x, Column = BOARD_SIZE - 1 })).ToList(),
                _ => throw new NotSupportedException()
            };

            List<Task> getChessDetailTaskList = new List<Task>();
            foreach (var startPosition in startPositionList)
            {
                Task getChessDetailTask = Task.Run(() =>
                {
                    List<GoBangChess> lineChesses = new List<GoBangChess>();
                    List<GoBangChessType> lineChessTypees = new List<GoBangChessType>();
                    GoBangChess currentChess = this[startPosition];
                    while (currentChess.ChessType != GoBangChessType.Wall)
                    {
                        lineChesses.Add(currentChess);
                        lineChessTypees.Add(currentChess.ChessType);
                        currentChess = currentChess.PositiveMove(this, direction);
                    }

                    List<int> matchIndexList = searcher.Search(lineChessTypees, definition.Pattern);
                    foreach (var detail in matchIndexList.Select(i => new GoBangChessGroupDetail(definition, lineChesses[i], direction)))
                    {
                        details.Add(detail);
                    }
                });

                getChessDetailTaskList.Add(getChessDetailTask);

            }

            await Task.WhenAll(getChessDetailTaskList);
            return details.ToList();
        }

        public long GetValue()
        {
            throw new NotImplementedException();
        }

        public List<GoBangBoard> GetChildElements()
        {
            throw new NotImplementedException();
        }
    }
}
