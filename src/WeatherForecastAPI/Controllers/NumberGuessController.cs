using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models.Requests.NumberGuessRequests;
using WeatherForecastAPI.Models.Responses.NumberGuessResponses;
using WeatherForecastAPI.Models.NumberGuess;
using System.Linq;

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

        [HttpPost]
        public StartGameResponse StartGame(StartGameReuqest request)
        {
            var info = _gameStatus.CreateGameStatusInformation();
            return new StartGameResponse
            {
                Header = new ResponseHeader
                {
                    ResponseId = request.Header.RequestId
                },
                GameIdentifier = info.GameIdentifier.ToString(),
                GameRetry = info.GameRetry,
                GameStatus = info.GameStatus,
                GameHistories = info.GameHistories.Select(x => $"{x.Input} - {x.Result}").ToArray()
            };
        }
    }
}