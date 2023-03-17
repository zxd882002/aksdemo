using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.KMP;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangAnalyzer
    {
        long BlackChessScore { get; }
        long WhiteChessScore { get; }
        bool BlackChessWin { get; }
        bool WhiteChessWin { get; }
        Task AnalyzeAllDefinitions(GoBangBoard board);
        List<Task> AnalyzeBlackWinDefinitions(GoBangBoard board);
        List<Task> AnalyzeWhiteWinDefinitions(GoBangBoard board);
        IGoBangAnalyzer Reset();
    }

    public class GoBangAnalyzer : IGoBangAnalyzer
    {
        public bool IsBackWinDefinitionAnalyzed { get; set; }
        public bool IsBackCriticalDefinitionAnalyzed { get; set; }
        public bool IsBackNormalDefinitionAnalyzed { get; set; }
        public bool IsWhiteWinDefinitionAnalyzed { get; set; }
        public bool IsWhiteCriticalDefinitionAnalyzed { get; set; }
        public bool IsWhiteNormalDefinitionAnalyzed { get; set; }
        public GoBangChessGroupDetailCollection _goBangChessGroupDetailCollection = new GoBangChessGroupDetailCollection();

        public List<Task> AnalyzeBlackWinDefinitions(GoBangBoard board)
        {
            if (!IsBackWinDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(board, GoBangChessGroupDefinitionCollection.AllWinBlack);
                IsBackWinDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        public List<Task> AnalyzeWhiteWinDefinitions(GoBangBoard board)
        {
            if (!IsWhiteWinDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(board, GoBangChessGroupDefinitionCollection.AllWinWhite);
                IsWhiteWinDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        public List<Task> AnalyzeBlackCriticalDefinitions(GoBangBoard board)
        {
            if (!IsBackCriticalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(board, GoBangChessGroupDefinitionCollection.AllCriticalBlack);
                IsBackCriticalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        public List<Task> AnalyzeWhiteCriticalDefinitions(GoBangBoard board)
        {
            if (!IsWhiteCriticalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(board, GoBangChessGroupDefinitionCollection.AllCriticalWhite);
                IsWhiteCriticalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        public List<Task> AnalyzeBlackNormalDefinitions(GoBangBoard board)
        {
            if (!IsBackNormalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(board, GoBangChessGroupDefinitionCollection.AllNormalBlack);
                IsBackNormalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        public List<Task> AnalyzeWhiteNormalDefinitions(GoBangBoard board)
        {
            if (!IsWhiteNormalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(board, GoBangChessGroupDefinitionCollection.AllNormalWhite);
                IsWhiteNormalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        public async Task AnalyzeAllDefinitions(GoBangBoard board)
        {
            List<Task> tasks1 = AnalyzeWhiteWinDefinitions(board);
            List<Task> tasks2 = AnalyzeWhiteCriticalDefinitions(board);
            List<Task> tasks3 = AnalyzeWhiteNormalDefinitions(board);
            List<Task> tasks4 = AnalyzeBlackWinDefinitions(board);
            List<Task> tasks5 = AnalyzeBlackCriticalDefinitions(board);
            List<Task> tasks6 = AnalyzeBlackNormalDefinitions(board);
            tasks1.AddRange(tasks2);
            tasks1.AddRange(tasks3);
            tasks1.AddRange(tasks4);
            tasks1.AddRange(tasks5);
            tasks1.AddRange(tasks6);
            await Task.WhenAll(tasks1);
        }

        public IGoBangAnalyzer Reset()
        {
            IsBackWinDefinitionAnalyzed = false;
            IsBackCriticalDefinitionAnalyzed = false;
            IsBackNormalDefinitionAnalyzed = false;
            IsWhiteWinDefinitionAnalyzed = false;
            IsWhiteCriticalDefinitionAnalyzed = false;
            IsWhiteNormalDefinitionAnalyzed = false;
            _goBangChessGroupDetailCollection.Clear();
            return this;
        }

        public long BlackChessScore => _goBangChessGroupDetailCollection.SumSore(GoBangChessType.BlackChess);
        public long WhiteChessScore => _goBangChessGroupDetailCollection.SumSore(GoBangChessType.WhiteChess);
        public bool BlackChessWin => _goBangChessGroupDetailCollection.ContainsAlreadyWinDefinition(GoBangChessType.BlackChess);
        public bool WhiteChessWin => _goBangChessGroupDetailCollection.ContainsAlreadyWinDefinition(GoBangChessType.WhiteChess);

        private async Task AnalyzeDefinition(GoBangBoard board, GoBangChessGroupDefinition definition)
        {
            Task<List<GoBangChessGroupDetail>> line1 = AnalyzeLine(board, @"-", definition);
            Task<List<GoBangChessGroupDetail>> line2 = AnalyzeLine(board, @"|", definition);
            Task<List<GoBangChessGroupDetail>> line3 = AnalyzeLine(board, @"/", definition);
            Task<List<GoBangChessGroupDetail>> line4 = AnalyzeLine(board, @"\", definition);

            await Task.WhenAll(line1, line2, line3, line4);

            _goBangChessGroupDetailCollection.AddRange(line1.Result);
            _goBangChessGroupDetailCollection.AddRange(line2.Result);
            _goBangChessGroupDetailCollection.AddRange(line3.Result);
            _goBangChessGroupDetailCollection.AddRange(line4.Result);
        }

        private async Task<List<GoBangChessGroupDetail>> AnalyzeLine(GoBangBoard board, string direction, GoBangChessGroupDefinition definition)
        {
            KmpSearcher<GoBangChessType> searcher = new KmpSearcher<GoBangChessType>();
            ConcurrentBag<GoBangChessGroupDetail> details = new ConcurrentBag<GoBangChessGroupDetail>();
            List<GoBangChessPosition> startPositionList = direction switch
            {
                @"-" => ArrayExtensions.CreateInt(0, GoBangBoard.BOARD_SIZE).Select(x => new GoBangChessPosition { Row = x, Column = 0 }).ToList(),
                @"|" => ArrayExtensions.CreateInt(0, GoBangBoard.BOARD_SIZE).Select(x => new GoBangChessPosition { Row = 0, Column = x }).ToList(),
                @"/" => ArrayExtensions.CreateInt(0, GoBangBoard.BOARD_SIZE).Select(x => new GoBangChessPosition { Row = x, Column = 0 })
                 .Union(ArrayExtensions.CreateInt(1, GoBangBoard.BOARD_SIZE).Select(x => new GoBangChessPosition { Row = GoBangBoard.BOARD_SIZE - 1, Column = x })).ToList(),
                @"\" => ArrayExtensions.CreateInt(0, GoBangBoard.BOARD_SIZE).Select(x => new GoBangChessPosition { Row = 0, Column = x })
                 .Union(ArrayExtensions.CreateInt(1, GoBangBoard.BOARD_SIZE).Select(x => new GoBangChessPosition { Row = x, Column = GoBangBoard.BOARD_SIZE - 1 })).ToList(),
                _ => throw new NotSupportedException()
            };

            List<Task> getChessDetailTaskList = new List<Task>();
            foreach (var startPosition in startPositionList)
            {
                Task getChessDetailTask = Task.Run(() =>
                {
                    if (startPosition.Row == 0 && startPosition.Column == 0 && direction == "\\" && definition.DefinitionName == "BBAABB")
                    {
                        // get all chesses in current line
                        List<GoBangChess> lineChesses = new List<GoBangChess>();
                        List<GoBangChessType> lineChessTypees = new List<GoBangChessType>();
                        GoBangChess currentChess = board[startPosition];
                        while (currentChess.ChessType != GoBangChessType.Wall)
                        {
                            lineChesses.Add(currentChess);
                            lineChessTypees.Add(currentChess.ChessType);
                            currentChess = currentChess.PositiveMove(board, direction);
                        }

                        // kmp search whether there is chesses matched the definition
                        List<int> matchIndexList = searcher.Search(lineChessTypees, definition.Pattern);
                        foreach (var detail in matchIndexList.Select(i => new GoBangChessGroupDetail(definition, lineChesses[i], direction)))
                        {
                            details.Add(detail);
                        }
                    }
                    else
                    {
                        // get all chesses in current line
                        List<GoBangChess> lineChesses = new List<GoBangChess>();
                        List<GoBangChessType> lineChessTypees = new List<GoBangChessType>();
                        GoBangChess currentChess = board[startPosition];
                        while (currentChess.ChessType != GoBangChessType.Wall)
                        {
                            lineChesses.Add(currentChess);
                            lineChessTypees.Add(currentChess.ChessType);
                            currentChess = currentChess.PositiveMove(board, direction);
                        }

                        // kmp search whether there is chesses matched the definition
                        List<int> matchIndexList = searcher.Search(lineChessTypees, definition.Pattern);
                        foreach (var detail in matchIndexList.Select(i => new GoBangChessGroupDetail(definition, lineChesses[i], direction)))
                        {
                            details.Add(detail);
                        }
                    }
                });

                getChessDetailTaskList.Add(getChessDetailTask);

            }

            await Task.WhenAll(getChessDetailTaskList);
            return details.ToList();
        }

        private List<Task> GetAnalyzeDefinitionTask(GoBangBoard board, GoBangChessGroupDefinition[] definitions)
        {
            List<Task> tasks = new List<Task>();
            foreach (var definition in definitions)
            {
                var analyzeDefinitionTask = AnalyzeDefinition(board, definition);
                tasks.Add(analyzeDefinitionTask);
            }
            return tasks;
        }
    }
}
