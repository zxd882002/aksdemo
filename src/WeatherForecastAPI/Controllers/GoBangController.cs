using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models.GoBang.GoBangRequest;
using WeatherForecastAPI.Models.GoBang.GoBangResponse;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoBangController : ControllerBase
    {
        [HttpPost("GetNextStepPoint")]
        public GetNextStepPointResponse GetNextStepPoint(GetNextStepPointRequest request)
        {
            return null;
        }
    }
}