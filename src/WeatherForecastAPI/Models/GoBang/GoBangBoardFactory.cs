using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Redis;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangBoardFactory
    {
        GoBangBoard Parse(int[][] board, int lastRow, int lastColumn, int chessType);
        GoBangBoard PutChess(GoBangChess[][] board, GoBangChess chess);
    }

    public class GoBangBoardFactory : IGoBangBoardFactory
    {
        public GoBangBoardFactory() { }

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

        public GoBangBoard PutChess(GoBangChess[][] board, GoBangChess chess)
        {
            GoBangChess[][] goBangChesses = new GoBangChess[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                goBangChesses[i] = new GoBangChess[board.Length];
                for (int j = 0; j < board[i].Length; j++)
                {
                    GoBangChess c = new GoBangChess(board[i][j].ChessType, board[i][j].Position.Row, board[i][j].Position.Column);
                    goBangChesses[i][j] = c;
                }
            }

            goBangChesses[chess.Position.Row][chess.Position.Column] = chess;
            return new GoBangBoard(goBangChesses, chess);
        }
    }
}