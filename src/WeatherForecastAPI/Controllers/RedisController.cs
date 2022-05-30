using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WeatherForecastAPI.ConfigOptions;
using WeatherForecastAPI.Infrastructure.Redis;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IRedisHelper _redisHelper;
        private readonly IOptions<WeatherForecastApiOptions> _weatherForecastApiOptions;

        public RedisController(IRedisHelper redisHelper, IOptions<WeatherForecastApiOptions> weatherForecastApiOptions)
        {
            _redisHelper = redisHelper;
            _weatherForecastApiOptions = weatherForecastApiOptions;
        }

        [HttpPost("SetValue")]
        public async Task<bool> SetValue(string key, string value, int? expirySecond)
        {
            return await _redisHelper.SaveToRedis(key, value, expirySecond == null
                ? (TimeSpan?)null
                : TimeSpan.FromSeconds(expirySecond!.Value));
        }

        [HttpPost("GetValue")]
        public async Task<string> GetValue(string key)
        {
            string? value = await _redisHelper.GetFromRedis<string>(key);
            return value ?? "<NULL!>";
        }
    }
}
