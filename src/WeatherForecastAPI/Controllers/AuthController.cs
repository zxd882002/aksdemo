using Microsoft.AspNetCore.Mvc;
using System;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.Redis;
using WeatherForecastAPI.Models.Responses.AuthResponses;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRedisHelper _redisHelper;

        public AuthController(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        [HttpGet]
        public GetSaultResponse GetSault()
        {
            var sault = 4.GenerateNoDupeRandomNumber().ToString() ?? "0000";
            var traceId = Guid.NewGuid().ToString();
            _redisHelper.SaveToRedis(traceId, sault, TimeSpan.FromMinutes(10));
            return new GetSaultResponse
            {
                Sault = sault,
                TraceId = Guid.NewGuid().ToString()
            };
        }
    }
}