using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Models.NumberGuess;
using WeatherForecastAPI.Models.NumberGuess.NumberGuessRequests;
using WeatherForecastAPI.Models.NumberGuess.NumberGuessResponses;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberGuessController : ControllerBase
    {
        private GameStatus _gameStatus;

        public NumberGuessController(GameStatus gameStatus)
        {
            _gameStatus = gameStatus;
        }

        [HttpPost("StartGame")]
        public async Task<StartGameResponse> StartGame()
        {
            var info = await _gameStatus.CreateGameStatusInformation();
            return new StartGameResponse
            {
                GameIdentifier = info.GameIdentifier,
                GameRetry = info.GameRetry,
                GameStatus = info.GameStatus,
                GameHistories = info.GameHistories.Select(x => $"{x.Input} - {x.Result}").ToArray()
            };
        }

        [HttpPost("CheckResult")]
        public async Task<CheckResultResponse> CheckResult(CheckResultRequest request)
        {
            var info = await _gameStatus.CheckResult(request.GameIdentifier, request.Input);
            return new CheckResultResponse
            {             
                GameIdentifier = info.GameIdentifier,
                GameRetry = info.GameRetry,
                GameStatus = info.GameStatus,
                GameHistories = info.GameHistories.Select(x => $"{x.Input} - {x.Result}").ToArray(),
                GameAnswer = info.GameStatus == "Pass" || info.GameStatus == "Fail" ? string.Join("", info.GameAnswer) : null
            };
        }
    }
}