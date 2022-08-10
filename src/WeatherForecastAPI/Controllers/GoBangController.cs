using Microsoft.AspNetCore.Mvc;
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
        private readonly IGoBangAnalyzer analyzer;

        public GoBangController(IGoBangBoardFactory factory, IGoBangAnalyzer analyzer)
        {
            _factory = factory;
            this.analyzer = analyzer;
        }

        [HttpPost("GetBoardInfo")]
        public async Task<GetBoardInfoResponse> GetBoardInfo(GetBoardInfoRequest request)
        {
            GoBangBoard board =  _factory.Parse(request.GameBoard, request.Row, request.Column, request.LastChessType);
            await board.AnalyzeAllDefinitions();
            GetBoardInfoResponse response = new GetBoardInfoResponse
            {
                BlackChessScore = board.BlackChessScore,
                WhiteChessScore = board.WhiteChessScore                
            };
            return response;
        }

        //[HttpPost("GetNextStepPoint")]
        //public async Task<GetNextStepPointResponse> GetNextStepPoint(GetNextStepPointRequest request)
        //{
        //    GetNextStepPointResponse response;
        //    GoBangBoard board = _factory.Parse(request.GameBoard);
        //    if (board.GoBangChessGroupDetailCollection.BlackWin)
        //    {
        //        response = new GetNextStepPointResponse
        //        {
        //            GameStatus = GoBangGameStatus.BlackWin
        //        };
        //    }
        //    else if (board.GoBangChessGroupDetailCollection.WhiteWin)
        //    {
        //        response = new GetNextStepPointResponse
        //        {
        //            GameStatus = GoBangGameStatus.WhiteWin
        //        };
        //    }
        //    else
        //    {
        //        GoBangBoard expectedBoard = analyzer.Analyze(board, request.Deep);
        //        if (expectedBoard.IsAllFilled() && !expectedBoard.GoBangChessGroupDetailCollection.BlackWin && !expectedBoard.GoBangChessGroupDetailCollection.WhiteWin)
        //            response = new GetNextStepPointResponse
        //            {
        //                Row = expectedBoard.LastChess.Position.Row,
        //                Column = expectedBoard.LastChess.Position.Column,
        //                GameStatus = GoBangGameStatus.Tie
        //            };
        //        else if(expectedBoard.GoBangChessGroupDetailCollection.BlackWin)
        //            response = new GetNextStepPointResponse
        //            {
        //                Row = expectedBoard.LastChess.Position.Row,
        //                Column = expectedBoard.LastChess.Position.Column,
        //                GameStatus = GoBangGameStatus.BlackWin
        //            };
        //        else if(expectedBoard.GoBangChessGroupDetailCollection.WhiteWin)
        //            response = new GetNextStepPointResponse
        //            {
        //                Row = expectedBoard.LastChess.Position.Row,
        //                Column = expectedBoard.LastChess.Position.Column,
        //                GameStatus = GoBangGameStatus.WhiteWin
        //            };
        //        else
        //            response = new GetNextStepPointResponse
        //            {
        //                Row = expectedBoard.LastChess.Position.Row,
        //                Column = expectedBoard.LastChess.Position.Column,
        //                GameStatus = GoBangGameStatus.Started
        //            };
        //    }

        //    return response;
        //}
    }
}