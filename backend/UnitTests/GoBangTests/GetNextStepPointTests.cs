using System.Diagnostics;
using WeatherForecastAPI.Controllers;
using WeatherForecastAPI.Models.GoBang;
using WeatherForecastAPI.Models.GoBang.GoBangRequest;

namespace UnitTests.GoBangTests
{
    internal class GetNextStepPointTests
    {
        private IGoBangBoardFactory _goBangBoardFactory;
        private IGoBangNextStepAnalyzer _goBangNextStepAnalyzer;
        private GoBangController _goBangController;

        [SetUp]
        public void Setup()
        {
            _goBangBoardFactory = new GoBangBoardFactory();
            _goBangNextStepAnalyzer = new GoBangNextStepAnalyzer(_goBangBoardFactory);
            _goBangController = new GoBangController(_goBangBoardFactory, _goBangNextStepAnalyzer);
        }

        [Test]
        public async Task GetNextStepPoint_Test()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var board = GetEmptyBoard();
            PutChess(board, 7, 7, 1);
            PutChess(board, 6, 8, 2);
            PutChess(board, 6, 6, 1);
            PutChess(board, 8, 8, 2);
            var request = PutChess(board, 7, 8, 1);
            var response = await _goBangController.GetNextStepPoint(request);
            Console.WriteLine($"next step: [{response.Row},{response.Column}]");
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();

            //// run again
            //response = await _goBangController.GetNextStepPoint(request);
            //Console.WriteLine($"next step: [{response.Row},{response.Column}]");
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //stopwatch.Restart();

            ////  again and again
            //response = await _goBangController.GetNextStepPoint(request);
            //Console.WriteLine($"next step: [{response.Row},{response.Column}]");
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //stopwatch.Restart();

            //// again and again and again
            //response = await _goBangController.GetNextStepPoint(request);
            //Console.WriteLine($"next step: [{response.Row},{response.Column}]");
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //stopwatch.Restart();

            //// again and again and again and again
            //response = await _goBangController.GetNextStepPoint(request);
            //Console.WriteLine($"next step: [{response.Row},{response.Column}]");
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        private int[][] GetEmptyBoard()
        {
            var emptyBoard = new int[15][];
            for (int i = 0; i < 15; i++)
            {
                emptyBoard[i] = new int[15];
            }
            return emptyBoard;
        }

        private GetNextStepPointRequest PutChess(int[][] board, int row, int column, int chessType)
        {
            board[row][column] = chessType;
            var request = new GetNextStepPointRequest()
            {
                GameBoard = board,
                Row = row,
                Column = column,
                LastChessType = chessType,
                Deep = 4
            };
            return request;
        }
    }
}
