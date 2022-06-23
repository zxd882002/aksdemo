using System;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Redis;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangBoardFactory
    {
        public Task<GoBangBoard> Parse(int[][] board, int lastRow, int lastColumn);
    }

    public class GoBangBoardFactory : IGoBangBoardFactory
    {
        private IRedisHelper _redisHelper;
        public GoBangBoardFactory(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        public async Task<GoBangBoard> Parse(int[][] board, int lastRow, int lastColumn)
        {
            // get board from cache
            string currentBoardHash = GetBoardHash(board);
            string? currentBoardString = await _redisHelper.GetFromRedis<string>(currentBoardHash);
            if (currentBoardString != null)
            {
                await _redisHelper.SaveToRedis(currentBoardHash, currentBoardString, TimeSpan.FromDays(1));
                GoBangBoard goBangBoard = new GoBangBoard();
                goBangBoard.Deserialize(board, currentBoardString);
                return goBangBoard;
            }

            // if cache not found, generate from previous board
            GoBangChessType currentChess = board[lastRow][lastColumn] == 1 ? GoBangChessType.BlackChess : GoBangChessType.WhiteChess;
            board[lastRow][lastColumn] = 0;

            GoBangBoard previousGoBangBoard = new GoBangBoard();
            string previousBoardHash = GetBoardHash(board);
            string? previousBoardString = await _redisHelper.GetFromRedis<string>(previousBoardHash);
            if (previousBoardString == null)
            {
                // if previous board is not found, previous is all blank (no chess)
                previousGoBangBoard.InitializeEmptyBoard();
            }
            else
            {
                previousGoBangBoard.Deserialize(board, previousBoardString);
            }
            GoBangBoard currentBoard = previousGoBangBoard.GetBoardInfoAfterPuttingChess(new GoBangChess { Position = new GoBangChessPosition { Row = lastRow, Column = lastColumn }, Chess = currentChess });
            currentBoardString = currentBoard.Serialize();
            await _redisHelper.SaveToRedis(currentBoardHash, currentBoardString, TimeSpan.FromDays(1));
            return currentBoard;
        }

        private string GetBoardHash(int[][] board)
        {
            StringBuilder sb = new StringBuilder(GoBangBoard.BOARD_SIZE * GoBangBoard.BOARD_SIZE);
            foreach (var row in board)
            {
                foreach (var chess in row)
                {
                    sb.Append(chess);
                }
            }
            return sb.ToString();
        }
    }
}
