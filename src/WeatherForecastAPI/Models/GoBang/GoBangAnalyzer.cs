using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.KMP;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangAnalyzer
    {
        Task<long> GetBlackChessScore();
        Task<long> GetWhiteChessScore();
        Task AnalyzeAllChesses();
        Task<GoBangChessType?> GetWinnerChessType();
        Task<List<GoBangChessPosition>> GetMustFollowChessPositions();
    }

    public class GoBangAnalyzer : IGoBangAnalyzer
    {
        private readonly GoBangBoard _board;
        private bool _isBackWinDefinitionAnalyzed;
        private bool _isBackCriticalDefinitionAnalyzed;
        private bool _isBackNormalDefinitionAnalyzed;
        private bool _isWhiteWinDefinitionAnalyzed;
        private bool _isWhiteCriticalDefinitionAnalyzed;
        private bool _isWhiteNormalDefinitionAnalyzed;
        private readonly GoBangChessGroupDetailCollection _goBangChessGroupDetailCollection;

        public GoBangAnalyzer(GoBangBoard board)
        {
            _board = board;
            _isBackWinDefinitionAnalyzed = false;
            _isBackCriticalDefinitionAnalyzed = false;
            _isBackNormalDefinitionAnalyzed = false;
            _isWhiteWinDefinitionAnalyzed = false;
            _isWhiteCriticalDefinitionAnalyzed = false;
            _isWhiteNormalDefinitionAnalyzed = false;
            _goBangChessGroupDetailCollection = new GoBangChessGroupDetailCollection();
        }

        public async Task<long> GetBlackChessScore()
        {
            List<Task> tasks1 = AnalyzeBlackWinDefinitions();
            List<Task> tasks2 = AnalyzeBlackCriticalDefinitions();
            List<Task> tasks3 = AnalyzeBlackNormalDefinitions();
            tasks1.AddRange(tasks2);
            tasks1.AddRange(tasks3);
            await Task.WhenAll(tasks1);
            return _goBangChessGroupDetailCollection.SumSore(GoBangChessType.BlackChess);
        }
        public async Task<long> GetWhiteChessScore()
        {
            List<Task> tasks1 = AnalyzeWhiteWinDefinitions();
            List<Task> tasks2 = AnalyzeWhiteCriticalDefinitions();
            List<Task> tasks3 = AnalyzeWhiteNormalDefinitions();
            tasks1.AddRange(tasks2);
            tasks1.AddRange(tasks3);
            await Task.WhenAll(tasks1);
            return _goBangChessGroupDetailCollection.SumSore(GoBangChessType.WhiteChess);
        }
        public async Task AnalyzeAllChesses()
        {
            List<Task> tasks1 = AnalyzeWhiteWinDefinitions();
            List<Task> tasks2 = AnalyzeWhiteCriticalDefinitions();
            List<Task> tasks3 = AnalyzeWhiteNormalDefinitions();
            List<Task> tasks4 = AnalyzeBlackWinDefinitions();
            List<Task> tasks5 = AnalyzeBlackCriticalDefinitions();
            List<Task> tasks6 = AnalyzeBlackNormalDefinitions();
            tasks1.AddRange(tasks2);
            tasks1.AddRange(tasks3);
            tasks1.AddRange(tasks4);
            tasks1.AddRange(tasks5);
            tasks1.AddRange(tasks6);
            await Task.WhenAll(tasks1);
        }
        public async Task<GoBangChessType?> GetWinnerChessType()
        {
            if (_board.LastChess.ChessType == GoBangChessType.BlackChess)
            {
                await Task.WhenAll(AnalyzeBlackWinDefinitions());
                return _goBangChessGroupDetailCollection.ContainsAlreadyWinDefinition(GoBangChessType.BlackChess) ? GoBangChessType.BlackChess : null;
            }
            else
            {
                await Task.WhenAll(AnalyzeWhiteWinDefinitions());
                return _goBangChessGroupDetailCollection.ContainsAlreadyWinDefinition(GoBangChessType.WhiteChess) ? GoBangChessType.WhiteChess : null;
            }
        }
        public async Task<List<GoBangChessPosition>> GetMustFollowChessPositions()
        {
            if (_board.LastChess.ChessType == GoBangChessType.BlackChess)
            {
                await Task.WhenAll(AnalyzeBlackCriticalDefinitions());
                return _goBangChessGroupDetailCollection.GetMustFollowChessPosition(GoBangChessType.BlackChess, _board);
            }
            else
            {
                await Task.WhenAll(AnalyzeWhiteCriticalDefinitions());
                return _goBangChessGroupDetailCollection.GetMustFollowChessPosition(GoBangChessType.WhiteChess, _board);
            }
        }

        private List<Task> AnalyzeBlackWinDefinitions()
        {
            if (!_isBackWinDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(GoBangChessGroupDefinitionCollection.AllWinBlack);
                _isBackWinDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }
        private List<Task> AnalyzeWhiteWinDefinitions()
        {
            if (!_isWhiteWinDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(GoBangChessGroupDefinitionCollection.AllWinWhite);
                _isWhiteWinDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }
        private List<Task> AnalyzeBlackCriticalDefinitions()
        {
            if (!_isBackCriticalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(GoBangChessGroupDefinitionCollection.AllCriticalBlack);
                _isBackCriticalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }
        private List<Task> AnalyzeWhiteCriticalDefinitions()
        {
            if (!_isWhiteCriticalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(GoBangChessGroupDefinitionCollection.AllCriticalWhite);
                _isWhiteCriticalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }
        private List<Task> AnalyzeBlackNormalDefinitions()
        {
            if (!_isBackNormalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(GoBangChessGroupDefinitionCollection.AllNormalBlack);
                _isBackNormalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }
        private List<Task> AnalyzeWhiteNormalDefinitions()
        {
            if (!_isWhiteNormalDefinitionAnalyzed)
            {
                var tasks = GetAnalyzeDefinitionTask(GoBangChessGroupDefinitionCollection.AllNormalWhite);
                _isWhiteNormalDefinitionAnalyzed = true;
                return tasks;
            }

            return new List<Task>();
        }

        private List<Task> GetAnalyzeDefinitionTask(GoBangChessGroupDefinition[] definitions)
        {
            List<Task> tasks = new List<Task>();
            foreach (var definition in definitions)
            {
                tasks.AddRange(AnalyzeLine(@"-", definition));
                tasks.AddRange(AnalyzeLine(@"|", definition));
                tasks.AddRange(AnalyzeLine(@"/", definition));
                tasks.AddRange(AnalyzeLine(@"\", definition));
            }
            return tasks;
        }
        private List<Task> AnalyzeLine(string direction, GoBangChessGroupDefinition definition)
        {
            KmpSearcher<GoBangChessType> searcher = new KmpSearcher<GoBangChessType>();
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
                    // get all chesses in current line
                    List<GoBangChess> lineChesses = new List<GoBangChess>();
                    List<GoBangChessType> lineChessTypees = new List<GoBangChessType>();
                    GoBangChess currentChess = _board[startPosition];
                    while (currentChess.ChessType != GoBangChessType.Wall)
                    {
                        lineChesses.Add(currentChess);
                        lineChessTypees.Add(currentChess.ChessType);
                        currentChess = currentChess.PositiveMove(_board, direction);
                    }

                    // kmp search whether there is chesses matched the definition
                    List<int> matchIndexList = searcher.Search(lineChessTypees, definition.Pattern);
                    foreach (var detail in matchIndexList.Select(i => new GoBangChessGroupDetail(definition, lineChesses[i], direction)))
                    {
                        _goBangChessGroupDetailCollection.Add(detail);
                    }
                });
                getChessDetailTaskList.Add(getChessDetailTask);
            }

            return getChessDetailTaskList;
        }
    }
}
