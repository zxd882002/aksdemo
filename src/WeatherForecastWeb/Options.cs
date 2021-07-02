using System;
using System.Collections.Generic;

namespace WeatherForecastWeb
{
    public class Options
    {
        public List<ServiceConnection> ServiceConnections { get; set; } = null!;
    }

    public class ServiceConnection
    {
        public string ServiceName { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public TimeSpan Timeout { get; set; }
    }
}