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
        private readonly IGoBangAnalyzer _analyzer;
        private readonly IGoBangNextStepAnalyzer _nextStepAnalyzer;

        public GoBangController(IGoBangBoardFactory factory, IGoBangAnalyzer analyzer, IGoBangNextStepAnalyzer nextStepAnalyzer)
        {
            _factory = factory;
            _analyzer = analyzer;
            _nextStepAnalyzer = nextStepAnalyzer;
        }

        [HttpPost("GetBoardInfo")]
        public async Task<GetBoardInfoResponse> GetBoardInfo(GetBoardInfoRequest request)
        {
            GoBangBoard board = _factory.Parse(request.GameBoard, request.Row, request.Column, request.LastChessType);
            await _analyzer.Reset().AnalyzeAllDefinitions(board);
            GetBoardInfoResponse response = new GetBoardInfoResponse
            {
                BlackChessScore = _analyzer.BlackChessScore,
                WhiteChessScore = _analyzer.WhiteChessScore
            };
            return response;
        }

        [HttpPost("GetNextStepPoint")]
        public async Task<GetNextStepPointResponse> GetNextStepPoint(GetNextStepPointRequest request)
        {
            GetNextStepPointResponse response;
            GoBangBoard board = _factory.Parse(request.GameBoard, request.Row, request.Column, request.LastChessType);
            var tasks = request.LastChessType == 1
                ? _analyzer.Reset().AnalyzeBlackWinDefinitions(board)
                : _analyzer.Reset().AnalyzeWhiteWinDefinitions(board);
            await Task.WhenAll(tasks);
            if (_analyzer.BlackChessWin)
            {
                response = new GetNextStepPointResponse
                {
                    GameStatus = GoBangGameStatus.BlackWin
                };
            }
            else if (_analyzer.WhiteChessWin)
            {
                response = new GetNextStepPointResponse
                {
                    GameStatus = GoBangGameStatus.WhiteWin
                };
            }
            else
            {
                GoBangBoard expectedBoard = _nextStepAnalyzer.AnalyzeNextStep(board, request.Deep);
                throw new NotImplementedException();
                //if (expectedBoard.IsAllFilledBoard && !expectedBoard.BlackChessWin && !expectedBoard.WhiteChessWin)
                //    response = new GetNextStepPointResponse
                //    {
                //        Row = expectedBoard.LastChess.Position.Row,
                //        Column = expectedBoard.LastChess.Position.Column,
                //        GameStatus = GoBangGameStatus.Tie
                //    };
                //else if (expectedBoard.BlackChessWin)
                //    response = new GetNextStepPointResponse
                //    {
                //        Row = expectedBoard.LastChess.Position.Row,
                //        Column = expectedBoard.LastChess.Position.Column,
                //        GameStatus = GoBangGameStatus.BlackWin
                //    };
                //else if (expectedBoard.WhiteChessWin)
                //    response = new GetNextStepPointResponse
                //    {
                //        Row = expectedBoard.LastChess.Position.Row,
                //        Column = expectedBoard.LastChess.Position.Column,
                //        GameStatus = GoBangGameStatus.WhiteWin
                //    };
                //else
                //    response = new GetNextStepPointResponse
                //    {
                //        Row = expectedBoard.LastChess.Position.Row,
                //        Column = expectedBoard.LastChess.Position.Column,
                //        GameStatus = GoBangGameStatus.Started
                //    };
            }

            return response;
        }
    }
}