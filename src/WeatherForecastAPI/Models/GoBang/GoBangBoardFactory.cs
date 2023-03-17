using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Redis;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangBoardFactory
    {
        GoBangBoard Parse(int[][] board, int lastRow, int lastColumn, int chessType);
        GoBangBoard Parse(GoBangChessType[][] currentBoard, GoBangChess lastChess);

        Task<ObsoleteGoBangBoard> ObseleteParse(int[][] board, int lastRow, int lastColumn);
        Task<ObsoleteGoBangBoard> ObseleteGenerateBoard(GoBangChessType[][] currentBoard, int lastRow, int lastColumn);
    }

    public class GoBangBoardFactory : IGoBangBoardFactory
    {
        private IRedisHelper _redisHelper = null!;
        private ConcurrentDictionary<string, ObsoleteGoBangBoard> _obseleteGoBangBoardDictionary;

        public GoBangBoardFactory()
        {
            _obseleteGoBangBoardDictionary = new ConcurrentDictionary<string, ObsoleteGoBangBoard>();
        }

        public async Task<ObsoleteGoBangBoard> ObseleteParse(int[][] board, int lastRow, int lastColumn)
        {
            GoBangChessType[][] typedCurrentBoard = new GoBangChessType[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                typedCurrentBoard[i] = new GoBangChessType[board[i].Length];
                for (int j = 0; j < board[i].Length; j++)
                {
                    typedCurrentBoard[i][j] = GoBangChessType.Parse(board[i][j]);
                }
            }

            return await ObseleteGenerateBoard(typedCurrentBoard, lastRow, lastColumn);
        }

        public async Task<ObsoleteGoBangBoard> ObseleteGenerateBoard(GoBangChessType[][] currentBoard, int lastRow, int lastColumn)
        {
            GoBangChess lastChess = new GoBangChess
            {
                Position = new GoBangChessPosition { Row = lastRow, Column = lastColumn },
                ChessType = currentBoard[lastRow][lastColumn]
            };
            ObsoleteGoBangBoard? goBangBoard = await ObsoleteGetGoBangBoardByCurrentBoard(currentBoard);
            if (goBangBoard != null)
            {
                goBangBoard.LastChess = lastChess;
                return goBangBoard;
            }

            currentBoard[lastRow][lastColumn] = GoBangChessType.Blank;
            goBangBoard = await ObsoleteGetGoGangBoardByPreviousBoard(currentBoard, lastChess);
            return goBangBoard;
        }

        private async Task<ObsoleteGoBangBoard?> ObsoleteGetGoBangBoardByCurrentBoard(GoBangChessType[][] currentBoard)
        {
            ObsoleteGoBangBoard goBangBoard = new ObsoleteGoBangBoard(currentBoard, this);

            string boardHash = goBangBoard.GetBoardHash();

            if (_obseleteGoBangBoardDictionary.ContainsKey(boardHash))
            {
                return _obseleteGoBangBoardDictionary[boardHash];
            }
            string? boardString = await _redisHelper.GetFromRedis<string>(boardHash);
            if (boardString != null)
            {
                _redisHelper.SaveToRedis(boardHash, boardString, TimeSpan.FromDays(1));
                goBangBoard.Deserialize(boardString);
                _obseleteGoBangBoardDictionary[boardHash] = goBangBoard;
                return goBangBoard;
            }

            if (goBangBoard.IsEmptyBoard())
                return goBangBoard;

            return null;
        }

        private async Task<ObsoleteGoBangBoard> ObsoleteGetGoGangBoardByPreviousBoard(GoBangChessType[][] previousBoard, GoBangChess lastChess)
        {
            ObsoleteGoBangBoard? previousGoBangBoard = await ObsoleteGetGoBangBoardByCurrentBoard(previousBoard);
            ObsoleteGoBangBoard currentBoard = previousGoBangBoard!.GetBoardInfoAfterPuttingChess(lastChess);
            string currentBoardHash = currentBoard.GetBoardHash();
            _obseleteGoBangBoardDictionary[currentBoardHash] = currentBoard;
            string currentBoardString = currentBoard.Serialize();
            _redisHelper.SaveToRedis(currentBoardHash, currentBoardString, TimeSpan.FromDays(1));
            return currentBoard;
        }

        public GoBangBoard Parse(int[][] board, int lastRow, int lastColumn, int lastChessType)
        {
            GoBangChess lastChess = new GoBangChess(lastChessType, lastRow, lastColumn);
            GoBangChess[][] goBangChesses = new GoBangChess[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                goBangChesses[i] = new GoBangChess[board.Length];
                for (int j = 0; j < board[i].Length; j++)
                {
                    GoBangChess chess = new GoBangChess(board[i][j], i, j);
                    goBangChesses[i][j] = chess;
                }
            }
            return new GoBangBoard(goBangChesses, lastChess);
        }

        public GoBangBoard Parse(GoBangChessType[][] board, GoBangChess lastChess)
        {
            GoBangChess[][] goBangChesses = new GoBangChess[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                goBangChesses[i] = new GoBangChess[board.Length];
                for (int j = 0; j < board[i].Length; j++)
                {
                    GoBangChess chess = new GoBangChess(board[i][j], i, j);
                    goBangChesses[i][j] = chess;
                }
            }
            return new GoBangBoard(goBangChesses, lastChess);
        }
    }
}
