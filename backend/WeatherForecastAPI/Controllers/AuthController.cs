using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Encryption;
using WeatherForecastAPI.Infrastructure.Entensions;
using WeatherForecastAPI.Infrastructure.Redis;
using WeatherForecastAPI.Models.Auth.AuthRequests;
using WeatherForecastAPI.Models.Auth.AuthResponses;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRedisHelper _redisHelper;
        private ECDsSigner _signer;

        public AuthController(IRedisHelper redisHelper, ECDsSigner signer)
        {
            _redisHelper = redisHelper;
            _signer = signer;
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
            if (result != null && string.Equals(result, request.PasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                await _redisHelper.RemoveFromRedis(request.TraceId);
                return new AuthenticateResponse
                {
                    AuthSuccess = true,
                    AccessToken = _signer.SignJwt("admin", new TimeSpan(0, 0, 0), new TimeSpan(0, 5, 0)),
                    RefreshToken = _signer.SignJwt("exAdmin", new TimeSpan(0, 5, 0), new TimeSpan(0, 30, 0))
                };
            }
            return new AuthenticateResponse
            {
                AuthSuccess = false
            };
        }

        [HttpPost("RefreshToken")]
        public RefreshTokenResponse RefreshToken()
        {
            return new RefreshTokenResponse
            {
                AuthSuccess = true,
                AccessToken = _signer.SignJwt("admin", new TimeSpan(0, 0, 0), new TimeSpan(0, 5, 0)),
                RefreshToken = _signer.SignJwt("exAdmin", new TimeSpan(0, 5, 0), new TimeSpan(0, 30, 0))
            };
        }
    }
}