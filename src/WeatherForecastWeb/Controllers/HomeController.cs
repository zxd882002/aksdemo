using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherForecastWeb.Models;

namespace WeatherForecastWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IList<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            weatherForecasts.Add(new WeatherForecast { Date = DateTime.Today, Summary = "Cold", TemperatureC = 0 });
            weatherForecasts.Add(new WeatherForecast { Date = DateTime.Today.AddDays(1), Summary = "Cool", TemperatureC = 10 });
            weatherForecasts.Add(new WeatherForecast { Date = DateTime.Today.AddDays(2), Summary = "Warm", TemperatureC = 20 });
            weatherForecasts.Add(new WeatherForecast { Date = DateTime.Today.AddDays(3), Summary = "Hot", TemperatureC = 30 });

            return View(weatherForecasts);
        }
    }
}
