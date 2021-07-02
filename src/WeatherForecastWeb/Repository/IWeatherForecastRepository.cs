using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecastWeb.Models;

namespace WeatherForecastWeb.Repository
{
    public interface IWeatherForecastRepository
    {
        Task<IList<WeatherForecast>> GetWeatherForecastList();
    }
}