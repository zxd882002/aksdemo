using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.Redis;
using WeatherForecastAPI.Models.Requests.AuthRequests;
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

        [HttpGet("GetSalt")]
        public async Task<GetSaltResponse> GetSault()
        {
            string salt = string.Join("", 4.GenerateNoDupeRandomNumber());
            string traceId = Guid.NewGuid().ToString();
            string password = "2018-11-29-6733-65-402-" + salt;
            string hash = password.GetSHA256String();

            await _redisHelper.SaveToRedis(traceId, hash, TimeSpan.FromMinutes(10));
            return new GetSaltResponse
            {
                Salt = salt,
                TraceId = traceId
            };
        }

        [HttpPost("Authenticate")]
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var result = await _redisHelper.GetFromRedis<string>(request.TraceId);
            if (result != null && string.Equals(result,request.PasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                bool removed = await _redisHelper.RemoveFromRedis(request.TraceId);
                return new AuthenticateResponse
                {
                    Header = new ResponseHeader
                    {
                        ResponseId = request.Header.RequestId,
                        StatusCode = 200
                    },
                    AuthSuccess = true,
                    AuthToken = "mockToken"
                };
            }
            return new AuthenticateResponse
            {
                Header = new ResponseHeader
                {
                    ResponseId = request.Header.RequestId,
                    StatusCode = 200
                },
                AuthSuccess = false
            };
        }
    }
}