using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastWeb.Models;

namespace WeatherForecastWeb.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private const string url = "/api/weatherforecast";
        private readonly IHttpClientFactory _clientFactory;

        public WeatherForecastRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IList<WeatherForecast>> GetWeatherForecastList()
        {
            using var client = _clientFactory.CreateClient("WeatherForecast");
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IList<WeatherForecast>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}