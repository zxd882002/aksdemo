using System;

namespace WeatherForecastAPI
{
    public class ResponseHeader
    {
        public Guid ResponseId { get; set; }
        public int StatusCode { get; set; }
    }
}