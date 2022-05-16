using Microsoft.AspNetCore.Mvc;
using System;
using WeatherForecastAPI.Models.Responses.AuthResponses;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public GetSaultResponse GetSault()
        {
            return new GetSaultResponse
            {
                Sault = "1234",
                TraceId = Guid.NewGuid().ToString()
            };
        }
    }
}