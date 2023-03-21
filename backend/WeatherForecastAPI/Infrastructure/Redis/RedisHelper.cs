using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastAPI.ConfigOptions;

namespace WeatherForecastAPI.Infrastructure.Redis
{
    public interface IRedisHelper
    {
        Task<string> PingRedis();
        Task<bool> SaveToRedis<T>(string key, T value, TimeSpan? expiry);
        Task<T?> GetFromRedis<T>(string key);
        Task<bool> RemoveFromRedis(string key);
        Task<string> ClearRedis();
    }

    public class RedisHelper : IRedisHelper
    {
        private readonly WeatherForecastApiOptions _weatherForecastApiOptions;
        private ConnectionMultiplexer? _multiplexer = null;

        public RedisHelper(IOptions<WeatherForecastApiOptions> weatherForecastApiOptions)
        {
            _weatherForecastApiOptions = weatherForecastApiOptions.Value;
        }

        public async Task<string> PingRedis()
        {
            GetConnectionDataBase();

            var db = GetConnectionDataBase();
            var pong = await db.PingAsync();
            return pong.ToString();
        }

        public async Task<bool> SaveToRedis<T>(string key, T value, TimeSpan? expiry)
        {
            string serializedValue;
            if (typeof(T) == typeof(string))
            {
                serializedValue = (value as string)!;
            }
            else
            {
                serializedValue = JsonSerializer.Serialize(value);
            }
            var db = GetConnectionDataBase();
            bool result = await db.StringSetAsync(key, serializedValue, expiry);
            return result;
        }

        public async Task<T?> GetFromRedis<T>(string key)
        {
            var db = GetConnectionDataBase();
            string? serializedValue = await db.StringGetAsync(key);

            if (serializedValue == null)
                return default(T);

            serializedValue = serializedValue.Trim('"');

            if (serializedValue is T stringValue)
                return stringValue;

            T? value = JsonSerializer.Deserialize<T>(serializedValue);
            return value;
        }

        public async Task<bool> RemoveFromRedis(string key)
        {
            var db = GetConnectionDataBase();
            var result = await db.KeyDeleteAsync(key);
            return result;
        }

        public async Task<string> ClearRedis()
        {
            try
            {
                var db = GetConnectionDataBase();
                var endpoints = db.Multiplexer.GetEndPoints(true);
                foreach (var endpoint in endpoints)
                {
                    var server = db.Multiplexer.GetServer(endpoint);
                    await server.FlushAllDatabasesAsync();
                }
                return "ok";
            }catch(Exception e)
            {
                return e.ToString();
            }
        }

        private IDatabase GetConnectionDataBase()
        {
            if (_multiplexer == null)
            {
                _multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = { _weatherForecastApiOptions.RedisEndPoint },
                    Password = string.IsNullOrWhiteSpace(_weatherForecastApiOptions.RedisPassword) ? null : _weatherForecastApiOptions.RedisPassword,
                    AllowAdmin = true
                });
            }

            if (_multiplexer == null)
                throw new ArgumentNullException($"Cannot connect to {_weatherForecastApiOptions.RedisEndPoint}");

            return _multiplexer.GetDatabase();
        }
    }
}