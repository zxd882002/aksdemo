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
        public GoBangController(IGoBangBoardFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("GetBoardInfo")]
        public async Task<GetBoardInfoResponse> GetBoardInfo(GetBoardInfoRequest request)
        {
            GoBangBoard board = await _factory.Parse(request.GameBoard, request.Row, request.Column);
            GetBoardInfoResponse response = new GetBoardInfoResponse
            {
                BlackChessScore = board.BlackChessScore,
                WhiteChessScore = board.WhiteChessScore,
                BlackWin = board.GoBangChessGroupDetailCollection.BlackWin,
                WhiteWin = board.GoBangChessGroupDetailCollection.WhiteWin,
                BlackHasLiveFour = board.GoBangChessGroupDetailCollection.BlackHasLiveFour,
                WhiteHasLiveFour = board.GoBangChessGroupDetailCollection.WhiteHasLiveFour,
                BlackHasDoubleLiveThree = board.GoBangChessGroupDetailCollection.BlackHasDoubleLiveThree,
                WhiteHasDoubleLiveThree = board.GoBangChessGroupDetailCollection.WhiteHasDoubleLiveThree,
                BlackHasDoubleDeadFour = board.GoBangChessGroupDetailCollection.BlackHasDoubleDeadFour,
                WhiteHasDoubleDeadFour = board.GoBangChessGroupDetailCollection.WhiteHasDoubleDeadFour,
                BlackHasDeadFourLiveThree = board.GoBangChessGroupDetailCollection.BlackHasDeadFourLiveThree,
                WhiteHasDeadFourLiveThree = board.GoBangChessGroupDetailCollection.WhiteHasDeadFourLiveThree,
                BlackMustFollow = board.GoBangChessGroupDetailCollection.BlackMustFollow,
                WhiteMustFollow = board.GoBangChessGroupDetailCollection.WhiteMustFollow,
            };
            return response;
        }

        [HttpPost("GetNextStepPoint")]
        public GetNextStepPointResponse GetNextStepPoint(GetNextStepPointRequest request)
        {
            return null!;
        }
    }
}