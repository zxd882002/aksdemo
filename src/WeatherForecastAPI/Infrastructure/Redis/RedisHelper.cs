using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Infrastructure.Redis
{
    public interface IRedisHelper
    {
        Task<string> PingRedis();
        Task<bool> SaveToRedis<T>(string key, T value, TimeSpan? expiry);
        Task<T?> GetFromRedis<T>(string key);
        Task<bool> RemoveFromRedis(string key);
    }

    public class RedisHelper : IRedisHelper
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisHelper(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<string> PingRedis()
        {
            var db = _redis.GetDatabase();
            var pong = await db.PingAsync();
            return pong.ToString();
        }

        public async Task<bool> SaveToRedis<T>(string key, T value, TimeSpan? expiry)
        {
            string serializedValue = JsonSerializer.Serialize(value);
            var db = _redis.GetDatabase();
            bool result = await db.StringSetAsync(key, serializedValue, expiry);
            return result;
        }

        public async Task<T?> GetFromRedis<T>(string key)
        {
            var db = _redis.GetDatabase();
            string? serializedValue = await db.StringGetAsync(key);

            if(serializedValue == null)
                return default(T);

            if (serializedValue is T stringValue)
                return stringValue;

            T? value = JsonSerializer.Deserialize<T>(serializedValue);
            return value;
        }

        public async Task<bool> RemoveFromRedis(string key)
        {
            var db = _redis.GetDatabase();
            var result = await db.KeyDeleteAsync(key);
            return result;
        }
    }
}