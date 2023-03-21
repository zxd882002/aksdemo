using System.Diagnostics;
using WeatherForecastAPI.Controllers;
using WeatherForecastAPI.Models.GoBang;
using WeatherForecastAPI.Models.GoBang.GoBangRequest;

namespace UnitTests.GoBangTests
{
    public class GetBoardInfoTests
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
        public async Task DefinitionContains()
        {
            string[] sList =
            {
                "AAAAAA",
                "AAAAA",
                "BAAAAB",
                "BAAAAE",
                "EAAAAB",
                "ABAAA",
                "AAABA",
                "AABAA",
                "BBAAABB",
                "EBAAABB",
                "BBAAABE",
                "BABAAB",
                "BAABAB",
                "BBAAAE",
                "EAAABB",
                "BABAAE",
                "EAABAB",
                "BAABAE",
                "EABAAB",
                "ABBAA",
                "AABBA",
                "ABABA",
                "EBAAABE",
                "BBAABB",
                "BBAABE",
                "EBAABB",
                "BABAB",
                "BABBAB",
                "BBBAAE",
                "EAABBB",
                "BBABAE",
                "EABABB",
                "BABBAE",
                "EABBAB",
                "ABBBA",
            };

            for (int i = 0; i < sList.Length; i++)
            {
                for (int j = 0; j < sList.Length; j++)
                {
                    if (i == j) continue;
                    string s1 = sList[i];
                    string s2 = sList[j];
                    if (s1.Contains(s2))
                    {
                        Console.WriteLine($"{s1} contains {s2}");
                    }
                }
            }

            Assert.Pass();
        }

        [Test]
        public async Task GetBoardInfo_Test()
        {
            var board = GetEmptyBoard();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // step 1
            var request = PutChess(board, 7, 7, 1);
            var response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(0, response.BlackChessScore);
            Assert.AreEqual(0, response.WhiteChessScore);

            // step 2
            request = PutChess(board, 6, 8, 2);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(0, response.BlackChessScore);
            Assert.AreEqual(0, response.WhiteChessScore);

            // step 3
            request = PutChess(board, 6, 6, 1);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(50, response.BlackChessScore);
            Assert.AreEqual(0, response.WhiteChessScore);

            // step 4
            request = PutChess(board, 8, 8, 2);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(10, response.BlackChessScore);
            Assert.AreEqual(50, response.WhiteChessScore);

            // step 5
            request = PutChess(board, 7, 8, 1);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(60, response.BlackChessScore);
            Assert.AreEqual(0, response.WhiteChessScore);

            // step 6
            request = PutChess(board, 7, 9, 2);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(20, response.BlackChessScore);
            Assert.AreEqual(100, response.WhiteChessScore);

            // step 7
            request = PutChess(board, 5, 7, 1);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(120, response.BlackChessScore);
            Assert.AreEqual(60, response.WhiteChessScore);

            // step 8
            request = PutChess(board, 8, 10, 2);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(120, response.BlackChessScore);
            Assert.AreEqual(600, response.WhiteChessScore);

            // step 9
            request = PutChess(board, 6, 7, 1);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(8130, response.BlackChessScore);
            Assert.AreEqual(600, response.WhiteChessScore);

            // step 10
            request = PutChess(board, 8, 7, 2);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(630, response.BlackChessScore);
            Assert.AreEqual(7550, response.WhiteChessScore);

            // step 11
            request = PutChess(board, 8, 9, 1);
            response = await _goBangController.GetBoardInfo(request);
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            Assert.AreEqual(8580, response.BlackChessScore);
            Assert.AreEqual(560, response.WhiteChessScore);
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

        private GetBoardInfoRequest PutChess(int[][] board, int row, int column, int chessType)
        {
            board[row][column] = chessType;
            var request = new GetBoardInfoRequest()
            {
                GameBoard = board,
                Row = row,
                Column = column,
                LastChessType = chessType
            };
            return request;
        }
    }
}