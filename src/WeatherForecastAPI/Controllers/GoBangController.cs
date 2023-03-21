using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherForecastAPI.Models.GoBang;
using WeatherForecastAPI.Models.GoBang.GoBangRequest;
using WeatherForecastAPI.Models.GoBang.GoBangResponse;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoBangController : ControllerBase
    {
        private readonly IGoBangBoardFactory _factory;
        private readonly IGoBangNextStepAnalyzer _nextStepAnalyzer;

        public GoBangController(IGoBangBoardFactory factory, IGoBangNextStepAnalyzer nextStepAnalyzer)
        {
            _factory = factory;
            _nextStepAnalyzer = nextStepAnalyzer;
        }

        [HttpPost("GetBoardInfo")]
        public async Task<GetBoardInfoResponse> GetBoardInfo(GetBoardInfoRequest request)
        {
            GoBangBoard board = _factory.Parse(request.GameBoard, request.Row, request.Column, request.LastChessType);
            IGoBangAnalyzer analyzer = new GoBangAnalyzer(board);
            GetBoardInfoResponse response = new GetBoardInfoResponse
            {
                BlackChessScore = await analyzer.GetBlackChessScore(),
                WhiteChessScore = await analyzer.GetWhiteChessScore()
            };
            return response;
        }

        [HttpPost("GetNextStepPoint")]
        public async Task<GetNextStepPointResponse> GetNextStepPoint(GetNextStepPointRequest request)
        {
            GetNextStepPointResponse response;
            GoBangBoard board = _factory.Parse(request.GameBoard, request.Row, request.Column, request.LastChessType);
            IGoBangAnalyzer analyzer = new GoBangAnalyzer(board);

            GoBangChessType? winnerChessType = await analyzer.GetWinnerChessType();
            if (winnerChessType != null)
            {
                return new GetNextStepPointResponse
                {
                    GameStatus = winnerChessType == GoBangChessType.BlackChess
                    ? GoBangGameStatus.BlackWin
                    : GoBangGameStatus.WhiteWin
                };
            }

            var expectedBoard = _nextStepAnalyzer.AnalyzeNextStep(board, request.Deep);
            winnerChessType = await expectedBoard!.Analyzer.GetWinnerChessType();

            if (expectedBoard.Board.IsAllFilledBoard && winnerChessType == null)
                response = new GetNextStepPointResponse
                {
                    Row = expectedBoard.Board.LastChess.Position.Row,
                    Column = expectedBoard.Board.LastChess.Position.Column,
                    GameStatus = GoBangGameStatus.Tie
                };
            else if (winnerChessType != null)
                response = new GetNextStepPointResponse
                {
                    Row = expectedBoard.Board.LastChess.Position.Row,
                    Column = expectedBoard.Board.LastChess.Position.Column,
                    GameStatus = winnerChessType == GoBangChessType.BlackChess
                    ? GoBangGameStatus.BlackWin
                    : GoBangGameStatus.WhiteWin
                };
            else
                response = new GetNextStepPointResponse
                {
                    Row = expectedBoard.Board.LastChess.Position.Row,
                    Column = expectedBoard.Board.LastChess.Position.Column,
                    GameStatus = GoBangGameStatus.Started
                };

            return response;
        }
    }
}