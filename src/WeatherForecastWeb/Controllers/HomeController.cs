using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecastWeb.Models;
using WeatherForecastWeb.Repository;

namespace WeatherForecastWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastRepository _repository;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IList<WeatherForecast> weatherForecasts = await _repository.GetWeatherForecastList();
                return View(weatherForecasts);
            }
            catch
            {
                IList<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
                weatherForecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Cooooooool",
                    TemperatureC = 25
                });
                return View(weatherForecasts);
            }
        }
    }
}
