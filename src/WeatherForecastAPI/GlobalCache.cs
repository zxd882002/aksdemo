using System;
using System.Runtime.Caching;

namespace WeatherForecastAPI
{
    public static class GlobalCache
    {
        private static MemoryCache _memoryCache = MemoryCache.Default;

        public static void SetValue(string key, long value)
        {
            _memoryCache.Set(key, value, DateTime.Now.AddHours(1));
        }

        public static long? GetValue(string key)
        {
            var obj = _memoryCache.Get(key);
            if (obj == null)
                return null;
            return (long)obj;
        }
    }
}
